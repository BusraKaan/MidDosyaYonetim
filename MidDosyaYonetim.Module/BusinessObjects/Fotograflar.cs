using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Utils;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using DevExpress.DataProcessing;
using NPOI.Util;
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Fotograflar : FileAttachmentBase, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Fotograflar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OlusturmaTarihi = DateTime.Now;
            if (SecuritySystem.CurrentUser != null)
            {
                var olusturanKisi = SecuritySystem.CurrentUserName;
                OlusturanKisi = olusturanKisi.ToString();



            }
        }
        private UrunGrubu urungrup;
        private UrunSerisi urunseri;
        private UrunAilesi urunaile;

        [XafDisplayName("Ürün Ailesi")]
        [Association("FotografToAilesi")]
        public UrunAilesi urunAilesi
        {
            get { return getAile(); }
            set { SetPropertyValue(nameof(urunAilesi), ref urunaile, value); }
        }
        private UrunAilesi getAile()
        {
            if (urunseri != null)
            {
                urunaile = urunseri.urunAilesi;
                return urunaile;
            }
            return urunaile;
        }

        [XafDisplayName("Ürün Grubu")]
        [Association("FotografToGrubu")]
        public UrunGrubu urunGrubu
        {

            get { return getGrup(); }
            set { SetPropertyValue(nameof(urunGrubu), ref urungrup, value); }
        }
        private UrunGrubu getGrup()
        {
            if (urunSerisi != null)
            {
                urungrup = urunSerisi.urunGrubu;
                return urungrup;
            }
            return urungrup;

        }

        [XafDisplayName("Ürün Serisi")]
        [Association("FotografToSerisi")]
        public UrunSerisi urunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        }
        [Association("FotografToUrunler")]
        [XafDisplayName("Ürünler")]
        public Urunler urunler { get; set; }
        [XafDisplayName("Aksesuar")]
        [Association("FotografToAksesuar")]
        public Aksesuar aksesuar;
        [XafDisplayName("Kapak Opsiyonları")]
        [Association("FotografToOpsiyon")]
        public KapakOpsiyonlari kapakOpsiyonlari;

        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Content;

        byte[] imageBytes;

        private Image img;

        private DateTime _SonGuncellemeTarihi;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue<DateTime>(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }
        //protected override void EndEdit()
        //{
        //    base.EndEdit();
        //}
        [Browsable(false)]
        public bool EditKontrol = false;
        protected override void DoEndEditAction()
        {
            base.DoEndEditAction();
            EditKontrol = true;
        }
        protected override void OnSaving()
        {
            SonGuncellemeTarihi = DateTime.Now;
            if (urunler != null)
            {
                this.urunAilesi = urunler.urunAilesi;
                this.urunGrubu = urunler.urunGrubu;
                this.urunSerisi = urunler.urunSerisi;
                this.urunler = urunler;


            }
            else
            {
                if (urunSerisi != null)
                {
                    this.urunAilesi = urunSerisi.urunAilesi;
                    this.urunGrubu = urunSerisi.urunGrubu;
                    this.urunSerisi = urunSerisi;

                }
                else
                {
                    if (urunGrubu != null)
                    {
                        this.urunAilesi = urunGrubu.urunAilesi;
                        this.urunGrubu = urunGrubu;

                    }
                    else
                    {
                        if (urunAilesi != null)
                        {
                            this.urunAilesi = urunAilesi;

                        }
                    }
                }

            }

            using (MemoryStream ms = new MemoryStream())
            {

                File.SaveToStream(ms);
                ms.Flush();
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                Content = sr.ReadToEnd();
                imageBytes = Encoding.UTF8.GetBytes(Content);
                ms.Write(imageBytes, 0, imageBytes.Length);
                ms.Position = 0; //insert this line
                img = Image.FromStream(ms);
                img.Save(ms, ImageFormat.Jpeg);

                fotograf = ms.ToArray();
                this.fotograf = fotograf;


            }

            if (urunler != null || aksesuar != null || urungrup != null || urunseri != null || urunaile != null)
            {
                if (KayitKontrol == false)
                {
                    WebFotografOlustur();
                }

                if (EditKontrol == true)
                {
                    WebFotografGuncelle();
                }
            }

            base.OnSaving();
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            WebFotografSil();
        }
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        #region Fotoğraf Boyutlandırma
        [Browsable(false)]
        public bool KayitKontrol = false;
        public void WebFotografOlustur()
        {
            //CriteriaOperator crtiteria = CriteriaOperator.Parse("urunler=?", urunler.Oid);
            //Fotograflar Fotograf = (Fotograflar)objectSpace.FindObject(typeof(Fotograflar), crtiteria);
            if (fotograf != null)
            {
                Image newImage = byteArrayToImage(fotograf);
                //int genislik = newImage.Width;
                //int yukseklik = newImage.Height;
                Bitmap yeniimg = new Bitmap(300, 300);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(newImage, 0, 0, 300, 300);


                MemoryStream stream = new MemoryStream();
                yeniimg.Save(stream, ImageFormat.Jpeg);

                WebFotograf webfoto = objectSpace.CreateObject<WebFotograf>();
                webfoto.fotograf = stream.GetBuffer();
                if (urunler != null)
                {
                    webfoto.Urun = urunler;
                }
                if (urungrup != null)
                {
                    webfoto.UrunGrubu = urungrup;
                }
                if (urunseri != null)
                {
                    webfoto.UrunSerisi = urunseri;
                }
                if (aksesuar != null)
                {
                    webfoto.Aksesuar = aksesuar;
                }
                webfoto.KaliteliFotografOid = this;
                webfoto.Web = Web;
                webfoto.EngWeb = EngWeb;
                webfoto.Index = Index;

            }
            KayitKontrol = true;
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                Image returnImage = Image.FromStream(ms, true);//Exception occurs here
                return returnImage;
            }
            catch
            {
                return null;
            }
        }

        private void WebFotografGuncelle()
        {
            CriteriaOperator cri = CriteriaOperator.Parse("KaliteliFotografOid =?", this);
            WebFotograf Fotograf = (WebFotograf)objectSpace.FindObject(typeof(WebFotograf), cri);
            if (Fotograf!=null)
            {
                Image newImage = byteArrayToImage(fotograf);
                //int genislik = newImage.Width;
                //int yukseklik = newImage.Height;
                Bitmap yeniimg = new Bitmap(300, 300);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(newImage, 0, 0, 300, 300);


                MemoryStream stream = new MemoryStream();
                yeniimg.Save(stream, ImageFormat.Jpeg);

                Fotograf.fotograf = stream.GetBuffer();
                if (urunler != null)
                {
                    Fotograf.Urun = urunler;
                }
                if (urungrup != null)
                {
                    Fotograf.UrunGrubu = urungrup;
                }
                if (urunseri != null)
                {
                    Fotograf.UrunSerisi = urunseri;
                }
                if (aksesuar != null)
                {
                    Fotograf.Aksesuar = aksesuar;
                }
                Fotograf.KaliteliFotografOid = this;
                Fotograf.Web = Web;
                Fotograf.EngWeb = EngWeb;
                Fotograf.Index = Index;
            }
            else
            {
                MessageBox.Show("Güncellenecek Web Fotoğrafı bulunamadı.");
            }
            
        }

        private void WebFotografSil()
        {
            CriteriaOperator cri = CriteriaOperator.Parse("KaliteliFotografOid =?", this);
            WebFotograf Fotograf = (WebFotograf)objectSpace.FindObject(typeof(WebFotograf), cri);
            if (Fotograf!=null)
            {
                Fotograf.Delete();
            }
        }
        #endregion

        [XafDisplayName("Parçalar")]
        [Association("FotografToParca")]
        public Parcalar parcalar;


        [XafDisplayName("Revizeler")]
        [Association("FotografToRevize")]
        public XPCollection<Revizeler> revizeler
        {
            get
            {
                return GetCollection<Revizeler>(nameof(revizeler));
            }



        }
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), ToolTip("1000x1000 Kare Fotoğraf Olmalıdır.")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }



        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;

        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToFotograflar")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }



        }
        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
        }

        protected override void OnSaved()
        {
            string sorgu = "Update FileData SET SonGuncellemeTarihi = '" + DateTime.Now.ToString() + "' Where Oid='" + File.Oid.ToString() + "'";
            con = new SqlConnection("Server=10.26.0.30; Database=test3; User Id=sa;Password=Mdsf2020@");
            cmd = new SqlCommand(sorgu, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            base.OnSaved();
        }

        private SqlConnection con;
        private SqlCommand cmd;
    }

}