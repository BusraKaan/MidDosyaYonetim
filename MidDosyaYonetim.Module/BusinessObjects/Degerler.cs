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
    public class Degerler : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Degerler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            OlusturmaTarihi = DateTime.Now;
            if (SecuritySystem.CurrentUser != null)
            {
                var olusturanKisi = SecuritySystem.CurrentUserName;
                OlusturanKisi = olusturanKisi.ToString();
            }
        }
        [XafDisplayName("Değer Adı(TR)")]
        public string DegerAdi;

        [XafDisplayName("Değer Adı (ENG)")]
        public string EngDegerAdi;

        [XafDisplayName("Değer Tipi")]
        public DegerTipleri DegerTipi;
        [XafDisplayName("Özellik")]
        public Ozellikler ozellikler;

        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Oluşturulma Tarihi")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;

        [XafDisplayName("Urun Değerler")]
        [Association("DegerToDegerler")]
        public XPCollection<UrunDegerler> degerler
        {
            get { return GetCollection<UrunDegerler>(nameof(degerler)); }
        }

        [XafDisplayName("Aksesuar Değerler")]
        [Association("aksDegerToDegerler")]

        public XPCollection<aksesuarDeger> aksesuarDegerler
        {
            get { return GetCollection<aksesuarDeger>(nameof(aksesuarDegerler)); }
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
            //if (EngWeb == true && EngDegerAdi == null)
            //{
            //    throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Akesesuar Adının ingilizcesini giriniz.");
            //}
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
    }
}