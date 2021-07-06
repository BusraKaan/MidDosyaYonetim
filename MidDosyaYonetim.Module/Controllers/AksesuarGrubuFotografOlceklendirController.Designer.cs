
namespace MidDosyaYonetim.Module.Controllers
{
    partial class AksesuarGrubuFotografOlceklendirController
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
            this.AksGrupFotoOlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // AksGrupFotoOlceklendirAction
            // 
            this.AksGrupFotoOlceklendirAction.Caption = "Aks Grup Foto Olceklendir";
            this.AksGrupFotoOlceklendirAction.ConfirmationMessage = null;
            this.AksGrupFotoOlceklendirAction.Id = "AksGrupFotoOlceklendir";
            this.AksGrupFotoOlceklendirAction.ToolTip = null;
            this.AksGrupFotoOlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AksGrupFotoOlceklendirAction_Execute);
            // 
            // AksesuarGrubuFotografOlceklendirController
            // 
            this.Actions.Add(this.AksGrupFotoOlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction AksGrupFotoOlceklendirAction;
    }
}
