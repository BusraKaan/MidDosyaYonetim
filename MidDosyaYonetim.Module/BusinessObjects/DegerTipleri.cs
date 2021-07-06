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
    public class DegerTipleri : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DegerTipleri(Session session)
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
        [XafDisplayName("Değer Tipi (TR)")]
        public string DegerTipi;

        private string _EngDegerTipi;

        [XafDisplayName("Değer Tipi (ENG)")]
        public string EngDegerTipi
        {
            get { return _EngDegerTipi; }
            set { SetPropertyValue<string>(nameof(EngDegerTipi), ref _EngDegerTipi, value); }
        }

        private DateTime tarih;
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy hh:mm}")]
        public DateTime OlusturmaTarihi
        {
            get { return tarih; }
            set { SetPropertyValue(nameof(OlusturmaTarihi), ref tarih, DateTime.Now); }
        }
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        //[XafDisplayName("Web'de Göster (TR)")]
        //public bool Web;

        //[XafDisplayName("Web'de Göster (ENG)")]
        //public bool EngWeb;

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
            base.OnSaving();
            SonGuncellemeTarihi = DateTime.Now;
        }
    }
}