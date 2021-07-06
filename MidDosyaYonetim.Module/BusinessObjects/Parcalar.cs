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
    public class Parcalar : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Parcalar(Session session)
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
        [XafDisplayName("Parça Kodu")]
        public String ParcaKodu;
        [XafDisplayName("Parça Adı")]
        public String ParcaAdi;
        [XafDisplayName("Seri Kısa Kodu")]
        public String SeriKisaKodu;
        private UrunGrubu urungrup;
        private UrunSerisi urunseri;
        private Urunler urun;
        [XafDisplayName("Ürün Ailesi")]
        [Association("ParcalartoUrunAilesi")]
        public UrunAilesi urunAilesi { get; set; }
        [DataSourceCriteria("urunAilesi= '@This.urunAilesi'")]
        [XafDisplayName("Ürün Grubu")]
        [Association("ParcalarToGrup")]
        public UrunGrubu urunGrubu
        {



            get { return urungrup; }
            set { SetPropertyValue(nameof(urunGrubu), ref urungrup, value); }



        }



        [DataSourceCriteria("urunGrubu= '@This.urunGrubu'")]
        [XafDisplayName("Ürün Serisi")]
        [Association("ParcalartoSeri")]
        public UrunSerisi urunSerisi
        {
            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }
        }
        [DataSourceCriteria("urunSerisi= '@This.urunSerisi'")]
        [XafDisplayName("Ürünler")]
        [Association("ParcalartoUrun")]
        public Urunler urunler { get; set; }
        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [Association("TeknikToParca")]
        [XafDisplayName("Teknik Çizimler")]
        public XPCollection<TeknikCizimler> teknikCizimler
        {
            get { return GetCollection<TeknikCizimler>(nameof(teknikCizimler)); }

        }

        [XafDisplayName("Üretim Dökümanları")]
        [Association("UretimToParca")]
        public XPCollection<UretimDokumanlari> uretimDokumanlari
        {
            get
            {
                return GetCollection<UretimDokumanlari>(nameof(uretimDokumanlari));
            }
        }
        
        
        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToParca")]
        public XPCollection<Fotograflar> fotogtaflar
        {
            get
            {
                return GetCollection<Fotograflar>(nameof(fotogtaflar));
            }
        }
        [XafDisplayName("Kalite Dökümanları")]
        [Association("KaliteToParca")]
        public XPCollection<KaliteDokumanlari> kaliteDokumanlari
        {
            get
            {
                return GetCollection<KaliteDokumanlari>(nameof(kaliteDokumanlari));
            }
        }
        [XafDisplayName("Sertifikalar")]
        [Association("SertifikaToParca")]
        public XPCollection<Sertifikalar> sertifikaDokumanlari
        {
            get
            {
                return GetCollection<Sertifikalar>(nameof(sertifikaDokumanlari));
            }
        }
        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToParca")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }



        }
        [XafDisplayName("Diger Dökümanlar")]
        [Association("DigerToParca")]
        public XPCollection<DigerDokumanlar> digerDokumanlar
        {
            get
            {
                return GetCollection<DigerDokumanlar>(nameof(digerDokumanlar));
            }
        }
        protected override void OnSaving()
        {
            if (urunler != null)
            {
                urunAilesi = urunler.urunAilesi;
                urunAilesi.Save();
                urunGrubu = urunler.urunGrubu;
                urunGrubu.Save();
                urunSerisi = urunler.urunSerisi;
                urunSerisi.Save();
                urunler = urunler;
                urunler.Save();



            }
            else
            {
                if (urunSerisi != null)
                {
                    urunAilesi = urunSerisi.urunAilesi;
                    urunAilesi.Save();
                    urunGrubu = urunSerisi.urunGrubu;
                    urunGrubu.Save();
                    urunSerisi = urunSerisi;
                    urunSerisi.Save();
                }
                else
                {
                    if (urunGrubu != null)
                    {
                        urunAilesi = urunGrubu.urunAilesi;
                        urunAilesi.Save();
                        urunGrubu = urunGrubu;
                        urunGrubu.Save();
                    }
                    else
                    {
                        if (urunAilesi != null)
                        {
                            urunAilesi = urunAilesi;
                            urunAilesi.Save();
                        }
                    }
                }
            }
            base.OnSaving();
        }
        [XafDisplayName("Fiyat Listesi")]
        [Association("FiyatToParca")]
        public XPCollection<FiyatListesi> fiyatListesi
        {
            get { return GetCollection<FiyatListesi>(nameof(fiyatListesi)); }
        }
        [XafDisplayName("Web'de Göster")]
        public bool Web;
        [Association("MontajToParca")]
        [XafDisplayName("Montaj Kılavuzları")]
        public XPCollection<MontajKlavuzlari> montajKlavuzlari
        {
            get { return GetCollection<MontajKlavuzlari>(nameof(montajKlavuzlari)); }

        }
    }
}