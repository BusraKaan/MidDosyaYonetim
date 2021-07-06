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
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Bcpg;

namespace MidDosyaYonetim.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CloneUrun : ViewController
    {
        public CloneUrun()
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

       

        private void CloneUrunAction_Execute(object sender, SimpleActionExecuteEventArgs e)
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
            UrunlerObject.OlusturmaTarihi = urun.OlusturmaTarihi;
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
            
            //foreach (Aksesuar aksesuar in urun.aksesuar)
            //{
            //    Aksesuar aks = ObjectSpace.CreateObject<Aksesuar>();
            //    aks.Aciklama = aksesuar.Aciklama;
            //    aks.AksesuarAdi = aksesuar.AksesuarAdi;
            //    aks.AksesuarGrubu = aksesuar.AksesuarGrubu;
            //    aks.AksesuarKodu = aksesuar.AksesuarKodu;
            //    aks.boyut = aksesuar.boyut;
            //    aks.OlusturanKisi = aksesuar.OlusturanKisi;
            //    aks.OlusturmaTarihi = aksesuar.OlusturmaTarihi;
            //    aks.Web = aksesuar.Web;


            //    aks.yukseklik = aksesuar.yukseklik;

            //    //aks.urunGrubu = aksesuar.urunGrubu;


            //    CriteriaOperator criteria = CriteriaOperator.Parse("UrunGrubuAdi == ?", aksesuar.urunGrubu.UrunGrubuAdi);





            //    //aks.urunGrubu.UrunGrubuAdi = aksesuar.urunGrubu.UrunGrubuAdi;


            //    aks.urunler = aksesuar.urunler;
                

            //    aks.urunSerisi = aksesuar.urunSerisi;
            //    //aks.urunSerisi.UrunSerisiAdi = aksesuar.urunSerisi.UrunSerisiAdi;
            //    aks.urunAilesi = aksesuar.urunAilesi;

            //    //aks.urunAilesi.UrunAilesiAdi = aksesuar.urunAilesi.UrunAilesiAdi;
            //    UrunlerObject.aksesuar.Add(aks);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();
            //}

            //foreach (var item in urun.teknikCizimler)
            //{
            //    TeknikCizimler teknikCizimler = ObjectSpace.CreateObject<TeknikCizimler>();

            //    teknikCizimler.aksesuar = item.aksesuar;
            //    teknikCizimler.OlusturanKisi = item.OlusturanKisi;
            //    teknikCizimler.OlusturmaTarihi = item.OlusturmaTarihi;
            //    teknikCizimler.onay = item.onay;
            //    teknikCizimler.parcalar = item.parcalar;
            //    teknikCizimler.red = item.red;
            //    teknikCizimler.Web = item.Web;
            //    teknikCizimler.DokumanAdi = item.DokumanAdi;
            //    teknikCizimler.DokumanType = item.DokumanType;
            //    teknikCizimler.dokumanUzanti = item.dokumanUzanti;
            //    teknikCizimler.File = item.File;
            //    teknikCizimler.urunAilesi = item.urunAilesi;
            //    teknikCizimler.urunGrubu = item.urunGrubu;
            //    teknikCizimler.urunler = item.urunler;
            //    teknikCizimler.urunSerisi = item.urunSerisi;
            //    teknikCizimler.WebDokumanAdi = item.WebDokumanAdi;
            //    UrunlerObject.teknikCizimler.Add(teknikCizimler);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.montajKlavuzlari)
            //{
            //    MontajKlavuzlari mon = ObjectSpace.CreateObject<MontajKlavuzlari>();

            //    mon.aksesuar = item.aksesuar;
            //    mon.OlusturanKisi = item.OlusturanKisi;
            //    mon.OlusturmaTarihi = item.OlusturmaTarihi;
            //    mon.onay = item.onay;
            //    mon.parcalar = item.parcalar;
            //    mon.red = item.red;
            //    mon.Web = item.Web;
            //    mon.DokumanAdi = item.DokumanAdi;
            //    mon.DokumanType = item.DokumanType;
            //    mon.dokumanUzanti = item.dokumanUzanti;
            //    mon.File = item.File;
            //    mon.urunAilesi = item.urunAilesi;
            //    mon.urunGrubu = item.urunGrubu;
            //    mon.urunler = item.urunler;
            //    mon.urunSerisi = item.urunSerisi;
            //    mon.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.montajKlavuzlari.Add(mon);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}
            //foreach (var item in urun.kapakOpsiyonlari)
            //{
            //    KapakOpsiyonlari kapakOpsiyonlari = ObjectSpace.CreateObject<KapakOpsiyonlari>();

            //    kapakOpsiyonlari.OlusturanKisi = item.OlusturanKisi;
            //    kapakOpsiyonlari.OlusturmaTarihi = item.OlusturmaTarihi;
            //    kapakOpsiyonlari.Opsiyon = item.Opsiyon;
            //    kapakOpsiyonlari.OpsiyonKodu = item.OpsiyonKodu;
            //    kapakOpsiyonlari.Web = item.Web;
            //    kapakOpsiyonlari.urunAilesi = item.urunAilesi;
            //    kapakOpsiyonlari.urunGrubu = item.urunGrubu;
            //    kapakOpsiyonlari.urunler = item.urunler;
            //    kapakOpsiyonlari.urunSerisi = item.urunSerisi;

            //    UrunlerObject.kapakOpsiyonlari.Add(kapakOpsiyonlari);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.fotograflar)
            //{
            //    Fotograflar foto = ObjectSpace.CreateObject<Fotograflar>();

            //    foto.aksesuar = item.aksesuar;
            //    foto.Content = item.Content;
            //    foto.kapakOpsiyonlari = item.kapakOpsiyonlari;
            //    foto.OlusturanKisi = item.OlusturanKisi;
            //    foto.OlusturmaTarihi = item.OlusturmaTarihi;
            //    foto.parcalar = item.parcalar;
            //    foto.Web = item.Web;
            //    foto.File = item.File;
            //    foto.fotograf = item.fotograf;
            //    foto.urunAilesi = item.urunAilesi;
            //    foto.urunGrubu = item.urunGrubu;
            //    foto.urunler = item.urunler;
            //    foto.urunSerisi = item.urunSerisi;

            //    UrunlerObject.fotograflar.Add(foto);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();


            //}
            //foreach (var item in urun.kaliteDokumanlari)
            //{
            //    KaliteDokumanlari kaliteDokumanlari = ObjectSpace.CreateObject<KaliteDokumanlari>();

            //    kaliteDokumanlari.aksesuar = item.aksesuar;
            //    kaliteDokumanlari.OlusturanKisi = item.OlusturanKisi;
            //    kaliteDokumanlari.OlusturmaTarihi = item.OlusturmaTarihi;
            //    kaliteDokumanlari.onay = item.onay;
            //    kaliteDokumanlari.parcalar = item.parcalar;
            //    kaliteDokumanlari.red = item.red;
            //    kaliteDokumanlari.Web = item.Web;
            //    kaliteDokumanlari.DokumanAdi = item.DokumanAdi;
            //    kaliteDokumanlari.DokumanType = item.DokumanType;
            //    kaliteDokumanlari.dokumanUzanti = item.dokumanUzanti;
            //    kaliteDokumanlari.File = item.File;
            //    kaliteDokumanlari.fotograf = item.fotograf;
            //    kaliteDokumanlari.urunAilesi = item.urunAilesi;
            //    kaliteDokumanlari.urunGrubu = item.urunGrubu;
            //    kaliteDokumanlari.urunler = item.urunler;
            //    kaliteDokumanlari.urunSerisi = item.urunSerisi;
            //    kaliteDokumanlari.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.kaliteDokumanlari.Add(kaliteDokumanlari);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.sertifikaDokumanlari)
            //{
            //    Sertifikalar sertifikalar = ObjectSpace.CreateObject<Sertifikalar>();

            //    sertifikalar.aksesuar = item.aksesuar;
            //    sertifikalar.OlusturanKisi = item.OlusturanKisi;
            //    sertifikalar.OlusturmaTarihi = item.OlusturmaTarihi;
            //    sertifikalar.onay = item.onay;
            //    sertifikalar.parcalar = item.parcalar;
            //    sertifikalar.red = item.red;
            //    sertifikalar.Web = item.Web;
            //    sertifikalar.DokumanAdi = item.DokumanAdi;
            //    sertifikalar.DokumanType = item.DokumanType;
            //    sertifikalar.dokumanUzanti = item.dokumanUzanti;
            //    sertifikalar.File = item.File;
            //    sertifikalar.fotograf = item.fotograf;
            //    sertifikalar.urunAilesi = item.urunAilesi;
            //    sertifikalar.urunGrubu = item.urunGrubu;
            //    sertifikalar.urunler = item.urunler;
            //    sertifikalar.urunSerisi = item.urunSerisi;
            //    sertifikalar.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.sertifikaDokumanlari.Add(sertifikalar);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.uretimDokumanlari)
            //{
            //    UretimDokumanlari uretimDokumanlari = ObjectSpace.CreateObject<UretimDokumanlari>();

            //    uretimDokumanlari.aksesuar = item.aksesuar;
            //    uretimDokumanlari.OlusturanKisi = item.OlusturanKisi;
            //    uretimDokumanlari.OlusturmaTarihi = item.OlusturmaTarihi;
            //    uretimDokumanlari.onay = item.onay;
            //    uretimDokumanlari.parcalar = item.parcalar;
            //    uretimDokumanlari.red = item.red;
            //    uretimDokumanlari.Web = item.Web;
            //    uretimDokumanlari.DokumanAdi = item.DokumanAdi;
            //    uretimDokumanlari.DokumanType = item.DokumanType;
            //    uretimDokumanlari.dokumanUzanti = item.dokumanUzanti;
            //    uretimDokumanlari.File = item.File;
            //    uretimDokumanlari.urunAilesi = item.urunAilesi;
            //    uretimDokumanlari.urunGrubu = item.urunGrubu;
            //    uretimDokumanlari.urunler = item.urunler;
            //    uretimDokumanlari.urunSerisi = item.urunSerisi;
            //    uretimDokumanlari.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.uretimDokumanlari.Add(uretimDokumanlari);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.teknikSartname)
            //{
            //    TeknikSartname teknik = ObjectSpace.CreateObject<TeknikSartname>();

            //    teknik.aksesuar = item.aksesuar;
            //    teknik.OlusturanKisi = item.OlusturanKisi;
            //    teknik.OlusturmaTarihi = item.OlusturmaTarihi;
            //    teknik.onay = item.onay;
            //    teknik.red = item.red;
            //    teknik.Web = item.Web;
            //    teknik.DokumanAdi = item.DokumanAdi;
            //    teknik.DokumanType = item.DokumanType;
            //    teknik.dokumanUzanti = item.dokumanUzanti;
            //    teknik.File = item.File;
            //    teknik.urunAilesi = item.urunAilesi;
            //    teknik.urunGrubu = item.urunGrubu;
            //    teknik.urunler = item.urunler;
            //    teknik.urunSerisi = item.urunSerisi;
            //    teknik.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.teknikSartname.Add(teknik);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}

            //foreach (var item in urun.katalog)
            //{
            //    Kataloglar kataloglar = ObjectSpace.CreateObject<Kataloglar>();

            //    kataloglar.KatalogAdi = item.KatalogAdi;
            //    kataloglar.fotograf = item.fotograf;
            //    kataloglar.File = item.File;
            //    kataloglar.urunAilesi = item.urunAilesi;
            //    kataloglar.urunGrubu = item.urunGrubu;
            //    kataloglar.urunler = item.urunler;
            //    kataloglar.urunSerisi = item.urunSerisi;
            //    kataloglar.WebDokumanAdi = item.WebDokumanAdi;

            //    UrunlerObject.katalog.Add(kataloglar);
            //    UrunlerObject.Save();
            //    ObjectSpace.CommitChanges();

            //}
            UrunlerObject.Save();
            ObjectSpace.CommitChanges();



            //foreach(Urunler item in selectedUrun)
            //{
            //    urunlers.Add
            //}
        }
    }
}
