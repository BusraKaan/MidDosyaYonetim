using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
using DevExpress.XtraLayout;
using MidDosyaYonetim.Module.BusinessObjects;
using MidDosyaYonetim.Module.Forms;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class KaliteEkleController : ViewController
    {
        public KaliteEkleController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
           //Application.CustomizeTemplate += Application_CustomizeTemplate;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            //Application.CustomizeTemplate -= Application_CustomizeTemplate;
            base.OnDeactivated();
            
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IObjectSpace objectSpace = Application.CreateObjectSpace();           

            var space = Application.CreateObjectSpace(typeof(KaliteFile));
            LayoutControl layoutControl = new LayoutControl();
            layoutControl.Dock = System.Windows.Forms.DockStyle.Top;

            Object objectToShow = space.CreateObject(typeof(KaliteFile));
            DetailView createdView = Application.CreateDetailView(space, objectToShow);
            createdView.ViewEditMode = ViewEditMode.Edit;

            KaliteEkleForm form = new KaliteEkleForm(objectSpace,createdView);
            
            form.Controls.Add(layoutControl);

          
            Frame frame = Application.CreateFrame(TemplateContext.NestedFrame);           
            frame.CreateTemplate();            
            frame.SetView(createdView);
            var item2 = new LayoutControlItem();
            item2.Parent = layoutControl.Root;
            item2.Text = " ";
            item2.Control = (Control)frame.Template;


            form.ShowDialog();
        }
       
    }
}
