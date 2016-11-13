using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain.MasterData
{
	public class Designation
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
		public virtual int DesignationId { get; set; }

		[DisplayName("পদবী"), MemberOrder(20), Required]
		[MaxLength(150), Title]
		public virtual string Name { get; set; }

		[DisplayName("পদবীর ধরণ"), MemberOrder(30), Required]
		//[EnumDataType(typeof(AllEnums.DesignationType))]
		public virtual TypeOfDesignation DesignationType { get; set; }

		public enum TypeOfDesignation
		{
			কেন্দ্রীয়_কমিটি = 1,
			জেলা_মহানগর_উপজেলা_কমিটি = 2,
			শাখা_আসর_কমিটি = 3,
			আহ্বায়ক_কমিটি = 4
		}

		[DisplayName("পদবীর ক্রম"), MemberOrder(40), Optionally]
		public virtual int DesignationOrder { get; set; }


		public string ValidateName (string name)
		{
			var rb = new ReasonBuilder();

			Designation desig = (from obj in Container.Instances<Designation>()
													 where obj.Name == name
													 select obj).FirstOrDefault();

			if (desig != null)
			{
				if (this.DesignationId != desig.DesignationId)
				{
					rb.AppendOnCondition(true, "Duplicate Designation Name");
				}
			}
			return rb.Reason;
		}
		//Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

	}
}

