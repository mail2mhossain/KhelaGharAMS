using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace KhelaGhar.AMS.Model.DbAccess
{
    public class KhelaGharAMSAuditContext : DbContext
    {
        public KhelaGharAMSAuditContext(string name) : base(name) { }
        public KhelaGharAMSAuditContext() { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Initialisation
            //Use the Naked Objects > DbInitialiser template to add a custom initialiser, then reference thus:
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KhelaGharAMSAuditContext>());

            //Mappings            //Use the Naked Objects > Mapping template to add mapping classes & reference them thus:
            //modelBuilder.Configurations.Add(new Employee_Mapping());
        }
    }
}
