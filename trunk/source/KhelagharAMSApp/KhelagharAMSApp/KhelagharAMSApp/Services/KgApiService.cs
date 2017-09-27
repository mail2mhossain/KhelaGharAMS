using KhelagharAMSApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharAMSApp.Services
{
  public class KgApiService : BaseProvider, IKgApiService
  {
    public KgApiService()
    {
      _baseUrl = "http://appsapi.khelaghar.net/";
    }
    public async Task<List<AsarInfo>> GetAsars(string name)
    {
      return await Get<List<AsarInfo>>(name);
    }
  }
}
