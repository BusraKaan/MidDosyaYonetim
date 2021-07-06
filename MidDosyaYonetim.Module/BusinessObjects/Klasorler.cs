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
using DevExpress.Persistent.Base.General;
using System.Drawing;
using DevExpress.ExpressApp.Utils;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Klasorler : BaseObject, ITreeNode, ITreeNodeImageProvider
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Klasorler(Session session)
            : base(session)
        {
        }
        public Enums1.KlasorTipiEnum klasorTipiEnum { get; set; }
        public String KlasorAdi;
        public String KlasorKodu;
        private Klasorler _parentKlasor;
        
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

       
        
        protected override void OnSaving()
        {
            string code;
            if (!(Session is NestedUnitOfWork) && (Session.DataLayer != null) && (Session.IsNewObject(this)) && KlasorKodu == null)
            {
                OtoKodTanim kod_tanim = Session.FindObject<OtoKodTanim>(new BinaryOperator(nameof(kod_tanim.ObjectType),this.GetType()));
                int deger;
                deger = DistributedIdGeneratorHelper.Generate(Session.DataLayer, this.GetType().FullName, "MyServerPrefix");
                if (kod_tanim != null)
                {
                    string ondeger = kod_tanim.kod_on_tanim;
                    int uzunluk = kod_tanim.uzunluk;
                    KlasorKodu = string.Format("{0}{1:D" + uzunluk.ToString() + "}", ondeger, deger);
                    code = KlasorKodu;

                    if(Children != null && _parentKlasor !=null)
                    {
                        ondeger = kod_tanim.kod_on_tanim;
                        uzunluk = kod_tanim.uzunluk;
                        _parentKlasor.KlasorKodu = code + "" + string.Format("{0}{1:D" + uzunluk.ToString() + "}", ondeger, deger);
                    }
                }              
                else
                    KlasorKodu = string.Format("{0:D4}", deger);
            }
            base.OnSaving();
        }
       

        [Browsable(false)]
        [Association("KlasortoKlasor")]
        public Klasorler parentKlasor
        {
            get
            {
                return _parentKlasor;
            }
            set
            {
                SetPropertyValue<Klasorler>(nameof(parentKlasor), ref _parentKlasor, value);
            }
        }
        [Association("KlasortoKlasor")]
        [DevExpress.Xpo.Aggregated]
        public XPCollection<Klasorler> NestedKlasor
        {
            get
            {
                return GetCollection<Klasorler>(nameof(NestedKlasor));
            }
        }
        public string Name
        {
            get
            {
                return KlasorAdi;
            }
        }

        public ITreeNode Parent
        {
            get
            {
                return parentKlasor;
            }
        }

        public IBindingList Children
        {
            get 
            { 
                return NestedKlasor; 
            }
        }
        public Image GetImage(out string imageName)
        {
            imageName = "BO_Folder";
           
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        [Association("KlasorToDokuman")]
        [XafDisplayName("Klasör Dökümanları")]
        public XPCollection<KlasorDokumanlari> klasorDokumanlari
        {
            get { return GetCollection<KlasorDokumanlari>(nameof(klasorDokumanlari)); }

        }

        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        public string KlasorYolu
        {

            get { return getKlasorAdi(); }
        }



        private string FolderPath = null;
        private string getKlasorAdi()
        {
            
            if (parentKlasor != null)
            {
                FolderPath = parentKlasor.Name + "/" + KlasorAdi;
                return FolderPath;
            }
            else
            {
                FolderPath = KlasorAdi;
                return FolderPath;
            }


        }
        
    }
}