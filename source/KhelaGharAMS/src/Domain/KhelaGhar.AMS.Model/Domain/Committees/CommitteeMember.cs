using KhelaGhar.AMS.Model.Domain.MasterData;
using KhelaGhar.AMS.Model.Domain.Members;
using KhelaGhar.AMS.Model.Domain.Shared;
using KhelaGhar.AMS.Model.Domain.Workers;
using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain.Committees
{
  public class CommitteeMember
  {
    #region Injected Services
    public IDomainObjectContainer Container { set; protected get; }
    #endregion

    #region Life Cycle Methods
    // This region should contain any of the 'life cycle' convention methods (such as
    // Created(), Persisted() etc) called by the framework at specified stages in the lifecycle.


    #endregion

    public string Title()
    {
      var t = Container.NewTitleBuilder();
      if (this.Worker != null)
      {
        t.Append(this.Worker.Name).Append(", ").Append(this.Designation.Name);
      }
      else
      {
        t.Append("");
      }
      return t.ToString();
    }

    [Key, NakedObjectsIgnore]
    public virtual int CommitteeMemberId { get; set; }

    //public virtual string Responsibilities { get; set; }

    [DisplayName("মেম্বার"), MemberOrder(10), Required]
    public virtual Worker Worker { get; set; }

    [DisplayName("পদবী"), MemberOrder(20), Required]
    public virtual Designation Designation { get; set; }

    [DisplayName("কমিটি"), MemberOrder(40), Required]
    [NakedObjectsIgnore]
    public virtual Committee Committee { get; set; }
  }
}
