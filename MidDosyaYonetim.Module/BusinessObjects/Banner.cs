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
    [ImageName("Picture")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Banner : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Banner(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private UrunSerisi urunseri;
        [Association("BannerToSeri")]
        public UrunSerisi UrunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(UrunSerisi), ref urunseri, value); }

        }
        
        private UrunGrubu urunGrup;
        [Association("BannerToGrup")]
        public UrunGrubu UrunGrup
        {

            get { return urunGrup; }
            set { SetPropertyValue(nameof(UrunGrup), ref urunGrup, value); }

        }
        
        private AksesuarGrubu aksesuarGrup;
        [Association("BannerToAksGrup")]
        public AksesuarGrubu AksesuarGrup
        {

            get { return aksesuarGrup; }
            set { SetPropertyValue(nameof(AksesuarGrup), ref aksesuarGrup, value); }

        }

        private IK_VizyonPolitika _IK_VizyonPolitika;
        [Association("BannerToIK")]
        public IK_VizyonPolitika IK_VizyonPolitika
        {

            get { return _IK_VizyonPolitika; }
            set { SetPropertyValue(nameof(IK_VizyonPolitika), ref _IK_VizyonPolitika, value); }

        }

        private GizlilikPolitikasi _GizlilikPolitikasi;
        [Association("BannerToGizlilikPolitikasi")]
        public GizlilikPolitikasi GizlilikPolitikasi
        {

            get { return _GizlilikPolitikasi; }
            set { SetPropertyValue(nameof(GizlilikPolitikasi), ref _GizlilikPolitikasi, value); }

        }

        private CerezPolitikasi _CerezPolitikasi;
        [Association("BannerToCerezPolitikasi")]
        public CerezPolitikasi CerezPolitikasi
        {

            get { return _CerezPolitikasi; }
            set { SetPropertyValue(nameof(CerezPolitikasi), ref _CerezPolitikasi, value); }

        }

        private AydinlatmaMetni _AydinlatmaMetni;
        [Association("BannerToAydinlatmaMetni")]
        public AydinlatmaMetni AydinlatmaMetni
        {

            get { return _AydinlatmaMetni; }
            set { SetPropertyValue(nameof(AydinlatmaMetni), ref _AydinlatmaMetni, value); }

        }
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
         DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Banner Fotoğraf (TR)"), ToolTip("Önerilen Ölçeklendirme : 970x250")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
      DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Banner Fotoğraf (ENG)"), ToolTip("Önerilen Ölçeklendirme : 970x250")]
        public byte[] fotografENG
        {
            get { return GetPropertyValue<byte[]>(nameof(fotografENG)); }
            set { SetPropertyValue<byte[]>(nameof(fotografENG), value); }
        }

        private bool _Tumseriler;
        [XafDisplayName("Tüm Seriler Listesinde Göster")]
        public bool TumSeriler
        {
            get { return _Tumseriler; }
            set { SetPropertyValue<bool>(nameof(TumSeriler), ref _Tumseriler, value); }
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