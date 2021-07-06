using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MidDosyaYonetim.Module.Controllers
{
    public partial class PdfViewerForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public PdfViewerForm(IFileData fileData)
        {
            InitializeComponent();
            using (MemoryStream pdfStream = new MemoryStream())
            {
                fileData.SaveToStream(pdfStream);
                pdfStream.Flush();
                pdfStream.Position = 0;            
                pdfViewer1.LoadDocument(pdfStream);

            }
          
        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
