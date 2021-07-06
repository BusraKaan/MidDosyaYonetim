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
using DevExpress.XtraExport.Helpers;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class UrunAilesi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UrunAilesi(Session session)
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
        private string _UrunAilesiAdi;
        [XafDisplayName("Ürün Ailesi Adı")]
        public string UrunAilesiAdi
        {
            get { return _UrunAilesiAdi; }
            set
            {
                SetPropertyValue<string>(nameof(UrunAilesiAdi), ref _UrunAilesiAdi, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(UrunAilesiAdi);
                }
            }
        }

        private string _EngUrunAilesiAdi;
        //Webde ana menüler statik olarak girilmiştir. Girilme sebebi Birden fazla ürün ailesinin tek başlık altında toplanmasıdır.
        [XafDisplayName("Ürün Ailesi Adı (ENG)")]
        public string EngUrunAilesiAdi
        {
            get { return _EngUrunAilesiAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngUrunAilesiAdi), ref _EngUrunAilesiAdi, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(EngUrunAilesiAdi);
                }
            }
        }

        [Association("AilesiToGrup")]
        private XPCollection<UrunGrubu> urunGrubu;

        [Association("UrunAilesi")]
        private XPCollection<Urunler> urunler;

        [Association("SeriToAile")]
        private XPCollection<UrunSerisi> urunSerisi;

        //[Association("UrunAilesiToAksesuar")]
        //public XPCollection<Aksesuar> aksesuar
        //{
        //    get { return GetCollection<Aksesuar>(nameof(aksesuar)); }

        //}

        [Association("UrunAilesiToOpsiyon")]
        public XPCollection<KapakOpsiyonlari> kapakOpsiyonlari
        {
            get { return GetCollection<KapakOpsiyonlari>(nameof(kapakOpsiyonlari)); }

        }
        [Association("ParcalartoUrunAilesi")]
        private XPCollection<Parcalar> parcalar
        {
            get { return GetCollection<Parcalar>(nameof(parcalar)); }

        }
        ///////////
        //Dokumanlar association
        [Association("TeknikToAilesi")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }

        [Association("MontajToAilesi")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }


        [XafDisplayName("Üretim Dökümanlar")]
        [Association("UretimToAilesi")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get { return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari)); }

        }

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToAilesi")]
        public XPCollection<TeknikSartname> teknikSartname
        {
            get { return GetCollection<TeknikSartname>(nameof(teknikSartname)); }
        }

        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToAilesi")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }

        }
        [XafDisplayName("Kalite Dökümanları")]
        [Association("KaliteToAilesi")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }

        }
        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToAilesi")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get { return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari)); }

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
        [Association("TagToAilesi")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }

        }
        [XafDisplayName("Diğer Dökümanlar")]
        [Association("DigerToAilesi")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get { return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar)); }

        }

        [XafDisplayName("Kataloglar")]
        [Association("KatalogToAile")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
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
            set
            {
                SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value);
                //if (!IsLoading)
                //{
                //    EngWebUrl = UrlOlustur(EngWebUrl);
                //}
            }
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
            if (EngWeb == true && EngUrunAilesiAdi == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster İngilizceyi kaldırınız veya Urun Ailesinin İngilizce Adını Giriniz.");
            }
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
    }

}