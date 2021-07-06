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
    public class UrunGrubu : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UrunGrubu(Session session)
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

        private string _UrunGrubuAdi;
        [XafDisplayName("Ürün Grubu Adı (TR)")]
        public string UrunGrubuAdi
        {
            get { return _UrunGrubuAdi; }
            set
            {
                SetPropertyValue<string>(nameof(UrunGrubuAdi), ref _UrunGrubuAdi, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(UrunGrubuAdi);
                }
            }
        }
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
            set { SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value);
                //if (!IsLoading)
                //{
                //    EngWebUrl = UrlOlustur(EngWebUrl);
                //}
            }
        }


        private string _EngUrunGrubuAdi;
        [XafDisplayName("Ürün Grubu Adı (ENG)")]
        public string EngUrunGrubuAdi
        {
            get { return _EngUrunGrubuAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngUrunGrubuAdi), ref _EngUrunGrubuAdi, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(EngUrunGrubuAdi);
                }
            }
        }

        [XafDisplayName("Ürün Ailesi")]
        [Association("AilesiToGrup")]
        public UrunAilesi urunAilesi;



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


        [Association("UrunlerToGrup")]
        private XPCollection<Urunler> urunler;

        [Association("SeriToGrup")]
        private XPCollection<UrunSerisi> urunSerisi;
        //[XafDisplayName("Aksesuarlar")]
        //[Association("UrunGrubuToAksesuar")]
        //public XPCollection<Aksesuar> aksesuar
        //{
        //    get { return GetCollection<Aksesuar>(nameof(aksesuar)); }

        //}
        [XafDisplayName("Kapak Opsiyonları")]
        [Association("UrunGrubuToOpsiyon")]
        public XPCollection<KapakOpsiyonlari> kapakOpsiyonlari
        {
            get { return GetCollection<KapakOpsiyonlari>(nameof(kapakOpsiyonlari)); }

        }
        [XafDisplayName("Parçalar")]
        [Association("ParcalarToGrup")]
        private XPCollection<Parcalar> parcalar
        {
            get { return GetCollection<Parcalar>(nameof(parcalar)); }

        }
        [Association("TeknikToGrubu")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }
        [Association("MontajToGrubu")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }

        [XafDisplayName("Üretim Dökümanları")]
        [Association("UretimToGrubu")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get { return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari)); }

        }

        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToGrubu")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }

        }
        [XafDisplayName("Kalite Dökümanları")]
        [Association("KaliteToGrubu")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }

        }
        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToGrubu")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get { return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari)); }

        }

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToGrubu")]
        public XPCollection<TeknikSartname> teknikSartname
        {
            get { return GetCollection<TeknikSartname>(nameof(teknikSartname)); }
        }

        [XafDisplayName("Banner")]
        [Association("BannerToGrup")]
        public XPCollection<Banner> banner
        {
            get { return GetCollection<Banner>(nameof(banner)); }
        }

        [XafDisplayName("Web Fotograf")]
        [Association("webFotografToUrunGrubu")]
        public XPCollection<WebFotograf> WebFotograf
        {
            get { return GetCollection<WebFotograf>(nameof(WebFotograf)); }
        }

        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        [XafDisplayName("Açıklama")]
        public string Aciklama;

        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama;

        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToGrubu")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }

        }
        [XafDisplayName("Diger Dökümanlar")]
        [Association("DigerToGrubu")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get { return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar)); }

        }

        [XafDisplayName("Kataloglar")]
        [Association("KatalogToGrup")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
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
            if (EngWeb == true && EngUrunGrubuAdi == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster ingilizceyi kaldırınız veya Urun Grubu Adının İngilizcesini giriniz.");
            }
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }

    }
}