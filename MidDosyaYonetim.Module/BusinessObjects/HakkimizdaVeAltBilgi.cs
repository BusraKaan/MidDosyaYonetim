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
    [ImageName("About")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HakkimizdaVeAltBilgi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HakkimizdaVeAltBilgi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        

        #region Hakkimizda (TR)

        private string _HakkimizdaBaslk;
        [XafDisplayName("Başlık 1 (TR)")]
        public string HakkimizdaBaslk
        {
            get { return _HakkimizdaBaslk; }
            set
            {
                SetPropertyValue<string>(nameof(HakkimizdaBaslk), ref _HakkimizdaBaslk, value);
            }
        }
        private string _HakkimizdaAciklama;
        [XafDisplayName("Paregraf 1 (TR)")]
        public string HakkimizdaAciklama
        {
            get { return _HakkimizdaAciklama; }
            set
            {
                SetPropertyValue<string>(nameof(HakkimizdaAciklama), ref _HakkimizdaAciklama, value);
            }
        }
        private string _HakkimizdaBaslk2;
        [XafDisplayName("Başlık 2 (TR)")]
        public string HakkimizdaBaslk2
        {
            get { return _HakkimizdaBaslk2; }
            set
            {
                SetPropertyValue<string>(nameof(HakkimizdaBaslk2), ref _HakkimizdaBaslk2, value);
            }
        }
        private string _HakkimizdaAciklama2;
        [XafDisplayName("Paragraf 2 (TR)")]
        public string HakkimizdaAciklama2
        {
            get { return _HakkimizdaAciklama2; }
            set
            {
                SetPropertyValue<string>(nameof(HakkimizdaAciklama2), ref _HakkimizdaAciklama2, value);
            }
        }
        #endregion

        #region Hakkimizda (ENG)

        private string _EngHakkimizdaBaslk;
        [XafDisplayName("Başlık 1 (ENG)")]
        public string EngHakkimizdaBaslk
        {
            get { return _EngHakkimizdaBaslk; }
            set
            {
                SetPropertyValue<string>(nameof(EngHakkimizdaBaslk), ref _EngHakkimizdaBaslk, value);
            }
        }
        private string _EngHakkimizdaAciklama;
        [XafDisplayName("Paregraf 1 (ENG)")]
        public string EngHakkimizdaAciklama
        {
            get { return _EngHakkimizdaAciklama; }
            set
            {
                SetPropertyValue<string>(nameof(EngHakkimizdaAciklama), ref _EngHakkimizdaAciklama, value);
            }
        }
        private string _EngHakkimizdaBaslk2;
        [XafDisplayName("Başlık 2 (ENG)")]
        public string EngHakkimizdaBaslk2
        {
            get { return _EngHakkimizdaBaslk2; }
            set
            {
                SetPropertyValue<string>(nameof(EngHakkimizdaBaslk2), ref _EngHakkimizdaBaslk2, value);
            }
        }
        private string _EngHakkimizdaAciklama2;
        [XafDisplayName("Paragraf 2 (ENG)")]
        public string EngHakkimizdaAciklama2
        {
            get { return _EngHakkimizdaAciklama2; }
            set
            {
                SetPropertyValue<string>(nameof(EngHakkimizdaAciklama2), ref _EngHakkimizdaAciklama2, value);
            }
        }
        #endregion

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
         DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Arka Plan Görseli"), ToolTip("Max Ölçeklendirme: 1000x500")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }

        [DevExpress.Xpo.Aggregated, Association("HakkimizdaToAltGrup"), XafDisplayName("Alt Grup Listesi")]
        public XPCollection<HakkimizdaAltGrup> AltGrupList
        {
            get { return GetCollection<HakkimizdaAltGrup>(nameof(AltGrupList)); }
        }
        
        [DevExpress.Xpo.Aggregated, Association("HakkimizdaToYonetimKadrosu"), XafDisplayName("Yönetim Kadrosu")]
        public XPCollection<YonetimKadrosu> YonetimKadrosu
        {
            get { return GetCollection<YonetimKadrosu>(nameof(YonetimKadrosu)); }
        }

        private DateTime _SonGuncellemeTarihi;
        [XafDisplayName("Son Güncelleme Tarihi"), Browsable(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            SonGuncellemeTarihi = DateTime.Now;
        }
    }
}