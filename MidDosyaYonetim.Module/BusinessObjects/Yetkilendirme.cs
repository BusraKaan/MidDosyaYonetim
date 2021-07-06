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
using System.Collections;
using DevExpress.ExpressApp.Utils;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Yetkilendirme : BaseObject, IObjectSpaceLink, INotifyPropertyChanged
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Yetkilendirme(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private int Id =1;
        [XafDisplayName("Yetki Sırası")]
        public int id
        {
            get
            {
                return Id;
            }
            set
            {
                SetPropertyValue(nameof(id), ref Id, value);
                if (!IsLoading && !IsSaving)
                {
                    OnPropertyChanged("id");
                }
            }
        }
        [XafDisplayName("Yetkili Kişi")]
        [DataSourceCriteria("departmanlar= '@This.departmanlar'")]
        public UserMid2 yetkili { get; set; }
        [XafDisplayName("Departman")]
        public Departmanlar departmanlar;
       

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            IList list = objectSpace.GetObjects(typeof(OnayBekleyenDokumanlar));
            foreach (OnayBekleyenDokumanlar bekleyenDokumanlar in list)
            {

                if (bekleyenDokumanlar.ObjectType == ObjectType)
                {
                    onayBekleyenDokumanlar.Add(bekleyenDokumanlar);
                }


            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
      

        [Association("YetkiToOnayDoc")]
        public XPCollection<OnayBekleyenDokumanlar> onayBekleyenDokumanlar
        {
            get { return GetCollection<OnayBekleyenDokumanlar>(nameof(onayBekleyenDokumanlar)); }
        }
        
         private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }
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
          
            }
        }
        protected override void OnSaving()
        {


            List<Yetkilendirme> yetkilendirme = new List<Yetkilendirme>();
            IList listyetkiler = objectSpace.GetObjects(typeof(Yetkilendirme));
            foreach (Yetkilendirme satir in listyetkiler)
            {
                if (satir.ObjectType == ObjectType)
                {
                    yetkilendirme.Add(satir);
                }

            }
            if (!yetkilendirme.Select(x => x.Oid).Contains(Oid))
            {

                foreach (Yetkilendirme satir in yetkilendirme)
                {

                    if (yetkilendirme.Select(x => x.id).Contains(id))
                    {
                        id = yetkilendirme.Select(x => x.id).Max() + 1;
                    }

                }
            }

        }
    }
    }
