
namespace MidDosyaYonetim.Module.Controllers
{
    partial class DigerDokumanFotografOlceklendirmeController
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
            this.DigerDokFotoOlceklendirmeAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // DigerDokFotoOlceklendirmeAction
            // 
            this.DigerDokFotoOlceklendirmeAction.Caption = "Diger Dok Foto Olceklendirme";
            this.DigerDokFotoOlceklendirmeAction.ConfirmationMessage = null;
            this.DigerDokFotoOlceklendirmeAction.Id = "DigerDokFotoOlceklendirme";
            this.DigerDokFotoOlceklendirmeAction.ToolTip = null;
            this.DigerDokFotoOlceklendirmeAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DigerDokFotoOlceklendirmeAction_Execute);
            // 
            // DigerDokumanFotografOlceklendirmeController
            // 
            this.Actions.Add(this.DigerDokFotoOlceklendirmeAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction DigerDokFotoOlceklendirmeAction;
    }
}
