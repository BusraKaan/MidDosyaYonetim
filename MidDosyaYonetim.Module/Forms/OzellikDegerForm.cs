using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.ExpressApp;
using MidDosyaYonetim.Module.BusinessObjects;
using System.Collections;
using DevExpress.Utils.Drawing;
using System.Reflection.Emit;
using DevExpress.Utils.Extensions;
using DevExpress.Data.Filtering;
using DevExpress.DataProcessing;
using System.Security.Cryptography;
using DevExpress.Data.Helpers;
using System.Drawing.Text;

namespace MidDosyaYonetim.Module.Forms
{

    public partial class OzellikDegerForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private IObjectSpace objectSpace;
        private Urunler Urunler;
        private Aksesuar Aksesuar;
        Dictionary<Guid, string> values = new Dictionary<Guid, string>();
        private static string bilgilendir;
        public static string urunView;
        public static string aksView;
        //Dictionary<string, Dictionary<string, string>> oidvalues = new Dictionary<string, Dictionary<string, string>>();


        //CBC5E2
        Color colorpanel = ColorTranslator.FromHtml("#CBC5E2");
        Color colorozellikAdi = ColorTranslator.FromHtml("#7769A6");
        Color colordegerAdi = ColorTranslator.FromHtml("#A398C7");
        //Color colordegerAdi = ColorTranslator.FromHtml(hexString);

        public OzellikDegerForm(IObjectSpace objectSpace, string detay, string urunDetail, string aksDetail, Guid GuidCatcher)
        {
            InitializeComponent();
            this.objectSpace = objectSpace;
            bilgilendir = detay;
            if (detay == aksDetail)
            {
                this.Aksesuar = objectSpace.FindObject<Aksesuar>(CriteriaOperator.Parse("[Oid]=?", GuidCatcher));
            }
            else
            {
                this.Urunler = objectSpace.FindObject<Urunler>(CriteriaOperator.Parse("[Oid]=?", GuidCatcher));
            }
            urunView = urunDetail;
            aksView = aksDetail;
            //this.Urunler = Urunler;

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BorderStyle = BorderStyle.None;
            createOzellik();
        }

        public static string urunguid;




