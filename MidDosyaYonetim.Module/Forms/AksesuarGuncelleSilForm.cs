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


namespace MidDosyaYonetim.Module.Forms
{
    public partial class AksesuarGuncelleSilForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        IObjectSpace objectSpace;
        List<String> urunAilesiAdiList = new List<String>();
        List<String> urunGrubuAdiList = new List<String>();
        List<String> urunSerisiAdiList = new List<String>();
        List<String> urunlerList = new List<String>();
        List<String> urunlerYukseklilList = new List<String>();
        public AksesuarGuncelleSilForm(IObjectSpace objectSpace)
        {
            InitializeComponent();
            this.objectSpace = objectSpace;         
            
            filltreeview();
            fillAksGrup();
            
            treeView1.BeforeSelect += TreeView1_BeforeSelect;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
          
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
            CriteriaOperator criterias = CriteriaOperator.Parse("[AksesuarKodu] =?", comboBox1.SelectedItem.ToString());
            IList Aksliste = objectSpace.GetObjects(typeof(Aksesuar), criterias);

            List<string> aksesuarAdi = new List<string>();

            foreach(Aksesuar satir in Aksliste)
            {
                aksesuarAdi.Add(satir.AksesuarAdi); 
            }


            foreach (TreeNode node in checked_nodes)
            {

                string fullPath = node.FullPath;
                var pathItems = fullPath.Split('.');

                

                if (pathItems.Length == 4)
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


                    if (!seri.aksesuar.Select(y => y.AksesuarKodu).Contains(comboBox1.SelectedItem.ToString()) && !seri.urunAilesi.aksesuar.Select(y => y.AksesuarKodu).Contains(comboBox1.SelectedItem.ToString()))
                    {
                        Aksesuar akse = objectSpace.CreateObject<Aksesuar>();
                        akse.AksesuarKodu = comboBox1.SelectedItem.ToString();
                        akse.OlusturmaTarihi = DateTime.Today;
                        akse.AksesuarAdi = aksesuarAdi.FirstOrDefault();
                        seri.aksesuar.Add(akse);
                        seri.urunAilesi.aksesuar.Add(akse);
                        seri.urunGrubu.aksesuar.Add(akse);
                        seri.Save();
                        objectSpace.CommitChanges();

                    }
                    //Ürün serisi bu olan tüm ürünlerin aksesuarlar listesine bu aksesuarı ekle metodu                        

                    IList liste = objectSpace.GetObjects(typeof(Urunler), criteria1);


                    foreach (Urunler satir in liste)
                    {
                        Aksesuar aks = objectSpace.CreateObject<Aksesuar>();
                        aks.AksesuarAdi = aksesuarAdi.FirstOrDefault();
                        aks.AksesuarKodu = comboBox1.SelectedItem.ToString();
                        aks.boyut = satir.boyut;
                        satir.aksesuar.Add(aks);
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
        //public void FillCheckBox()
        //{

        //    if (comboBox1.SelectedItem != null)
        //    {

        //        OperandValue[] parameters;
        //        CriteriaOperator cri = CriteriaOperator.Parse("[AksesuarKodu] =? ", out parameters);
        //        parameters[0].Value = comboBox1.SelectedItem.ToString();
        //        IList list = objectSpace.GetObjects(typeof(Aksesuar), cri);
        //        List<TreeNode> checked_nodes = CheckedNodes(treeView1);


        //        foreach (Aksesuar satir in list)
        //        {

        //            if (satir.urunAilesi != null)
        //            {
        //                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunAilesi.UrunAilesiAdi, true);
        //                TreeNode parent = parentArray.FirstOrDefault();
        //                if (parent.Text.Equals(satir.urunAilesi.UrunAilesiAdi))
        //                {
        //                    parent.Checked = true;

        //                }
        //            }
        //            if (satir.urunGrubu != null)
        //            {
        //                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunGrubu.UrunGrubuAdi, true);
        //                TreeNode parent = parentArray.FirstOrDefault();

        //                if (parent.Text.Equals(satir.urunGrubu.UrunGrubuAdi))
        //                {
        //                    parent.Parent.Checked = true;
        //                    parent.Checked = true;
        //                }

        //            }
        //            if (satir.urunSerisi != null)
        //            {
        //                TreeNode[] parentArray = treeView1.Nodes.Find(satir.urunSerisi.UrunSerisiAdi, true);
        //                TreeNode parent = parentArray.FirstOrDefault();

        //                if (parent.Text.Equals(satir.urunSerisi.UrunSerisiAdi))
        //                {
        //                    parent.Parent.Checked = true;
        //                    parent.Checked = true;
        //                }
        //            }
        //            if (satir.boyut != null)
        //            {
        //                string str = satir.boyut.boyut;
        //                TreeNode[] parentArray = treeView1.Nodes.Find(str, false);
        //                TreeNode parent = parentArray.FirstOrDefault();

        //                //    if (parent.Text.Equals(satir.boyut.boyut))
        //                //    {
        //                //        parent.Parent.Checked = true;
        //                //        parent.Checked = true;
        //                //    }

        //            }

        //        }

        //        treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        //       | System.Windows.Forms.AnchorStyles.Left)
        //       | System.Windows.Forms.AnchorStyles.Right)));

        //        treeView1.CheckBoxes = true;
        //        treeView1.FullRowSelect = true;
        //        treeView1.Location = new System.Drawing.Point(12, 12);
        //        treeView1.AfterCheck += TreeView1_AfterCheck;       


        //    }

        //}
        // Return a list of the TreeNodes that are checked.       

        public void FillCheckBox()
        {
            if (comboBox1.SelectedItem != null)
            {
                urunAilesiAdiList.Clear();
                urunGrubuAdiList.Clear();
                urunSerisiAdiList.Clear();
                urunlerList.Clear();
                urunlerYukseklilList.Clear();
                OperandValue[] parameters;
                CriteriaOperator cri = CriteriaOperator.Parse("[AksesuarKodu] =? ", out parameters);
                parameters[0].Value = comboBox1.SelectedItem.ToString();
                IList list = objectSpace.GetObjects(typeof(Aksesuar), cri);



                foreach (Aksesuar satir in list)
                {
                    try
                    {
                        if (satir.urunAilesi != null) { urunAilesiAdiList.Add(satir.urunAilesi.UrunAilesiAdi); }
                        if (satir.urunGrubu != null) { urunGrubuAdiList.Add(satir.urunGrubu.UrunGrubuAdi); }
                        if (satir.urunSerisi != null) { urunSerisiAdiList.Add(satir.urunSerisi.UrunSerisiAdi); }
                        if (satir.boyut != null) { urunlerList.Add(satir.urunler.boyut.boyut); }
                        if (satir.yukseklik != null) { urunlerYukseklilList.Add(satir.urunler.yukseklik.Yuksek); }

                    }
                    catch (NullReferenceException ex)
                    {
                        //
                    }

                }
            }
            else { }
        }


        
        public void filltreeview()
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
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            imageSlider1.Images.Clear();
            FillCheckBox();
            createTreeWithChecked();
            List<byte[]> fotoList = new List<byte[]>();
            fotoList.Clear();
            CriteriaOperator criteria = CriteriaOperator.Parse("[AksesuarKodu] =?", comboBox1.SelectedItem.ToString());
            IList aksesuarList = objectSpace.GetObjects(typeof(Aksesuar),criteria);
           
            foreach (Aksesuar aksesuar in aksesuarList)
            {
                if (aksesuar.AksesuarKodu != null)
                {
                    foreach (Fotograflar satir2 in aksesuar.fotograflar)
                    {
                        fotoList.Add(satir2.fotograf);
                    }
                }

            }
            ImageConverter converter = new ImageConverter();            
 
            foreach(Byte[] satir in fotoList)
            {
                Image x = (Bitmap)(converter).ConvertFrom(satir);
                imageSlider1.Images.Add(x);
            }

            fotoList.Clear();
        }
        DataTable temp;
        DataTable bank;

        public void fillAksesuarList()
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            if (comboBox2.SelectedItem != null)
            {
                comboBox1.Items.Clear();
                IList aksesuarList = objectSpace.GetObjects(typeof(Aksesuar));
                IQueryable<Aksesuar> query = objectSpace.GetObjectsQuery<Aksesuar>(true);
                var list = from c in query
                           where c.AksesuarGrubu.AksesuarGrupAdi == comboBox2.SelectedItem.ToString()
                           orderby c.AksesuarKodu descending
                           group c by new { c.AksesuarKodu } into mygroup
                           select mygroup.FirstOrDefault();
            
                foreach (Aksesuar aksesuar in list)
                {
                    if (aksesuar.AksesuarKodu != null)
                    {
                        comboBox1.Items.Add(aksesuar.AksesuarKodu);
                    }


                }
            }
        }

        private void bbiReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("[AksesuarKodu] =?", comboBox1.SelectedItem.ToString());
            IList liste = objectSpace.GetObjects(typeof(Aksesuar),criteria);
            List<Aksesuar> aklist = new List<Aksesuar>();
                       

            foreach(Aksesuar satir in liste)
            {
                aklist.Add(satir);
            }
            foreach(Aksesuar satir in aklist)
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
            CriteriaOperator criterias = CriteriaOperator.Parse("[AksesuarKodu] =?", comboBox1.SelectedItem.ToString());
            IList Aksliste = objectSpace.GetObjects(typeof(Aksesuar), criterias);

            List<string> aksesuarAdi = new List<string>();

            foreach (Aksesuar satir in Aksliste)
            {
                aksesuarAdi.Add(satir.AksesuarAdi);
            }


            foreach (TreeNode node in checked_nodes)
            {

                string fullPath = node.FullPath;
                var pathItems = fullPath.Split('.');



                if (pathItems.Length == 4)
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


                    if (!seri.aksesuar.Select(y => y.AksesuarKodu).Contains(comboBox1.SelectedItem.ToString()) && !seri.urunAilesi.aksesuar.Select(y => y.AksesuarKodu).Contains(comboBox1.SelectedItem.ToString()))
                    {
                        Aksesuar akse = objectSpace.CreateObject<Aksesuar>();
                        akse.AksesuarKodu = comboBox1.SelectedItem.ToString();
                        akse.OlusturmaTarihi = DateTime.Today;
                        akse.AksesuarAdi = aksesuarAdi.FirstOrDefault();
                        seri.aksesuar.Add(akse);
                        seri.urunAilesi.aksesuar.Add(akse);
                        seri.urunGrubu.aksesuar.Add(akse);
                        seri.Save();
                        objectSpace.CommitChanges();

                    }
                    //Ürün serisi bu olan tüm ürünlerin aksesuarlar listesine bu aksesuarı ekle metodu                        

                    IList liste = objectSpace.GetObjects(typeof(Urunler), criteria1);


                    foreach (Urunler satir in liste)
                    {
                        Aksesuar aks = objectSpace.CreateObject<Aksesuar>();
                        aks.AksesuarAdi = aksesuarAdi.FirstOrDefault();
                        aks.AksesuarKodu = comboBox1.SelectedItem.ToString();
                        aks.boyut = satir.boyut;
                        satir.aksesuar.Add(aks);
                        satir.Save();
                        objectSpace.CommitChanges();
                    }
                }
                else
                {
                    // Do nothing
                }
            }

