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
using KhelaGhar.AMS.Model.Domain.Committees;

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
        IList<CommitteeMember> workers = repo.GetFilteredRunningCommitteeMembers(asar.AsarId);
        
        if (workers.Count > 0)
        {
          Worker president = workers.Where(w => w.Designation.DesignationOrder == 1)
                                     .Select(s=>s.Worker).FirstOrDefault();
          if (president != null)
          {
            info.President = president.Name;
            info.PresidentMobileNo = president.MobileNo;
            info.PresidentEmailAddress = president.Email;
          }
          Worker secretary = workers.Where(w => w.Designation.DesignationOrder != 1)
                                     .Select(s => s.Worker)
                                     .OrderBy(o=>o.Name)
                                     .FirstOrDefault();
          if (secretary != null)
          {
            info.Secretary = secretary.Name;
            info.SecretaryMobileNo = secretary.MobileNo;
            info.SecretaryEmailAddress = secretary.Email;
          }
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

    [HttpGet]
    [Route("AllAsar")]
    public IHttpActionResult GetAllAsar()
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        asarList = MapAllAsar(repo.GetAllAsar());
        return Content(HttpStatusCode.OK, asarList);
      }
    }
    private IList<AsarInfo> MapAllAsar(IList<Asar> list)
    {
      IList<AsarInfo> asarList = new List<AsarInfo>();
      foreach (Asar asar in list)
      {
        AsarInfo info = new AsarInfo();
        info.AsarId = asar.AsarId;
        info.AsarName = asar.Name;
        info.NameTosearch = asar.Name.Replace(" খেলাঘর আসর","");
        info.CommitteeType = asar.CommitteeType.ToString();
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
        asarList.Add(info);
      }

      return asarList;
    }

    [HttpGet]
    [Route("Committee")]
    public IHttpActionResult GetCommitteeMembers(int asarId)
    {
      using (KhelaGharAMSDbContext dbContext = new KhelaGharAMSDbContext())
      {
        ApiRepository repo = new ApiRepository(dbContext);
        IList<WorkerInfo> workers = MapWorkerInfo(repo.GetRunningCommitteeMembers(asarId));
        return Content(HttpStatusCode.OK, workers);
      }
    }
    private IList<WorkerInfo> MapWorkerInfo(IList<CommitteeMember> members)
    {
      IList<WorkerInfo> workers = new List<WorkerInfo>();
      foreach(CommitteeMember member in members)
      {
        WorkerInfo worker = new WorkerInfo();
        worker.WorkerId = member.Worker.WorkerId;
        worker.Name = member.Worker.Name;
        worker.Designation = member.Designation.Name;
        worker.DesignationOrder = member.Designation.DesignationOrder;
        worker.MobileNo = member.Worker.MobileNo;
        worker.Email = member.Worker.Email;
        worker.AddressLine = member.Worker.AddressLine;
        workers.Add(worker);
      }
      return workers;
    }
  }
}