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
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MidDosyaYonetim.Module.Forms;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Kataloglar : FileAttachmentBase, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Kataloglar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private UrunGrubu urungrup;
        private UrunSerisi urunseri;
        private UrunAilesi urunaile;

        private string _WebDokumanAdi;
        [XafDisplayName("Web için Dokuman Adı (TR)")]
        public string WebDokumanAdi
        {
            get { return _WebDokumanAdi; }
            set
            {
                SetPropertyValue<string>(nameof(WebDokumanAdi), ref _WebDokumanAdi, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(WebDokumanAdi);
                }
            }
        }

        [XafDisplayName("Web için Dokuman Adı (ENG)")]
        private string _EngWebDokumanAdi;
        public string EngWebDokumanAdi
        {
            get { return _EngWebDokumanAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngWebDokumanAdi), ref _EngWebDokumanAdi, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(EngWebDokumanAdi);
                }
            }
        }

        #region WebUrl Olusturma Islemleri
        char[] oldValue = new char[] { 'ö', 'ü', 'ç', 'ı', 'ğ', 'ş' };
        char[] newValue = new char[] { 'o', 'u', 'c', 'i', 'g', 's' };
        private string UrlOlustur(string value)
        {
            if (value != null)
            {
                string temp = value.ToLower();
                temp = temp.Trim();
                for (int sayac = 0; sayac < oldValue.Length; sayac++)
                {
                    temp = temp.Replace(oldValue[sayac], newValue[sayac]);
                }
                temp = temp.Replace(" ", "_");
                temp = temp.Replace(":", "æ");
                temp = temp.Replace("---", "-");
                temp = temp.Replace("?", "");
                temp = temp.Replace("/", "");
                temp = temp.Replace(".", "");
                temp = temp.Replace("'", "");
                temp = temp.Replace("#", "");
                temp = temp.Replace("%", "");
                temp = temp.Replace("&", "");
                temp = temp.Replace("*", "");
                temp = temp.Replace("!", "");
                temp = temp.Replace("@", "");
                temp = temp.Replace("+", "");
                return temp;
            }
            return "";
        }
        #endregion

        [Association("KatalogToAile")]
        public UrunAilesi urunAilesi
        {
            get { return getAile(); }
            set { }
        }

        private int _index;
        public int index
        {
            get { return _index; }
            set { SetPropertyValue<int>(nameof(index), ref _index, value); }
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
        [Association("KatalogToGrup")]
        public UrunGrubu urunGrubu
        {

            get { return getGrup(); }
            set { }
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
        [Association("KatalogToSeri")]
        public UrunSerisi urunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        }
        [Association("KatalogToUrun")]
        public Urunler urunler { get; set; }
        [Association("KatalogToAksesuar")]
        public Aksesuar Aksesuar { get; set; }


        [Association("KatalogToAksGrubu")]
        public AksesuarGrubu aksesuarGrubu;

        public string KatalogAdi;

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30)]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }


        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;
        private string _WebUrl;
        public string WebUrl
        {
            get { return _WebUrl; }
            set { SetPropertyValue<string>(nameof(WebUrl), ref _WebUrl, value); }
        }
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;
        private string _EngWebUrl;
        public string EngWebUrl
        {
            get { return _EngWebUrl; }
            set { SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value); }
        }

        [XafDisplayName("Türkçe Sayfada Lande Genel Katalog Yap")]
        public bool LandeGenelKatalog;

        [XafDisplayName("İngilizce Sayfada Lande Genel Katalog Yap")]
        public bool EngLandeGenelKatalog;

        [XafDisplayName("Web'de Kataloglarda Göster")]
        public bool KataloglarSayfasi;

        [XafDisplayName("Türkçe Sayfada Lande Kurumsal Logo Kullanımı")]
        public bool LKLK;

        [XafDisplayName("İngilizce Sayfada Lande Kurumsal Logo Kullanımı")]
        public bool EngLKLK;

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        [Association("KataloglarSayfasi - Kataloglar")]
        public KataloglarSayfasi kataloglarSayfa;

        private DateTime _SonGuncellemeTarihi;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue<DateTime>(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }


        protected override void OnSaving()
        {
            //if (EngWeb == true && EngWebDokumanAdi == null)
            //{
            //    throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Akesesuar Adının ingilizcesini giriniz.");
            //}

            /////////Fotoğraf boyutlandırma
            if (fotograf != null)
            {
                Image newImage = byteArrayToImage(fotograf);
                Bitmap yeniimg = new Bitmap(208, 294);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(newImage, 0, 0, 208, 294);

                MemoryStream stream = new MemoryStream();
                yeniimg.Save(stream, ImageFormat.Jpeg);

                fotograf = stream.GetBuffer();
            }
           

            /////////////////

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

            if (LandeGenelKatalog == true)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("[LandeGenelKatalog] = true");

                Kataloglar liste = (Kataloglar)objectSpace.FindObject(typeof(Kataloglar), criteria);
                List<Kataloglar> GenelKatalog = new List<Kataloglar>();
                if (liste != null)
                {
                    liste.LandeGenelKatalog = false;
                    liste.Save();
                    //objectSpace.CommitChanges();


                }
                LandeGenelKatalog = true;

            }

            if (EngLandeGenelKatalog == true)
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("[EngLandeGenelKatalog] = true");

                Kataloglar liste = (Kataloglar)objectSpace.FindObject(typeof(Kataloglar), criteria);
                List<Kataloglar> GenelKatalog = new List<Kataloglar>();
                if (liste != null)
                {
                    liste.EngLandeGenelKatalog = false;
                    liste.Save();

                }
                EngLandeGenelKatalog = true;
            }
            SonGuncellemeTarihi = DateTime.Now;


            base.OnSaving();
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
        public Image byteArrayToImage(byte[] byteArrayIn)
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

    }
}