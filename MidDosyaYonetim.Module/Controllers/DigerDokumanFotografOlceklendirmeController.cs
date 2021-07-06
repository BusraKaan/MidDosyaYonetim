using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MidDosyaYonetim.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DigerDokumanFotografOlceklendirmeController : ViewController
    {
        public DigerDokumanFotografOlceklendirmeController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void DigerDokFotoOlceklendirmeAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            IList digerdok = objectSpace.GetObjects(typeof(DigerDokumanlar));

            foreach (DigerDokumanlar item in digerdok)
            {
                Image newImage = byteArrayToImage(item.fotograf);
                Bitmap yeniimg = new Bitmap(208, 294);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                    g.DrawImage(newImage, 0, 0, 208, 294);

                MemoryStream stream = new MemoryStream();
                yeniimg.Save(stream, ImageFormat.Jpeg);
                item.fotograf = stream.GetBuffer();
                item.Save();
                objectSpace.CommitChanges();
            }
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