        public void createOzellik()
        {

            if (bilgilendir == urunView)
            {
                List<Ozellikler> ozelliklerList = new List<Ozellikler>();
                this.objectSpace.GetObjects(typeof(DegerTipleri));
                IList objects1 = this.objectSpace.GetObjects(typeof(Ozellikler));
                IList objects2 = this.objectSpace.GetObjects(typeof(UrunDegerler), CriteriaOperator.Parse("[urunler]=?", this.Urunler.Oid));
                IList objects3 = this.objectSpace.GetObjects(typeof(Degerler));
                List<Degerler> source1 = new List<Degerler>();
                foreach (Degerler degerler in objects3)
                {
                    source1.Add(degerler);
                }
                var groupings = source1.GroupBy(x => x.ozellikler);
                int num1 = 0;
                foreach (var source2 in groupings)
                {
                    int num2 = source2.Count<Degerler>();
                    if (num2 > num1)
                    {
                        num1 = num2;
                    }
                }
                foreach (Ozellikler ozellikler in objects1)
                {
                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    flowLayoutPanel.Height = 50 + 34 * num1;
                    flowLayoutPanel.BackColor = colorpanel;
                    flowLayoutPanel.Padding = new Padding(10);
                    flowLayoutPanel.Width = 350;

                    Button control1 = new Button();
                    control1.Text = ozellikler.OzellikAdi;
                    control1.Width = 330;
                    control1.Height = 50;
                    control1.BackColor = colorozellikAdi;
                    control1.ForeColor = Color.White;
                    control1.Font = new Font("Arial", 13, FontStyle.Bold);
                    control1.Padding = new Padding(3);
                    flowLayoutPanel.AddControl(control1);

                    CriteriaOperator criteria = CriteriaOperator.Parse("[ozellikler]=?", ozellikler);
                    IList listdegerler = objectSpace.GetObjects(typeof(Degerler), criteria);
                    foreach (Degerler degerler in listdegerler)
                    {
                        FlowLayoutPanel pnl2 = new FlowLayoutPanel();
                        pnl2.Height = 380;
                        Button control2 = new Button();
                        control2.Size = new System.Drawing.Size(50, 25);
                        control2.ForeColor = Color.White;
                        control2.BackColor = colordegerAdi;
                        control2.Font = new Font("Arial", 8, FontStyle.Bold);
                        control2.Width = 100;
                        System.Windows.Forms.Label control3 = new System.Windows.Forms.Label();
                        TextBox control4 = new TextBox();
                        control4.BorderStyle = BorderStyle.None;
                        control4.Width = 180;
                        control4.Padding = new Padding(10);
                        control4.TextChanged += Deger_TextChanged;
                        control2.Text = degerler.DegerAdi;
                        control3.Text = degerler.DegerTipi != null ? degerler.DegerTipi.DegerTipi : "";
                        control3.Width = 30;

                        foreach (UrunDegerler urunDegerler in objects2)
                        {
                            if (urunDegerler.degerler == degerler && urunDegerler.urunler == urunDegerler.Session.GetObjectByKey<Urunler>(Urunler.Oid))
                            { control4.Text = urunDegerler.Deger; }
                        }
                        //

                        Guid oid = degerler.Oid;
                        control4.Name = degerler.Oid.ToString();
                        flowLayoutPanel.AddControl(control2);
                        flowLayoutPanel.AddControl(control4);

                        flowLayoutPanel.AddControl(control3);

                        flowLayoutPanel1.AddControl(flowLayoutPanel);
                    }
                }
            }
            else
            {
                List<Ozellikler> ozelliklerList = new List<Ozellikler>();
                this.objectSpace.GetObjects(typeof(DegerTipleri));
                IList objects1 = this.objectSpace.GetObjects(typeof(Ozellikler));
                IList objects2 = this.objectSpace.GetObjects(typeof(aksesuarDeger), CriteriaOperator.Parse("[aksesuar]=?", this.Aksesuar.Oid));
                IList objects3 = this.objectSpace.GetObjects(typeof(Degerler));
                List<Degerler> source1 = new List<Degerler>();
                foreach (Degerler degerler in objects3)
                {
                    source1.Add(degerler);
                }
                var groupings = source1.GroupBy(x => x.ozellikler);
                int num1 = 0;
                foreach (var source2 in groupings)
                {
                    int num2 = source2.Count<Degerler>();
                    if (num2 > num1)
                    {
                        num1 = num2;
                    }
                }
                foreach (Ozellikler ozellikler in objects1)
                {
                    FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                    flowLayoutPanel.Height = 50 + 34 * num1;
                    flowLayoutPanel.BackColor = colorpanel;
                    flowLayoutPanel.Padding = new Padding(10);
                    flowLayoutPanel.Width = 350;

                    Button control1 = new Button();
                    control1.Text = ozellikler.OzellikAdi;
                    control1.Width = 330;
                    control1.Height = 50;
                    control1.BackColor = colorozellikAdi;
                    control1.ForeColor = Color.White;
                    control1.Font = new Font("Arial", 13, FontStyle.Bold);
                    control1.Padding = new Padding(3);
                    flowLayoutPanel.AddControl(control1);

                    CriteriaOperator criteria = CriteriaOperator.Parse("[ozellikler]=?", ozellikler);
                    IList listdegerler = objectSpace.GetObjects(typeof(Degerler), criteria);
                    foreach (Degerler degerler in listdegerler)
                    {
                        FlowLayoutPanel pnl2 = new FlowLayoutPanel();
                        pnl2.Height = 380;
                        Button control2 = new Button();
                        control2.Size = new System.Drawing.Size(50, 25);
                        control2.ForeColor = Color.White;
                        control2.BackColor = colordegerAdi;
                        control2.Font = new Font("Arial", 8, FontStyle.Bold);
                        control2.Width = 100;
                        System.Windows.Forms.Label control3 = new System.Windows.Forms.Label();
                        TextBox control4 = new TextBox();
                        control4.BorderStyle = BorderStyle.None;
                        control4.Width = 180;
                        control4.Padding = new Padding(10);
                        control4.TextChanged += Deger_TextChanged;
                        control2.Text = degerler.DegerAdi;
                        control3.Text = degerler.DegerTipi != null ? degerler.DegerTipi.DegerTipi : "";
                        control3.Width = 30;
                        //degerTipi.Text = satir2.DegerTipi.DegerTipi;

                        foreach (aksesuarDeger aksesuarDeger in objects2)
                        {
                            if (aksesuarDeger.degerler == degerler && aksesuarDeger.aksesuar == aksesuarDeger.Session.GetObjectByKey<Aksesuar>(Aksesuar.Oid))
                            { control4.Text = aksesuarDeger.Deger; }
                        }
                        //

                        Guid oid = degerler.Oid;
                        control4.Name = degerler.Oid.ToString();
                        flowLayoutPanel.AddControl(control2);
                        flowLayoutPanel.AddControl(control4);

                        flowLayoutPanel.AddControl(control3);

                        flowLayoutPanel1.AddControl(flowLayoutPanel);
                    }
                }
            }

        }

