using DevExpress.Data.Filtering;
using DevExpress.DataProcessing;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils.Extensions;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraRichEdit.Import.Html;
using MidDosyaYonetim.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidDosyaYonetim.Module.Forms
{
    public partial class KaliteEkleForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        IObjectSpace objectSpace;
        DetailView detail;
        public KaliteEkleForm(IObjectSpace objectSpace,DetailView detail)
        {
            InitializeComponent();
            this.objectSpace = objectSpace;
            this.detail = detail;
            createTreeWithChecked();
            treeView1.BeforeSelect += TreeView1_BeforeSelect;
        }
        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (SystemColors.GrayText == e.Node.ForeColor)
                e.Cancel = true;
        }
        IList liste1,liste2;
        public void createTreeWithChecked()
        {
            treeView1.Nodes.Clear();


            IList aileListe = objectSpace.GetObjects(typeof(UrunAilesi));
            TreeNode[] aileArray = new TreeNode[aileListe.Count];
            int i = 0;
            foreach (UrunAilesi satir in aileListe)
            {
                TreeNode node = new TreeNode();
                node.Text = satir.UrunAilesiAdi;
                aileArray[i] = node;

                i++;
              
            }
            foreach (TreeNode satir in aileArray)
            {
                treeView1.Nodes.Add(satir.Text, satir.Text);

            }

            IList grupListe = objectSpace.GetObjects(typeof(UrunGrubu));
            foreach (UrunGrubu satir in grupListe)
            {
                TreeNode child = new TreeNode();
                child.Text = satir.UrunGrubuAdi;

                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunAilesi.UrunAilesiAdi, true);
                TreeNode parent = parentArray.FirstOrDefault();
                parent.Nodes.Add(child.Text, child.Text);
              

            }

            IList seriListe = objectSpace.GetObjects(typeof(UrunSerisi));

            foreach (UrunSerisi satir in seriListe)
            {
                TreeNode child = new TreeNode();
                child.Text = satir.UrunSerisiAdi;
                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunGrubu.UrunGrubuAdi, true);

                TreeNode parent = parentArray.FirstOrDefault();
                parent.Nodes.Add(child.Text, child.Text);
              
            }
         
            IList urunListe = objectSpace.GetObjects(typeof(Urunler));
            

            foreach (Urunler satir in urunListe)
            {
                TreeNode child = new TreeNode();
                CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi] =?", satir.urunSerisi.UrunSerisiAdi);
                liste1 = objectSpace.GetObjects(typeof(Urunler), criteria);

                foreach (Urunler satir2 in liste1)
                {

                    if (satir2.boyut != null)
                    {

                        child.Text = satir2.boyut.boyut;
                        TreeNode[] parentArray = treeView1.Nodes.Find(satir2.urunSerisi.UrunSerisiAdi, true);
                        TreeNode parent = parentArray.FirstOrDefault();

                        if (parent != null && !parent.Nodes.ContainsKey(child.Text))
                        {
                            parent.Nodes.Add(child.Text, child.Text);

                        }


                    }
                }

            }
           
            IList urunListeyukseklik = objectSpace.GetObjects(typeof(Urunler));

            foreach (Urunler satir in urunListeyukseklik)
            {
                if (satir.boyut != null)
                {
                    TreeNode child = new TreeNode();
                    CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi] =? AND [boyut].[boyut]=?", satir.urunSerisi.UrunSerisiAdi, satir.boyut.boyut);
                    liste2 = objectSpace.GetObjects(typeof(Urunler), criteria);

                    foreach (Urunler satir2 in liste2)
                    {
                        if (satir2.yukseklik != null && satir2.boyut != null)
                        {

                            child.Text = satir2.yukseklik.Yuksek;
                            TreeNode[] parentArray = treeView1.Nodes.Find(satir.boyut.boyut, true);

                            foreach (TreeNode node in parentArray)
                            {
                                if (node != null && !node.Nodes.ContainsKey(child.Text) && node.Parent.Text.Equals(satir.urunSerisi.UrunSerisiAdi))
                                {
                                    node.Nodes.Add(child.Text, child.Text);

                                }
                            }

                        }
                    }
                }
            }
            treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));

            treeView1.CheckBoxes = true;
            treeView1.FullRowSelect = true;
            treeView1.Location = new System.Drawing.Point(12, 12);
            treeView1.AfterCheck += TreeView1_AfterCheck;
        }
        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                //
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    if (node.Checked == true)
                    {
                        this.CheckAllChildNodes(node, nodeChecked);
                    }

                }
            }
        }
        private List<TreeNode> CheckedNodes(TreeView trv)
        {
            List<TreeNode> checked_nodes = new List<TreeNode>();
            FindCheckedNodes(checked_nodes, treeView1.Nodes);
            return checked_nodes;
        }
        private void FindCheckedNodes(List<TreeNode> checked_nodes, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                // Add this node.
                if (node.Checked) checked_nodes.Add(node);



                // Check the node's descendants.
                FindCheckedNodes(checked_nodes, node.Nodes);
            }
        }
        private void bbiSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
                List<TreeNode> checked_nodes = CheckedNodes(treeView1);
                treeView1.PathSeparator = ".";

            if (checked_nodes.Count > 0)
            {

                foreach (TreeNode node in checked_nodes)
                {
                    string fullPath = node.FullPath;
                    var pathItems = fullPath.Split('.');


                    if (pathItems.Length == 5)
                    {
                        CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=? AND [urunGrubu].[UrunGrubuAdi] =? AND [yukseklik].[Yuksek]=?", pathItems[2], pathItems[3], pathItems[1], pathItems[4]);
                        IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                        KaliteFile filek = detail.CurrentObject as KaliteFile;
                        FileData currentFile = filek.File;
                        var stream = new MemoryStream();
                        currentFile.SaveToStream(stream);
                        stream.Position = 0;



                        foreach (Urunler satir in liste)
                        {

                            KaliteDokumanlari klt = objectSpace.CreateObject<KaliteDokumanlari>();
                            FileData fileCopy = objectSpace.CreateObject<FileData>();
                            fileCopy.LoadFromStream(currentFile.FileName, stream);
                            klt.urunAilesi = satir.urunAilesi;
                            klt.urunGrubu = satir.urunGrubu;
                            klt.urunSerisi = satir.urunSerisi;
                            klt.urunler = satir;
                            klt.File = fileCopy;
                            klt.DokumanAdi = fileCopy.FileName;
                            satir.kaliteDokumanlari.Add(klt);


                            satir.Save();
                            objectSpace.CommitChanges();

                        }


                    }
                    else if (pathItems.Length == 3)
                    {
                        CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi]=? AND [urunGrubu].[UrunGrubuAdi] =?", pathItems[2], pathItems[1]);
                        IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                        KaliteFile filek = detail.CurrentObject as KaliteFile;
                        FileData currentFile = filek.File;
                        var stream = new MemoryStream();
                        currentFile.SaveToStream(stream);
                        stream.Position = 0;



                        foreach (Urunler satir in liste)
                        {

                            KaliteDokumanlari klt = objectSpace.CreateObject<KaliteDokumanlari>();
                            FileData fileCopy = objectSpace.CreateObject<FileData>();
                            fileCopy.LoadFromStream(currentFile.FileName, stream);
                            klt.urunAilesi = satir.urunAilesi;
                            klt.urunGrubu = satir.urunGrubu;
                            klt.urunSerisi = satir.urunSerisi;
                            klt.urunler = satir;
                            klt.File = fileCopy;
                            klt.DokumanAdi = fileCopy.FileName;
                            satir.kaliteDokumanlari.Add(klt);


                            satir.Save();
                            objectSpace.CommitChanges();

                        }

                    }
                    else if (pathItems.Length == 2)
                    {
                        CriteriaOperator criteria = CriteriaOperator.Parse("[urunGrubu].[UrunGrubuAdi] =?", pathItems[1]);
                        IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                        KaliteFile filek = detail.CurrentObject as KaliteFile;
                        FileData currentFile = filek.File;
                        var stream = new MemoryStream();
                        currentFile.SaveToStream(stream);
                        stream.Position = 0;



                        foreach (Urunler satir in liste)
                        {

                            KaliteDokumanlari klt = objectSpace.CreateObject<KaliteDokumanlari>();
                            FileData fileCopy = objectSpace.CreateObject<FileData>();
                            fileCopy.LoadFromStream(currentFile.FileName, stream);
                            klt.urunAilesi = satir.urunAilesi;
                            klt.urunGrubu = satir.urunGrubu;
                            klt.urunSerisi = satir.urunSerisi;
                            klt.urunler = satir;
                            klt.File = fileCopy;
                            klt.DokumanAdi = fileCopy.FileName;
                            satir.kaliteDokumanlari.Add(klt);


                            satir.Save();
                            objectSpace.CommitChanges();

                        }
                    }
                }
                MessageBox.Show("İşlem Başarıyla Kaydedildi.");
                Close();
                }

                else
                {
                     MessageBox.Show("Lütfen en az bir adet kutucuğu işaretleyiniz.");
                }

        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Checked = false;
            }
        }        

    }
}
