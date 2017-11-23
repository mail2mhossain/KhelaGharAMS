using KhelaGhar.AMS.Model.DbAccess;
using KhelaGhar.AMS.Model.Domain.Asars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using KhelaGhar.AMS.Model.Domain.Committees;
using KhelaGhar.AMS.Model.Domain.Workers;
using KhelaGhar.AMS.Model.Domain.Areas;

namespace KhelaGhar.AMS.Model.Repository
{
  public class ApiRepository
  {
    private readonly KhelaGharAMSDbContext _dbContext;
    public ApiRepository(KhelaGharAMSDbContext dbContext)
    {
      _dbContext = dbContext;
    }
    public IList<Asar> GetAsarByName(string name)
    {
      if (name.Length <= 3)
      {
        return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Include(pp => pp.Area.Parent.Parent)
               .Where(w => w.Name.StartsWith(name.Trim()))
               .OrderBy(o => o.Name)
               .ToList();
      }
      else
      {
        return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Where(w => w.Name.Contains(name.Trim()))
               .OrderBy(o => o.Name)
               .ToList();
      }
    }
    public IList<Asar> GetAsarBySubdistrict(string sub)
    {
      return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Include(pp => pp.Area.Parent.Parent)
               .Where(w => w.Area.Name.StartsWith(sub.Trim()))
               .OrderBy(o => o.Name)
               .ToList();
    }
    public IList<Asar> GetAsarByDistrict(string district)
    {
      return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Include(pp => pp.Area.Parent.Parent)
               .Where(w => w.Area.Name.StartsWith(district.Trim()) || w.Area.Parent.Name.StartsWith(district.Trim()))
               .OrderBy(o => o.Name)
               .ToList();
    }
    public void UpdateGeoLocation(Asar asar, decimal latitude, decimal longitude)
    {
      asar.Latitude = latitude;
      asar.Longitude = longitude;
    }
    public Asar GetAsar(int asarId)
    {
      return _dbContext.Asars.Include(x=>x.Area).Where(w => w.AsarId == asarId).FirstOrDefault(); 
    }
    public IList<CommitteeMember> GetFilteredRunningCommitteeMembers(int asarId)
    {
      IList<CommitteeMember> workers = new List<CommitteeMember>();
      Committee runningCommittee = _dbContext.Committees.Where(w => w.Asar.AsarId == asarId
                                                               && w.DateOfExpiration == null)
                                                        .FirstOrDefault();
      if (runningCommittee != null)
      {
        IList<CommitteeMember> members = _dbContext.CommitteeMembers.Include(m=>m.Worker).Include(d=>d.Designation)
                                                   .Where(w => w.Committee.CommitteeId == runningCommittee.CommitteeId)
                                                   .OrderBy(o => o.Designation.DesignationOrder)
                                                   .ToList();
        if(members.Count > 0)
        {
          if (runningCommittee.CommitteeType == Committee.TypeOfCommittee.আহ্বায়ক)
          {
            workers = members.Where(w => w.Designation.DesignationOrder == 1 ||
                                         w.Designation.DesignationOrder == 2)
                             .OrderBy(o=>o.Designation.DesignationOrder)
                             .ToList();
          }
          else
          {
            workers = members.Where(w => w.Designation.DesignationOrder == 1 ||
                                        w.Designation.DesignationOrder == 3)
                             .OrderBy(o => o.Designation.DesignationOrder)
                             .ToList();
          }
        }
      }
      return workers;
    }
    public IList<CommitteeMember> GetRunningCommitteeMembers(int asarId)
    {
      IList<CommitteeMember> workers = new List<CommitteeMember>();
      Committee runningCommittee = _dbContext.Committees.Where(w => w.Asar.AsarId == asarId
                                                               && w.DateOfExpiration == null)
                                                        .FirstOrDefault();
      if (runningCommittee != null)
      {
        workers = _dbContext.CommitteeMembers.Include(m => m.Worker).Include(d => d.Designation)
                            .Where(w => w.Committee.CommitteeId == runningCommittee.CommitteeId)
                            .OrderBy(o => o.Designation.DesignationOrder)
                            .ToList();
      }
      return workers;
    }
    public IList<Area> GetAllUpojela()
    {
      return _dbContext.Areas
               .Include(a => a.Parent)
               .Include(p => p.Parent.Parent)
               .OrderBy(o => o.Name)
               .ToList();
    }

    public IList<Asar> GetAllAsar()
    {
      return _dbContext.Asars
                       .OrderBy(o => o.Name)
                       .ToList();
    }
  }
}
