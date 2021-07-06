namespace MidDosyaYonetim.Module.Controllers
{
    partial class DokumanGuncelle
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
            this.dokumanGuncelleAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // dokumanGuncelleAction
            // 
            this.dokumanGuncelleAction.Caption = "Döküman Güncelle";
            this.dokumanGuncelleAction.ConfirmationMessage = null;
            this.dokumanGuncelleAction.Id = "dokumanGuncelleAction";
            this.dokumanGuncelleAction.ToolTip = null;
            this.dokumanGuncelleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.dokumanGuncelleAction_Execute);
            // 
            // DokumanGuncelle
            // 
            this.Actions.Add(this.dokumanGuncelleAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction dokumanGuncelleAction;
    }
}
