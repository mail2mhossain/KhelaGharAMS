using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhelaGhar.AMS.Model.Domain
{
    public class AllEnums
    {
        public enum ActionTypes
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum CommitteeType
        {
            পূর্ণাঙ্গ = 1,
            আহ্বায়ক = 2
        }

        public enum DesignationType
        {
            পূর্ণাঙ্গ_কমিটি = 1,
            আহ্বায়ক_কমিটি = 2,
            উভয়_কমিটি = 3
        }

        public enum AsarStatus
        {
            সচল = 1,
            নিষ্ক্রিয় = 2
        }     

        public enum TopLevelRoles
        {
            ADMIN = 1,
            FMS_ADMIN = 2,
            MANAGEMENT = 3
        }
        public const string ADMIN = "Admin";
        public const string AMS_ADMIN = "AMSAdmin";
        public const string MANAGEMENT = "Management";

        public static IList<string> GetCommitteeType()
        {
            IList<string> types = new List<string>();

            types.Add("কমিটিহীন");
            types.Add("পূর্ণাঙ্গ");
            types.Add("আহ্বায়ক");

            return types;
        } 
    }
}
