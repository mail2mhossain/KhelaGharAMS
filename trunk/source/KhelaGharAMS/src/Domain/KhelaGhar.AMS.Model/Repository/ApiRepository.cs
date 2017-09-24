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
               .Where(w => w.Name.StartsWith(name))
               .OrderBy(o => o.Name)
               .ToList().ToList();
      }
      else
      {
        return _dbContext.Asars
               .Include(a => a.Area)
               .Include(p => p.Area.Parent)
               .Where(w => w.Name.Contains(name))
               .OrderBy(o => o.Name)
               .ToList().ToList();
      }
    }
  }
}
