using NakedObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGhar.AMS.Model.Domain
{
  [ComplexType]
  public class AuditFields
  {
    #region Primitive Properties
    #region InsertedBy (String)
    [MemberOrder(130)]
    [Hidden,Required]
    public virtual string InsertedBy { get; set; }

    #endregion
    #region InsertedDateTime (DateTime)
    [MemberOrder(140), Mask("g")]
    [Hidden, Required]
    public virtual System.DateTime InsertedDateTime { get; set; }

    #endregion
    #region LastUpdatedBy (String)
    [MemberOrder(150)]
    [Hidden, Required]
    public virtual string LastUpdatedBy { get; set; }

    #endregion
    #region LastUpdatedDateTime (DateTime)
    [MemberOrder(160), Mask("g")]
    [Hidden, Required]
    [ConcurrencyCheck]
    public virtual System.DateTime LastUpdatedDateTime { get; set; }

    #endregion

    #endregion
  }
}
