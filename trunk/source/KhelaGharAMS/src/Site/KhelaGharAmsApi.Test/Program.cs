using KhelaGharAmsApi.Test.Services;
using KhelagharMobileApps.Core.Models;
using KhelagharMobileApps.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelaGharAmsApi.Test
{
  class Program
  {
    private static string _baseUrl = "http://appsapi.khelaghar.net/Asar?name=আনন্দ";
    static void Main(string[] args)
    {
      //IList<AsarInfo> asars = GetAsars().GetAwaiter().GetResult();
      IList<AsarInfo> data = DataService.GetDataFromService("Asar?name=আনন্দ");
      foreach (AsarInfo info in data)
      {
        Console.WriteLine(info.AsarName);
      }
      Console.ReadLine();
    }
    private static async Task<List<AsarInfo>> GetAsars()
    {
      string name = "আনন্দ";
      IKgApiService apiService = new KgApiService();
      return await apiService.GetAsars(name);
    }
  }
}
