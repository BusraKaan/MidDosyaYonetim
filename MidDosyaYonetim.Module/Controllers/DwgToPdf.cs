using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DwgToPdf : ViewController
    {
        public DwgToPdf()
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

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Aspose.CAD.License lic = new Aspose.CAD.License();
            lic.SetLicense("License.txt");
            string sourceFilePath = "C:/Users/oztas/Desktop/Drawing3.dwg";
            Aspose.CAD.Image image = Aspose.CAD.Image.Load(sourceFilePath);
            var rasterizationOptions = new Aspose.CAD.ImageOptions.CadRasterizationOptions();
            rasterizationOptions.BackgroundColor = Aspose.CAD.Color.White;
            rasterizationOptions.PageWidth = 3508;
            rasterizationOptions.PageHeight = 2480;
            rasterizationOptions.Layouts = new string[] { "Layout1" };
            Aspose.CAD.ImageOptions.PngOptions pdfOptions = new Aspose.CAD.ImageOptions.PngOptions();
            pdfOptions.VectorRasterizationOptions = rasterizationOptions;
            //Export the DWG to PDF
            image.Save("C:/Users/oztas/Desktop/a3pdf.pdf", pdfOptions);
            MessageBox.Show("ok");
        }
    }
}
