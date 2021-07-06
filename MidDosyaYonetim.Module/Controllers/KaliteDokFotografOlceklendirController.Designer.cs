
namespace MidDosyaYonetim.Module.Controllers
{
    partial class KaliteDokFotografOlceklendirController
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
            this.KaliteDokFotoOlceklendirmeAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // KaliteDokFotoOlceklendirmeAction
            // 
            this.KaliteDokFotoOlceklendirmeAction.Caption = "Kalite Dok Foto Olceklendirme";
            this.KaliteDokFotoOlceklendirmeAction.ConfirmationMessage = null;
            this.KaliteDokFotoOlceklendirmeAction.Id = "KaliteDokFotoOlceklendirme";
            this.KaliteDokFotoOlceklendirmeAction.ToolTip = null;
            this.KaliteDokFotoOlceklendirmeAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.KaliteDokFotoOlceklendirmeAction_Execute);
            // 
            // KaliteDokFotografOlceklendirController
            // 
            this.Actions.Add(this.KaliteDokFotoOlceklendirmeAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction KaliteDokFotoOlceklendirmeAction;
    }
}
