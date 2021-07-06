namespace MidDosyaYonetim.Module.Controllers
{
    partial class RevizeOlustur
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
            this.RevizeOlusturAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // RevizeOlusturAction
            // 
            this.RevizeOlusturAction.AcceptButtonCaption = null;
            this.RevizeOlusturAction.CancelButtonCaption = null;
            this.RevizeOlusturAction.Caption = "Revize Oluştur";
            this.RevizeOlusturAction.ConfirmationMessage = null;
            this.RevizeOlusturAction.Id = "RevizeOlusturAction";
            this.RevizeOlusturAction.ToolTip = null;

            // 
            // RevizeOlustur
            // 
            this.Actions.Add(this.RevizeOlusturAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RevizeOlusturAction;
    }
}
