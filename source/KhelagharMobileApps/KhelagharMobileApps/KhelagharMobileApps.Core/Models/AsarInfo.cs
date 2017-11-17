using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharMobileApps.Core.Models
{
  public class AsarInfo
  {
    public int AsarId { get; set; }
    public string AsarName { get; set; }
    public string CommitteeType { get; set; }
    public string AsarStatus { get; set; }
    public string AddressLine { get; set; }
    public string Subdistrict { get; set; }
    public string District { get; set; }
    public string Division { get; set; }
    public string President { get; set; }
    public string PresidentMobileNo { get; set; }
    public string PresidentEmailAddress { get; set; }
    public string Secretary { get; set; }
    public string SecretaryMobileNo { get; set; }
    public string SecretaryEmailAddress { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    
    public string Address
    {
      get
      {
        string address = String.IsNullOrEmpty(AddressLine) ? Subdistrict : AddressLine +  Environment.NewLine + Subdistrict;
        if (District != null)
        {
          address = address + ", জেলা-" + District;
          address = address + ", বিভাগ-" + Division;
        }
        else
        {
          address = address + ", বিভাগ-" + Division;
        }
        return address;
      }
    }
    public string AddressForDetailView
    {
      get
      {
        string address = String.IsNullOrEmpty(AddressLine) ? Subdistrict : AddressLine + ", " + Subdistrict;
        if (District != null)
        {
          address = address + ", জেলা-" + District;
          address = address + ", বিভাগ-" + Division;
        }
        else
        {
          address = address + ", বিভাগ-" + Division;
        }
        return address;
      }
    }
    public string DisplayCommitteeType
    {
      get
      {
        return "কমিটির ধরণ - " + CommitteeType;
      }
    }
    public string DisplayAsarStatus
    {
      get
      {
        return "আসরের অবস্থা - " + AsarStatus;
      }
    }
    public string SecretaryMobile
    {
      get
      {
        return "মোবাইল নং - " + SecretaryMobileNo;
      }
    }
    public string SecretaryName
    {
      get
      {
        return "সাধারণ সম্পাদক - " + Secretary ;
      }
    }
    public bool HasMobileNo
    {
      get
      {
        if (this.SecretaryMobileNo != null)
          return true;
        return false;
      }
    }
  }
}
