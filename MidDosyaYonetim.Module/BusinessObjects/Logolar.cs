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
    public class Logolar : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Logolar(Session session)
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
        [XafDisplayName("Kalite Dökümanları")]
        [Association("LogoToKalite")]
        public KaliteDokumanlari kaliteDokumanlari { get; set; }

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;

        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30)]
        public byte[] LogoResmi
        {
            get { return GetPropertyValue<byte[]>(nameof(LogoResmi)); }
            set { SetPropertyValue<byte[]>(nameof(LogoResmi), value); }
        }
        //protected override void OnSaving()
        //{

        //    if (kaliteDokumanlari != null)
        //    {
        //        kaliteDokumanlari = kaliteDokumanlari;
        //        kaliteDokumanlari.Save();
        //    }       
        
        //    base.OnSaving();
        //}
        [XafDisplayName("Web'de Göster")]
        public bool Web;
    }
}