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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WebFotograf : BaseObject, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public WebFotograf(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
        DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), ToolTip("MAX Boyut: 300x300 olmalıdır.")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }

        [XafDisplayName("Web'de Göster (TR)")]
        public bool Web;
        [XafDisplayName("Web'de Göster (ENG)")]
        public bool EngWeb;

        private int _Index;
        public int Index
        {
            get { return _Index; }
            set { SetPropertyValue<int>(nameof(Index), ref _Index, value); }
        }

        private Urunler _Urun;
        [XafDisplayName("Ürün")]
        [Association("webFotografToUrunler")]
        public Urunler Urun
        {
            get { return _Urun; }
            set { SetPropertyValue(nameof(Urun), ref _Urun, value); }
        }
        private UrunSerisi _UrunSerisi;
        [XafDisplayName("Ürün Serisi")]
        [Association("webFotografToUrunSerisi")]
        public UrunSerisi UrunSerisi
        {
            get { return _UrunSerisi; }
            set { SetPropertyValue(nameof(UrunSerisi), ref _UrunSerisi, value); }
        }
        private UrunGrubu _UrunGrubu;
        [XafDisplayName("Ürün Grubu")]
        [Association("webFotografToUrunGrubu")]
        public UrunGrubu UrunGrubu
        {
            get { return _UrunGrubu; }
            set { SetPropertyValue(nameof(UrunGrubu), ref _UrunGrubu, value); }
        }
        
        private Aksesuar _Aksesuar;
        [XafDisplayName("Aksesuar")]
        [Association("webFotografToAksesuarlar")]
        public Aksesuar Aksesuar
        {
            get { return _Aksesuar; }
            set { SetPropertyValue(nameof(Aksesuar), ref _Aksesuar, value); }
        }
        
        private AksesuarGrubu _AksesuarGrubu;
        [XafDisplayName("Aksesuar Grubu")]
        [Association("webFotografToAksesuarGrubu")]
        public AksesuarGrubu AksesuarGrubu
        {
            get { return _AksesuarGrubu; }
            set { SetPropertyValue(nameof(AksesuarGrubu), ref _AksesuarGrubu, value); }
        }
        
        private Fotograflar _KaliteliFotografOid;
        [XafDisplayName("Fotoğraf ID")]
        public Fotograflar KaliteliFotografOid
        {
            get { return _KaliteliFotografOid; }
            set { SetPropertyValue(nameof(KaliteliFotografOid), ref _KaliteliFotografOid, value); }
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

            if (fotograf != null)
            {
                Image newImage = byteArrayToImage(fotograf);
                Bitmap yeniimg = new Bitmap(300, 300);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(newImage, 0, 0, 300, 300);

                MemoryStream stream = new MemoryStream();
                yeniimg.Save(stream, ImageFormat.Jpeg);

                fotograf = stream.GetBuffer();
            }
        }
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                Image returnImage = Image.FromStream(ms, true);//Exception occurs here
                return returnImage;
            }
            catch
            {
                return null;
            }
        }
    }
}