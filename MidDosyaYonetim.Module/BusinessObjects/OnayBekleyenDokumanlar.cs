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
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Helpers;
using MidDosyaYonetim.Module.Controllers;
using DevExpress.Xpo.DB;
using System.IO;
using PdfSharp.Drawing;
using System.Diagnostics;
using IronOcr;
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]

    [Appearance("onay", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = false && red =false", Context = "ListView", BackColor = "#f2f668",
        FontColor = "Black", Priority = 1)]
    [Appearance("onay1", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = false && red =true", Context = "ListView", BackColor = "#ec3838",
        FontColor = "Black", Priority = 1)]
    [Appearance("onay2", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = true && red =false", Context = "ListView", BackColor = "#A5D6A7",
        FontColor = "Black", Priority = 1)]
    public class OnayBekleyenDokumanlar : FileAttachmentBase, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public OnayBekleyenDokumanlar(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }



        [XafDisplayName("Departman Müdürü")]
        public UserMid2 UserMid2;
        private bool onayp;
        private bool redp;

        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Ürün Ailesi")]
        public string urunAilesi;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Ürün Grubu")]
        public string urunGrubu;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Ürün Serisi")]
        public string urunSerisi;
        [ModelDefault("AllowEdit", "False")]
        [XafDisplayName("Ürün Stok Kodu")]
        public string urunler;

        [XafDisplayName("Onay Durumu")]
        public bool onay
        {
            get
            {
                return onayp;
            }
            set
            {
                SetPropertyValue(nameof(onay), ref onayp, value);
                if (!IsLoading && !IsSaving)
                {
                    OnPropertyChanged("onay");
                }
            }
        }
        [XafDisplayName("Red Durumu")]
        public bool red
        {
            get
            {
                return redp;
            }
            set
            {
                SetPropertyValue(nameof(red), ref redp, value);
                if (!IsLoading && !IsSaving)
                {
                    OnPropertyChanged("red");
                }
            }
        }
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Döküman Oluşturan")]
        [ModelDefault("AllowEdit", "false")]
        public string DokumanOlusturanKisi;
        [XafDisplayName("Döküman Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy hh:mm}")]
        public DateTime DokumanOlusturulmaTarihi;
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Döküman Onaylanma Tarihi")]
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy hh:mm}")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime DokumanOnaylanmaTarihi;
        [XafDisplayName("Obje Tipi")]
        [ValueConverter(typeof(TypeToStringConverter))]
        [ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [ModelDefault("AllowEdit", "false")]
        public Type ObjectType
        {
            get
            {
                return GetPropertyValue<Type>(nameof(ObjectType));
            }
            set
            {
                SetPropertyValue<Type>(nameof(ObjectType), value);
                //Criterion = string.Empty;
            }
        }
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }
        [Association("YetkiToOnayDoc")]
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Yetkili")]
        public Yetkilendirme yetkilendirme
        {
            get; set;
        }


        public Guid FileOid;

        private void addOnayToDoc(List<Yetkilendirme> allYetki, int minid, bool controlonay, bool controlred)
        {
            IList mailList = objectSpace.GetObjects(typeof(EmailListesi));

            if (allYetki.Select(x => x.id).Max() + 1 == minid)
            {
                CriteriaOperator cri = CriteriaOperator.Parse("[Oid] =?", FileOid);
                switch (ObjectType.Name.ToString())
                {

                    case "TeknikCizimler":

                        TeknikCizimler teknikCizimler = objectSpace.FindObject<TeknikCizimler>(cri);
                        teknikCizimler.onay = controlonay;
                        teknikCizimler.red = controlred;
                        teknikCizimler.Save();
                        break;
                    case "DigerDokumanlar":
                        DigerDokumanlar digerDokumanlar = objectSpace.FindObject<DigerDokumanlar>(cri);
                        digerDokumanlar.onay = controlonay;
                        digerDokumanlar.red = controlred;
                        digerDokumanlar.Save();
                        break;
                    case "KaliteDokumanlari":
                        KaliteDokumanlari kaliteDokumanlari = objectSpace.FindObject<KaliteDokumanlari>(cri);
                        kaliteDokumanlari.onay = controlonay;
                        kaliteDokumanlari.red = controlred;
                        kaliteDokumanlari.Save();
                        break;
                    case "MontajKlavuzlari":
                        MontajKlavuzlari montajKlavuzlari = objectSpace.FindObject<MontajKlavuzlari>(cri);
                        montajKlavuzlari.onay = controlonay;
                        montajKlavuzlari.red = controlred;
                        montajKlavuzlari.Save();
                        break;
                    case "Sertifikalar":
                        Sertifikalar sertifikalar = objectSpace.FindObject<Sertifikalar>(cri);
                        sertifikalar.onay = controlonay;
                        sertifikalar.red = controlred;
                        sertifikalar.Save();
                        break;
                    case "UretimDokumanlari":
                        UretimDokumanlari uretimDokumanlari = objectSpace.FindObject<UretimDokumanlari>(cri);
                        uretimDokumanlari.onay = controlonay;
                        uretimDokumanlari.red = controlred;
                        uretimDokumanlari.Save();
                        break;
                    case "KlasorDokumanlari":
                        KlasorDokumanlari klasorDokumanlari = objectSpace.FindObject<KlasorDokumanlari>(cri);
                        klasorDokumanlari.onay = controlonay;
                        klasorDokumanlari.red = controlred;
                        klasorDokumanlari.Save();
                        break;

                    case "TeknikSartname":
                        TeknikSartname teknikSartname = objectSpace.FindObject<TeknikSartname>(cri);
                        teknikSartname.onay = controlonay;
                        teknikSartname.red = controlred;
                        teknikSartname.Save();
                        break;
                }

            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            List<Yetkilendirme> allYetki = new List<Yetkilendirme>();
            IList listyetkiler = objectSpace.GetObjects(typeof(Yetkilendirme));
            foreach (Yetkilendirme satir in listyetkiler)
            {
                if (satir.ObjectType == ObjectType)
                {
                    allYetki.Add(satir);
                }

            }
            List<OnayBekleyenDokumanlar> allonaydoc = new List<OnayBekleyenDokumanlar>();
            allonaydoc.Clear();
            IList liste = objectSpace.GetObjects(typeof(OnayBekleyenDokumanlar));


            foreach (OnayBekleyenDokumanlar satir in liste)
            {
                if (satir.FileOid == FileOid)
                {
                    allonaydoc.Add(satir);
                }

            }
            int minid = allonaydoc.Select(x => x.yetkilendirme.id).Max() + 1;
            if (onay == true)
            {
                if (allYetki.Select(x => x.id).Max() == allYetki.Select(x => x.id).Min())
                {
                    addOnayToDoc(allYetki, minid, true, false);
                }
                else
                {

                    OnayBekleyenDokumanlar onaydoc = objectSpace.CreateObject<OnayBekleyenDokumanlar>();

                    onaydoc.DokumanOlusturanKisi = DokumanOlusturanKisi;
                    onaydoc.DokumanOlusturulmaTarihi = DokumanOlusturulmaTarihi;
                    onaydoc.File = File;
                    onaydoc.ObjectType = ObjectType;
                    if (onaydoc.UserMid2 == null)
                    {
                        foreach (Yetkilendirme satir in allYetki)
                        {
                            if (allYetki.Select(x => x.id).Contains(minid) && allYetki.Select(x => x.ObjectType).Contains(ObjectType))
                            {
                                if (satir.id == minid)
                                {
                                    onaydoc.UserMid2 = satir.departmanlar.DepartmanMuduru;
                                    onaydoc.yetkilendirme = satir;
                                    onaydoc.FileOid = FileOid;
                                    onaydoc.DokumanOnaylanmaTarihi = DateTime.Now;



                                }

                            }

                        }

                    }

                    onaydoc.Save();
                    addOnayToDoc(allYetki, minid, true, false);

                }
            }
            else
            {
                if (red == false)
                {
                    addOnayToDoc(allYetki, minid, false, false);

                }
                else
                {
                    addOnayToDoc(allYetki, minid, false, true);
                }

            }

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
        //           // Process.Start(pdfFilename);
        //        }

        //        var ocr = new AutoOcr();
        //        ocr.Language = IronOcr.Languages.Turkish.OcrLanguagePack;
        //        var result = ocr.ReadPdf(AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf");
        //        string text = result.Text.Replace("'", "");


        //        string queryString1 = string.Format("UPDATE [dbo].[OnayBekleyenDokumanlar] SET OcrText = '" + text + "' WHERE [Oid] = '" + Oid + "'");
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