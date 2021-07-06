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
    public partial class DokumanGuncelle : ViewController
    {
        public DokumanGuncelle()
        {
            SimpleAction showWindowAction = new SimpleAction(this, "Döküman Güncelle", DevExpress.Persistent.Base.PredefinedCategory.View);
            showWindowAction.ImageName = "BO_Audit_ChangeHistory";
            showWindowAction.TargetObjectType = typeof(Dokumanlar);
            showWindowAction.TargetViewType = ViewType.DetailView;
            showWindowAction.Execute += ShowWindowAction_Execute;
        }

        private void ShowWindowAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Dokumanlar dokuman = (Dokumanlar)View.CurrentObject;
            IObjectSpace space = Application.CreateObjectSpace();
            DokumanGuncelleForm form = new DokumanGuncelleForm(space, dokuman.Oid);
            form.ShowDialog();
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

        private void dokumanGuncelleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Dokumanlar dokuman = (Dokumanlar)View.CurrentObject;
            IObjectSpace space = Application.CreateObjectSpace();
            DokumanGuncelleForm form = new DokumanGuncelleForm(space, dokuman.Oid);
            form.ShowDialog();
        }
    }
}

