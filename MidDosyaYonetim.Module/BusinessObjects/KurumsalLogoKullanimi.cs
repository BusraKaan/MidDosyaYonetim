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

    public class KurumsalLogoKullanimi : BaseObject
    { 
        public KurumsalLogoKullanimi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
        private string _adi;
        [XafDisplayName("Adı(TR)")]
        public string Adi
        {
            get { return _adi; }
            set { SetPropertyValue(nameof(Adi), ref _adi, value); }
        }
        private string _enadi;
        [XafDisplayName("Adı(EN)")]
        public string ENAdi
        {
            get { return _enadi; }
            set { SetPropertyValue(nameof(ENAdi), ref _enadi, value); }
        }
        private FileData _katalog;
        [XafDisplayName("Katalog(TR)")]
        public FileData Katalog
        {
            get { return _katalog; }
            set { SetPropertyValue(nameof(Katalog), ref _katalog, value); }
        }
        private FileData _enkatalog;
        [XafDisplayName("Katalog(EN)")]
        public FileData ENKatalog
        {
            get { return _enkatalog; }
            set { SetPropertyValue(nameof(ENKatalog), ref _enkatalog, value); }
        }

        private DateTime _SonGuncellemeTarihi;
        [XafDisplayName("Son Güncelleme Tarihi"), Browsable(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }

        }
        protected override void OnSaving()
        {
            base.OnSaving();
            SonGuncellemeTarihi = DateTime.Now;
        }
    }
}