using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using MidDosyaYonetim.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidDosyaYonetim.Module.Forms
{
    public partial class KapakOpsiyonlariGuncelleSilForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        IObjectSpace objectSpace;
        List<String> urunAilesiAdiList = new List<String>();
        List<String> urunGrubuAdiList = new List<String>();
        List<String> urunSerisiAdiList = new List<String>();
        List<String> urunlerList = new List<String>();
        List<String> urunlerYukseklilList = new List<String>();
        public KapakOpsiyonlariGuncelleSilForm(IObjectSpace objectSpace)
        {
            InitializeComponent();
            this.objectSpace = objectSpace;
            filltreeview();
            treeView1.BeforeSelect += TreeView1_BeforeSelect;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            fillOpsiyonList();
        }
        public void filltreeview()
        {
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
                if (satir.boyut != null)
                {
                    TreeNode child = new TreeNode();
                    child.Text = satir.boyut.boyut;
                    TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunSerisi.UrunSerisiAdi, true);
                    TreeNode parent = parentArray.FirstOrDefault();
                    parent.Nodes.Add(child.Text, child.Text);
                }
            }
            IList urunListeyukseklik = objectSpace.GetObjects(typeof(Urunler));

            foreach (Urunler satir in urunListeyukseklik)
            {

                TreeNode child = new TreeNode();

                if (satir.yukseklik != null)
                {

                    child.Text = satir.yukseklik.Yuksek;
                    TreeNode[] parentArray = treeView1.Nodes.Find(satir.boyut.boyut, true);
                    TreeNode parent = parentArray.FirstOrDefault();
                    if (parent != null)
                    {
                        parent.Nodes.Add(child);
                       
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

        private void TreeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (SystemColors.GrayText == e.Node.ForeColor)
                e.Cancel = true;
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
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            FillCheckBox();
            createTreeWithChecked();
          
           
        }
        public void FillCheckBox()
        {
            if (comboBox1.SelectedItem != null)
            {
                urunAilesiAdiList.Clear();
                urunGrubuAdiList.Clear();
                urunSerisiAdiList.Clear();
                urunlerList.Clear();
                urunlerYukseklilList.Clear();
                CriteriaOperator cri = CriteriaOperator.Parse("[OpsiyonKodu] =? ", comboBox1.SelectedItem.ToString());
                IList list = objectSpace.GetObjects(typeof(KapakOpsiyonlari), cri);
                foreach (KapakOpsiyonlari satir in list)
                {
                    try
                    {
                        if (satir.urunAilesi != null) { urunAilesiAdiList.Add(satir.urunAilesi.UrunAilesiAdi); }
                        if (satir.urunGrubu != null) { urunGrubuAdiList.Add(satir.urunGrubu.UrunGrubuAdi); }
                        if (satir.urunSerisi != null) { urunSerisiAdiList.Add(satir.urunSerisi.UrunSerisiAdi); }
                        if (satir.urunler.boyut != null) { urunlerList.Add(satir.urunler.boyut.boyut); }
                        if (satir.urunler.yukseklik != null) { urunlerYukseklilList.Add(satir.urunler.yukseklik.Yuksek); }

                    }
                    catch (NullReferenceException ex)
                    {
                        //
                    }

                }
            }
            else { }
        }
        public void fillOpsiyonList()
        {
          
                comboBox1.Items.Clear();
                IList aksesuarList = objectSpace.GetObjects(typeof(KapakOpsiyonlari));
                IQueryable<KapakOpsiyonlari> query = objectSpace.GetObjectsQuery<KapakOpsiyonlari>(true);
                var list = from c in query                           
                           orderby c.OpsiyonKodu descending
                           group c by new { c.OpsiyonKodu } into mygroup
                           select mygroup.FirstOrDefault();
                foreach (KapakOpsiyonlari ops in list)
                {
                    if (ops.OpsiyonKodu != null)
                    {
                        comboBox1.Items.Add(ops.OpsiyonKodu);
                    }


                }
            
        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("[OpsiyonKodu] =?", comboBox1.SelectedItem.ToString());
            IList liste = objectSpace.GetObjects(typeof(KapakOpsiyonlari), criteria);
            List<KapakOpsiyonlari> aklist = new List<KapakOpsiyonlari>();


            foreach (KapakOpsiyonlari satir in liste)
            {
                aklist.Add(satir);
            }
            foreach (KapakOpsiyonlari satir in aklist)
            {
                satir.Delete();
                objectSpace.CommitChanges();

            }
            Close();

            MessageBox.Show("Başarıyla silindi.");

        }

        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<TreeNode> checked_nodes = CheckedNodes(treeView1);
            treeView1.PathSeparator = ".";
            CriteriaOperator criterias = CriteriaOperator.Parse("[OpsiyonKodu] =?", comboBox1.SelectedItem.ToString());
            IList Aksliste = objectSpace.GetObjects(typeof(KapakOpsiyonlari), criterias);

            List<string> opsiyonAdi = new List<string>();

            foreach (KapakOpsiyonlari satir in Aksliste)
            {
                opsiyonAdi.Add(satir.Opsiyon);
            }


            foreach (TreeNode node in checked_nodes)
            {

                string fullPath = node.FullPath;
                var pathItems = fullPath.Split('.');



                if (pathItems.Length == 5)
                {

                    OperandValue[] parameters;
                    CriteriaOperator criteria1 = CriteriaOperator.Parse("[urunGrubu].[UrunGrubuAdi] =? AND [urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=? AND [yukseklik].[Yuksek]=?", out parameters);
                    parameters[0].Value = pathItems[1];
                    parameters[1].Value = pathItems[2];
                    parameters[2].Value = pathItems[3];
                    parameters[3].Value = pathItems[4];
                    IList liste = objectSpace.GetObjects(typeof(Urunler), criteria1);


                    foreach (Urunler satir in liste)
                    {
                        if (!satir.kapakOpsiyonlari.Select(y => y.OpsiyonKodu).Contains(comboBox1.SelectedItem.ToString()))
                        {

                            KapakOpsiyonlari ops = objectSpace.CreateObject<KapakOpsiyonlari>();
                            ops.Opsiyon = opsiyonAdi.FirstOrDefault();
                            ops.Opsiyon = comboBox1.SelectedItem.ToString();

                            ops.urunler = satir;
                            ops.urunGrubu = satir.urunGrubu;
                            ops.urunAilesi = satir.urunAilesi;
                            ops.urunSerisi = satir.urunSerisi;
                            satir.kapakOpsiyonlari.Add(ops);
                            ops.Save();
                            satir.Save();
                            objectSpace.CommitChanges();
                        }
                    }
                }
                else
                {
                    // Do nothing
                }
            }

            MessageBox.Show("İşlem Başarıyla Kaydedildi.");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            fillOpsiyonList();
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
                if (urunAilesiAdiList.Contains(satir.UrunAilesiAdi))
                {
                    node.Checked = true;

                }
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
                if (urunGrubuAdiList.Contains(satir.UrunGrubuAdi))
                {
                    parent.Checked = true;
                    //child.Checked = true;
                }

            }

            IList seriListe = objectSpace.GetObjects(typeof(UrunSerisi));

            foreach (UrunSerisi satir in seriListe)
            {
                TreeNode child = new TreeNode();
                child.Text = satir.UrunSerisiAdi;
                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunGrubu.UrunGrubuAdi, true);

                TreeNode parent = parentArray.FirstOrDefault();
                parent.Nodes.Add(child.Text, child.Text);
                if (urunSerisiAdiList.Contains(satir.UrunSerisiAdi))
                {
                    parent.Checked = true;
                    //child.Checked = true;

                }
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
                        if (urunlerList.Contains(satir2.boyut.boyut) && urunSerisiAdiList.Contains(satir2.urunSerisi.UrunSerisiAdi))
                        {
                            parent.Checked = true;
                            //child.Checked = true;

                        }


                    }
                }

            }

            IList urunListeyukseklik = objectSpace.GetObjects(typeof(Urunler));

            foreach (Urunler satir in urunListeyukseklik)
            {

                TreeNode child = new TreeNode();
                CriteriaOperator criteria = CriteriaOperator.Parse("[urunSerisi].[UrunSerisiAdi] =? AND [boyut].[boyut]=?", satir.urunSerisi.UrunSerisiAdi, satir.boyut.boyut);
                liste2 = objectSpace.GetObjects(typeof(Urunler), criteria);

                foreach (Urunler satir2 in liste2)
                {
                    if (satir2.yukseklik != null)
                    {

                        child.Text = satir2.yukseklik.Yuksek;
                        TreeNode[] parentArray = treeView1.Nodes.Find(satir.boyut.boyut, true);

                        foreach (TreeNode node in parentArray)
                        {
                            if (node != null && !node.Nodes.ContainsKey(child.Text) && node.Parent.Text.Equals(satir.urunSerisi.UrunSerisiAdi))
                            {
                                node.Nodes.Add(child.Text, child.Text);

                            }
                            if (urunlerYukseklilList.Contains(satir.yukseklik.Yuksek) && urunlerList.Contains(satir2.boyut.boyut))
                            {
                                node.Checked = true;
                                child.Checked = true;

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
        private void bbiSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<TreeNode> checked_nodes = CheckedNodes(treeView1);
            treeView1.PathSeparator = ".";
            CriteriaOperator criterias = CriteriaOperator.Parse("[OpsiyonKodu] =?", comboBox1.SelectedItem.ToString());
            IList Aksliste = objectSpace.GetObjects(typeof(KapakOpsiyonlari), criterias);

            List<string> opsiyonAdi = new List<string>();

            foreach (KapakOpsiyonlari satir in Aksliste)
            {
                opsiyonAdi.Add(satir.Opsiyon);
            }


            foreach (TreeNode node in checked_nodes)
            {

                string fullPath = node.FullPath;
                var pathItems = fullPath.Split('.');



                if (pathItems.Length == 5)
                {

                    OperandValue[] parameters;
                    CriteriaOperator criteria1 = CriteriaOperator.Parse("[urunGrubu].[UrunGrubuAdi] =? AND [urunSerisi].[UrunSerisiAdi]=? AND [boyut].[boyut]=?", out parameters);
                    parameters[0].Value = pathItems[1];
                    parameters[1].Value = pathItems[2];
                    parameters[2].Value = pathItems[3];


                    OperandValue[] parameters1;
                    CriteriaOperator criteria2 = CriteriaOperator.Parse("[urunGrubu].[UrunGrubuAdi] =? AND [UrunSerisiAdi]=?", out parameters1);
                    parameters1[0].Value = pathItems[1];
                    parameters1[1].Value = pathItems[2];

                    IList urunSeriListe = objectSpace.GetObjects(typeof(UrunSerisi), criteria2);

                    UrunSerisi seri = null;

                    foreach (UrunSerisi satir in urunSeriListe)
                    {
                        seri = satir;

                    }


                    if (!seri.kapakOpsiyonlari.Select(y => y.OpsiyonKodu).Contains(comboBox1.SelectedItem.ToString()) && !seri.urunAilesi.kapakOpsiyonlari.Select(y => y.OpsiyonKodu).Contains(comboBox1.SelectedItem.ToString()))
                    {
                        KapakOpsiyonlari ops = objectSpace.CreateObject<KapakOpsiyonlari>();
                        ops.OpsiyonKodu = comboBox1.SelectedItem.ToString();
                        ops.OlusturmaTarihi = DateTime.Today;
                        ops.Opsiyon = opsiyonAdi.FirstOrDefault();
                        seri.kapakOpsiyonlari.Add(ops);
                        seri.urunAilesi.kapakOpsiyonlari.Add(ops);
                        seri.urunGrubu.kapakOpsiyonlari.Add(ops);
                        seri.Save();
                        objectSpace.CommitChanges();

                    }
                    //Ürün serisi bu olan tüm ürünlerin aksesuarlar listesine bu aksesuarı ekle metodu                        

                    IList liste = objectSpace.GetObjects(typeof(Urunler), criteria1);


                    foreach (Urunler satir in liste)
                    {
                        KapakOpsiyonlari ops = objectSpace.CreateObject<KapakOpsiyonlari>();
                        ops.OpsiyonKodu = comboBox1.SelectedItem.ToString();
                        ops.Opsiyon = opsiyonAdi.FirstOrDefault();
                        satir.kapakOpsiyonlari.Add(ops);
                        satir.Save();
                        objectSpace.CommitChanges();
                    }
                }
                else
                {
                    // Do nothing
                }
            }
            Close();

            MessageBox.Show("İşlem Başarıyla Kaydedildi.");




        }
    }
}
