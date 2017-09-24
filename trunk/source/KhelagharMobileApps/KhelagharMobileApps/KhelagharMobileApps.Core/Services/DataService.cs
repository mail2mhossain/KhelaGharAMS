using KhelagharMobileApps.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharMobileApps.Core.Services
{
  public class DataService
  {
    private static string _baseUrl = "http://appsapi.khelaghar.net/";
    public static IList<AsarInfo> GetDataFromService(string queryString)
    {
      HttpClient client = new HttpClient();
      List<AsarInfo> data = new List<AsarInfo>();
      try
      {
        Task<HttpResponseMessage> getResponse = client.GetAsync(_baseUrl + queryString);
        HttpResponseMessage response = getResponse.Result;

        if (response != null)
        {
          string json = response.Content.ReadAsStringAsync().Result;
          data = JsonConvert.DeserializeObject<List<AsarInfo>>(json);
        }
      }
      catch (Exception ex)
      {
        string errorMessage = ex.ToString();
      }
      return data;
    }
  }
}
