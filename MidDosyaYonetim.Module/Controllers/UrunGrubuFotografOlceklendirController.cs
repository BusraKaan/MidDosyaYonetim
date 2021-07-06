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
    public partial class UrunGrubuFotografOlceklendirController : ViewController
    {
        public UrunGrubuFotografOlceklendirController()
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

        private void UrunGrupFotoOlceklendirAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            IList urungrubu = objectSpace.GetObjects(typeof(UrunGrubu));

            foreach (UrunGrubu item in urungrubu)
            {
                CriteriaOperator crtiteria = CriteriaOperator.Parse("urunGrubu=? AND urunSerisi is null and urunler is null", item.Oid);
                //Fotograflar fotograf = (Fotograflar)ObjectSpace.FindObject(typeof(Fotograflar), crtiteria);
                IList fotograflar = objectSpace.GetObjects(typeof(Fotograflar), crtiteria);

                if (fotograflar != null)
                {
                    foreach (Fotograflar foti in fotograflar)
                    {
                        Image newImage = byteArrayToImage(foti.fotograf);
                        Bitmap yeniimg = new Bitmap(200, 200);
                        using (Graphics g = Graphics.FromImage((System.Drawing.Image)yeniimg))
                            g.DrawImage(newImage, 0, 0, 200, 200);

                        CriteriaOperator cr = CriteriaOperator.Parse("UrunGrubu=?", item.Oid);
                        WebFotograf wf = (WebFotograf)ObjectSpace.FindObject(typeof(WebFotograf), cr);
                        MemoryStream stream = new MemoryStream();
                        yeniimg.Save(stream, ImageFormat.Jpeg);
                        //if (wf == null)
                        //{
                        WebFotograf webfoto = objectSpace.CreateObject<WebFotograf>();
                        webfoto.fotograf = stream.GetBuffer();
                        webfoto.UrunGrubu = item;
                        webfoto.Web = foti.Web;
                        webfoto.EngWeb = foti.EngWeb;
                        webfoto.Index = foti.Index;
                        webfoto.KaliteliFotografOid = foti;
                        objectSpace.CommitChanges();
                        //}
                        //else
                        //{
                        //    wf.fotograf = stream.GetBuffer();
                        //    ObjectSpace.CommitChanges();
                        //}
                    }

                }
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
