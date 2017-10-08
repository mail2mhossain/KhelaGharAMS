using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.MasterData
{
  public class MasterActivity
  {
    #region Primitive Properties
    [Key, NakedObjectsIgnore]
    public virtual int MasterActivityId { get; set; }
    [DisplayName("কার্যক্রম")]
    [MemberOrder(10), Required]
    public virtual string ActivityName { get; set; }
    #endregion
  }
}
