using System;
using System.Collections;
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
using DevExpress.XtraScheduler.Native;
using MidDosyaYonetim.Module.BusinessObjects;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TopluDocOnayController : ViewController
    {
        public TopluDocOnayController()
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
            IObjectSpace space = Application.CreateObjectSpace(typeof(OnayBekleyenDokumanlar));
            IList liste = e.SelectedObjects;

            foreach(OnayBekleyenDokumanlar satir in liste)
            {
                if(satir.ObjectType == typeof(DigerDokumanlar))
                {
                    satir.ObjectType = typeof(DigerDokumanlar);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else if(satir.ObjectType == typeof(KaliteDokumanlari))
                {
                    satir.ObjectType = typeof(KaliteDokumanlari);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else if(satir.ObjectType == typeof(KlasorDokumanlari))
                {
                    satir.ObjectType = typeof(KlasorDokumanlari);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else if(satir.ObjectType == typeof(MontajKlavuzlari))
                {
                    satir.ObjectType = typeof(MontajKlavuzlari);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else if(satir.ObjectType == typeof(Sertifikalar))
                {
                    satir.ObjectType = typeof(Sertifikalar);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else if(satir.ObjectType == typeof(TeknikCizimler))
                {
                    satir.ObjectType = typeof(TeknikCizimler);
                    satir.onay = true;
                    space.CommitChanges();

                }
                else if(satir.ObjectType == typeof(UretimDokumanlari))
                {
                    satir.ObjectType = typeof(UretimDokumanlari);
                    satir.onay = true;
                    space.CommitChanges();
                }
                else
                {
                    MessageBox.Show("Dosya seçilmedi!");
                }
               
            }

        }
    }
}
