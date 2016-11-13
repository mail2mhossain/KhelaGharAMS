using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using KhelaGhar.AMS.Model.Domain;

namespace KhelaGhar.AMS.Model.DbAccess
{
    public class KhelaGharAMSDbInitialiser : CreateDatabaseIfNotExists<KhelaGharAMSDbContext>
    {
        private KhelaGharAMSDbContext context;

        protected override void Seed(KhelaGharAMSDbContext context)
        {
            this.context = context;

            PopulateDbWithRequiredConfig(context);

        }
        private void PopulateDbWithRequiredConfig(KhelaGharAMSDbContext context)
        {
            CreateCentralKhelaGhar(context);
            AddActivities(context);           
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

        private void AddDivisions(KhelaGharAMSDbContext context)
        {
            AddDivision("Dhaka", "Dhaka Division", context);
        }
        private Division AddDivision(string name, string description, KhelaGharAMSDbContext context)
        {
            Division division = new Division
            {
                Name = name,
                Description = description
            };

            context.Divisions.Add(division);
            return division;
        }

        private void AddActivities(KhelaGharAMSDbContext context)
        {
            AddActivity("গান", context);
            AddActivity("নাচ", context);
            AddActivity("আবৃত্তি", context);
            AddActivity("ছবি আঁকা", context);
            AddActivity("নাটক", context);
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
    }
}