        private void Deger_TextChanged(object sender, EventArgs e)
        {
            var deger = sender as TextBox;
            if (deger.Name != "")
            {
                if (values.ContainsKey(Guid.Parse(deger.Name)))
                {
                    values[Guid.Parse(deger.Name)] = deger.Text;
                }
                else
                {

                    values.Add(Guid.Parse(deger.Name), deger.Text);

                }

            }

        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (OzellikDegerForm.bilgilendir == OzellikDegerForm.urunView)
            {
                List<UrunDegerler> source = new List<UrunDegerler>();
                IList objects = this.objectSpace.GetObjects(typeof(Degerler));
                foreach (UrunDegerler urunDegerler in this.objectSpace.GetObjects(typeof(UrunDegerler), CriteriaOperator.Parse("[urunler]=?", this.Urunler.Oid)))
                {
                    if (urunDegerler.urunler == urunDegerler.Session.GetObjectByKey<Urunler>(this.Urunler.Oid))
                    {
                        source.Add(urunDegerler);
                        OzellikDegerForm.urunguid = urunDegerler.urunler.Oid.ToString();
                    }
                }
                foreach (Degerler degerler1 in objects)
                {
                    if (this.values.ContainsKey(degerler1.Oid))
                    {
                        CriteriaOperator criteria1 = CriteriaOperator.Parse("[Oid]=?", (object)degerler1.Oid);
                        CriteriaOperator criteria2 = CriteriaOperator.Parse("[Oid]=?", (object)this.Urunler.Oid);
                        Degerler degerler2 = this.objectSpace.FindObject<Degerler>(criteria1);
                        this.objectSpace.FindObject<Urunler>(criteria2);
                        if (!source.Select<UrunDegerler, Degerler>((Func<UrunDegerler, Degerler>)(x => x.degerler)).Contains<Degerler>(degerler2))
                        {
                            UrunDegerler urunDegerler = this.objectSpace.CreateObject<UrunDegerler>();
                            urunDegerler.Deger = this.values[degerler1.Oid];
                            urunDegerler.urunler = urunDegerler.Session.GetObjectByKey<Urunler>((object)this.Urunler.Oid);
                            urunDegerler.degerler = degerler2;
                            urunDegerler.Save();
                            this.objectSpace.CommitChanges();
                        }
                        else
                        {
                            UrunDegerler urunDegerler = (UrunDegerler)this.objectSpace.FindObject(typeof(UrunDegerler), CriteriaOperator.Parse("[degerler]=? and [urunler]=?", degerler1.Oid, OzellikDegerForm.urunguid));
                            urunDegerler.Deger = this.values[degerler1.Oid];
                            urunDegerler.Save();
                            this.objectSpace.CommitChanges();
                        }
                    }
                }
            }
            else if (OzellikDegerForm.bilgilendir == OzellikDegerForm.aksView)
            {
                List<aksesuarDeger> source = new List<aksesuarDeger>();
                IList objects = this.objectSpace.GetObjects(typeof(Degerler));
                foreach (aksesuarDeger aksesuarDeger in (IEnumerable)this.objectSpace.GetObjects(typeof(aksesuarDeger), CriteriaOperator.Parse("[aksesuar]=?", this.Aksesuar.Oid)))
                {
                    if (aksesuarDeger.aksesuar == aksesuarDeger.Session.GetObjectByKey<Aksesuar>((object)this.Aksesuar.Oid))
                    {
                        source.Add(aksesuarDeger);
                        OzellikDegerForm.urunguid = aksesuarDeger.aksesuar.Oid.ToString();
                    }
                }
                foreach (Degerler degerler1 in objects)
                {
                    if (this.values.ContainsKey(degerler1.Oid))
                    {
                        CriteriaOperator criteria1 = CriteriaOperator.Parse("[Oid]=?", (object)degerler1.Oid);
                        CriteriaOperator criteria2 = CriteriaOperator.Parse("[Oid]=?", (object)this.Aksesuar.Oid);
                        Degerler degerler2 = this.objectSpace.FindObject<Degerler>(criteria1);
                        this.objectSpace.FindObject<Aksesuar>(criteria2);
                        if (!source.Select<aksesuarDeger, Degerler>((Func<aksesuarDeger, Degerler>)(x => x.degerler)).Contains<Degerler>(degerler2))
                        {
                            aksesuarDeger aksesuarDeger = this.objectSpace.CreateObject<aksesuarDeger>();
                            aksesuarDeger.Deger = this.values[degerler1.Oid];
                            aksesuarDeger.aksesuar = aksesuarDeger.Session.GetObjectByKey<Aksesuar>(this.Aksesuar.Oid);
                            aksesuarDeger.degerler = degerler2;
                            aksesuarDeger.Save();
                            this.objectSpace.CommitChanges();
                        }
                        else
                        {
                            aksesuarDeger aksesuarDeger = (aksesuarDeger)this.objectSpace.FindObject(typeof(aksesuarDeger), CriteriaOperator.Parse("[degerler]=? and [aksesuar]=?",degerler1.Oid, OzellikDegerForm.urunguid));
                            aksesuarDeger.Deger = this.values[degerler1.Oid];
                            aksesuarDeger.Save();
                            this.objectSpace.CommitChanges();
                        }
                    }
                }
            }
            else
            {
                int num1 = (int)MessageBox.Show("Bir Hata ile Karşılaşıldı. Lütfen Teknik Destek için Midsoft ile iletişime Geçiniz");
            }
            int num2 = (int)MessageBox.Show("Kayıt Başarılı");

            //List<UrunDegerler> list = new List<UrunDegerler>();
            //IList listdegerler = objectSpace.GetObjects(typeof(Degerler));
            //IList listurundegerler = objectSpace.GetObjects(typeof(UrunDegerler));
            ////foreach (UrunDegerler  satir2 in listurundegerler) { list.Add(satir2); urunguid = satir2.urunler.Oid.ToString(); }


            //foreach (UrunDegerler satir2 in listurundegerler)
            //{

            //    if (satir2.urunler == satir2.Session.GetObjectByKey<Urunler>(Urunler.Oid)) { list.Add(satir2); urunguid = satir2.urunler.Oid.ToString(); }


            //}

            //foreach (Degerler satir in listdegerler)
            //{
            //    if (values.ContainsKey(satir.Oid))
            //    {

            //        CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", satir.Oid);
            //        CriteriaOperator criteriaurun = CriteriaOperator.Parse("[Oid]=?", Urunler.Oid);
            //        Degerler degerlerobj = objectSpace.FindObject<Degerler>(criteria);
            //        Urunler urunlerobj = objectSpace.FindObject<Urunler>(criteriaurun);

            //        if (!list.Select(x => x.degerler).Contains(degerlerobj))
            //        {
            //            UrunDegerler urunDegerler = objectSpace.CreateObject<UrunDegerler>();
            //            urunDegerler.Deger = values[satir.Oid];
            //            urunDegerler.urunler = urunDegerler.Session.GetObjectByKey<Urunler>(Urunler.Oid);
            //            urunDegerler.degerler = degerlerobj;
            //            urunDegerler.Save();
            //            objectSpace.CommitChanges();
            //        }
            //        else
            //        {

            //            CriteriaOperator cri = CriteriaOperator.Parse("[degerler]=? and [urunler]=?", satir.Oid, urunguid);
            //            UrunDegerler urunDegerler = (UrunDegerler)objectSpace.FindObject(typeof(UrunDegerler), cri);

            //            urunDegerler.Deger = values[satir.Oid];
            //            urunDegerler.Save();
            //            objectSpace.CommitChanges();

            //        }
            //    }
            //}

            //MessageBox.Show("Kayıt Başarılı");
        }

