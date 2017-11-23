using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhelaGharAmsApi.ViewModel
{
  public class WorkerInfo
  {
    public int WorkerId { get; set; }
    public string Name { get; set; }
    public string Designation { get; set; }
    public int DesignationOrder { get; set; }
    public string MobileNo { get; set; }
    public string Email { get; set; }
    public string AddressLine { get; set; }

  }
}