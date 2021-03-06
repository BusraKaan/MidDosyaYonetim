using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.IO;
using DevExpress.Persistent.Base;

namespace MidDosyaYonetim.Module.Forms
{
    public partial class RichEditForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RichEditForm(IFileData fileData)
        {
            InitializeComponent();
            using (MemoryStream pdfStream = new MemoryStream())
            {
                fileData.SaveToStream(pdfStream);
                pdfStream.Flush();
                pdfStream.Position = 0;
                richEditControl1.LoadDocument(pdfStream);

            }
        }
    }
}