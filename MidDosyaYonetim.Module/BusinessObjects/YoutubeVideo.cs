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
    public class YoutubeVideo : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public YoutubeVideo(Session session)
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
        [XafDisplayName("Video link")]
        public string link;
        [XafDisplayName("Video Açıklama")]
        public string aciklama;

        [XafDisplayName("Video Açıklama (ENG)")]
        public string EngAciklama;

        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;

        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [ModelDefault("AllowEdit", "False")]
        public string ResimLinki;
        [ModelDefault("AllowEdit", "False")]
        public string Value;
        [XafDisplayName("Anasayfada Göster")]
        public bool AnasayfadaGoster;

        private VideolarSayfasi _videolarSayfasi;
        [Association("VideolarSayfasi - YoutubeVideo")]
        [XafDisplayName("Videolar Web Sayfasi")]
        public VideolarSayfasi videolarSayfasi
        {
            get { return _videolarSayfasi; }
            set { SetPropertyValue(nameof(videolarSayfasi), ref _videolarSayfasi, value); }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
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
            base.OnSaving();

            if (link.Contains("watch?"))
            {
                string[] kelimeler = link.Split('=');
                Value = kelimeler[1];
                ResimLinki = "https://i.ytimg.com/vi/" + Value + "/sddefault.jpg";
            }
            else
            {
                string[] kelimeler = link.Split('/');
                Value = kelimeler[3];
                ResimLinki = "https://i.ytimg.com/vi/" + Value + "/sddefault.jpg";

            }
            SonGuncellemeTarihi = DateTime.Now;


        }
    }
}