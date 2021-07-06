using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.XtraBars;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPdfViewer.Bars;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidDosyaYonetim.Module.Controllers
{
    [PropertyEditor(typeof(object), "PdfViewerEditor", false)]
    public class PdfViewerEditor : WinPropertyEditor
    {
        private PdfViewer pdfViewer;
        private PdfBarController pdfBarController;
        private BarManager barManager;
        public PdfViewerEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        {
        }
        protected override object CreateControlCore()
        {
            System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
            pdfViewer = new PdfViewer();
            pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            pdfViewer.DetachStreamAfterLoadComplete = false;
            panel.Controls.Add(pdfViewer);
            pdfBarController = new PdfBarController();
            pdfBarController.Control = pdfViewer;
            barManager = new BarManager();
            barManager.Form = panel;
            pdfViewer.Load += pdfViewer_Load;
            return panel;
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            if (pdfViewer != null)
            {
                pdfViewer.Load -= pdfViewer_Load;
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (this.pdfBarController != null)
                    {
                        this.pdfBarController.Dispose();
                    }
                    if (this.barManager != null)
                    {
                        this.barManager.Dispose();
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        void pdfViewer_Load(object sender, EventArgs e)
        {
            pdfViewer.CreateBars();
        }
        Stream pdfStream;
        protected override void ReadValueCore()
        {
            IFileData fileData = PropertyValue as IFileData;
            if (pdfViewer != null && fileData != null && fileData.FileName.ToLower().Contains(".pdf"))
            {
                pdfStream = new MemoryStream();
                fileData.SaveToStream(pdfStream);
                pdfStream.Position = 0;
                pdfViewer.LoadDocument(pdfStream);
            }
            else
            {
                pdfStream = null;
                pdfViewer.CloseDocument();
            }
        }
    }
}
