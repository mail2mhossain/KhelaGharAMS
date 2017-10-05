using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using KhelagharAMSApp.Models;
using System.Collections.Generic;
using KhelagharAMSApp.Services;

namespace KhelagharAMSApp.Droid
{
  [Activity(Label = "Khelaghar AMS App", MainLauncher = false, Icon = "@drawable/icon")]
  public class MainActivity : Activity
	{
    //private string _queryUrl = "Asar?name=";
    private ListView _asarList;

    protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
      _asarList = FindViewById<ListView>(Resource.Id.listView);
      Button button = FindViewById<Button>(Resource.Id.AsarSearchBtn);
      RadioButton asarRadioButton = FindViewById<RadioButton>(Resource.Id.radio_asar);
      asarRadioButton.Checked = true;
      button.Click += Button_Click;
    }
    private async void Button_Click(object sender, EventArgs e)
    {
      EditText asarNameEntry = FindViewById<EditText>(Resource.Id.AsarNameEntry);

      if (!String.IsNullOrEmpty(asarNameEntry.Text))
      {
        RadioGroup radioGroup = FindViewById<RadioGroup>(Resource.Id.radio_search_group);
        RadioButton radioButton = FindViewById<RadioButton>(radioGroup.CheckedRadioButtonId);
        IKgApiService apiService = new KgApiService();
        string queryUrl = GetQueryUrl(radioButton);
        IList<AsarInfo> asarInfoList = await apiService.GetAsars(queryUrl + asarNameEntry.Text);
        IList<string> asars = GetFormattedAsarList(asarInfoList);
        ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, asars);
        _asarList.Adapter = adapter;
      }
    }
    private IList<string> GetFormattedAsarList(IList<AsarInfo> asarInfoList)
    {
      IList<string> asars = new List<string>();
      int i = 1;
      foreach (AsarInfo asar in asarInfoList)
      {
        string asarNameWithAddress = GetSerialNo(i) + asar.AsarName;
        if (!String.IsNullOrEmpty(asar.AddressLine))
        {
          asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine + asar.AddressLine;
          asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine + asar.Subdistrict;
          if (asar.District != null)
          {
            asarNameWithAddress = asarNameWithAddress + ", " + asar.District;
            asarNameWithAddress = asarNameWithAddress + ", " + asar.Division;
          }
          else
          {
            asarNameWithAddress = asarNameWithAddress + ", " + asar.Division;
          }
        }
        else
        {
          asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine;
          asarNameWithAddress = asarNameWithAddress + asar.Subdistrict;
          if (asar.District != null)
          {
            asarNameWithAddress = asarNameWithAddress + ", " + asar.District;
            asarNameWithAddress = asarNameWithAddress + ", " + asar.Division;
          }
          else
          {
            asarNameWithAddress = asarNameWithAddress + ", " + asar.Division;
          }
        }
        asars.Add(asarNameWithAddress);
        i = i + 1;
      }
      return asars;
    }
    private string GetQueryUrl(RadioButton radioButton)
    {
      string queryUrl = "Asar?name=";
      switch (radioButton.Id)
      {
        case Resource.Id.radio_asar:
          queryUrl = "Asar?name=";
          return queryUrl;
        case Resource.Id.radio_upojela:
          queryUrl = "Upojela?upojela=";
          return queryUrl;
        case Resource.Id.radio_jela:
          queryUrl = "Jela?jela=";
          return queryUrl;
      }
      return queryUrl;
    }
    private string GetSerialNo(int serialNo)
    {
      if (serialNo < 10)
        return "0" + serialNo + ". ";
      return serialNo + ". ";
    }
    public override void OnBackPressed()
    {
      Finish();
    }
    protected override void OnDestroy()
    {
      base.OnDestroy();
    }
  }
}


