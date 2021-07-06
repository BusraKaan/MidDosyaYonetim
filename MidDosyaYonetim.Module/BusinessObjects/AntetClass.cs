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
    [DomainComponent]
    [DefaultClassOptions]
    [NonPersistent]

    public class AntetClass : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public AntetClass(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        
        public string TeknikResimNo;
        [XafDisplayName("Çizen")]
        public string Cizen;
        public DateTime Tarih;
        public Material Malzeme;
        [XafDisplayName("Yüzey İşlem")]
        public string YuzeyIslem;
        [XafDisplayName("Ürün/Parça Kodu")]
        public string ParcaKodu;
        [XafDisplayName("Ürün/Parça Tanımı")]
        public string ParcaTanimi;


    }
}