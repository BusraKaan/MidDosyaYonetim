using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;


namespace MidDosyaYonetim.Module.BusinessObjects
{
    // <ImageName("BO_Contact")> _
    // <DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
    // <DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
    // <Persistent("DatabaseTableName")> _
    [DefaultClassOptions()]
    public class OtoKodTanim : BaseObject// Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701). // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    {
        public OtoKodTanim(Session session) : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        [XafDisplayName("Obje Adı")]
        public string obje_adi { get; set; }

        [XafDisplayName("Obje Tipi")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type ObjectType
        {
            get
            {
                return GetPropertyValue<Type>(nameof(ObjectType));
            }
            set
            {
                SetPropertyValue<Type>(nameof(ObjectType), value);
                //Criterion = string.Empty;
            }
        }
        //[CriteriaOptions("ObjectType")]
        //[Size(SizeAttribute.Unlimited)]
        //[EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        //public string Criterion
        //{
        //    get
        //    {
        //        return GetPropertyValue<string>(nameof(Criterion));
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>(nameof(Criterion), value);
        //    }
        //}
        [XafDisplayName("Kod Ön Tanımı")]
        public string kod_on_tanim { get; set; }

        [XafDisplayName("Kod Uzunluğu")]
        public int uzunluk { get; set; }
    }
}