using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhelaGharAmsApi.ViewModel
{
  public class AsarDetailInfo : AsarInfo
  {
    public string Subdistrict { get; set; }
    public string District { get; set; }
    public string Division { get; set; }
    public string President { get; set; }
    public string PresidentMobileNo { get; set; }
    public string PresidentEmailAddress { get; set; }
    public string Secretary { get; set; }
    public string SecretaryMobileNo { get; set; }
    public string SecretaryEmailAddress { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
  }
}