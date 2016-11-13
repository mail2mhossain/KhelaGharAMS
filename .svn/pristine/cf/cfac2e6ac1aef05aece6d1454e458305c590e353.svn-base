using NakedObjects.Boot;
using NakedObjects.Core.Context;
using NakedObjects.Core.NakedObjectsSystem;
using NakedObjects.Core.Util;
using NakedObjects.EntityObjectStore;
using NakedObjects.Web.Mvc;
using NakedObjects.Web.Mvc.Helpers;
using NakedObjects.Reflector.Audit;
using KhelaGhar.AMS.Model.KhelaGharAMSAudit;
using KhelaGhar.AMS.Model.DbAccess;
using System.Data.Entity;
using KhelaGhar.AMS.Model.Repository;

namespace KhelaGharAMS.App_Start {
    public class RunWeb : RunMvc {

		// Return null for no REST support
		// Return a non empty string to support REST at that URL segment
        public static string RestRoot {
            get { return null; }
        }

        protected override NakedObjectsContext Context {
            get { return HttpContextContext.CreateInstance(); }
        }

        protected override IServicesInstaller MenuServices {
            get
            {
                //return new ServicesInstaller();
                return new ServicesInstaller(new object[] { 
                    new CentralKhelaGharRepository(),
                    new AsarRepository(),
                    new SetupRepository()
                    
                    //new ReportDataProcessRepository(),
                    //new SetupRepository()                    
                });

            }
        }

        protected override IServicesInstaller ContributedActions {
            get { return new ServicesInstaller(); }
        }

        protected override IServicesInstaller SystemServices {
            get { return new ServicesInstaller(new SimpleEncryptDecrypt()); }
        }


		// example functions that gets types for AssociateTypes below  
		//private static Type[] AdventureWorksTypes() {
        //    var allTypes =  AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "AdventureWorksModel").GetTypes();
        //    return allTypes.Where(t => t.BaseType == typeof(AWDomainObject) && !t.IsAbstract).ToArray();
        //}
		//
		//private static Type[] CodeFirstTypes() {
        //    return new[] {typeof(Class1), typeof(Class2)};
        //}

        //protected override IAuditorInstaller Auditor
        //{
        //    get
        //    {
        //        return new AuditInstaller(new KhelaGharAMSAuditor());

        //    }
        //}
        protected override IObjectPersistorInstaller Persistor
        {
            get
            {
                // Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"); //For in-memory database
                //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSDbContext>()); //Optional behaviour for CodeFirst
                //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSAuditContext>());
                //Database.SetInitializer(new KhelaGharAMSDbInitialiser());
                var installer = new EntityPersistorInstaller();

                installer.UsingCodeFirstContext(() => new KhelaGharAMSDbContext());
                installer.UsingCodeFirstContext(() => new KhelaGharAMSAuditContext());

                return installer;
            }
        }

        public static void Run() {
		    Assert.AssertTrue("Rest root may not be empty", RestRoot == null || RestRoot.Trim().Length > 0);
            new RunWeb().Start();
        }
    }
}