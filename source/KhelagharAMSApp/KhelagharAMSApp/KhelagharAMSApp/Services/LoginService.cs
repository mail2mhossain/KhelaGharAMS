using Acr.UserDialogs;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharAMSApp.Services
{
  public class LoginService
  {
    private string success = "";
    public string Login(string username, string password, string authenticationUserName, string authenticationUserPassword)
    {
      try
      {
        if(!CrossConnectivity.Current.IsConnected)
        {
          UserDialogs.Instance.AlertAsync("You are offline");
          success = "Failure";
          return success;
        }
        string LoginUrl = MobileAPIUrl.Url + "Login?username=" + username + "&password=" + password;
        Task.Run(() => LoadDataAsync(LoginUrl, authenticationUserName, authenticationUserPassword)).Wait();
      }
      catch (Exception ex)
      {

      }
      return success;
    }
    private void LoadDataAsync(string uri, string authenticationUserName, string authenticationUserPassword)
    {
      if (success != null)
      {
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        httpClientHandler.AllowAutoRedirect = false;

        using (var httpClient = new HttpClient(httpClientHandler))
        {
          var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", authenticationUserName, authenticationUserPassword)));
          try
          {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            httpClient.Timeout = new TimeSpan(0, 0, 10);
            Task<HttpResponseMessage> getResponse = httpClient.GetAsync(uri);
            HttpResponseMessage response = getResponse.Result;
            if ((int)response.StatusCode == 404)
            {
              success = "Failure";
            }
            else if ((int)response.StatusCode == 500)
            {
              success = "Internal Server Error";
            }
            else if ((int)response.StatusCode == 200)
            {
              success = "Login Success";
            }
            else
            {
              success = "Wrong User Id";
            }
          }
          catch (TimeoutException ex)
          {

          }
          catch (TaskCanceledException ex)
          {
            throw ex;// DisplayAlert("Alert", "You have been alerted", "OK");
          }
        }
      }
    }
  }
}
