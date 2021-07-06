using System;
using System.Collections;
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
    public partial class OzellikDegerEkle : ViewController
    {
        public OzellikDegerEkle()
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
            string detay = View.Id;

            string urunler = Application.GetDetailViewId(typeof(Urunler));
            string aksesuar = Application.GetDetailViewId(typeof(Aksesuar));

            if (detay == urunler)
            {
                Urunler currentobject = (Urunler)View.CurrentObject;
                IObjectSpace objectspace = Application.CreateObjectSpace();
                OzellikDegerForm form = new OzellikDegerForm(objectspace, detay, urunler, aksesuar, currentobject.Oid);
                CriteriaOperator cri = CriteriaOperator.Parse("[Oid]=?", currentobject.Oid);
                IList liste = ObjectSpace.GetObjects(typeof(Urunler), cri);
                if (liste.Count > 0)
                {
                    //foreach (Urunler item in liste)
                    //{
                    //    if (currentobject.Oid == item.Oid)
                    //    {
                    //        
                    //    }

                    //}
                    form.ShowDialog();

                }


            }

            else
            {
                //Urunler _currentobject;
                Aksesuar _aksesuar1 = (Aksesuar)View.CurrentObject;
                IObjectSpace _objectspace = Application.CreateObjectSpace();
                OzellikDegerForm _form = new OzellikDegerForm(_objectspace, detay, urunler, aksesuar, _aksesuar1.Oid);
                CriteriaOperator _cri = CriteriaOperator.Parse("[Oid]=?", _aksesuar1.Oid);
                IList _liste = ObjectSpace.GetObjects(typeof(Aksesuar), _cri);
                if (_liste.Count > 0)
                {
                    //foreach (Urunler item in liste)
                    //{
                    //    if (currentobject.Oid == item.Oid)
                    //    {
                    //        
                    //    }

                    //}
                    _form.ShowDialog();

                }
                else
                {
                    throw new DevExpress.ExpressApp.UserFriendlyException("Lütfen Girdiğiniz Ürünü Kaydedin.");
                }
            }

        }
    }
}
