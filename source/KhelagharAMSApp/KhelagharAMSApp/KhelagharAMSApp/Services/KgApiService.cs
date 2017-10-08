using Acr.UserDialogs;
using KhelagharAMSApp.Models;
using Plugin.Connectivity;
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
      _baseUrl = MobileAPIUrl.Url;
    }
    public async Task<List<AsarInfo>> GetAsars(string name)
    {
      if (!CrossConnectivity.Current.IsConnected)
      {
        await UserDialogs.Instance.AlertAsync("You are offline");
        return new List<AsarInfo>();
      }
      return await Get<List<AsarInfo>>(name);
    }
  }
}
