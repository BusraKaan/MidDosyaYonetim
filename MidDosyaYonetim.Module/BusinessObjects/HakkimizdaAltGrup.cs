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
    public class HakkimizdaAltGrup : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HakkimizdaAltGrup(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
         DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 30), DevExpress.Xpo.DisplayName("Görsel"), ToolTip("Önerilen Ölçeklendirme: 200x200 (Kare Fotoğraf)")]
        public byte[] fotograf
        {
            get { return GetPropertyValue<byte[]>(nameof(fotograf)); }
            set { SetPropertyValue<byte[]>(nameof(fotograf), value); }
        }

        private string _GrupBaslik;
        [XafDisplayName("Grup Başlık (TR)")]
        public string GrupBaslik
        {
            get { return _GrupBaslik; }
            set
            {
                SetPropertyValue<string>(nameof(GrupBaslik), ref _GrupBaslik, value);
            }
        }
        private string _GrupAciklama;
        [XafDisplayName("Grup Açıklama (TR)")]
        public string GrupAciklama
        {
            get { return _GrupAciklama; }
            set
            {
                SetPropertyValue<string>(nameof(GrupAciklama), ref _GrupAciklama, value);
            }
        }
        
        private string _EngGrupBaslik;
        [XafDisplayName("Grup Başlık (ENG)")]
        public string EngGrupBaslik2
        {
            get { return _EngGrupBaslik; }
            set
            {
                SetPropertyValue<string>(nameof(EngGrupBaslik2), ref _EngGrupBaslik, value);
            }
        }
        private string _EngGrupAciklama;
        [XafDisplayName("Grup Açıklama (ENG)")]
        public string EngGrupAciklama
        {
            get { return _EngGrupAciklama; }
            set
            {
                SetPropertyValue<string>(nameof(EngGrupAciklama), ref _EngGrupAciklama, value);
            }
        }

        private HakkimizdaVeAltBilgi _Hakkimizda;
        [Association("HakkimizdaToAltGrup"), XafDisplayName("Hakkimizda Ve Alt Bilgi")]
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

        protected override void OnSaved()
        {
            base.OnSaved();
            SonGuncellemeTarihi = DateTime.Now;
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