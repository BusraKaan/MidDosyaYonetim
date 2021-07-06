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
using System.Collections;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo.DB;
using PdfSharp.Drawing;
using IronOcr;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class KlasorDokumanlari : FileAttachmentBase, IObjectSpaceLink
    {

        public KlasorDokumanlari(Session session)
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
                //user = (UserMid2) SecuritySystem.CurrentUser;
                //UserRole = user.Roles.
                OlusturanKisi = olusturanKisi.ToString();
            }
        }


        protected override void OnSaving()
        {
            DokumanAdi = File.FileName;
            dokumanUzanti = Path.GetExtension(File.FileName).Replace(".", "");
            DokumanType = dok_type(Path.GetExtension(File.FileName));


            AddOnay();
            base.OnSaving();
        }
        private void AddOnay()
        {
            CriteriaOperator cri = CriteriaOperator.Parse("[FileOid] =?", Oid);
            OnayBekleyenDokumanlar control = objectSpace.FindObject<OnayBekleyenDokumanlar>(cri);

            if (control == null)
            {

                List<FileData> allFile = new List<FileData>();
                List<Yetkilendirme> allYetki = new List<Yetkilendirme>();

                IList listyetkiler = objectSpace.GetObjects(typeof(Yetkilendirme));
                foreach (Yetkilendirme satir in listyetkiler)
                {
                    if (satir.ObjectType == typeof(KlasorDokumanlari))
                    {
                        allYetki.Add(satir);
                    }

                }
                //if (onaydoc.UserMid2 == null)
                //{
                int minid = allYetki.Select(x => x.id).Min();
                foreach (Yetkilendirme satir in allYetki)
                {
                    if (allYetki.Select(x => x.id).Contains(minid))
                    {
                        if (satir.id == minid)
                        {
                            OnayBekleyenDokumanlar onaydoc = objectSpace.CreateObject<OnayBekleyenDokumanlar>();
                            onaydoc.DokumanOlusturulmaTarihi = DateTime.Now;
                            onaydoc.DokumanOlusturanKisi = OlusturanKisi;
                            //onaydoc.onay = false;
                            onaydoc.red = false;
                            onaydoc.File = File;
                            onaydoc.ObjectType = typeof(KlasorDokumanlari);
                            onaydoc.UserMid2 = satir.departmanlar.DepartmanMuduru;
                            onaydoc.yetkilendirme = satir;
                            onaydoc.FileOid = Oid;

                            if (satir.yetkili.ToString() == OlusturanKisi)
                            {
                                onaydoc.onay = true;
                                onaydoc.DokumanOnaylanmaTarihi = DateTime.Now;
                                onaydoc.Save();
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
                                            onaydoc2.ObjectType = typeof(KlasorDokumanlari);
                                            onaydoc2.UserMid2 = satir2.departmanlar.DepartmanMuduru;
                                            onaydoc2.yetkilendirme = satir2;
                                            onaydoc2.FileOid = Oid;
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
                control.DokumanOlusturulmaTarihi = DateTime.Now;
                control.DokumanOlusturanKisi = OlusturanKisi;
                control.File = File;
                control.Save();
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
        static PermissionPolicyRole role;
        protected override void OnSaved()
        {

            CriteriaOperator criteria = CriteriaOperator.Parse("[UserName] =?", OlusturanKisi);
            PermissionPolicyUser user = objectSpace.FindObject(typeof(PermissionPolicyUser), criteria) as PermissionPolicyUser;

            if (onay == true && user != null && !user.IsUserInRole("kalite"))
            {

                IList mailList = objectSpace.GetObjects(typeof(EmailListesi));
                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = File.FileName + "  " + "isimli dokuman dosyası" + " " + OlusturanKisi + " " + "tarafından" +
                         " " + OlusturmaTarihi + " " + "tarihinde" + " " + klasorler.KlasorYolu + " " + "klasörü içine eklenmiştir.";
                        mailMessage.From =
                        new System.Net.Mail.MailAddress("ldms@lande.com.tr");
                        var smtp = new System.Net.Mail.SmtpClient();

                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Credentials =
                        new System.Net.NetworkCredential(
                        "ldms@lande.com.tr", "ldms2020!");
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);

                    }
                }

            }

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
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { if (value != null) objectSpace = value; }
        }
        public string DokumanAdi { get; set; }
        [ModelDefault("AllowEdit", "False")]
        public string dokumanUzanti { get; set; }
        [ModelDefault("AllowEdit", "False")]
        public Enums.DocumentType DokumanType { get; set; }
        [Association("klasorToRevize")]
        public XPCollection<Revizeler> revizeler
        {
            get
            {
                return GetCollection<Revizeler>(nameof(revizeler));
            }

        }
        [ModelDefault("DisplayFormat", "{0:dd.MM.yyyy HH:mm}")]
        [XafDisplayName("Oluşturulma Tarihi")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime OlusturmaTarihi;
        [XafDisplayName("Oluşturan Kişi")]
        [ModelDefault("AllowEdit", "False")]
        public String OlusturanKisi;

        //[VisibleInDetailView(false)]
        //[VisibleInListView(false)]
        //public bool SendMail;

        [XafDisplayName("Taglar ve Etiketler")]
        [Association("KlasorToTag")]
        public XPCollection<Taglar> taglar
        {
            get { return GetCollection<Taglar>(nameof(taglar)); }

        }

        [XafDisplayName("Klasör")]
        [Association("KlasorToDokuman")]
        public Klasorler klasorler;
    }
}