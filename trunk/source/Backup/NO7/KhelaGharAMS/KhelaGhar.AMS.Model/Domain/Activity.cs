using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
    [DisplayName("কার্যক্রম")]
    public class Activity
    {
        #region Injected Services
        // This region should contain properties to hold references to any services required by the
        // object.  Use the 'injs' shortcut to add a new service; 'injc' to add an injected Container
        public IDomainObjectContainer Container { set; protected get; }
        #endregion

        #region Life Cycle Methods
        // This region should contain any of the 'life cycle' convention methods (such as
        // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


        #endregion

        [Key, NakedObjectsIgnore]
        public virtual int ActivityId { get; set; }

        [MemberOrder(10), Title]
        [DisplayName("নাম")]
        [MaxLength(250), Required]
        public virtual string ActivityName { get; set; }

        public string ValidateActivityName(string asarname)
        {
            var rb = new ReasonBuilder();

            Activity activity = (from obj in Container.Instances<Activity>() 
                                 where obj.ActivityName == asarname
                                 select obj).FirstOrDefault();

            if (activity != null)
            {
                if (this.ActivityId != activity.ActivityId)
                {
                    rb.AppendOnCondition(true, "Duplicate activity Name");
                }
            }
            return rb.Reason;
        }

        //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts
    }
}