        private void bbiSaveAndClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (OzellikDegerForm.bilgilendir == OzellikDegerForm.urunView)
            {
                List<UrunDegerler> source = new List<UrunDegerler>();
                IList objects = this.objectSpace.GetObjects(typeof(Degerler));
                foreach (UrunDegerler urunDegerler in this.objectSpace.GetObjects(typeof(UrunDegerler), CriteriaOperator.Parse("[urunler]=?", (object)this.Urunler.Oid)))
                {
                    if (urunDegerler.urunler == urunDegerler.Session.GetObjectByKey<Urunler>((object)this.Urunler.Oid))
                    {
                        source.Add(urunDegerler);
                        OzellikDegerForm.urunguid = urunDegerler.urunler.Oid.ToString();
                    }
                }
                foreach (Degerler degerler1 in objects)
                {
                    if (this.values.ContainsKey(degerler1.Oid))
                    {
                        CriteriaOperator criteria1 = CriteriaOperator.Parse("[Oid]=?", (object)degerler1.Oid);
                        CriteriaOperator criteria2 = CriteriaOperator.Parse("[Oid]=?", (object)this.Urunler.Oid);
                        Degerler degerler2 = this.objectSpace.FindObject<Degerler>(criteria1);
                        this.objectSpace.FindObject<Urunler>(criteria2);
                        if (!source.Select<UrunDegerler, Degerler>((Func<UrunDegerler, Degerler>)(x => x.degerler)).Contains<Degerler>(degerler2))
                        {
                            UrunDegerler urunDegerler = this.objectSpace.CreateObject<UrunDegerler>();
                            urunDegerler.Deger = this.values[degerler1.Oid];
                            urunDegerler.urunler = urunDegerler.Session.GetObjectByKey<Urunler>((object)this.Urunler.Oid);
                            urunDegerler.degerler = degerler2;
                            urunDegerler.Save();
                            this.objectSpace.CommitChanges();
                        }
                        else
                        {
                            UrunDegerler urunDegerler = (UrunDegerler)this.objectSpace.FindObject(typeof(UrunDegerler), CriteriaOperator.Parse("[degerler]=? and [urunler]=?", degerler1.Oid,OzellikDegerForm.urunguid));
                            urunDegerler.Deger = this.values[degerler1.Oid];
                            urunDegerler.Save();
                            this.objectSpace.CommitChanges();
                        }
                    }
                }
            }
            else if (OzellikDegerForm.bilgilendir == OzellikDegerForm.aksView)
            {
                List<aksesuarDeger> source = new List<aksesuarDeger>();
                IList objects = this.objectSpace.GetObjects(typeof(Degerler));
                foreach (aksesuarDeger aksesuarDeger in (IEnumerable)this.objectSpace.GetObjects(typeof(aksesuarDeger), CriteriaOperator.Parse("[aksesuar]=?", (object)this.Aksesuar.Oid)))
                {
                    if (aksesuarDeger.aksesuar == aksesuarDeger.Session.GetObjectByKey<Aksesuar>((object)this.Aksesuar.Oid))
                    {
                        source.Add(aksesuarDeger);
                        OzellikDegerForm.urunguid = aksesuarDeger.aksesuar.Oid.ToString();
                    }
                }
                foreach (Degerler degerler1 in objects)
                {
                    if (this.values.ContainsKey(degerler1.Oid))
                    {
                        CriteriaOperator criteria1 = CriteriaOperator.Parse("[Oid]=?", degerler1.Oid);
                        CriteriaOperator criteria2 = CriteriaOperator.Parse("[Oid]=?", this.Aksesuar.Oid);
                        Degerler degerler2 = this.objectSpace.FindObject<Degerler>(criteria1);
                        this.objectSpace.FindObject<Aksesuar>(criteria2);
                        if (!source.Select<aksesuarDeger, Degerler>((Func<aksesuarDeger, Degerler>)(x => x.degerler)).Contains<Degerler>(degerler2))
                        {
                            aksesuarDeger aksesuarDeger = this.objectSpace.CreateObject<aksesuarDeger>();
                            aksesuarDeger.Deger = this.values[degerler1.Oid];
                            aksesuarDeger.aksesuar = aksesuarDeger.Session.GetObjectByKey<Aksesuar>(this.Aksesuar.Oid);
                            aksesuarDeger.degerler = degerler2;
                            aksesuarDeger.Save();
                            this.objectSpace.CommitChanges();
                        }
                        else
                        {
                            aksesuarDeger aksesuarDeger = (aksesuarDeger)this.objectSpace.FindObject(typeof(aksesuarDeger), CriteriaOperator.Parse("[degerler]=? and [aksesuar]=?", degerler1.Oid, OzellikDegerForm.urunguid));
                            aksesuarDeger.Deger = this.values[degerler1.Oid];
                            aksesuarDeger.Save();
                            this.objectSpace.CommitChanges();
                        }
                    }
                }
            }
            else
            {
                int num1 = (int)MessageBox.Show("Bir Hata ile Karşılaşıldı. Lütfen Teknik Destek için Midsoft ile iletişime Geçiniz");
            }
            int num2 = (int)MessageBox.Show("Kayıt Başarılı");
            this.Close();

