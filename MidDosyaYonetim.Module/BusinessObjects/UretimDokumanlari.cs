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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using System.Collections;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo.DB;
using PdfSharp.Drawing;
using System.Diagnostics;
using IronOcr;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("onay", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = false && red =false", Context = "ListView", BackColor = "#f2f668",
        FontColor = "Black", Priority = 1)]
    [Appearance("onay1", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = false && red =true", Context = "ListView", BackColor = "#ec3838",
        FontColor = "Black", Priority = 1)]
    [Appearance("onay2", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "onay = true && red =false", Context = "ListView", BackColor = "#A5D6A7",
        FontColor = "Black", Priority = 1)]
    public class UretimDokumanlari : FileAttachmentBase, IObjectSpaceLink
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public UretimDokumanlari(Session session)
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
        private UrunGrubu urungrup;
        private UrunSerisi urunseri;
        private UrunAilesi urunaile;

        [XafDisplayName("Ürün Ailesi")]
        [Association("UretimToAilesi")]
        public UrunAilesi urunAilesi
        {
            get { return getAile(); }
            set { }
        }
        private UrunAilesi getAile()
        {
            if (urunseri != null)
            {
                urunaile = urunseri.urunAilesi;
                return urunaile;
            }
            return urunaile;
        }
        [Association("UretimToGrubu")]
        [XafDisplayName("Ürün Grubu")]    
        public UrunGrubu urunGrubu
        {

            get { return getGrup(); }
            set { }
        }
        private UrunGrubu getGrup()
        {
            if (urunSerisi != null)
            {
                urungrup = urunSerisi.urunGrubu;
                return urungrup;
            }
            return urungrup;

        }

        [Association("UretimToSerisi")]       
        [XafDisplayName("Ürün Serisi")]
        public UrunSerisi urunSerisi
        {

            get { return urunseri; }
            set { SetPropertyValue(nameof(urunSerisi), ref urunseri, value); }

        }
        [Association("UretimToUrunler")]
        [XafDisplayName("Ürünler")]
        public Urunler urunler { get; set; }
        [Association("UretimToAksesuar")]
        public Aksesuar aksesuar;
        protected override void OnSaving()
        {

            DokumanAdi = File.FileName;
            dokumanUzanti = Path.GetExtension(File.FileName).Replace(".", "");
            DokumanType = dok_type(Path.GetExtension(File.FileName));


            if (urunler != null)
            {
                this.urunAilesi = urunler.urunAilesi;
                this.urunGrubu = urunler.urunGrubu;
                this.urunSerisi = urunler.urunSerisi;
                this.urunler = urunler;
            }
            else
            {
                if (urunSerisi != null)
                {
                    this.urunAilesi = urunSerisi.urunAilesi;
                    this.urunGrubu = urunSerisi.urunGrubu;
                    this.urunSerisi = urunSerisi;

                }
                else
                {
                    if (urunGrubu != null)
                    {
                        this.urunAilesi = urunGrubu.urunAilesi;
                        this.urunGrubu = urunGrubu;

                    }
                    else
                    {
                        if (urunAilesi != null)
                        {
                            this.urunAilesi = urunAilesi;

                        }
                    }
                }
            }
            
            AddOnay();
            

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
        //           // Process.Start(pdfFilename);
        //        }

        //        var ocr = new AutoOcr();
        //        ocr.Language = IronOcr.Languages.Turkish.OcrLanguagePack;
        //        var result = ocr.ReadPdf(AppDomain.CurrentDomain.BaseDirectory + @"OcrText.pdf");
        //        string text = result.Text.Replace("'", "");


        //        string queryString1 = string.Format("UPDATE [dbo].[UretimDokumanlari] SET OcrText = '" + text + "' WHERE [Oid] = '" + Oid + "'");
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

        private void AddOnay()
        {
            CriteriaOperator cri = CriteriaOperator.Parse("[FileOid] =?", Oid);
            OnayBekleyenDokumanlar control = objectSpace.FindObject<OnayBekleyenDokumanlar>(cri);

            if (control == null)
            {

                List<FileData> allFile = new List<FileData>();
                List<Yetkilendirme> allYetki = new List<Yetkilendirme>();
                OnayBekleyenDokumanlar onaydoc = objectSpace.CreateObject<OnayBekleyenDokumanlar>();
                onaydoc.DokumanOlusturulmaTarihi = DateTime.Now;
                onaydoc.DokumanOlusturanKisi = OlusturanKisi;
                onaydoc.onay = false;
                onaydoc.red = false;
                onaydoc.File = File;
                onaydoc.ObjectType = typeof(UretimDokumanlari);
                IList listyetkiler = objectSpace.GetObjects(typeof(Yetkilendirme));
                foreach (Yetkilendirme satir in listyetkiler)
                {
                    if (satir.ObjectType == typeof(UretimDokumanlari))
                    {
                        allYetki.Add(satir);
                    }

                }
                if (onaydoc.UserMid2 == null)
                {
                    int minid = allYetki.Select(x => x.id).Min();
                    foreach (Yetkilendirme satir in allYetki)
                    {
                        if (allYetki.Select(x => x.id).Contains(minid))
                        {
                            if (satir.id == minid)
                            {
                                onaydoc.UserMid2 = satir.departmanlar.DepartmanMuduru;
                                onaydoc.yetkilendirme = satir;
                                onaydoc.FileOid = Oid;
                                if (urunAilesi != null)
                                {
                                    onaydoc.urunAilesi = urunAilesi.UrunAilesiAdi;
                                }

                                if (urunGrubu != null)
                                {
                                    onaydoc.urunGrubu = urunGrubu.UrunGrubuAdi;
                                }

                                if (urunSerisi != null)
                                {
                                    onaydoc.urunSerisi = urunSerisi.UrunSerisiAdi;
                                }

                                if (urunler != null)
                                {
                                    onaydoc.urunler = urunler.StokKodu;
                                }
                                if (satir.yetkili.ToString() == OlusturanKisi)
                                {
                                    onaydoc.onay = true;
                                    onaydoc.DokumanOnaylanmaTarihi = DateTime.Now;


                                    if (allYetki.Count > 1)
                                    {
                                        foreach (Yetkilendirme satir2 in allYetki)
                                        {

                                            if (satir2.id == minid + 1)
                                            {
                                                OnayBekleyenDokumanlar onaydoc2 = objectSpace.CreateObject<OnayBekleyenDokumanlar>();
                                                onaydoc2.DokumanOlusturulmaTarihi = DateTime.Now;
                                                onaydoc2.DokumanOlusturanKisi = OlusturanKisi;
                                                onaydoc2.onay = false;
                                                onaydoc2.red = false;
                                                onaydoc2.File = File;
                                                onaydoc2.ObjectType = typeof(UretimDokumanlari);
                                                onaydoc2.UserMid2 = satir2.departmanlar.DepartmanMuduru;
                                                onaydoc2.yetkilendirme = satir2;
                                                onaydoc2.FileOid = Oid;
                                                if (urunAilesi != null)
                                                {
                                                    onaydoc2.urunAilesi = urunAilesi.UrunAilesiAdi;
                                                }

                                                if (urunGrubu != null)
                                                {
                                                    onaydoc2.urunGrubu = urunGrubu.UrunGrubuAdi;
                                                }

                                                if (urunSerisi != null)
                                                {
                                                    onaydoc2.urunSerisi = urunSerisi.UrunSerisiAdi;
                                                }

                                                if (urunler != null)
                                                {
                                                    onaydoc2.urunler = urunler.StokKodu;
                                                }
                                                onaydoc2.Save();
                                            }
                                        }
                                        if (allYetki.Select(x => x.id).Min() == allYetki.Select(x => x.id).Max())
                                        {
                                            onaydoc.onay = true;
                                            onaydoc.DokumanOnaylanmaTarihi = DateTime.Now;
                                            onay = true;
                                            onaydoc.Save();
                                        }
                                        else
                                        {
                                            onaydoc.onay = false;
                                            onaydoc.Save();
                                        }


                                    }
                                    else { onay = true; }
                                }
                                else
                                {
                                    if (allYetki.Select(x => x.id).Min() == allYetki.Select(x => x.id).Max())
                                    {
                                        onaydoc.onay = false;
                                        onaydoc.DokumanOnaylanmaTarihi = DateTime.Now;
                                        onay = false;
                                        onaydoc.Save();
                                    }
                                    else
                                    {
                                        onaydoc.onay = false;
                                        onaydoc.Save();
                                    }

                                }
                            }

                        }

                    }
                }
                else
                {
                }
                onaydoc.Save();

            }
            else
            {
                control.DokumanOlusturulmaTarihi = DateTime.Now;
                control.DokumanOlusturanKisi = OlusturanKisi;
                control.File = File;
                control.Save();
            }


        }
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Onay Durumu")]
        public bool onay;
        [ModelDefault("AllowEdit", "false")]
        [XafDisplayName("Red Durumu")]
        public bool red;
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
        [Association("UretimToParca")]
        public Parcalar parcalar;
        [ModelDefault("AllowEdit", "False")]
        public string DokumanAdi { get; set; }
        [ModelDefault("AllowEdit", "False")]
        public string dokumanUzanti { get; set; }
        [ModelDefault("AllowEdit", "False")]
        public Enums.DocumentType DokumanType { get; set; }
        [Association("UretimToRevize")]
        public XPCollection<Revizeler> revizeler
        {
            get
            {
                return GetCollection<Revizeler>(nameof(revizeler));
            }
        }
        private DateTime tarih;
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;
        [XafDisplayName("Taglar ve Etiketler")]
        [Association("TagToUretim")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }


        }
        [XafDisplayName("Web için Dokuman Adı")]
        public string WebDokumanAdi
        {
            get;
            set;
        }
        [XafDisplayName("Web'de Göster")]
        public bool Web;
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }
    }



}