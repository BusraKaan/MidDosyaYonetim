using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using DevExpress.Xpo;
using DevExpress.Xpo.Exceptions;
using DevExpress.XtraRichEdit.Mouse;
using DevExpress.XtraScheduler;
using MidDosyaYonetim.Module.BusinessObjects;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RevizeOlustur : ViewController
    {


        public RevizeOlustur()
        {
            PopupWindowShowAction showDetailViewAction = new PopupWindowShowAction(this, "Döküman Güncelle", PredefinedCategory.View);
            //showDetailViewAction.TargetObjectType = typeof(Dokumanlar);
            showDetailViewAction.TargetViewType = ViewType.DetailView;
            showDetailViewAction.TargetObjectsCriteria = "Not IsNewObject(This)";
            showDetailViewAction.CustomizePopupWindowParams += showDetailViewAction_CustomizePopupWindowParams;
            showDetailViewAction.ImageName = "BO_Audit_ChangeHistory";
            showDetailViewAction.Execute += Action_Execute;


        }

        private void showDetailViewAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            SchedulerStorage schedulerStorage1 = new SchedulerStorage();

            IObjectSpace newObjectSpace = Application.CreateObjectSpace(typeof(Revizeler));
            Object objectToShow = newObjectSpace.CreateObject<Revizeler>();
            DetailView createdView = Application.CreateDetailView(newObjectSpace, objectToShow);
            createdView.ViewEditMode = ViewEditMode.Edit;

            e.View = createdView;

            String Teknik = Application.GetDetailViewId(typeof(TeknikCizimler));
            String montaj = Application.GetDetailViewId(typeof(MontajKlavuzlari));
            String Uretimdoc = Application.GetDetailViewId(typeof(UretimDokumanlari));
            String KlasorDoc = Application.GetDetailViewId(typeof(KlasorDokumanlari));
            String kalitedoc = Application.GetDetailViewId(typeof(KaliteDokumanlari));
            String sertifikadoc = Application.GetDetailViewId(typeof(Sertifikalar));
            String digerDoc = Application.GetDetailViewId(typeof(DigerDokumanlar));

            if (View.Id == Teknik)
            {
                TeknikCizimler dokuman = View.CurrentObject as TeknikCizimler;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.teknikCizimler = revize.Session.GetObjectByKey<TeknikCizimler>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;




            }
            else if (View.Id == montaj)
            {
                MontajKlavuzlari dokuman = View.CurrentObject as MontajKlavuzlari;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.MontajKlavuzlari = revize.Session.GetObjectByKey<MontajKlavuzlari>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;
               
           
        }
            else if (View.Id == Uretimdoc)
            {
                UretimDokumanlari dokuman = View.CurrentObject as UretimDokumanlari;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.uretimDokumanlari = revize.Session.GetObjectByKey<UretimDokumanlari>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;

            }
            else if (View.Id == KlasorDoc)
            {
                KlasorDokumanlari dokuman = View.CurrentObject as KlasorDokumanlari;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.klasorDokumanlari = revize.Session.GetObjectByKey<KlasorDokumanlari>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;

            }
            else if (View.Id == kalitedoc)
            {
                KaliteDokumanlari dokuman = View.CurrentObject as KaliteDokumanlari;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.kaliteDokumanlari = revize.Session.GetObjectByKey<KaliteDokumanlari>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;

            }
            else if (View.Id == sertifikadoc)
            {
                Sertifikalar dokuman = View.CurrentObject as Sertifikalar;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.sertifikaDokumanlari = revize.Session.GetObjectByKey<Sertifikalar>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;



            }
            else if(View.Id == digerDoc)
            {
                DigerDokumanlar dokuman = View.CurrentObject as DigerDokumanlar;
                Revizeler revize = e.View.CurrentObject as Revizeler;
                FileData currentFile = dokuman.File;
                FileData fileCopy = newObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();
                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                newObjectSpace.CommitChanges();
                revize.RevizeDokuman = revize.Session.GetObjectByKey<FileData>(fileCopy.Oid);
                revize.digerDokumanlar = revize.Session.GetObjectByKey<DigerDokumanlar>(dokuman.Oid);
                revize.RevizeTarihi = DateTime.Now;

            }



        }
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


        private void Action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Revizeler revize = e.PopupWindowView.CurrentObject as Revizeler;
            //Dokumanlar dokuman = View.CurrentObject as Dokumanlar;
            String Teknik = Application.GetDetailViewId(typeof(TeknikCizimler));
            String montaj = Application.GetDetailViewId(typeof(MontajKlavuzlari));
            String Uretimdoc = Application.GetDetailViewId(typeof(UretimDokumanlari));
            String KlasorDoc = Application.GetDetailViewId(typeof(KlasorDokumanlari));
            String kalitedoc = Application.GetDetailViewId(typeof(KaliteDokumanlari));
            String sertifikadoc = Application.GetDetailViewId(typeof(Sertifikalar));
            String digerDoc = Application.GetDetailViewId(typeof(DigerDokumanlar));
            String sartname = Application.GetDetailViewId(typeof(TeknikSartname));

            IList mailList = ObjectSpace.GetObjects(typeof(EmailListesi));
            

            if (View.Id == Teknik)
            {
                TeknikCizimler dokuman = View.CurrentObject as TeknikCizimler;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(TeknikCizimler), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[teknikCizimler].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);


                string revizeAdi = "_" + (revList.Count + 1).ToString();

                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();



                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = "REV_" + words[0] + revizeAdi + "." + words[1];

                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                dokuman.File = fileCopy;
                dokuman.Save();


                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }

            }
            else if (View.Id == montaj)
            {
                MontajKlavuzlari dokuman = View.CurrentObject as MontajKlavuzlari;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(MontajKlavuzlari), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[MontajKlavuzlari].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();



                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();


                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }
            }
            else if (View.Id == Uretimdoc)
            {
                UretimDokumanlari dokuman = View.CurrentObject as UretimDokumanlari;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(UretimDokumanlari), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[uretimDokumanlari].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();


                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }
            }
            else if (View.Id == KlasorDoc)
            {
                KlasorDokumanlari dokuman = View.CurrentObject as KlasorDokumanlari;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(KlasorDokumanlari), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[klasorDokumanlari].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;
                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();



                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }
            }
            else if (View.Id == kalitedoc)
            {
                KaliteDokumanlari dokuman = View.CurrentObject as KaliteDokumanlari;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(KaliteDokumanlari), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[kaliteDokumanlari].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;
                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();


                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }
            }
            else if (View.Id == sertifikadoc)
            {
                Sertifikalar dokuman = View.CurrentObject as Sertifikalar;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(Sertifikalar), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[sertifikaDokumanlari].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();



                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }
            }
            else if(View.Id == digerDoc)
            {
                DigerDokumanlar dokuman = View.CurrentObject as DigerDokumanlar;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(DigerDokumanlar), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[digerDokumanlar].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();



                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }

            }
            else if (View.Id == sartname)
            {
                TeknikSartname dokuman = View.CurrentObject as TeknikSartname;
                CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokuman.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(TeknikSartname), criteria);


                CriteriaOperator cri = CriteriaOperator.Parse("[teknikSartname].[Oid]=? ", dokuman.Oid);
                IList revList = ObjectSpace.GetObjects(typeof(Revizeler), cri);



                string revizeAdi = "_" + (revList.Count + 1).ToString();


                FileData currentFile = revize.YeniDokuman;


                FileData fileCopy = ObjectSpace.CreateObject<FileData>();
                var stream = new MemoryStream();



                string[] words = currentFile.FileName.Split('.');
                currentFile.FileName = words[0] + revizeAdi + "." + words[1];


                currentFile.SaveToStream(stream);
                stream.Position = 0;
                fileCopy.LoadFromStream(currentFile.FileName, stream);
                ObjectSpace.CommitChanges();
                dokuman.File = fileCopy;
                dokuman.Save();

                foreach (EmailListesi satir in mailList)
                {

                    if (satir != null && satir.Aktif == true)
                    {

                        var mailMessage = new System.Net.Mail.MailMessage();

                        mailMessage.To.Add(satir.Email);
                        mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                        mailMessage.Body = revize.RevizeDokuman.FileName + "  " + "isimli dokuman dosyası" + " " + revize.RevizeEdenKisi + " " + "tarafından" +
                         " " + revize.RevizeTarihi + " " + "tarihinde" + " " + revize.YeniDokuman.FileName + " " + "olarak" + " " + "revize edilmiştir.";
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

                        MessageOptions options = new MessageOptions();
                        options.Duration = 2000;
                        options.Message = "İşlem Başarılı!";
                        options.Web.Position = InformationPosition.Bottom;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "";
                        options.OkDelegate = OkDelegate;

                        Application.ShowViewStrategy.ShowMessage(options);

                    }
                }

            }

        }

        private void OkDelegate()
        {
           
        }


        protected override void OnActivated()
        {

            base.OnActivated();

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {

            base.OnDeactivated();
        }

    }
}