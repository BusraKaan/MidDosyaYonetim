using System;
using System.Configuration;
using System.ServiceModel;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraEditors;

namespace MidDosyaYonetim.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            WindowsFormsSettings.LoadApplicationSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;



            //    MidDosyaYonetimWindowsFormsApplication winApplication = new MidDosyaYonetimWindowsFormsApplication();
            //    try
            //    {

            //        string connectionString = "http://10.26.0.30:1465/MidDosyaYonetimServiceHost.svc";
            //        var bd = ((WSHttpBinding)(WcfDataServerHelper.CreateDefaultBinding()));
            //        bd.Security.Mode = SecurityMode.None;
            //        WcfSecuredDataServerClient clientDataServer = new WcfSecuredDataServerClient(bd, new EndpointAddress(connectionString));
            //        ServerSecurityClient securityClient = new ServerSecurityClient(clientDataServer, new ClientInfoFactory());
            //        securityClient.SupportNavigationPermissionsForTypes = false;
            //        securityClient.IsSupportChangePassword = true;
            //        winApplication.Security = securityClient;


            //        winApplication.CreateCustomObjectSpaceProvider += delegate (object sender, CreateCustomObjectSpaceProviderEventArgs e)
            //        {
            //            e.ObjectSpaceProvider = new DataServerObjectSpaceProvider(clientDataServer, securityClient);
            //        };


            //        winApplication.Setup();
            //        winApplication.Start();
            //        clientDataServer.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        winApplication.HandleException(e);
            //    }
            //}


            if (Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
            {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
            MidDosyaYonetimWindowsFormsApplication winApplication = new MidDosyaYonetimWindowsFormsApplication();
            // Refer to the https://docs.devexpress.com/eXpressAppFramework/112680 help article for more details on how to provide a custom splash form.
            //       winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
            SecurityAdapterHelper.Enable();
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
                                                                                                    if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                                                                                                        winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
                                                                                                    }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            try
            {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }

    }
}
