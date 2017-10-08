using KhelaGharAmsApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KhelaGharAmsApi.Controllers
{
    public class LoginController : ApiController
    {
    [HttpGet]
    [Route("Login")]
    public IHttpActionResult CheckLogin(string username, string password)
    {
      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
      ApplicationUser user = userManager.Find(username, password);

      if (user == null)
        return NotFound();
      else
        return Content(HttpStatusCode.OK, "Login Success");
    }
  }
}
