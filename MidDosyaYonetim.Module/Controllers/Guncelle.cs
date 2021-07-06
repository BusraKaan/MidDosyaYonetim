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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Guncelle : ViewController
    {
        public Guncelle()
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

        private void GuncelleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            char[] oldValue = new char[] { 'ö', 'ü', 'ç', 'ı', 'ğ', 'ş' };
            char[] newValue = new char[] { 'o', 'u', 'c', 'i', 'g', 's' };

            IObjectSpace objectSpace = Application.CreateObjectSpace();
            //CriteriaOperator criteria = CriteriaOperator.Parse("Oid='CD5FC369-2F9C-450F-A700-0627808DAA02'");
            IList aksesuars = objectSpace.GetObjects(typeof(TeknikSartname));
            foreach (TeknikSartname item in aksesuars)
            {
                if (item.File.FileName != null)
                {
                    string temp = item.File.FileName;
                    temp = temp.ToLower();
                    temp = temp.Trim();
                    for (int sayac = 0; sayac < oldValue.Length; sayac++)
                    {
                        temp = temp.Replace(oldValue[sayac], newValue[sayac]);
                    }
                    temp = temp.Replace(" ", "_");
                    temp = temp.Replace(":", "æ");
                    temp = temp.Replace("---", "-");
                    temp = temp.Replace("?", "");
                    temp = temp.Replace("/", "");
                    temp = temp.Replace(".", "");
                    temp = temp.Replace("'", "");
                    temp = temp.Replace("#", "");
                    temp = temp.Replace("%", "");
                    temp = temp.Replace("&", "");
                    temp = temp.Replace("*", "");
                    temp = temp.Replace("!", "");
                    temp = temp.Replace("@", "");
                    temp = temp.Replace("+", "");
                    item.WebUrl = temp;
                }
                if (item.File.FileName != null)
                {
                    string EngTemp = item.File.FileName;
                    EngTemp = EngTemp.ToLower();
                    EngTemp = EngTemp.Trim();
                    for (int sayac = 0; sayac < oldValue.Length; sayac++)
                    {
                        EngTemp = EngTemp.Replace(oldValue[sayac], newValue[sayac]);
                    }
                    EngTemp = EngTemp.Replace(" ", "_");
                    EngTemp = EngTemp.Replace(":", "æ");
                    EngTemp = EngTemp.Replace("---", "-");
                    EngTemp = EngTemp.Replace("?", "");
                    EngTemp = EngTemp.Replace("/", "");
                    EngTemp = EngTemp.Replace(".", "");
                    EngTemp = EngTemp.Replace("'", "");
                    EngTemp = EngTemp.Replace("#", "");
                    EngTemp = EngTemp.Replace("%", "");
                    EngTemp = EngTemp.Replace("&", "");
                    EngTemp = EngTemp.Replace("*", "");
                    EngTemp = EngTemp.Replace("!", "");
                    EngTemp = EngTemp.Replace("@", "");
                    EngTemp = EngTemp.Replace("+", "");
                    item.EngWebUrl = EngTemp;
                }
                item.Save();
                objectSpace.CommitChanges();
            }

        }
    }
}
