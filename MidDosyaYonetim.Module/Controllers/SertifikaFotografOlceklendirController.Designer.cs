
namespace MidDosyaYonetim.Module.Controllers
{
    partial class SertifikaFotografOlceklendirController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SerrifikaFotoOlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SerrifikaFotoOlceklendirAction
            // 
            this.SerrifikaFotoOlceklendirAction.Caption = "Sertifika Foto Olceklendir";
            this.SerrifikaFotoOlceklendirAction.ConfirmationMessage = null;
            this.SerrifikaFotoOlceklendirAction.Id = "SertifikaFotoOlceklendir";
            this.SerrifikaFotoOlceklendirAction.ToolTip = null;
            this.SerrifikaFotoOlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SerrifikaFotoOlceklendirAction_Execute);
            // 
            // SertifikaFotografOlceklendirController
            // 
            this.Actions.Add(this.SerrifikaFotoOlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction SerrifikaFotoOlceklendirAction;
    }
}
