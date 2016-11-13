namespace KhelaGhar.AMS.Model.MigrationAMSDbContext
{
    using KhelaGhar.AMS.Model.DbAccess;
    using KhelaGhar.AMS.Model.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KhelaGharAMSDbContext>
    {
        private KhelaGharAMSDbContext context;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationAMSDbContext";
        }

        protected override void Seed(KhelaGharAMSDbContext context)
        {
            this.context = context;
            //PopulateDbWithRequiredConfig(context);
        }

        private void PopulateDbWithRequiredConfig(KhelaGharAMSDbContext context)
        {
            CreateCentralKhelaGhar(context);

            AddActivities(context);

            AreaType division = AddAreaType("বিভাগ", null, context);
            AddAreaTypes(context, division);

            AddDivisions(context, division);
        }

        private void CreateCentralKhelaGhar(KhelaGharAMSDbContext context)
        {
            CentralKhelaGhar khelaghar = new CentralKhelaGhar
            {
                Name = "কেন্দ্রীয় খেলাঘর আসর",
                AddressLine = "২/২২ বাবর রোড, মোহাম্মদপুর, ঢাকা"
            };

            context.CentralKhelaGhars.Add(khelaghar);
        }

        private void AddActivities(KhelaGharAMSDbContext context)
        {
            AddActivity("গান", context);
            AddActivity("নাচ", context);
            AddActivity("আবৃত্তি", context);
            AddActivity("ছবি আঁকা", context);
            AddActivity("নাটক", context);
            AddActivity("গীটার", context);
            AddActivity("পাঠ চক্র", context);
        }

        private Activity AddActivity(string activityname, KhelaGharAMSDbContext context)
        {
            Activity activity = new Activity
            {
                ActivityName = activityname
            };

            context.Activities.Add(activity);
            return activity;
        }

        private void AddAreaTypes(KhelaGharAMSDbContext context, AreaType division)
        {
            AreaType district = AddAreaType("জেলা", division, context);

            AreaType subdistrict = AddAreaType("উপজেলা", district, context);
            AreaType unioin = AddAreaType("ইউনিয়ন", subdistrict, context);
            AreaType village = AddAreaType("গ্রাম", unioin, context);

            AreaType city = AddAreaType("মেট্রোপলিটন শহর", district, context);
            AreaType thana = AddAreaType("থানা", city, context);
            AreaType ward = AddAreaType("ওয়ার্ড", thana, context);
        }

        private AreaType AddAreaType(string name, AreaType parent, KhelaGharAMSDbContext context)
        {
            AreaType area = new AreaType
            {
                Name = name,
                Parent = parent
            };

            context.AreaTypes.Add(area);
            return area;
        }

        private void AddDivisions(KhelaGharAMSDbContext context, AreaType division)
        {
            AddDivision("ঢাকা", division);
            AddDivision("চট্টগ্রাম", division);
            AddDivision("রাজশাহী", division);
            AddDivision("খুলনা", division);
            AddDivision("বরিশাল", division);
            AddDivision("সিলেট", division);
            AddDivision("রংপুর", division);
        }

        private void AddDivision(string name, AreaType division)
        {
            Area area = new Area
            {
                Name = name,
                Description = "",
                AreaType = division,
                Parent = null
            };

            context.Areas.Add(area);
        }
    }
}
