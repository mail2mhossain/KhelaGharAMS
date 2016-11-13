﻿using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Committees;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KhelaGhar.AMS.Model.Domain.Committees.Committee;

namespace KhelaGhar.AMS.Model.Domain.Conferences
{
	[DisplayName("সম্মেলন")]
	public class Conference
	{
		public string Title()
		{
			string title = String.Empty;
			if (this.Asar is ShakhaAsar)
			{
				title = this.Asar.Name + " সম্মেলন - " + this.StartDate.Year;
			}
			else
			{
				title = this.Asar.Area.Name + " সম্মেলন - " + this.StartDate.Year;
			}

			return title;
		}

		#region Injected Services
		public IDomainObjectContainer Container { set; protected get; }
		#endregion

		#region Life Cycle Methods
		public virtual void Persisting ()
		{
			AuditFields.InsertedBy = Container.Principal.Identity.Name;
			AuditFields.InsertedDateTime = DateTime.Now;
			AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
			AuditFields.LastUpdatedDateTime = DateTime.Now;
		}
		public virtual void Updating ()
		{
			AuditFields.LastUpdatedBy = Container.Principal.Identity.Name;
			AuditFields.LastUpdatedDateTime = DateTime.Now;
		}
		#endregion

		#region Primitive Properties
		[Key, NakedObjectsIgnore]
		public virtual long ConferenceId { get; set; }
		[MemberOrder(10)]
		[Required, Mask("d")]
		public virtual DateTime StartDate { get; set; }
		[MemberOrder(20)]
		[Required, Mask("d")]
		public virtual DateTime EndDate { get; set; }
		[MemberOrder(30)]
		[DisplayName("Slogan"), Optionally]
		[MaxLength(250)]
		public virtual string Slogan { get; set; }
		[MemberOrder(40)]
		[DisplayName("Location"), Optionally]
		[MaxLength(250)]
		public virtual string Location { get; set; }
		#endregion

		#region Get Properties
		[MemberOrder(60), NotMapped]
		[DisplayName("কমিটি")]
		public Committee Committee
		{
			get
			{
				return GetCurrentCommittee();
			}
		}

		[MemberOrder(70), NotMapped]
		[DisplayName("প্রতিনিধি")]
		//[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(false, "Worker", "Asar")]
		public IList<ConferenceDelegate> Delegates
		{
			get
			{
				return Container.Instances<ConferenceDelegate>().Where(w => w.Conference.ConferenceId == this.ConferenceId && w.IsObserver == false).OrderBy(o => o.Worker.Name).ToList();
			}
		}

		[MemberOrder(80), NotMapped]
		[DisplayName("পর্যবেক্ষক")]
		//[Eagerly(EagerlyAttribute.Do.Rendering)]
		[TableView(false, "Worker", "Asar")]
		public IList<ConferenceDelegate> ObserverDelegate
		{
			get
			{
				return Container.Instances<ConferenceDelegate>().Where(w => w.Conference.ConferenceId == this.ConferenceId && w.IsObserver == true).OrderBy(o => o.Worker.Name).ToList();
			}
		}
		#endregion

		#region Complex Properties
		#region AuditFields (AuditFields)
		private AuditFields _auditFields = new AuditFields();

		[MemberOrder(250)]
		[Required]
		public virtual AuditFields AuditFields
		{
			get
			{
				return _auditFields;
			}
			set
			{
				_auditFields = value;
			}
		}
		public bool HideAuditFields ()
		{
			return true;
		}
		#endregion
		#endregion

		#region Navigation Properties
		
		[MemberOrder(50), Required]
		[DisplayName("আসর")]
		public virtual Asar Asar { get; set; }
		#endregion

		#region Behavior

		#region New Committee
		[DisplayName("পূর্ণাঙ্গ কমিটি গঠন")]
		public FullCommittee CreateFullCommittee (DateTime dateOfFormation)
		{
			FullCommittee committee = Container.NewTransientInstance<FullCommittee>();
			committee.DateOfFormation = dateOfFormation;
			committee.CommitteeType = TypeOfCommittee.পূর্ণাঙ্গ;
			committee.Asar = this.Asar;
			Container.Persist(ref committee);

			this.Asar.CommitteeType = TypeOfCommittee.পূর্ণাঙ্গ;
			Committee lastCommittee = GetCurrentCommittee();

			if (lastCommittee != null)
			{
				lastCommittee.DateOfExpiration = dateOfFormation.AddDays(-1);
			}
			return committee;
		}
		#endregion

		#region Add Delegate
		public void AddDelegate (Worker worker, bool isObserver)
		{
			ConferenceDelegate confDelegate = Container.NewTransientInstance<ConferenceDelegate>();
			confDelegate.Worker = worker;
			confDelegate.IsObserver = isObserver;
			confDelegate.Conference = this;
			Container.Persist(ref confDelegate);
		}

		[PageSize(10)]
		public IQueryable<Worker> AutoComplete0AddDelegate ([MinLength(1)] string name)
		{
			return Container.Instances<Worker>().Where(w => w.Name.Contains(name) && w.Asar.AsarId == this.Asar.AsarId).OrderBy(o => o.Name);
		}
		#endregion

		private Committee GetCurrentCommittee ()
		{
			Committee committee = Container.Instances<Committee>().Where(w => w.Asar.AsarId == this.Asar.AsarId && w.DateOfExpiration == null).FirstOrDefault();

			return committee;
		}
		#endregion
	}
}
