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
    public class KapakOpsiyonlari : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public KapakOpsiyonlari(Session session)
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

        [XafDisplayName("Kapak Opsiyon Dokümanları")]
        [Association("KapakOpsToKapakDoc")]
        public XPCollection<KapakOpsiyonDoc> kapakOpsiyons
        {
            get { return GetCollection<KapakOpsiyonDoc>(nameof(kapakOpsiyons)); }
        }

        [XafDisplayName("Kapak Opsiyon Adı (TR)")]
        public string Opsiyon;
        [XafDisplayName("Kapak Opsiyon Adı (ENG)")]
        public string ENGOpsiyon;

        [XafDisplayName("Kapak Opsiyon Kodu")]
        public string OpsiyonKodu;
        [XafDisplayName("Ürün")]
        [Association("UrunlerToOpsiyon")]
        public Urunler urunler { get; set; }


        [XafDisplayName("Ürün Grubu")]
        [Association("UrunGrubuToOpsiyon")]
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

        [XafDisplayName("Ürün Serisi")]
        [Association("UrunSerisiToOpsiyon")]
        public UrunSerisi urunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        }

        [XafDisplayName("Ürün Ailesi")]
        [Association("UrunAilesiToOpsiyon")]
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

        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToOpsiyon")]
        public XPCollection<Fotograflar> fotograflar
        {
            get { return GetCollection<Fotograflar>(nameof(fotograflar)); }

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
        [Association("TagToOpsiyon")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }



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
            //if (ENGWeb == true && ENGOpsiyon == null)
            //{
            //    throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Akesesuar Adının ingilizcesini giriniz.");
            //}
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
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;

        [XafDisplayName("Web'de Göster (ENG)")]
        public bool ENGWeb;
    }
}