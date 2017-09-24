using KhelagharMobileApps.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharMobileApps.Core.Services
{
  public class BaseProvider
  {
    protected string _baseUrl;

    protected HttpClient GetClient()
    {
      return GetClient(_baseUrl);
    }
    protected virtual HttpClient GetClient(string baseUrl)
    {
      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(baseUrl);

      return client;
    }
    protected async Task Get(string url)
    {
      using (HttpClient client = GetClient())
      {
        try
        {
          var response = await client.GetAsync(url);
          if (!response.IsSuccessStatusCode)
          {
            var error = await response.Content.ReadAsAsync<KhelagharAppsApiError>();
            throw new KGAppsApiException(error.Message, response.StatusCode);
          }
        }
        catch (HttpRequestException ex)
        {
          throw new KGAppsApiException("", false, ex);
        }
      }
    }
    protected async Task<T> Get<T>(string url)
    {
      using (HttpClient client = GetClient())
      {
        try
        {
          var response = await client.GetAsync(url);
          if (!response.IsSuccessStatusCode)
          {
            var error = await response.Content.ReadAsAsync<KhelagharAppsApiError>();
            var message = error != null ? error.Message : "";
            throw new KGAppsApiException(message, response.StatusCode);
          }
          return await response.Content.ReadAsAsync<T>();
        }
        catch (HttpRequestException ex)
        {
          throw new KGAppsApiException("", false, ex);
        }
        catch (UnsupportedMediaTypeException ex)
        {
          throw new KGAppsApiException("", false, ex);
        }
      }
    }
  }
}
