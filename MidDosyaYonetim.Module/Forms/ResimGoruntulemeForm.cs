using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using MidDosyaYonetim.Module.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MidDosyaYonetim.Module.Forms
{
    public partial class ResimGoruntulemeForm : Form
    {
        private IObjectSpace objectspace;
        private Guid oid;
        private String objectname;
        CriteriaOperator criteria;
        public ResimGoruntulemeForm(IObjectSpace objectspace, Guid oid, String objectname)
        {
            InitializeComponent();
            this.objectspace = objectspace;
            this.oid = oid;
            this.BackColor = Color.White;
            this.objectname = objectname;
            //this.FormBorderStyle = FormBorderStyle.None;


            getAllimage();
        }



        public void getAllimage()
        {
            int i = 0;
            if (objectname == "Urunler")
            {
                criteria = CriteriaOperator.Parse("[urunler].[Oid]=?", oid);
            }
            else if (objectname == "UrunSerisi")
            {
                criteria = CriteriaOperator.Parse("[urunSerisi].[Oid]=?", oid);
            }
            else if (objectname == "UrunGrubu")
            {
                criteria = CriteriaOperator.Parse("[urunGrubu].[Oid]=?", oid);
            }
            else if (objectname == "UrunAilesi")
            {
                criteria = CriteriaOperator.Parse("[urunAilesi].[Oid]=?", oid);
            }
            else if (objectname == "Parcalar")
            {
                criteria = CriteriaOperator.Parse("[parcalar].[Oid]=?", oid);
            }
            else if (objectname == "Aksesuar")
            {
                criteria = CriteriaOperator.Parse("[aksesuar].[Oid]=?", oid);
            }
            IList liste = objectspace.GetObjects(typeof(Fotograflar), criteria);



            foreach (Fotograflar satir in liste)
            {
                byte[] images = satir.fotograf;
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(images));
                imageSlider1.Images.Add(x);
                i++;
            }
            if (i > 0) { label1.Text = i + " Fotoğraf görüntüleniyor."; }
            else { label1.Text = "Hiç fotoğraf bulunamadı."; }
            i = 0;
        }

       
    }
}