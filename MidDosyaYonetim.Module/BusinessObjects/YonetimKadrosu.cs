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
    [ImageName("BO_Position_v92")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class YonetimKadrosu : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public YonetimKadrosu(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
         DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Görsel"), ToolTip("Önerilen Ölçeklendirme : 150x150 (Kare Fotoğraf)")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }

        private string _Adi;
        [XafDisplayName("Yönetici Adı Soyadı")]
        public string Adi
        {
            get { return _Adi; }
            set
            {
                SetPropertyValue<string>(nameof(Adi), ref _Adi, value);
            }
        }
        private string _Görevi;
        [XafDisplayName("Yönetici Görevi (TR)")]
        public string Görevi
        {
            get { return _Görevi; }
            set
            {
                SetPropertyValue<string>(nameof(Görevi), ref _Görevi, value);
            }
        }
        private string _GöreviEng;
        [XafDisplayName("Yönetici Görevi (ENG)")]
        public string GöreviEng
        {
            get { return _GöreviEng; }
            set
            {
                SetPropertyValue<string>(nameof(GöreviEng), ref _GöreviEng, value);
            }
        }
        private int _Index;
        [XafDisplayName("Sıralama Indexi")]
        public int Index
        {
            get { return _Index; }
            set
            {
                SetPropertyValue<int>(nameof(Index), ref _Index, value);
            }
        }
        private HakkimizdaVeAltBilgi _Hakkimizda;
        [Association("HakkimizdaToYonetimKadrosu"), XafDisplayName("Hakkımızda")]
        public HakkimizdaVeAltBilgi Hakkimizda
        {
            get { return _Hakkimizda; }
            set { SetPropertyValue<HakkimizdaVeAltBilgi>(nameof(Hakkimizda), ref _Hakkimizda, value); }
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
            Image newImage = byteArrayToImage(fotograf);
            Bitmap yeniimg = new Bitmap(200, 200);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                g.DrawImage(newImage, 0, 0, 200, 200);
            MemoryStream stream = new MemoryStream();
            yeniimg.Save(stream, ImageFormat.Jpeg);

            fotograf = stream.GetBuffer();

            SonGuncellemeTarihi = DateTime.Now;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
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