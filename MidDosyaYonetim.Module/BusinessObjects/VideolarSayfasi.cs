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
    public class VideolarSayfasi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public VideolarSayfasi(Session session)
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

        private string _Baslik;

        [XafDisplayName("Başlık")]
        public string Baslik
        {
            get { return _Baslik; }
            set { SetPropertyValue<string>(nameof(Baslik), ref _Baslik, value); }
        }

        private string _BaslikEng;

        [XafDisplayName("Başlık (ENG)")]
        public string BaslikEng
        {
            get { return _BaslikEng; }
            set { SetPropertyValue<string>(nameof(BaslikEng), ref _BaslikEng, value); }
        }

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
        }


        [Association("VideolarSayfasi - YoutubeVideo")]
        [XafDisplayName("Youtube Videoları")]
        public XPCollection<YoutubeVideo> Video
        {
            get { return GetCollection<YoutubeVideo>(nameof(Video)); }


        }

        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;

        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;

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
            if (EngWeb == true && BaslikEng == null)
            {
                throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Web'de göster İngilizceyi kaldırınız veya İngilizce başlık giriniz.");
            }
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();
        }
    }
}