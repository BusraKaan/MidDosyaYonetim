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

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class UrunKlonlaController : ViewController
    {
        public UrunKlonlaController()
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
            Urunler UrunlerObject = ObjectSpace.CreateObject<Urunler>();

            IList selectedUrun = e.SelectedObjects;

            List<Urunler> urunlers = new List<Urunler>();
            foreach (Urunler item in selectedUrun)
            {
                urunlers.Add(item);
            }

            Urunler urun = urunlers.FirstOrDefault();
            UrunlerObject.Aciklama = urun.Aciklama;
            UrunlerObject.AltUrunGrubu = urun.AltUrunGrubu;
            UrunlerObject.AltUrunTipi = urun.AltUrunTipi;
            UrunlerObject.AnaUrunGrubu = urun.AnaUrunGrubu;
            UrunlerObject.Barkod = urun.Barkod;
            UrunlerObject.Birim = urun.Birim;
            UrunlerObject.boyut = urun.boyut;
            UrunlerObject.IngStokAdi = urun.IngStokAdi;
            UrunlerObject.KatalogKodu = urun.KatalogKodu;
            UrunlerObject.OlusturanKisi = urun.OlusturanKisi;
            UrunlerObject.OlusturmaTarihi = DateTime.Now;
            UrunlerObject.renk = urun.renk;
            UrunlerObject.SatinAlma = urun.SatinAlma;
            UrunlerObject.Satis = urun.Satis;
            UrunlerObject.SatisAnalizGrubu = urun.SatisAnalizGrubu;
            UrunlerObject.StokAdi = urun.StokAdi;
            UrunlerObject.StokKodu = urun.StokKodu;
            UrunlerObject.Uretim = urun.Uretim;
            UrunlerObject.UrunCinsi = urun.UrunCinsi;
            UrunlerObject.UrunTuru = urun.UrunTuru;
            UrunlerObject.Web = urun.Web;
            UrunlerObject.yukseklik = urun.yukseklik;
            UrunlerObject.EngAciklama = urun.EngAciklama;
            UrunlerObject.EngWeb = urun.EngWeb;
            UrunlerObject.ilkFoto = urun.ilkFoto;
            UrunlerObject.IngStokAdi = urun.IngStokAdi;




            UrunlerObject.urunAilesi = ObjectSpace.GetObject(urun.urunAilesi);
            UrunlerObject.urunGrubu = ObjectSpace.GetObject(urun.urunGrubu);
            UrunlerObject.urunSerisi = ObjectSpace.GetObject(urun.urunSerisi);

            UrunlerObject.Save();
            ObjectSpace.CommitChanges();


            foreach (UrunDegerler item in urun.degerler)
            {
                UrunDegerler urunDegerler = ObjectSpace.CreateObject<UrunDegerler>();

                urunDegerler.Deger = item.Deger;





                urunDegerler.OlusturanKisi = item.OlusturanKisi;
                urunDegerler.OlusturmaTarihi = DateTime.Now;
                urunDegerler.degerler = ObjectSpace.GetObject(item.degerler);

                urunDegerler.urunler = ObjectSpace.GetObject(UrunlerObject);

                urunDegerler.Save();
                ObjectSpace.CommitChanges();
                
            }

        }

    }
}
