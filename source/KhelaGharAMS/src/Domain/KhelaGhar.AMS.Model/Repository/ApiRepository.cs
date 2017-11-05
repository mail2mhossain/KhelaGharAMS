using KhelaGhar.AMS.Model.DbAccess;
using KhelaGhar.AMS.Model.Domain.Asars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

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
               .ToList().ToList();
      }
      else
      {
        return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Where(w => w.Name.Contains(name.Trim()))
               .OrderBy(o => o.Name)
               .ToList().ToList();
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
               .ToList().ToList();
    }
    public IList<Asar> GetAsarByDistrict(string district)
    {
      return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Include(pp => pp.Area.Parent.Parent)
               .Where(w => w.Area.Parent.Name.StartsWith(district.Trim()))
               .OrderBy(o => o.Name)
               .ToList().ToList();
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
  }
}
