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
          info.Subdistrict = asar.Area.Name;
          info.District = asar.Area.Parent.Name;

          asarList.Add(info);
        }
      }
      return asarList;
    }
  }
}
