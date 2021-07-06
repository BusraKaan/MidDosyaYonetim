using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.DataProcessing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraScheduler;
using MidDosyaYonetim.Module.BusinessObjects;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TooltipController : ObjectViewController<DevExpress.ExpressApp.ListView, Fotograflar>
    {
        public TooltipController()
        {
            InitializeComponent();

        }
        ToolTipController tooltipController;
        protected override void OnActivated()
        {
            base.OnActivated();
            tooltipController = new ToolTipController();           
            tooltipController.GetActiveObjectInfo += tooltipController_GetActiveObjectInfo;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            GridListEditor gridListEditor = View.Editor as GridListEditor;
            if (gridListEditor != null)
            {
                gridListEditor.Grid.ToolTipController = tooltipController;
            }
        }
        protected override void OnDeactivated()
        {
            if (tooltipController != null)
            {
                tooltipController.GetActiveObjectInfo -= tooltipController_GetActiveObjectInfo;
                tooltipController = null;
            }
            base.OnDeactivated();
        }
        void tooltipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {

            if (e.SelectedControl is GridControl)
            {
                GridView view = ((GridControl)e.SelectedControl).GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                Fotograflar foto = view.GetRow(hi.RowHandle) as Fotograflar;
                Image img;
                var ms2 = new MemoryStream();

                // object toolTipInfoIdentifier = String.Format("{0}", "Ürün miktarı kritik seviyede!");

                if (foto != null && foto.fotograf != null)
                {
                    using (var ms = new MemoryStream(foto.fotograf))
                    {
                        img = Image.FromStream(ms);
                        //img.Name(foto.File.FileName);
                        img.Save(ms2, ImageFormat.Jpeg);



                    }
                    //Image imageObject = VaryQualityLevel(img);

                    object toolTipInfoIdentifier = img;                  
                    var toolTipControlInfo = new ToolTipControlInfo(toolTipInfoIdentifier, " ");
                    toolTipControlInfo.ToolTipImage = img;
                    toolTipControlInfo.ImmediateToolTip = false;
                    e.Info = toolTipControlInfo;


                }
            }

        }
        //private Image VaryQualityLevel(Image img)
        //{

        //    string fileName = "convertedImage";
        //    // Get a bitmap.
        //    Bitmap bmp1 = new Bitmap(img);
        //    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Png);

        //    // Create an Encoder object based on the GUID
        //    // for the Quality parameter category.
        //    System.Drawing.Imaging.Encoder myEncoder =
        //        System.Drawing.Imaging.Encoder.Quality;

        //    // Create an EncoderParameters object.
        //    // An EncoderParameters object has an array of EncoderParameter
        //    // objects. In this case, there is only one
        //    // EncoderParameter object in the array.
        //    EncoderParameters myEncoderParameters = new EncoderParameters(1);

        //    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
        //        50L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(fileName, jpgEncoder,
        //        myEncoderParameters);

        //    myEncoderParameter = new EncoderParameter(myEncoder, 100L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(fileName, jpgEncoder,
        //        myEncoderParameters);

        //    // Save the bitmap as a JPG file with zero quality level compression.
        //    myEncoderParameter = new EncoderParameter(myEncoder, 0L);
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    bmp1.Save(fileName, jpgEncoder,
        //        myEncoderParameters);

        //    return bmp1;
        //}


        //private ImageCodecInfo GetEncoder(ImageFormat format)
        //{

        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.FormatID == format.Guid)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}

    }
}

