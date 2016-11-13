using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Activities
	{
    [Table("Meetings")]
    public class Meeting : Activity
		{
			[Required]
			[MemberOrder(20)]
			public virtual DateTime StartDateTime { get; set; }
			[Optionally]
			[MemberOrder(30)]
			public virtual DateTime? EndDateTime { get; set; }
			[Required]
			[MemberOrder(80)]
			public virtual string Agenda { get; set; }
			[Required]
			[MemberOrder(90)]
			public virtual string Decisions { get; set; }
		}
	}
