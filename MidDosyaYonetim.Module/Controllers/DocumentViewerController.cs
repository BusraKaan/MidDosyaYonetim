using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.XtraPrinting.Native;
using MidDosyaYonetim.Module.BusinessObjects;
using MidDosyaYonetim.Module.Forms;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DocumentViewerController : ViewController
    {
        public DocumentViewerController()
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
            String Teknik = Application.GetDetailViewId(typeof(TeknikCizimler));
            String montaj = Application.GetDetailViewId(typeof(MontajKlavuzlari));
            String Uretimdoc = Application.GetDetailViewId(typeof(UretimDokumanlari));
            String KlasorDoc = Application.GetDetailViewId(typeof(KlasorDokumanlari));
            String kalitedoc = Application.GetDetailViewId(typeof(KaliteDokumanlari));
            String sertifikadoc = Application.GetDetailViewId(typeof(Sertifikalar));
            String digerDoc = Application.GetDetailViewId(typeof(DigerDokumanlar));
            String sartname = Application.GetDetailViewId(typeof(TeknikSartname));

            if (View.Id == Teknik)
            {
                var teknik = e.CurrentObject as TeknikCizimler;
                FileData file = teknik.File;
                string[] words = file.FileName.Split('.');
                string extension = words[words.Length];

                if (extension.Equals(".pdf"))
                {
                    PdfViewerForm pdfForm = new PdfViewerForm(file);
                    pdfForm.ShowDialog();
                }
                else if(extension.Equals(".docx") || extension.Equals(".txt") || extension.Equals(".html") || extension.Equals(".doc"))
                {
                    RichEditForm richEditForm = new RichEditForm(file);
                    richEditForm.ShowDialog();
                }
                else if(extension.Equals(".xls") || extension.Equals(".xlsx"))
                {
                    SpreadSheetForm spreadSheetForm = new SpreadSheetForm(file);
                    spreadSheetForm.ShowDialog();
                }

              

            }
        }
    }
}
