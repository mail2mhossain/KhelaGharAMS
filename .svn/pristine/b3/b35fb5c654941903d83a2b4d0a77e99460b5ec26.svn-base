using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NakedObjects;
using System.ComponentModel;

namespace KhelaGhar.AMS.Model.Domain
{
  [DisplayName("টীকা")]
  [Bounded]
  public class NoteType
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

    [Key, Hidden]
    public virtual int NoteTypeId { get; set; }

    [MemberOrder(20)]
    [DisplayName("টীকা"), Required]
    [MaxLength(150), Title]
    public virtual string Name { get; set; }

    public string ValidateName(string name)
    {
      NoteType note = (from obj in Container.Instances<NoteType>()
                       where obj.Name == name
                       select obj).FirstOrDefault();

      if (note != null)
      {
        if (this.NoteTypeId != note.NoteTypeId)
        {
          return "Duplicate Note Name";
        }
      }
      return null;
    }
      
    //Add properties with 'propv', collections with 'coll', actions with 'act' shortcuts

  }
}

