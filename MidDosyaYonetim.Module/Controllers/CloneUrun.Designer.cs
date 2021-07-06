namespace MidDosyaYonetim.Module.Controllers
{
    partial class CloneUrun
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
            this.CloneUrunAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CloneUrunAction
            // 
            this.CloneUrunAction.Caption = "Ürünü Klonla";
            this.CloneUrunAction.Category = "View";
            this.CloneUrunAction.ConfirmationMessage = null;
            this.CloneUrunAction.Id = "CloneUrunAction";
            this.CloneUrunAction.TargetObjectType = typeof(MidDosyaYonetim.Module.BusinessObjects.Urunler);
            this.CloneUrunAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CloneUrunAction.ToolTip = null;
            this.CloneUrunAction.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CloneUrunAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CloneUrunAction_Execute);
            // 
            // CloneUrun
            // 
            this.Actions.Add(this.CloneUrunAction);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CloneUrunAction;
    }
}
