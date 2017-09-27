using KhelagharAMSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharAMSApp.Services
{
  public interface IKgApiService
  {
    Task<List<AsarInfo>> GetAsars(string queryUrl);
  }
}
