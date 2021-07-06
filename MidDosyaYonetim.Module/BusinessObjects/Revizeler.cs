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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Appearance("Islenmedi", AppearanceItemType = "ViewItem", TargetItems = "*",
   Criteria = "MakineyeIslendi = False", Context = "ListView", BackColor = "#ec3838",
       FontColor = "Black", Priority = 1)] 
    [Appearance("Islendi", AppearanceItemType = "ViewItem", TargetItems = "*",
   Criteria = "MakineyeIslendi = True", Context = "ListView", BackColor = "#007304",
       FontColor = "Black", Priority = 1)]
    public class Revizeler : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Revizeler(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            RevizeTarihi = DateTime.Now;
            if (SecuritySystem.CurrentUser != null)
            {
                var olusturanKisi = SecuritySystem.CurrentUserName;
                RevizeEdenKisi = olusturanKisi.ToString();

            }
        }
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }
        [XafDisplayName("Teknik Çizimler")]
        [Association("TeknikToRevize")]
        public TeknikCizimler teknikCizimler;

       
        [XafDisplayName("Üretim Dökümanları")]
        [Association("UretimToRevize")]
        public UretimDokumanlari uretimDokumanlari;

       

        [XafDisplayName("Fotoğraflar")]
        [Association("FotografToRevize")]
        public Fotograflar fotograflar;

        [Association("KaliteToRevize")]
        [XafDisplayName("Kalite Dökümanları")]
        public KaliteDokumanlari kaliteDokumanlari;
        
        [XafDisplayName("Klasör Dökümanları")]
        [Association("klasorToRevize")]
        public KlasorDokumanlari klasorDokumanlari;

        [Association("SertifikaToRevize")]
        [XafDisplayName("Sertifikalar")]
        public Sertifikalar sertifikaDokumanlari;

        [XafDisplayName("Teknik Şartnameler")]
        [Association("SartnameToRevize")]
        public TeknikSartname teknikSartname;

        [XafDisplayName("Revize Eden Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String RevizeEdenKisi;
        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Revize Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime RevizeTarihi;
        [XafDisplayName("Revize Nedeni")]
        public RevizeNedenleri RevizeNedenleri;

        [XafDisplayName("Revize Edilen Döküman")]
        public FileData RevizeDokuman;

        [XafDisplayName("Yeni Döküman")]
        public FileData YeniDokuman;
        [XafDisplayName("Diğer Dökümanlar")]
        [Association("DigerToRevize")]
        public DigerDokumanlar digerDokumanlar;
       
        [XafDisplayName("Web'de Göster")]
        public bool Web;
        [Association("MontajToRevize")]
        [XafDisplayName("Montaj Kılavuzları")]
        public MontajKlavuzlari MontajKlavuzlari;

        [XafDisplayName("Makineye işlendi")]
        public bool MakineyeIslendi;


    }
}