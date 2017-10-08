using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharMobileApps.Core.Exceptions
{
  public class KGAppsApiException : Exception
  {
    public HttpStatusCode StatusCode { get; set; }
    public bool Connection { get; set; }

    public KGAppsApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
      StatusCode = statusCode;
      Connection = true;
    }
    public KGAppsApiException(string message, bool connection, Exception inner)
            : base(message, inner)
        {
      Connection = connection;
      StatusCode = HttpStatusCode.ServiceUnavailable;
    }
  }

  public class KhelagharAppsApiError
  {
    public string Message { get; set; }
  }
}
