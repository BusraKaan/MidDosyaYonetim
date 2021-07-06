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
using DevExpress.Persistent.Validation;
using MidDosyaYonetim.Module.BusinessObjects;
using MidDosyaYonetim.Module.Forms;



namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ResimGoruntuleme : ViewController
    {
        public ResimGoruntuleme()
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
            String urunler = Application.GetDetailViewId(typeof(Urunler));
            String urungrubu = Application.GetDetailViewId(typeof(UrunGrubu));
            String urunailesi = Application.GetDetailViewId(typeof(UrunAilesi));
            String parcalar = Application.GetDetailViewId(typeof(Parcalar));
            String aksesuarlar = Application.GetDetailViewId(typeof(Aksesuar));
            String urunserisi = Application.GetDetailViewId(typeof(UrunSerisi));
            string objectname;
            if (View.Id == urunserisi)
            {



                UrunSerisi currentobject = (UrunSerisi)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                objectname = "UrunSerisi";
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }
            if (View.Id == urunler)
            {
                objectname = "Urunler";
                Urunler currentobject = (Urunler)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }
            else if (View.Id == urungrubu)
            {
                objectname = "UrunGrubu";
                UrunGrubu currentobject = (UrunGrubu)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }
            else if (View.Id == urunailesi)
            {
                objectname = "UrunAilesi";
                UrunAilesi currentobject = (UrunAilesi)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }
            else if (View.Id == parcalar)
            {
                objectname = "Parcalar";
                Parcalar currentobject = (Parcalar)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }
            else if (View.Id == aksesuarlar)
            {
                objectname = "Aksesuar";
                Aksesuar currentobject = (Aksesuar)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                ResimGoruntulemeForm form = new ResimGoruntulemeForm(objectspace, currentobject.Oid, objectname);
                form.ShowDialog();
            }

        }
    }
}