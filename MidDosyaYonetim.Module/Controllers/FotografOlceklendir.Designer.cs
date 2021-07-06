
namespace MidDosyaYonetim.Module.Controllers
{
    partial class FotografOlceklendir
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
            this.OlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // OlceklendirAction
            // 
            this.OlceklendirAction.Caption = "Urun foto Olceklendir";
            this.OlceklendirAction.ConfirmationMessage = null;
            this.OlceklendirAction.Id = "Olceklendir";
            this.OlceklendirAction.ToolTip = null;
            this.OlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.OlceklendirAction_Execute);
            // 
            // FotografOlceklendir
            // 
            this.Actions.Add(this.OlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction OlceklendirAction;
    }
}
