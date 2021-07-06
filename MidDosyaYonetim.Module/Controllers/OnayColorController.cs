using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class OnayColorController : ViewController
    {
        public OnayColorController()
        {
            InitializeComponent();
            //Target required Views(via the TargetXXX properties) and create their Actions.
            //SimpleAction simple = new SimpleAction(this, "", PredefinedCategory.View);
            //simple.TargetViewId = "OnayBekleyenDokumanlar_ListView";
            //simple.TargetObjectsCriteria = "Not IsNewObject(This)";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            try
            {
                Frame.GetController<AppearanceController>().CustomApplyAppearance += WinViewController1_CustomApplyAppearance;
            }
            catch (Exception r)
            {
                // Do nothing
            }



        }
        void WinViewController1_CustomApplyAppearance(object sender, ApplyAppearanceEventArgs e)
        {
            if (e.ItemType == "ViewItem" && e.ContextObjects.Length == 1 && View.SelectedObjects.Contains(e.ContextObjects[0]))
            {
                foreach (IConditionalAppearanceItem appearanceItem in e.AppearanceItems)
                {
                   
                    if (appearanceItem is AppearanceItemBackColor)
                    {
                        ((AppearanceItemBackColor)appearanceItem).BackColor = Color.Transparent;
                        
                    }
                }
            }
        }

        protected override void OnDeactivated()
        {

            base.OnDeactivated();
            try
            {
                Frame.GetController<AppearanceController>().CustomApplyAppearance -= WinViewController1_CustomApplyAppearance;
            }
            catch (Exception r)
            {
                // Do nothing
            }
        }
    }
}