using KhelaGhar.AMS.Model.DbAccess;
using KhelaGhar.AMS.Model.Repository;
using KhelaGharAmsApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KhelaGhar.AMS.Model.Domain.Asars;
using KhelaGhar.AMS.Model.Domain.Areas;

namespace KhelaGharAmsApi.Controllers
{
  public class AsarController : ApiController
  {
    [HttpGet]
    [Route("Asar")]
    public IHttpActionResult GetAsar(string name)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        asarList = MapAsar(repo.GetAsarByName(name));
        return Content(HttpStatusCode.OK, asarList);
      }
    }

    [HttpGet]
    [Route("Upojela")]
    public IHttpActionResult GetAsarBySubdistrcit(string upojela)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        asarList = MapAsar(repo.GetAsarBySubdistrict(upojela));
        return Content(HttpStatusCode.OK, asarList);
      }
    }

    [HttpGet]
    [Route("Jela")]
    public IHttpActionResult GetAsarByDistrcit(string jela)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        asarList = MapAsar(repo.GetAsarByDistrict(jela));
        return Content(HttpStatusCode.OK, asarList);
      }
    }
    private IList<AsarInfo> MapAsar(IList<Asar> list)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      foreach(Asar asar in list)
      {
        if (asar is ShakhaAsar)
        {
          AsarInfo info = new AsarInfo();
          info.AsarName = asar.Name;
          info.CommitteeType = asar.CommitteeType.ToString();
          info.AsarStatus = ((ShakhaAsar)asar).AsarStatus.ToString();
          info.AddressLine = asar.AddressLine;
          if (asar.Area is SubDistrict)
          {
            info.Subdistrict = asar.Area.Name;
            info.District = asar.Area.Parent.Name;
            info.Division = asar.Area.Parent.Parent.Name;
          }
          else
          {
            info.Subdistrict = asar.Area.Name;
            info.Division = asar.Area.Parent.Name;
          }

          asarList.Add(info);
        }
      }
      return asarList;
    }
  }
}
