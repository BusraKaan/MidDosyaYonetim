using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.ExpressApp.Security.ClientServer;
using System.Configuration;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp;
using System.Collections;
using System.ServiceModel;
using DevExpress.ExpressApp.Security.ClientServer.Wcf;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.MiddleTier;
using DevExpress.Xpo.Metadata;

namespace MidDosyaYonetim.AppServer.ApplicationServer {
    class Program {
        static Program() {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
        }
        private static void serverApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
        static void Main(string[] args) {
            try
            {
                SecurityAdapterHelper.Enable();
                ValueManager.ValueManagerType = typeof(MultiThreadValueManager<>).GetGenericTypeDefinition();

                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                /*string connectionString = InMemoryDataStoreProvider.ConnectionString;*/

                ServerApplication serverApplication = new ServerApplication();
                serverApplication.ApplicationName = "MidDosyaYonetim";
                serverApplication.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached && serverApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
                {
                    serverApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                }
#endif

                serverApplication.Modules.BeginInit();
                serverApplication.Modules.Add(new SecurityModule());
                serverApplication.Modules.Add(new MidDosyaYonetim.Module.MidDosyaYonetimModule());
               serverApplication.Modules.Add(new MidDosyaYonetim.Module.Win.MidDosyaYonetimWindowsFormsModule());
                serverApplication.Modules.Add(new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule());
                serverApplication.Modules.EndInit();

                serverApplication.DatabaseVersionMismatch += new EventHandler<DatabaseVersionMismatchEventArgs>(serverApplication_DatabaseVersionMismatch);
                serverApplication.CreateCustomObjectSpaceProvider += new EventHandler<CreateCustomObjectSpaceProviderEventArgs>(serverApplication_CreateCustomObjectSpaceProvider);

                serverApplication.ConnectionString = connectionString;
                Console.WriteLine("Setup...");
                serverApplication.Setup();
                Console.WriteLine("CheckCompatibility...");
                serverApplication.CheckCompatibility();
                serverApplication.Dispose();

                Console.WriteLine("Starting server...");
                QueryRequestSecurityStrategyHandler securityProviderHandler = () =>
                {
                    SecurityStrategyComplex security = new SecurityStrategyComplex(typeof(PermissionPolicyUser), typeof(PermissionPolicyRole), new AuthenticationStandard());
                    security.SupportNavigationPermissionsForTypes = false;
                    return security;
                };
                XPDictionary dictionary = XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary;
                IXpoDataStoreProvider dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, true);
                IDisposable[] disposableObjects;
                ThreadSafeDataLayer threadSafeDataLayer = new ThreadSafeDataLayer(dictionary, dataStoreProvider.CreateWorkingStore(out disposableObjects));
                Func<IDataLayer> dataLayerProvider = () => threadSafeDataLayer;

                SecuredDataServer dataServer = new SecuredDataServer(connectionString, XpoTypesInfoHelper.GetXpoTypeInfoSource().XPDictionary, securityProviderHandler);
                ServiceHost serviceHost = new ServiceHost(new WcfSecuredDataServer(dataServer));
                var bd = ((WSHttpBinding)(WcfDataServerHelper.CreateDefaultBinding()));
                bd.Security.Mode = SecurityMode.None;
                serviceHost.AddServiceEndpoint(typeof(IWcfSecuredDataServer), bd, new Uri("http://10.26.0.30:1465/MidDosyaYonetimServiceHost.svc"));

                serviceHost.Open();
                Console.WriteLine("Server is started. Press Enter to stop.");
                Console.ReadLine();
                Console.WriteLine("Stopping...");
                serviceHost.Close();
                Console.WriteLine("Server is stopped.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurs: " + e.Message);
                Console.WriteLine("Press Enter to close.");
                Console.ReadLine();
            }


        }

        private static void serverApplication_CreateCustomObjectSpaceProvider(object sender, CreateCustomObjectSpaceProviderEventArgs e)
        {
            e.ObjectSpaceProvider = new XPObjectSpaceProvider(e.ConnectionString, e.Connection);
        }
    }
    
}
