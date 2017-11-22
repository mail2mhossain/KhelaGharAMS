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
using System.Data.Entity.Validation;
using KhelaGhar.AMS.Model.Domain.Workers;

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
        asarList = MapAsar(repo.GetAsarByName(name),repo);
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
        asarList = MapAsar(repo.GetAsarBySubdistrict(upojela),repo);
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
        asarList = MapAsar(repo.GetAsarByDistrict(jela),repo);
        return Content(HttpStatusCode.OK, asarList);
      }
    }
    private IList<AsarInfo> MapAsar(IList<Asar> list, ApiRepository repo)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      foreach (Asar asar in list)
      {
        AsarInfo info = new AsarInfo();
        info.AsarId = asar.AsarId;
        info.AsarName = asar.Name;
        info.CommitteeType = asar.CommitteeType.ToString();
        if (asar is ShakhaAsar)
        {
          info.AsarStatus = ((ShakhaAsar)asar).AsarStatus.ToString();
          info.AsarType = "ShakhaAsar";
        }
        if (asar is UpojelaAsar)
        {
          info.AsarType = "UpojelaAsar";
        }
        if (asar is JelaAsar)
        {
          info.AsarType = "JelaAsar";
        }
        if (asar is MohanagarAsar)
        {
          info.AsarType = "MohanagarAsar";
        }
        info.Contacts = asar.Contacts;
        info.AddressLine = asar.AddressLine;
        Worker worker = repo.GetRunningCommittee(asar.AsarId);
        if (worker != null)
        {
          info.Secretary = worker.Name;
          info.SecretaryMobileNo = worker.MobileNo;
          info.SecretaryEmailAddress = worker.Email;
        }
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
        info.Latitude = asar.Latitude ?? 0;
        info.Longitude = asar.Longitude ?? 0;
        asarList.Add(info);
      }

      return asarList;
    }

    [HttpPost]
    [Route("UpdateGeoLocation")]
    public IHttpActionResult UpdateGeoLocation(int asarId, decimal latitude, decimal longitude)
    {
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        Asar asar = repo.GetAsar(asarId);
        if(asar != null)
        {
          repo.UpdateGeoLocation(asar, latitude, longitude);
          dbContext.SaveChanges();
         
          return Content(HttpStatusCode.OK, "Update Success");
        }
        return NotFound();
      }
    }

    [HttpGet]
    [Route("AllUpojela")]
    public IHttpActionResult AllUpojela()
    {
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        IList<UpojelaInfo> upojelas = MapArea(repo.GetAllUpojela());

        return Content(HttpStatusCode.OK, upojelas); 
      }
    }
    private IList<UpojelaInfo> MapArea(IList<Area> areaList)
    {
      IList<UpojelaInfo> upojelaList = new List<UpojelaInfo>();
      foreach (Area area in areaList)
      {
        if (area is SubDistrict || area is MetropolitanCity)
        {
          UpojelaInfo info = new UpojelaInfo();
          info.AreaId = area.AreaId;
          info.Description = area.Description;
          if (area is SubDistrict)
          {
            if (area.Name.Contains("সদর"))
            {
              info.Name = area.Name;
            }
            else
            {
              info.Name = area.Name + " উপজেলা";
            }
            info.District = area.Parent.Name + " জেলা";
            info.Division = area.Parent.Parent.Name + " বিভাগ";
            info.AreaType = "SubDistrict";
          }
          if (area is MetropolitanCity)
          {
            info.Name = area.Name;
            info.Division = area.Parent.Name + " বিভাগ";
            info.AreaType = "MetropolitanCity";
          }
          upojelaList.Add(info);
        }
      }

      return upojelaList;
    }
  }

}