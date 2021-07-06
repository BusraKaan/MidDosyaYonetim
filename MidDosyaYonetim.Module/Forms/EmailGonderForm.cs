using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.ExpressApp.Win.Templates.Utils;
using DevExpress.Images;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using MidDosyaYonetim.Module.BusinessObjects;

namespace MidDosyaYonetim.Module.Forms
{
    public partial class EmailGonderForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {    
        private IObjectSpace objectSpace;
        private string EskiDoc, YeniDoc, Kisi, Tarih;
        
        public EmailGonderForm(IObjectSpace objectSpace, Revizeler currentobject)
        {
            InitializeComponent();
            progressBar1.Visible = false;
          
            this.objectSpace = objectSpace;

            EskiDoc = currentobject.RevizeDokuman.FileName;
            Kisi = currentobject.RevizeEdenKisi;
            Tarih = currentobject.RevizeTarihi.ToString();

            dataGridView1.Columns.Add("Personel Mail Adresi", "Personel Mail Adresi");            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            IList userListe = objectSpace.GetObjects(typeof(UserMid2));
            foreach (UserMid2 satir in userListe)
            {
                if(satir.email != null)
                {
                    dataGridView1.Rows.Add(satir.email);
                }
             
            }

            var deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "dataGridViewDeleteButton";
            deleteButton.HeaderText = "Listeden Kaldır";           
            //deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            this.dataGridView1.Columns.Add(deleteButton);


            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellClick += dataGridView_CellClick;
       

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dataGridView1.Columns["dataGridViewDeleteButton"].Index)
            {
                var image = Properties.Resources.delete_icon; //An image

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete_icon.Width;
                var h = Properties.Resources.delete_icon.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(image, new System.Drawing.Rectangle(x, y, w, h));
                e.Handled = true;

            }
        }

        void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;

            //Check if click is on specific column 
            if (e.ColumnIndex == dataGridView1.Columns["dataGridViewDeleteButton"].Index)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);                
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;

            int i = dataGridView1.Columns.Count;
            int temp = 100 / i;

            foreach (DataGridViewRow satir in dataGridView1.Rows)
            {
                if (satir.Cells["Personel Mail Adresi"].Value != null)
                {
                    var mailMessage = new System.Net.Mail.MailMessage();

                    mailMessage.To.Add(satir.Cells["Personel Mail Adresi"].Value.ToString());
                    mailMessage.Subject = "Bu bir bilgilendirme mailidir.";
                    mailMessage.Body = EskiDoc + "  " + "isimli dokuman dosyası" + " " + Kisi + " " + "tarafından" +
                     " " + Tarih + " " + "tarihinde" + " " + "revize edilmiştir.";
                    mailMessage.From =
                    new System.Net.Mail.MailAddress("ldms@lande.com.tr");
                    var smtp = new System.Net.Mail.SmtpClient();

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials =
                    new System.Net.NetworkCredential(
                    "ldms@lande.com.tr", "ldms2020!");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);

                }
                progressBar1.Value += temp;
                if (progressBar1.Value == 100)
                {
                    MessageBox.Show("Mail gönderildi!");
                    Close();
                }
               
            }

        }
       
    }

            
    }        

    

