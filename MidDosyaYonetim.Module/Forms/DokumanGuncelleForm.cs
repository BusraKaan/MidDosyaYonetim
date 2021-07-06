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
    public partial class DokumanGuncelleForm : Form
    {
        private IObjectSpace space;
        private Guid dokumanoid;
        public DokumanGuncelleForm(IObjectSpace space, Guid dokumanoid)
        {
            InitializeComponent();
            this.space = space;
            this.dokumanoid = dokumanoid;
            CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokumanoid);
            IList liste = space.GetObjects(typeof(Dokumanlar), criteria);

            foreach (Dokumanlar satir in liste)
            {
                textBox5.Text = satir.File.FileName;
            }


            textBox3.Text = DateTime.Today.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdlg = new OpenFileDialog();

            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(ofdlg.FileName);
                    //FileData fileData = new FileData(ofdlg.InitialDirectory);
                    textBox4.Text = ofdlg.FileName;
                   // FileData fileData = ofdlg.;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Dosya yüklenemedi.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Create(textBox4.Text);
            CriteriaOperator criteria = CriteriaOperator.Parse("[Oid]=?", dokumanoid);
            IList liste = space.GetObjects(typeof(Dokumanlar), criteria);

            foreach (Dokumanlar satir in liste)
            {
                Revizeler rvz = space.CreateObject<Revizeler>();
                rvz.RevizeEdenKisi = textBox1.Text;
                rvz.RevizeNedeni = textBox2.Text;
                rvz.RevizeTarihi = Convert.ToDateTime(textBox3.Text);
                //rvz.YeniDokuman = 
               // rvz.dokumanlar = satir;
                rvz.RevizeDokuman = satir.File;
                //rvz.YeniDokuman = textBox4.Text;
                rvz.Save();
                //dokuman.Save();
                satir.DokumanAdi = textBox4.Text;
                satir.Save();
                space.CommitChanges();
            }
           

            //doc.File = file;

            //dokuman = dokuman.Session.GetObjectByKey<Dokumanlar>(rvz.dokumanlar.Oid);
           
            //doc.Save();
            
            Close();


        }
    }
}
