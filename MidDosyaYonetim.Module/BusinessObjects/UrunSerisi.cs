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

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class UrunSerisi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UrunSerisi(Session session)
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
        private string _UrunSerisiAdi;
        [XafDisplayName("Ürün Serisi Adı (TR)")]
        public string UrunSerisiAdi
        {
            get { return _UrunSerisiAdi; }
            set
            {
                SetPropertyValue<string>(nameof(UrunSerisiAdi), ref _UrunSerisiAdi, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(UrunSerisiAdi);
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
        [XafDisplayName("Ürün Serisi Adı (ENG)")]
        private string _EngUrunSerisiAdi;
        public string EngUrunSerisiAdi
        {
            get { return _EngUrunSerisiAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngUrunSerisiAdi), ref _EngUrunSerisiAdi, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(EngUrunSerisiAdi);
                }
            }
        }

        [XafDisplayName("Ürün Ailesi")]
        [Association("SeriToAile")]
        public UrunAilesi urunAilesi
        {
            get { return getAile(); }
            set { }
        }
        private UrunAilesi urunaile;
        private UrunAilesi getAile()
        {
            if (urunGrubu != null)
            {
                urunaile = urunGrubu.urunAilesi;
                return urunaile;

            }
            return urunaile;
        }

        //[DataSourceCriteria("urunAilesi= '@This.urunAilesi'")]
        [XafDisplayName("Ürün Grubu")]
        [Association("SeriToGrup")]
        public UrunGrubu urunGrubu
        {

            get { return urungrup; }
            set { SetPropertyValue(nameof(urunGrubu), ref urungrup, value); }

        }

        [Association("SeriToUrunler")]
        private XPCollection<Urunler> urunler;
        //[XafDisplayName("Aksesuar")]
        //[Association("UrunSerisiToAksesuar")]
        //public XPCollection<Aksesuar> aksesuar
        //{
        //    get { return GetCollection<Aksesuar>(nameof(aksesuar)); }


        //}

        [Association("MontajToSerisi")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }
        [XafDisplayName("Kapak Opsiyonları")]
        [Association("UrunSerisiToOpsiyon")]
        public XPCollection<KapakOpsiyonlari> kapakOpsiyonlari
        {
            get { return GetCollection<KapakOpsiyonlari>(nameof(kapakOpsiyonlari)); }

        }
        [Association("ParcalartoSeri")]
        private XPCollection<Parcalar> parcalar
        {
            get { return GetCollection<Parcalar>(nameof(parcalar)); }

        }
        [Association("TeknikToSerisi")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }

        [Association("UretimToSerisi")]
        [XafDisplayName("Üretim Dökümanları")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get { return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari)); }

        }

        [Association("FotografToSerisi")]
        [XafDisplayName("Fotoğraflar")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }

        }
        [Association("KaliteToSerisi")]
        [XafDisplayName("Kalite Dökümanları")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }

        }
        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToSerisi")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get { return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari)); }

        }

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToSerisi")]
        public XPCollection<TeknikSartname> teknikSartname
        {
            get { return GetCollection<TeknikSartname>(nameof(teknikSartname)); }
        }

        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        [XafDisplayName("Açıklama (TR)")]
        public string Aciklama;
        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama;
        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToSerisi")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }

        }
        [Association("DigerToSerisi")]
        [XafDisplayName("Diğer Dökümanlar")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get { return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar)); }

        }
        [XafDisplayName("Kataloglar")]
        [Association("KatalogToSeri")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
        }
        [XafDisplayName("Banner")]
        [Association("BannerToSeri")]
        public XPCollection<Banner> banner
        {
            get { return GetCollection<Banner>(nameof(banner)); }
        }

        [XafDisplayName("Web Fotoğraf")]
        [Association("webFotografToUrunSerisi")]
        public XPCollection<WebFotograf> WebFotograf
        {
            get { return GetCollection<WebFotograf>(nameof(WebFotograf)); }
        }

        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;

        private string _WebUrl;
        public string WebUrl
        {
            get { return _WebUrl; }
            set
            {
                SetPropertyValue<string>(nameof(WebUrl), ref _WebUrl, value);
                //if (!IsLoading)
                //{
                //    WebUrl = UrlOlustur(WebUrl);
                //}
            }
        }
        private string _EngWebUrl;
        public string EngWebUrl
        {
            get { return _EngWebUrl; }
            set
            {
                SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value);
                //if (!IsLoading)
                //{
                //    EngWebUrl = UrlOlustur(EngWebUrl);
                //}
            }
        }
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
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

        protected override void OnSaving()
        {
            if (EngWeb == true && EngUrunSerisiAdi == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster İngilizceyi kaldırınız veya Ürün Serisi Adının İngilizcesini giriniz.");
            }
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
    }
}