            MessageBox.Show("İşlem Başarıyla Kaydedildi.");

        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (imageSlider1 != null) {
                Image current = imageSlider1.CurrentImage;
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(current, typeof(byte[]));
                MessageBox.Show("byte"+current+xByte);
                List<Fotograflar> objectsToDelete = new List<Fotograflar>();
                CriteriaOperator criteria = CriteriaOperator.Parse("[AksesuarKodu] =?", comboBox1.SelectedItem.ToString());
                IList aksesuarList = objectSpace.GetObjects(typeof(Aksesuar), criteria);

                foreach (Aksesuar aksesuar in aksesuarList)
                {
                    if (aksesuar.AksesuarKodu != null)
                    {
                        foreach (Fotograflar satir2 in aksesuar.fotograflar)
                        {
                            var x = satir2.fotograf.SequenceEqual(xByte);
                            if (x == true)
                            {
                                objectsToDelete.Add(satir2);
                                
                            }
                        }
                    }

                }
                objectSpace.Delete(objectsToDelete);
                MessageBox.Show("Silindi");
            }
        }
        public void fillAksGrup()
        {
            comboBox2.Items.Clear();
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
                    comboBox2.Items.Add(aksesuar.AksesuarGrupAdi);
                }


            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            fillAksesuarList();
        }
    }
}

