using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.IO;
using PdfSharp.Drawing;
using System.Diagnostics;
using IronOcr;
using DevExpress.Xpo.DB;
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class KapakOpsiyonDoc : FileAttachmentBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public KapakOpsiyonDoc(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            OlusturmaTarihi = DateTime.Now;
            if (SecuritySystem.CurrentUser != null)
            {
                var olusturanKisi = SecuritySystem.CurrentUserName;
                OlusturanKisi = olusturanKisi.ToString();
            }
        }

        [Association("KapakOpsToKapakDoc")]
        public KapakOpsiyonlari KapakOpsiyonlari;


        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Doküman Adı")]
        public string DokumanAdi { get; set; }
        [XafDisplayName("Doküman Uzantısı")]
        [ModelDefault("AllowEdit", "False")]
        public string dokumanUzanti { get; set; }
        [XafDisplayName("Doküman Tipi")]
        [ModelDefault("AllowEdit", "False")]
        public Enums.DocumentType DokumanType { get; set; }
        [XafDisplayName("Web'de Göster")]
        public bool Web;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        private Enums.DocumentType dok_type(string uzanti)
        {
            string noktasizuzanti;
            noktasizuzanti = uzanti.Replace(".", "");
            switch (noktasizuzanti)
            {
                case "xls":
                case "xlsx":
                    {
                        return Enums.DocumentType.Excel;
                    }



                case "doc":
                case "docx":
                    {
                        return Enums.DocumentType.Word;
                    }



                case "pdf":
                    {
                        return Enums.DocumentType.Pdf;
                    }



                case "png":
                case "jpg":
                case "jpeg":
                case "ico":
                case "svg":
                case "bmp":
                case "tiff":
                    {
                        return Enums.DocumentType.Resim;
                    }
                case "dwg":
                    {
                        return Enums.DocumentType.Dwg;
                    }
                case "dfx":
                    {
                        return Enums.DocumentType.Dfx;
                    }
                default:
                    {
                        return Enums.DocumentType.Diger;
                    }
            }
        }

        private DateTime _SonGuncellemeTarihi;
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        public DateTime SonGuncellemeTarihi
        {
            get { return _SonGuncellemeTarihi; }
            set { SetPropertyValue<DateTime>(nameof(SonGuncellemeTarihi), ref _SonGuncellemeTarihi, value); }
        }

        protected override void OnSaving()
        {
            DokumanAdi = File.FileName;
            dokumanUzanti = Path.GetExtension(File.FileName).Replace(".", "");
            DokumanType = dok_type(Path.GetExtension(File.FileName));
            SonGuncellemeTarihi = DateTime.Now;
            base.OnSaving();

        }
        private byte[] pdfArray;

        private string ocrtext;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false)]
        public string OcrText
        {
            get { return ocrtext; }
            set { SetPropertyValue(nameof(OcrText), ref ocrtext, value); }
        }
        //protected override void OnSaved()
        //{
        //    if (!IsDeleted && File.FileName.Contains(".pdf"))
        //    {
        //        string queryString = string.Format("SELECT Content FROM [dbo].[FileData] WHERE [Oid] = '{0}'", File.Oid);

        //        SelectedData selected = Session.ExecuteQuery(queryString);
        //        SelectStatementResult st = selected.ResultSet.FirstOrDefault();
        //        var firstRow = st.Rows.FirstOrDefault();
        //        var RowValueArray = firstRow.Values[0];


        //        //MemoryStream ms = CompressionUtils.Decompress(new MemoryStream((byte[])RowValueArray));
        //        pdfArray = CompressionUtils.Decompress(new MemoryStream((byte[])RowValueArray)).ToArray();

        //        string str = Convert.ToBase64String(pdfArray);

        //        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf"))
        //        {
        //            System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf");
        //            PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
        //            PdfSharp.Pdf.PdfPage pdfPage = pdf.AddPage();
        //            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        //            string pdfFilename = AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf";

        //            pdf.Save(pdfFilename);
        //            WriteByteArrayToPdf(str, AppDomain.CurrentDomain.BaseDirectory, "OcrText.pdf");
        //        }
        //        else
        //        {
        //            PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
        //            PdfSharp.Pdf.PdfPage pdfPage = pdf.AddPage();
        //            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
        //            string pdfFilename = AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf";

        //            pdf.Save(pdfFilename);
        //            //Process.Start(pdfFilename);
        //        }

        //        var ocr = new AutoOcr();
        //        ocr.Language = IronOcr.Languages.Turkish.OcrLanguagePack;
        //        var result = ocr.ReadPdf(AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf");
        //        string text = result.Text.Replace("'", "");


        //        string queryString1 = string.Format("UPDATE [dbo].[KapakOpsiyonDoc] SET OcrText = '" + text + "' WHERE [Oid] = '" + Oid + "'");
        //        SelectedData selected1 = Session.ExecuteQuery(queryString1);
        //    }
        //    base.OnSaved();
        //}




        public void WriteByteArrayToPdf(string inPDFByteArrayStream, string pdflocation, string fileName)
        {
            byte[] data = Convert.FromBase64String(inPDFByteArrayStream);
            if (Directory.Exists(pdflocation))
            {
                pdflocation = pdflocation + fileName;
                using (FileStream Writer = new System.IO.FileStream(pdflocation, FileMode.Create, FileAccess.Write))
                {

                    Writer.Write(data, 0, data.Length);
                }
            }
            else
            {
                throw new System.Exception("PDF Shared Location not found");
            }

        }
        protected override void OnSaved()
        {
            string sorgu = "Update FileData SET SonGuncellemeTarihi = '" + DateTime.Now.ToString() + "' Where Oid='" + File.Oid.ToString() + "'";
            con = new SqlConnection("Server=10.26.0.30; Database=test3; User Id=sa;Password=Mdsf2020@");
            cmd = new SqlCommand(sorgu, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            base.OnSaved();
        }

        private SqlConnection con;
        private SqlCommand cmd;
    }
}