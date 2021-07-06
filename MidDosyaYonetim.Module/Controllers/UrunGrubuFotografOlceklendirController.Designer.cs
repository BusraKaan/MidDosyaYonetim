
namespace MidDosyaYonetim.Module.Controllers
{
    partial class UrunGrubuFotografOlceklendirController
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
            this.UrunGrupFotoOlceklendirAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // UrunGrupFotoOlceklendirAction
            // 
            this.UrunGrupFotoOlceklendirAction.Caption = "Urun Grup Foto Olceklendir";
            this.UrunGrupFotoOlceklendirAction.ConfirmationMessage = null;
            this.UrunGrupFotoOlceklendirAction.Id = "UrunGrupFotoOlceklendir";
            this.UrunGrupFotoOlceklendirAction.ToolTip = null;
            this.UrunGrupFotoOlceklendirAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.UrunGrupFotoOlceklendirAction_Execute);
            // 
            // UrunGrubuFotografOlceklendirController
            // 
            this.Actions.Add(this.UrunGrupFotoOlceklendirAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction UrunGrupFotoOlceklendirAction;
    }
}
