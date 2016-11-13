using KhelaGhar.AMS.Model.Domain.Conferences;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using NakedObjects.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Asars
{
	public class KendrioAsar : Asar
	{
		#region Get Properties

		#region সভ্য সংখ্যা
		[MemberOrder(50), NotMapped]
		//[Eagerly(EagerlyAttribute.Do.Rendering)]
		[DisplayName("সভ্য সংখ্যা")]
		public int Members
		{
			get
			{
				List<int> allchildrenids = AsarRepository.GetAllChildrenIds();

				IList<ShakhaAsar> asars = Container.Instances<ShakhaAsar>().Where(d => allchildrenids.Contains(d.Area.AreaId)).ToList();

				if (asars.Count > 0)
					return asars.Sum(s => s.TotalMembers);

				return 0;
			}
		}
		#endregion

		#region Workers
		[MemberOrder(60), NotMapped]
		[DisplayName("কর্মী সংখ্যা")]
		public int Workers
		{
			get
			{
				List<int> allchildrenids = AsarRepository.GetChildrenIds(this.Area);

				return Container.Instances<Worker>().Where(d => allchildrenids.Contains(d.Asar.Area.AreaId)).Count();
			}
		}
		#endregion

		#region Last Conference Date
		[MemberOrder(20), NotMapped]
		[DisplayName("সর্বশেষ সম্মেলনের তারিখ")]
		[Mask("d")]
		public DateTime? LastConferenceDate
		{
			get
			{
				Conference conference = Container.Instances<Conference>().Where(w => w.Asar.AsarId == this.AsarId).OrderByDescending(o => o.StartDate).FirstOrDefault();

				if (conference != null)
				{
					return conference.StartDate;
				}
				return null;
			}
		}

		////public bool HideLastConferenceDate ()
		////{
		////	if (this.LastConferenceDate != null)
		////	{
		////		return false;
		////	}
		////	else
		////	{
		////		return true;
		////	}
		////}
		#endregion
		#endregion

		#region Menu
		public static void Menu (IMenu menu)
		{
			IMenu sub = menu.CreateSubMenu("সম্মেলন");
			sub.AddAction("NewConference");
			sub.AddAction("ShowAllConferences");
		}
		#endregion
	}
}
