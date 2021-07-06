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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using System.Collections;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Urunler : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Urunler(Session session)
            : base(session)
        {
        }

        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
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

        //private PermissionPolicyUser olusturanKisi;


        [XafDisplayName("Ürün Ailesi")]
        [Association("UrunAilesi")]
        [RuleRequiredField(Name = "Hata3", CustomMessageTemplate = "Lütfen Urun Ailesini Giriniz")]
        public UrunAilesi urunAilesi
        {
            get { return getAile(); }
            set { }
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

        [XafDisplayName("Ürün Serisi")]
        [Association("SeriToUrunler")]
        [RuleRequiredField(Name = "Hata2", CustomMessageTemplate = "Lütfen Urun Serisini Giriniz")]
        public UrunSerisi urunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        }

        [XafDisplayName("Ürün Grubu")]
        [Association("UrunlerToGrup")]
        [RuleRequiredField(Name = "Hata1", CustomMessageTemplate = "Lütfen Urun Grubunu Giriniz")]
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

        [XafDisplayName("Boyut")]
        public Boyut boyut;
        [XafDisplayName("Yükseklik")]
        public Yukseklik yukseklik;
        [XafDisplayName("Renk")]
        public Renk renk;


        [XafDisplayName("Aksesuar")]
        [Association("UrunlerToAksesuar")]
        public XPCollection<Aksesuar> aksesuar
        {
            get { return GetCollection<Aksesuar>(nameof(aksesuar)); }
        }
        [XafDisplayName("Kapak Opsiyonları")]
        [Association("UrunlerToOpsiyon")]
        public XPCollection<KapakOpsiyonlari> kapakOpsiyonlari
        {
            get { return GetCollection<KapakOpsiyonlari>(nameof(kapakOpsiyonlari)); }
        }

        [Association("MontajToUrunler")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToUrunler")]
        public XPCollection<TeknikSartname> teknikSartname
        {
            get { return GetCollection<TeknikSartname>(nameof(teknikSartname)); }
        }

        [Association("webFotografToUrunler")]
        [XafDisplayName("Web Fotoğrafları")]
        public XPCollection<WebFotograf> WebFotograf
        {
            get { return GetCollection<WebFotograf>(nameof(WebFotograf)); }

        }

        [XafDisplayName("Stok Kodu")]
        public string StokKodu;

        private string _StokAdi;
        [XafDisplayName("Stok Adı (TR)")]
        public string StokAdi
        {
            get { return _StokAdi; }
            set
            {
                SetPropertyValue<string>(nameof(StokAdi), ref _StokAdi, value);
            }
        }

        private string _IngStokAdi;
        [XafDisplayName("Stok Adı (ENG)")]
        public string IngStokAdi
        {
            get { return _IngStokAdi; }
            set
            {
                SetPropertyValue<string>(nameof(IngStokAdi), ref _IngStokAdi, value);
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

        private string _KatalogKodu;
        [XafDisplayName("Katalog Kodu")]
        //[Indexed(Unique = true)]
        public string KatalogKodu
        {
            get { return _KatalogKodu; }
            set
            {
                SetPropertyValue<string>(nameof(KatalogKodu), ref _KatalogKodu, value);
                if (!IsLoading)
                {
                    EngWebUrl = UrlOlustur(KatalogKodu);
                    WebUrl = UrlOlustur(KatalogKodu);
                }
            }
        }


        public string Birim;
        [XafDisplayName("Üretim")]
        public int Uretim;
        [XafDisplayName("Satış")]
        public int Satis;
        [XafDisplayName("Satın Alma")]
        public int SatinAlma;
        [XafDisplayName("Ana Ürün Grubu")]
        public string AnaUrunGrubu;
        [XafDisplayName("Alt Ürün Tipi")]
        public string AltUrunTipi;
        [XafDisplayName("Satış Analiz Grubu")]
        public int SatisAnalizGrubu;
        [XafDisplayName("Alt Ürün Grubu")]
        public string AltUrunGrubu;
        [XafDisplayName("Ürün Cinsi")]
        public string UrunCinsi;
        [XafDisplayName("Ürün Türü")]
        public string UrunTuru;
        public string Barkod;
        [Association("TeknikToUrunler")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }
        [XafDisplayName("Üretim Dökümanlar")]
        [Association("UretimToUrunler")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get { return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari)); }

        }

        [Association("FotografToUrunler")]
        [XafDisplayName("Fotoğraflar")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }

        }
        [Association("KaliteToUrunler")]
        [XafDisplayName("Kalite Dökümanları")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }

        }
        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToUrunler")]
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

        [XafDisplayName("Açıklama (TR)")]
        public string Aciklama;
        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama;

        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToUrunler")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }

        }
        [XafDisplayName("Diger Dökümanlar")]
        [Association("DigerToUrunler")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get { return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar)); }

        }

        [XafDisplayName("Parçalar")]
        [Association("ParcalartoUrun")]
        private XPCollection<Parcalar> parcalar
        {
            get { return GetCollection<Parcalar>(nameof(parcalar)); }

        }
        [XafDisplayName("Değerler")]
        [Association("UrunlerToDegerler")]
        public XPCollection<UrunDegerler> degerler
        {
            get { return GetCollection<UrunDegerler>(nameof(degerler)); }
        }
        [XafDisplayName("Fiyat Listesi")]
        [Association("FiyatToUrunler")]
        public XPCollection<FiyatListesi> fiyatListesi
        {
            get { return GetCollection<FiyatListesi>(nameof(fiyatListesi)); }
        }

        [XafDisplayName("Kataloglar")]
        [Association("KatalogToUrun")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
        }

        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web; 
        private string _WebUrl;
        public string WebUrl
        {
            get { return _WebUrl; }
            set { SetPropertyValue<string>(nameof(WebUrl), ref _WebUrl, value);
            }
        }

        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;
        private string _EngWebUrl;
        public string EngWebUrl
        {
            get { return _EngWebUrl; }
            set { SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value); }
        }

        [XafDisplayName("Ürün Gruplarındaki İlk Fotoğraf")]
        public bool ilkFoto;

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

            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();

        }

        private SqlConnection con;
        private SqlCommand cmd;
        protected override void OnSaved()
        {

            string sorgu = "Update Urunlerurunler_Aksesuaraksesuar SET SonGuncellemeTarihi = '" + DateTime.Now.ToString() + "' Where urunler='" + this.Oid.ToString() + "'";
            con = new SqlConnection("Server=10.26.0.30; Database=test3; User Id=sa;Password=Mdsf2020@");
            cmd = new SqlCommand(sorgu, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            base.OnSaved();
        }
    }
}