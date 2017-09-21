using KhelaGhar.AMS.Model.Domain.Asars;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Activities
{
  [Table("WeeklyActivities")]
  [DisplayName("সাপ্তাহিক কার্যক্রম")]
  public class WeeklyActivity : Activity
  {
    [DisplayName("কার্যক্রম"), Title]
    [MemberOrder(10), Required]
    public virtual string ActivityName { get; set; }
    [MemberOrder(20)]
    [DisplayName("দিন"), Required]
    public virtual WeekDays Day { get; set; }
    public enum WeekDays
    {
      শুক্রবার = 1, //Friday
      শনিবার = 2, //Saturday
      রবিবার = 3, //Sunday
      সোমবার = 4, //Monday
      মঙ্গলবার = 5, //Tuesday
      বুধবার = 6, // Wednesday 
      বৃহস্পতিবার = 7  //Thursday
    }

    [MemberOrder(30)]
    [DisplayName("শুরুর সময়"), Optionally]
    [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
    public virtual string StartTime { get; set; }

    [MemberOrder(40)]
    [DisplayName("শেষ সময়"), Optionally]
    [RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
    public virtual string EndTime { get; set; }

    public IList<string> ChoicesActivityName()
    {
      return AsarRepository.GetWeeklyActivities();
    }

    #region Navigation Properties
    [DisplayName("শাখা আসর")]
    public virtual ShakhaAsar ShakhaAsar { get; set; }
    #endregion
  }
}
