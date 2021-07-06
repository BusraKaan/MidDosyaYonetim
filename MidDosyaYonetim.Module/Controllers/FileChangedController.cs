using System;
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
using DevExpress.Persistent.Validation;
using MidDosyaYonetim.Module.BusinessObjects;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FileChangedController : ViewController<DetailView>
    {
        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            String Teknik = Application.GetDetailViewId(typeof(TeknikCizimler));
            String montaj = Application.GetDetailViewId(typeof(MontajKlavuzlari));
            String Uretimdoc = Application.GetDetailViewId(typeof(UretimDokumanlari));
            String klasorDoc = Application.GetDetailViewId(typeof(KlasorDokumanlari));
            String kalitedoc = Application.GetDetailViewId(typeof(KaliteDokumanlari));
            String sertifikadoc = Application.GetDetailViewId(typeof(Sertifikalar));
            if (View.Id == Teknik)
            {
                TeknikCizimler doc = View.CurrentObject as TeknikCizimler;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }

            }
            else if (View.Id == montaj)
            {
                MontajKlavuzlari doc= View.CurrentObject as MontajKlavuzlari;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }
            }
            else if (View.Id == Uretimdoc)
            {
                UretimDokumanlari doc = View.CurrentObject as UretimDokumanlari;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }
            }
            else if (View.Id == klasorDoc)
            {
                KlasorDokumanlari doc = View.CurrentObject as KlasorDokumanlari;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }
            }
            else if (View.Id == kalitedoc)
            {
                KaliteDokumanlari doc = View.CurrentObject as KaliteDokumanlari;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }
            }
            else if (View.Id == sertifikadoc)
            {
                Sertifikalar doc = View.CurrentObject as Sertifikalar;
                if (e.Object == doc.File)
                {
                    doc.DokumanAdi = doc.File.FileName;
                    doc.DokumanType = dok_type(Path.GetExtension(doc.File.FileName));
                    doc.dokumanUzanti = Path.GetExtension(doc.File.FileName).Replace(".", "");
                    View.FindItem("DokumanAdi").Refresh();
                    View.FindItem("DokumanType").Refresh();
                    View.FindItem("dokumanUzanti").Refresh();
                }
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
        public FileChangedController()
        {
            this.TargetObjectType = typeof(MontajKlavuzlari);
            this.TargetObjectType = typeof(TeknikCizimler);
            this.TargetObjectType = typeof(Sertifikalar);
            this.TargetObjectType = typeof(UretimDokumanlari);
            this.TargetObjectType = typeof(KaliteDokumanlari);
            this.TargetObjectType = typeof(KlasorDokumanlari );

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
            base.OnDeactivated();
        }
    }
}
