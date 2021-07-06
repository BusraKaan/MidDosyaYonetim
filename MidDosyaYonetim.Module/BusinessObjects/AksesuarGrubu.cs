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
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class AksesuarGrubu : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public AksesuarGrubu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string _AksesuarGrupAdi;
        [XafDisplayName("Aksesuar Grup Adı (TR)")]
        public string AksesuarGrupAdi
        {
            get { return _AksesuarGrupAdi; }
            set
            {
                SetPropertyValue<string>(nameof(AksesuarGrupAdi), ref _AksesuarGrupAdi, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(AksesuarGrupAdi);
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



        private string _EngAksesuarGrupAdi;
        [XafDisplayName("Aksesuar Grup Adı (ENG)")]
        public string EngAksesuarGrupAdi
        {
            get { return _EngAksesuarGrupAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngAksesuarGrupAdi), ref _EngAksesuarGrupAdi, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(EngAksesuarGrupAdi);
                }
            }
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30)]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }
        [XafDisplayName("Açıklama (TR)")]
        public string Aciklama;
        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama;

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

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
        }

        [Association("MontajToAksGrubu")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }

        [XafDisplayName("Banner")]
        [Association("BannerToAksGrup")]
        public XPCollection<Banner> banner
        {
            get { return GetCollection<Banner>(nameof(banner)); }
        }

        [Association("TeknikToAksGrubu")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }

        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToAksGrubu")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get { return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari)); }

        }

        [XafDisplayName("Kataloglar")]
        [Association("KatalogToAksGrubu")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
        }

        [Association("KaliteToAksGrubu")]
        [XafDisplayName("Kalite Dökümanları")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }

        }

        [Association("webFotografToAksesuarGrubu")]
        [XafDisplayName("Web Fotoğrafları")]
        public XPCollection<WebFotograf> WebFotograf
        {
            get { return GetCollection<WebFotograf>(nameof(WebFotograf)); }

        }

        private DateTime _SonGuncellemeTarihi;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue<DateTime>(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        [Browsable(false)]
        public bool KayitKontrol = false;
        public void WebFotografOlustur()
        {
            //CriteriaOperator crtiteria = CriteriaOperator.Parse("urunler=?", urunler.Oid);
            //Fotograflar Fotograf = (Fotograflar)objectSpace.FindObject(typeof(Fotograflar), crtiteria);
            if (fotograf != null)
            {
                CriteriaOperator crtiteria = CriteriaOperator.Parse("AksesuarGrubu = ?", this);
                WebFotograf Fotograf = (WebFotograf)objectSpace.FindObject(typeof(WebFotograf), crtiteria);
                if (Fotograf != null)
                {
                    Image newImage = byteArrayToImage(fotograf);
                    Bitmap yeniimg = new Bitmap(300, 300);
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(newImage, 0, 0, 300, 300);

                    MemoryStream stream = new MemoryStream();
                    yeniimg.Save(stream, ImageFormat.Jpeg);

                    Fotograf.fotograf = stream.GetBuffer();
                    Fotograf.AksesuarGrubu = this;
                    Fotograf.Web = Web;
                    Fotograf.EngWeb = EngWeb;
                    Fotograf.Index = Index;
                }
                else
                {
                    Image newImage = byteArrayToImage(fotograf);
                    Bitmap yeniimg = new Bitmap(300, 300);
                    using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                        g.DrawImage(newImage, 0, 0, 300, 300);

                    MemoryStream stream = new MemoryStream();
                    yeniimg.Save(stream, ImageFormat.Jpeg);

                    WebFotograf webfoto = objectSpace.CreateObject<WebFotograf>();
                    webfoto.fotograf = stream.GetBuffer();
                    webfoto.AksesuarGrubu = this;
                    webfoto.Web = Web;
                    webfoto.EngWeb = EngWeb;
                    webfoto.Index = Index;
                }
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

        protected override void OnSaving()
        {
            if (EngWeb == true && EngAksesuarGrupAdi == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster İngilizceyi kaldırınız veya İngilizce Aksesuar Grup Adını giriniz.");
            }
            if (fotograf != null)
            {
                WebFotografOlustur();
            }

            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
    }
}