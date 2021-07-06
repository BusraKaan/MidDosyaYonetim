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
    [ImageName("GoToHeader")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HeaderFooter : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HeaderFooter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        #region Footer/AltBilgi

        private string _AltBilgi;
        [XafDisplayName("Web Alt Bilgisi (TR)")]
        public string AltBilgi
        {
            get { return _AltBilgi; }
            set
            {
                SetPropertyValue<string>(nameof(AltBilgi), ref _AltBilgi, value);
            }
        }
        private string _EngAltBilgi;
        [XafDisplayName("Web Alt Bilgisi (ENG)")]
        public string EngAltBilgi
        {
            get { return _EngAltBilgi; }
            set
            {
                SetPropertyValue<string>(nameof(EngAltBilgi), ref _EngAltBilgi, value);
            }
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
         DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Anasayfa Görseli")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
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
        #endregion
    }
}