            //List<UrunDegerler> list = new List<UrunDegerler>();
            //IList listdegerler = objectSpace.GetObjects(typeof(Degerler));
            //IList listurundegerler = objectSpace.GetObjects(typeof(UrunDegerler));

            //foreach (UrunDegerler satir2 in listurundegerler)
            //{

            //    if (satir2.urunler == satir2.Session.GetObjectByKey<Urunler>(Urunler.Oid)) { list.Add(satir2); urunguid = satir2.urunler.Oid.ToString(); }


            //}

            //foreach (Degerler satir in listdegerler)
            //{
            //    if (values.ContainsKey(satir.Oid))
            //    {

            //        CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", satir.Oid);
            //        Degerler degerlerobj = objectSpace.FindObject<Degerler>(criteria);

            //        if (!list.Select(x => x.degerler).Contains(degerlerobj))
            //        {
            //            UrunDegerler urunDegerler = objectSpace.CreateObject<UrunDegerler>();
            //            urunDegerler.Deger = values[satir.Oid];
            //            urunDegerler.urunler = urunDegerler.Session.GetObjectByKey<Urunler>(Urunler.Oid);
            //            urunDegerler.degerler = degerlerobj;
            //            urunDegerler.Save();
            //            objectSpace.CommitChanges();
            //        }
            //        else
            //        {

            //            CriteriaOperator cri = CriteriaOperator.Parse("[degerler]=? and [urunler]=?", satir.Oid, urunguid);
            //            UrunDegerler urunDegerler = (UrunDegerler)objectSpace.FindObject(typeof(UrunDegerler), cri);

            //            urunDegerler.Deger = values[satir.Oid];
            //            urunDegerler.Save();
            //            objectSpace.CommitChanges();

            //        }
            //    }
            //}

            //MessageBox.Show("Kayıt Başarılı");
            //Close();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Kaydetmeden çıkmak istiyor musunuz?", "Kapatmak istiyor musunuz?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Close();
            }
            else if (dialogResult == DialogResult.No)
            {

            }


        }
    }
}