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
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Iletisim : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Iletisim(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private string _IstTelefon;
        [XafDisplayName("Telefon (İstanbul)"), ModelDefault("EditMask", "(000)000-0000")]
        public string IstTelefon
        {
            get { return _IstTelefon; }
            set
            {
                SetPropertyValue<string>(nameof(IstTelefon), ref _IstTelefon, value);
            }
        }
        private string _IstFax;
        [XafDisplayName("Fax (İstanbul)"), ModelDefault("EditMask", "(000)000-0000")]
        public string IstFax
        {
            get { return _IstFax; }
            set
            {
                SetPropertyValue<string>(nameof(IstFax), ref _IstFax, value);
            }
        }
        private string _IstAdres;
        [XafDisplayName("Adres (İstanbul)")]
        public string IstAdres
        {
            get { return _IstAdres; }
            set
            {
                SetPropertyValue<string>(nameof(IstAdres), ref _IstAdres, value);
            }
        }
        private string _EskTelefon;
        [XafDisplayName("Telefon (Eskişehir)"), ModelDefault("EditMask", "(000)000-0000")]
        public string EskTelefon
        {
            get { return _EskTelefon; }
            set
            {
                SetPropertyValue<string>(nameof(EskTelefon), ref _EskTelefon, value);
            }
        }
        private string _EskFax;
        [XafDisplayName("Fax (Eskişehir)"), ModelDefault("EditMask", "(000)000-0000")]
        public string EskFax
        {
            get { return _EskFax; }
            set
            {
                SetPropertyValue<string>(nameof(EskFax), ref _EskFax, value);
            }
        }
        private string _EskAdres;
        [XafDisplayName("Adres (Eskişehir)")]
        public string EskAdres
        {
            get { return _EskAdres; }
            set
            {
                SetPropertyValue<string>(nameof(EskAdres), ref _EskAdres, value);
            }
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