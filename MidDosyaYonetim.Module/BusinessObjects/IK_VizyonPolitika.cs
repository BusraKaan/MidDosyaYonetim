using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("ShowLegend")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class IK_VizyonPolitika : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public IK_VizyonPolitika(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        #region (TR)

        private string _Baslik;
        [XafDisplayName("Başlık (TR)")]
        public string Baslik
        {
            get { return _Baslik; }
            set
            {
                SetPropertyValue<string>(nameof(Baslik), ref _Baslik, value);
            }
        }
        private string _Vizyonumuz;
        [XafDisplayName("Vizyonumuz (TR)")]
        public string Vizyonumuz
        {
            get { return _Vizyonumuz; }
            set
            {
                SetPropertyValue<string>(nameof(Vizyonumuz), ref _Vizyonumuz, value);
            }
        }
        private string _Politikamiz;
        [XafDisplayName("Politikamız (TR)")]
        public string Politikamiz
        {
            get { return _Politikamiz; }
            set
            {
                SetPropertyValue<string>(nameof(Politikamiz), ref _Politikamiz, value);
            }
        }
        #endregion

        #region (ENG)

        private string _EngBaslik;
        [XafDisplayName("Başlık (ENG)")]
        public string EngBaslik
        {
            get { return _EngBaslik; }
            set
            {
                SetPropertyValue<string>(nameof(EngBaslik), ref _EngBaslik, value);
            }
        }
        private string _EngVizyonumuz;
        [XafDisplayName("Vizyonumuz (ENG)")]
        public string EngVizyonumuz
        {
            get { return _EngVizyonumuz; }
            set
            {
                SetPropertyValue<string>(nameof(EngVizyonumuz), ref _EngVizyonumuz, value);
            }
        }
        private string _EngPolitikamiz;
        [XafDisplayName("Politikamız (ENG)")]
        public string EngPolitikamiz
        {
            get { return _EngPolitikamiz; }
            set
            {
                SetPropertyValue<string>(nameof(EngPolitikamiz), ref _EngPolitikamiz, value);
            }
        }
        #endregion

        [XafDisplayName("Banner")]
        [Association("BannerToIK")]
        public XPCollection<Banner> banner
        {
            get { return GetCollection<Banner>(nameof(banner)); }
        }

        private DateTime _SonGuncellemeTarihi;
        [XafDisplayName("Son Güncelleme Tarihi"), Browsable(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            SonGuncellemeTarihi = DateTime.Now;
        }
    }
}