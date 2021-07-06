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
    public class Taglar : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Taglar(Session session)
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
        [XafDisplayName("Ürün Ailesi")]
        [Association("TagToAilesi")]
        public UrunAilesi urunAilesi;
        [Association("TagToGrubu")]
        [DataSourceCriteria("urunAilesi= '@This.urunAilesi'")]
        public UrunGrubu urunGrubu { get; set; }
        [DataSourceCriteria("urunGrubu= '@This.urunGrubu'")]
        [Association("TagToSerisi")]
        public UrunSerisi urunSerisi { get; set; }
        [Association("TagToUrunler")]
        public Urunler urunler;
        [Association("TagToAksesuar")]
        public Aksesuar aksesuar;
        [Association("TagToParca")]
        public Parcalar parcalar;
        [Association("TagToTeknik")]
        public TeknikCizimler teknikCizimler;
       
        [Association("TagToSertifika")]
        public Sertifikalar sertifikaDokumanlari;
        [Association("TagToUretim")]
        public UretimDokumanlari uretimDokumanlari;
        [Association("TagToFotograflar")]
        public Fotograflar fotograflar;
        [Association("TagToKalite")]
        public KaliteDokumanlari kaliteDokumanlari;
        public string Tag;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy hh:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [Association("TagToDiger")]
        public DigerDokumanlar digerDokumanlar;
        [Association("TagToOpsiyon")]
        public KapakOpsiyonlari kapakOpsiyonlari;
        [Association("MontajToTag")]
        public MontajKlavuzlari montajKlavuzlari;
        [Association("KlasorToTag")]
        public KlasorDokumanlari klasorDokumanlari;
        [Association("TagToSartname")]
        public TeknikSartname teknikSartname;

    }
}