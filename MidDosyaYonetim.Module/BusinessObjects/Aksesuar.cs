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
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]

    public class Aksesuar : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Aksesuar(Session session)
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
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        private UrunGrubu urungrup;
        private UrunSerisi urunseri;
        private UrunAilesi urunaile;

        char[] oldValue = new char[] { 'ö', 'ü', 'ç', 'ı', 'ğ', 'ş' };
        char[] newValue = new char[] { 'o', 'u', 'c', 'i', 'g', 's' };


        private string _AksesuarAdi;
        [XafDisplayName("Aksesuar Adı (TR)")]
        public string AksesuarAdi
        {
            get { return _AksesuarAdi; }
            set
            {
                SetPropertyValue<string>(nameof(AksesuarAdi), ref _AksesuarAdi, value);

            }
        }

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

        private string _EngAksesuarAdi;
        [XafDisplayName("Aksesuar Adı (ENG)")]
        public string EngAksesuarAdi
        {
            get { return _EngAksesuarAdi; }
            set
            {
                SetPropertyValue<string>(nameof(EngAksesuarAdi), ref _EngAksesuarAdi, value);
            }
        }


        private string _AksesuarKodu;

        [XafDisplayName("Katalog Kodu")]
        [RuleRequiredField]
        public string AksesuarKodu
        {
            get { return _AksesuarKodu; }
            set
            {
                SetPropertyValue<string>(nameof(AksesuarKodu), ref _AksesuarKodu, value);
                if (!IsLoading)
                {
                    WebUrl = UrlOlustur(AksesuarKodu);
                    EngWebUrl = UrlOlustur(AksesuarKodu);
                }
            }
        }
        private string _StokKodu;
        [XafDisplayName("Stok Kodu")]
        public string StokKodu
        {
            get { return _StokKodu; }
            set { SetPropertyValue<string>(nameof(StokKodu), ref _StokKodu, value); }
        }

        [XafDisplayName("Yükseklik")]
        public Yukseklik yukseklik;

        [XafDisplayName("Açıklama (TR)")]
        public string Aciklama;

        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama;

        //[XafDisplayName("Ürün Grubu")]
        //[Association("UrunGrubuToAksesuar")]
        //public UrunGrubu urunGrubu
        //{

        //    get { return getGrup(); }
        //    set { }
        //}
        //private UrunGrubu getGrup()
        //{
        //    if (urunSerisi != null)
        //    {
        //        urungrup = urunSerisi.urunGrubu;
        //        return urungrup;
        //    }
        //    return urungrup;

        //}

        //[XafDisplayName("Ürün Serisi")]
        //[Association("UrunSerisiToAksesuar")]
        //public UrunSerisi urunSerisi
        //{

        //    get { return urunseri; }
        //    set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        //}

        [XafDisplayName("Ürün")]
        [Association("UrunlerToAksesuar")]
        public XPCollection<Urunler> urunler
        {
            get { return GetCollection<Urunler>(nameof(urunler)); }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
        }

        [Association("webFotografToAksesuarlar")]
        [XafDisplayName("Web Fotoğraf")]
        public XPCollection<WebFotograf> WebFotograf
        {
            get { return GetCollection<WebFotograf>(nameof(WebFotograf)); }

        }

        //[XafDisplayName("Ürün Ailesi")]
        //[Association("UrunAilesiToAksesuar")]
        //public UrunAilesi urunAilesi
        //{
        //    get { return getAile(); }
        //    set { }
        //}
        private UrunAilesi getAile()
        {
            if (urunseri != null)
            {
                urunaile = urunseri.urunAilesi;
                return urunaile;
            }
            return urunaile;
        }
        [XafDisplayName("Boyut")]
        public Boyut boyut;
        [Association("TeknikToAksesuar")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }
        [Association("MontajToAksesuar")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }
        [XafDisplayName("Üretim Dökümanları")]
        [Association("UretimToAksesuar")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get { return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari)); }



        }

        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToAksesuar")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }



        }
        [XafDisplayName("Kalite Dökümanları")]
        [Association("KaliteToAksesuar")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get { return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari)); }



        }

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToAksesuar")]
        public XPCollection<TeknikSartname> teknikSartname
        {
            get { return GetCollection<TeknikSartname>(nameof(teknikSartname)); }
        }

        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToAksesuar")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get { return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari)); }



        }

        private DateTime tarih;
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToAksesuar")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }



        }

        [XafDisplayName("Aksesuar Grup Adı")]
        public AksesuarGrubu AksesuarGrubu;
        [XafDisplayName("Diğer Dökümanlar")]
        [Association("DigerToAksesuar")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get { return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar)); }



        }

        [XafDisplayName("Değerler")]
        [Association("AksesuarToDeger")]
        public XPCollection<aksesuarDeger> Deger
        {
            get { return GetCollection<aksesuarDeger>(nameof(Deger)); }
        }


        [XafDisplayName("Kataloglar")]
        [Association("KatalogToAksesuar")]
        public XPCollection<Kataloglar> katalog
        {
            get { return GetCollection<Kataloglar>(nameof(katalog)); }
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
            if (EngWeb == true && EngAksesuarAdi == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster İngilizceyi kaldırınız veya İngilizce Akesesuar Adının giriniz.");
            }
            SonGuncellemeTarihi = DateTime.Now;
            //if (urunler != null)
            //{
            //    this.urunAilesi = urunler.urunAilesi;
            //    this.urunGrubu = urunler.urunGrubu;
            //    this.urunSerisi = urunler.urunSerisi;
            //    this.urunler = urunler;
            //}
            //else
            //{
            //    if (urunSerisi != null)
            //    {
            //        this.urunAilesi = urunSerisi.urunAilesi;
            //        this.urunGrubu = urunSerisi.urunGrubu;
            //        this.urunSerisi = urunSerisi;

            //    }
            //    else
            //    {
            //        if (urunGrubu != null)
            //        {
            //            this.urunAilesi = urunGrubu.urunAilesi;
            //            this.urunGrubu = urunGrubu;

            //        }
            //        else
            //        {
            //            if (urunAilesi != null)
            //            {
            //                this.urunAilesi = urunAilesi;

            //            }
            //        }
            //    }
            //}
            base.OnSaving();
        }

        private SqlConnection con;
        private SqlCommand cmd;
        protected override void OnSaved()
        {
            string sorgu = "Update Urunlerurunler_Aksesuaraksesuar SET SonGuncellemeTarihi = '" + DateTime.Now.ToString() + "' Where aksesuar='" + this.Oid.ToString() + "'";
            con = new SqlConnection("Server=10.26.0.30; Database=test3; User Id=sa;Password=Mdsf2020@");
            cmd = new SqlCommand(sorgu, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            base.OnSaved();
        }

        [XafDisplayName("Fiyat Listesi")]
        [Association("FiyatToAksesuar")]
        public XPCollection<FiyatListesi> fiyatListesi
        {
            get { return GetCollection<FiyatListesi>(nameof(fiyatListesi)); }
        }
        [XafDisplayName("Web'de Göster")]
        public bool Web;
        private string _WebUrl;
        public string WebUrl
        {
            get { return _WebUrl; }
            set { SetPropertyValue<string>(nameof(WebUrl), ref _WebUrl, value); }
        }
        [XafDisplayName("Web'de Göster")]
        public bool EngWeb;
        private string _EngWebUrl;
        public string EngWebUrl
        {
            get { return _EngWebUrl; }
            set { SetPropertyValue<string>(nameof(EngWebUrl), ref _EngWebUrl, value); }
        }
    }
}