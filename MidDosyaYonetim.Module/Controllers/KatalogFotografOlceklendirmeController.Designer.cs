
namespace MidDosyaYonetim.Module.Controllers
{
    partial class KatalogFotografOlceklendirmeController
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
            this.KataloglarFotoOlceklendirmeAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // KataloglarFotoOlceklendirmeAction
            // 
            this.KataloglarFotoOlceklendirmeAction.Caption = "Kataloglar Foto Olceklendirme";
            this.KataloglarFotoOlceklendirmeAction.ConfirmationMessage = null;
            this.KataloglarFotoOlceklendirmeAction.Id = "KataloglarFotoOlceklendirme";
            this.KataloglarFotoOlceklendirmeAction.ToolTip = null;
            this.KataloglarFotoOlceklendirmeAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.KataloglarFotoOlceklendirmeAction_Execute);
            // 
            // KatalogFotografOlceklendirmeController
            // 
            this.Actions.Add(this.KataloglarFotoOlceklendirmeAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction KataloglarFotoOlceklendirmeAction;
    }
}
