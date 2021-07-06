using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.DataAnnotations;
using System.IO;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraLayout;
using DevExpress.ExpressApp;
using System.Collections;
using MidDosyaYonetim.Module.BusinessObjects;
using DevExpress.Utils.Extensions;
using DevExpress.XtraTreeList;
using DevExpress.Data.Filtering;
using DevExpress.DataAccess.Native.EntityFramework;
using DevExpress.XtraEditors.Controls;

namespace MidDosyaYonetim.Module.Forms
{
    public partial class AksesuarEkleForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        IObjectSpace objectSpace;
        List<String> urunAilesiAdiList = new List<String>();
        List<String> urunGrubuAdiList = new List<String>();
        List<String> urunSerisiAdiList = new List<String>();
        List<String> urunlerList = new List<String>();
        List<byte[]> ImageAksesuar = new List<byte[]>();
        //byte[] ImageAksesuar;
        public AksesuarEkleForm(IObjectSpace objectSpace)
        {
            InitializeComponent();
            this.objectSpace = objectSpace;
            //panel2.Visible = false;
            //filltreeview();           
            createTreeWithChecked();
            treeView1.BeforeSelect += TreeView1_BeforeSelect;
            fillAksGrup();

        }

        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (SystemColors.GrayText == e.Node.ForeColor)
                e.Cancel = true;
        }
        IList liste1, liste2;
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
        public int controlcode()
        {
            int ctrl = 0;
            IList listaks = objectSpace.GetObjects(typeof(Aksesuar));
            foreach (Aksesuar satir in listaks)
            {
                if (satir.AksesuarAdi.Contains(textBox1.Text) || satir.AksesuarKodu.Contains(textBox2.Text))
                {
                    ctrl = 1;
                    break;
                }                    
            }            
            return ctrl;
        }
        private void bbiSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (textBox1.Text != "") {
                if (textBox2.Text != "")
                {
                    if (controlcode() == 0)
                    {

                        List<TreeNode> checked_nodes = CheckedNodes(treeView1);
                        treeView1.PathSeparator = ".";


                        foreach (TreeNode node in checked_nodes)
                        {
                            string fullPath = node.FullPath;
                            var pathItems = fullPath.Split('.');



                            if (pathItems.Length == 5)
                            {
                                CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=? AND [urunGrubu].[UrunGrubuAdi] =? AND [yukseklik].[Yuksek]=?", pathItems[2], pathItems[3], pathItems[1], pathItems[4]);
                                IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                                if (comboBox1.SelectedItem == null) { comboBox1.SelectedItem = "Diğer"; }
                                CriteriaOperator aksgrupcri = CriteriaOperator.Parse("[AksesuarGrupAdi] =?", comboBox1.SelectedItem.ToString());
                                AksesuarGrubu aksesuarGrubu = (AksesuarGrubu)objectSpace.FindObject(typeof(AksesuarGrubu), aksgrupcri);


                                foreach (Urunler satir in liste)
                                {
                                    //

                                    Aksesuar aks = objectSpace.CreateObject<Aksesuar>();
                                    aks.AksesuarAdi = textBox1.Text;
                                    aks.AksesuarKodu = textBox2.Text;
                                    aks.urunAilesi = satir.urunAilesi;
                                    aks.urunGrubu = satir.urunGrubu;
                                    aks.urunSerisi = satir.urunSerisi;
                                    aks.urunler = satir;
                                    aks.boyut = satir.boyut;
                                    aks.yukseklik = satir.yukseklik;
                                    aks.AksesuarGrubu = aksesuarGrubu;
                                    if (ImageAksesuar != null)
                                    {
                                        for (int i = 0; i < ImageAksesuar.Count; i++)
                                        {
                                            Fotograflar foto = objectSpace.CreateObject<Fotograflar>();
                                            foto.fotograf = ImageAksesuar[i];
                                            foto.OlusturmaTarihi = DateTime.Now;
                                            aks.fotograflar.Add(foto);
                                            aks.Save();
                                            objectSpace.CommitChanges();
                                        }
                                    }

                                    satir.aksesuar.Add(aks);

                                    satir.Save();
                                    objectSpace.CommitChanges();

                                }

                            }
                            else
                            {

                            }
                        }
                        MessageBox.Show("İşlem Başarıyla Kaydedildi.");
                        Close();
                    }
                    else { MessageBox.Show("Bu aksesuar zaten mevcut"); }
                }
                else { MessageBox.Show("Aksesuar kodu girmeniz gerekir"); }
            }
            else { MessageBox.Show("Aksesuar Adı girmeniz gerekir"); }
        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Checked = false;
            }
        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            
            //ımageSlider2.Images.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ımageSlider2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open Image";
            dlg.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Image image = new Bitmap(dlg.OpenFile());
                ımageSlider2.Images.Add(image);
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
                ImageAksesuar.Add(xByte);

            }

            dlg.Dispose();
        }
        public void fillAksGrup() 
        {
            comboBox1.Items.Clear();
            IList aksesuarList = objectSpace.GetObjects(typeof(AksesuarGrubu));
            IQueryable<AksesuarGrubu> query = objectSpace.GetObjectsQuery<AksesuarGrubu>(true);
            var list = from c in query
                       orderby c.AksesuarGrupAdi descending
                       group c by new { c.AksesuarGrupAdi } into mygroup
                       select mygroup.FirstOrDefault();
            foreach (AksesuarGrubu aksesuar in list)
            {
                if (aksesuar.AksesuarGrupAdi != null)
                {
                    comboBox1.Items.Add(aksesuar.AksesuarGrupAdi);
                }


            }

        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (controlcode() == 0)
                    {

                        List<TreeNode> checked_nodes = CheckedNodes(treeView1);
                        treeView1.PathSeparator = ".";


                        foreach (TreeNode node in checked_nodes)
                        {
                            string fullPath = node.FullPath;
                            var pathItems = fullPath.Split('.');



                            if (pathItems.Length == 5)
                            {
                                CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=? AND [urunGrubu].[UrunGrubuAdi] =? AND [yukseklik].[Yuksek]=?", pathItems[2], pathItems[3], pathItems[1], pathItems[4]);
                                IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                                if (comboBox1.SelectedItem == null) { comboBox1.SelectedItem = "Diğer"; }
                                CriteriaOperator aksgrupcri = CriteriaOperator.Parse("[AksesuarGrupAdi] =?", comboBox1.SelectedItem.ToString());
                                AksesuarGrubu aksesuarGrubu = (AksesuarGrubu)objectSpace.FindObject(typeof(AksesuarGrubu), aksgrupcri);


                                foreach (Urunler satir in liste)
                                {
                                    //

                                    Aksesuar aks = objectSpace.CreateObject<Aksesuar>();
                                    aks.AksesuarAdi = textBox1.Text;
                                    aks.AksesuarKodu = textBox2.Text;
                                    aks.urunAilesi = satir.urunAilesi;
                                    aks.urunGrubu = satir.urunGrubu;
                                    aks.urunSerisi = satir.urunSerisi;
                                    aks.urunler = satir;
                                    aks.boyut = satir.boyut;
                                    aks.yukseklik = satir.yukseklik;
                                    aks.AksesuarGrubu = aksesuarGrubu;
                                    if (ImageAksesuar != null)
                                    {
                                        for (int i = 0; i < ImageAksesuar.Count; i++)
                                        {
                                            Fotograflar foto = objectSpace.CreateObject<Fotograflar>();
                                            foto.fotograf = ImageAksesuar[i];
                                            foto.OlusturmaTarihi = DateTime.Now;
                                            aks.fotograflar.Add(foto);
                                            aks.Save();
                                            objectSpace.CommitChanges();
                                        }
                                    }

                                    satir.aksesuar.Add(aks);

                                    satir.Save();
                                    objectSpace.CommitChanges();

                                }

                            }
                            else
                            {

                            }
                        }
                        MessageBox.Show("İşlem Başarıyla Kaydedildi.");
                       // Close();
                    }
                    else { MessageBox.Show("Bu aksesuar zaten mevcut"); }
                }
                else { MessageBox.Show("Aksesuar kodu girmeniz gerekir"); }
            }
            else { MessageBox.Show("Aksesuar Adı girmeniz gerekir"); }
        }

        private void bbiSaveAndNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox2.Text != "")
                {
                    if (controlcode() == 0)
                    {

                        List<TreeNode> checked_nodes = CheckedNodes(treeView1);
                        treeView1.PathSeparator = ".";


                        foreach (TreeNode node in checked_nodes)
                        {
                            string fullPath = node.FullPath;
                            var pathItems = fullPath.Split('.');



                            if (pathItems.Length == 5)
                            {
                                CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=? AND [urunGrubu].[UrunGrubuAdi] =? AND [yukseklik].[Yuksek]=?", pathItems[2], pathItems[3], pathItems[1], pathItems[4]);
                                IList liste = objectSpace.GetObjects(typeof(Urunler), criteria);
                                if (comboBox1.SelectedItem == null) { comboBox1.SelectedItem = "Diğer"; }
                                CriteriaOperator aksgrupcri = CriteriaOperator.Parse("[AksesuarGrupAdi] =?", comboBox1.SelectedItem.ToString());
                                AksesuarGrubu aksesuarGrubu = (AksesuarGrubu)objectSpace.FindObject(typeof(AksesuarGrubu), aksgrupcri);


                                foreach (Urunler satir in liste)
                                {
                                    //

                                    Aksesuar aks = objectSpace.CreateObject<Aksesuar>();
                                    aks.AksesuarAdi = textBox1.Text;
                                    aks.AksesuarKodu = textBox2.Text;
                                    aks.urunAilesi = satir.urunAilesi;
                                    aks.urunGrubu = satir.urunGrubu;
                                    aks.urunSerisi = satir.urunSerisi;
                                    aks.urunler = satir;
                                    aks.boyut = satir.boyut;
                                    aks.yukseklik = satir.yukseklik;
                                    aks.AksesuarGrubu = aksesuarGrubu;
                                    if (ImageAksesuar != null)
                                    {
                                        for (int i = 0; i < ImageAksesuar.Count; i++)
                                        {
                                            Fotograflar foto = objectSpace.CreateObject<Fotograflar>();
                                            foto.fotograf = ImageAksesuar[i];
                                            foto.OlusturmaTarihi = DateTime.Now;
                                            aks.fotograflar.Add(foto);
                                            aks.Save();
                                            objectSpace.CommitChanges();
                                        }
                                    }

                                    satir.aksesuar.Add(aks);

                                    satir.Save();
                                    objectSpace.CommitChanges();

                                }

                            }
                            else
                            {

                            }
                        }
                        MessageBox.Show("İşlem Başarıyla Kaydedildi.");
                        refreshform();
                    }
                    else { MessageBox.Show("Bu aksesuar zaten mevcut"); }
                }
                else { MessageBox.Show("Aksesuar kodu girmeniz gerekir"); }
            }
            else { MessageBox.Show("Aksesuar Adı girmeniz gerekir"); }
        }
        public void refreshform() 
        {

            treeView1.SelectedNode.Checked = false;
            comboBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            ımageSlider2.Images.Clear();
        
        }
    }
}

