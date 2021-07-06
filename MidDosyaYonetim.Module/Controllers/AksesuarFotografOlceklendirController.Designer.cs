
namespace MidDosyaYonetim.Module.Controllers
{
    partial class AksesuarFotografOlceklendirController
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
            this.AksesuarFotoOlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // AksesuarFotoOlceklendirAction
            // 
            this.AksesuarFotoOlceklendirAction.Caption = "Aksesuar Foto Olceklendir";
            this.AksesuarFotoOlceklendirAction.ConfirmationMessage = null;
            this.AksesuarFotoOlceklendirAction.Id = "AksesuarFotoOlceklendir";
            this.AksesuarFotoOlceklendirAction.ToolTip = null;
            this.AksesuarFotoOlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AksesuarFotoOlceklendirAction_Execute);
            // 
            // AksesuarFotografOlceklendirController
            // 
            this.Actions.Add(this.AksesuarFotoOlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction AksesuarFotoOlceklendirAction;
    }
}
