
namespace MidDosyaYonetim.Module.Controllers
{
    partial class Guncelle
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
            this.GuncelleAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // GuncelleAction
            // 
            this.GuncelleAction.Caption = "Guncelle";
            this.GuncelleAction.ConfirmationMessage = null;
            this.GuncelleAction.Id = "Guncelle";
            this.GuncelleAction.ToolTip = null;
            this.GuncelleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GuncelleAction_Execute);
            // 
            // Guncelle
            // 
            this.Actions.Add(this.GuncelleAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction GuncelleAction;
    }
}
