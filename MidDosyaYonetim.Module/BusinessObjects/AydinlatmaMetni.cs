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
    [ImageName("EditExtractSource")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class AydinlatmaMetni : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public AydinlatmaMetni(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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
        private string _Aciklama;
        [XafDisplayName("Açıklama (TR)")]
        public string Aciklama
        {
            get { return _Aciklama; }
            set
            {
                SetPropertyValue<string>(nameof(Aciklama), ref _Aciklama, value);
            }
        }
        private string _Aciklama2;
        [XafDisplayName("Bilgilendirme (TR)")]
        public string Aciklama2
        {
            get { return _Aciklama2; }
            set
            {
                SetPropertyValue<string>(nameof(Aciklama2), ref _Aciklama2, value);
            }
        }
        private string _Aciklama3;
        [XafDisplayName("Kanun (TR)")]
        public string Aciklama3
        {
            get { return _Aciklama3; }
            set
            {
                SetPropertyValue<string>(nameof(Aciklama3), ref _Aciklama3, value);
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
        private string _EngAciklama;
        [XafDisplayName("Açıklama (ENG)")]
        public string EngAciklama
        {
            get { return _EngAciklama; }
            set
            {
                SetPropertyValue<string>(nameof(EngAciklama), ref _EngAciklama, value);
            }
        }
        private string _EngAciklama2;
        [XafDisplayName("Bilgilendirme (ENG)")]
        public string EngAciklama2
        {
            get { return _EngAciklama2; }
            set
            {
                SetPropertyValue<string>(nameof(EngAciklama2), ref _EngAciklama2, value);
            }
        }
        private string _EngAciklama3;
        [XafDisplayName("Kanun (ENG)")]
        public string EngAciklama3
        {
            get { return _EngAciklama3; }
            set
            {
                SetPropertyValue<string>(nameof(EngAciklama3), ref _EngAciklama3, value);
            }
        }
        #endregion

        [XafDisplayName("Banner")]
        [Association("BannerToAydinlatmaMetni")]
        public XPCollection<Banner> banner
        {
            get { return GetCollection<Banner>(nameof(banner)); }
        }
    }
}