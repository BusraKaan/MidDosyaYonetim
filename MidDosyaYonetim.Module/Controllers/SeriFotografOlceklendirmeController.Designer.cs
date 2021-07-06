
namespace MidDosyaYonetim.Module.Controllers
{
    partial class SeriFotografOlceklendirmeController
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
            this.SeriFotoOlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SeriFotoOlceklendirAction
            // 
            this.SeriFotoOlceklendirAction.Caption = "Seri Foto Olceklendir";
            this.SeriFotoOlceklendirAction.ConfirmationMessage = null;
            this.SeriFotoOlceklendirAction.Id = "SeriFotoOlceklendir";
            this.SeriFotoOlceklendirAction.ToolTip = null;
            this.SeriFotoOlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SeriFotoOlceklendirAction_Execute);
            // 
            // SeriFotografOlceklendirmeController
            // 
            this.Actions.Add(this.SeriFotoOlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction SeriFotoOlceklendirAction;
    }
}
