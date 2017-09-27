using System;
using System.Linq;
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
    private string _queryUrl = "Asar?name=";
    private ListView _asarList;
    private IList<AsarInfo> _asarInfoList = new List<AsarInfo>();
    IList<string> _asars = new List<string>();

    protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
      _asarList = FindViewById<ListView>(Resource.Id.listView);
      Button button = FindViewById<Button>(Resource.Id.AsarSearchBtn);

      button.Click += Button_Click;
    }
    private async void Button_Click(object sender, EventArgs e)
    {
      EditText asarNameEntry = FindViewById<EditText>(Resource.Id.AsarNameEntry);

      if (!String.IsNullOrEmpty(asarNameEntry.Text))
      {
        IKgApiService apiService = new KgApiService();
        _asarInfoList = await apiService.GetAsars(_queryUrl + asarNameEntry.Text);

        foreach (AsarInfo asar in _asarInfoList)
        {
          string asarNameWithAddress = asar.AsarName;
          if (!String.IsNullOrEmpty(asar.AddressLine))
          {
            asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine + asar.AddressLine;
            asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine + asar.Subdistrict;
            asarNameWithAddress = asarNameWithAddress + ", " + asar.District;
          }
          else
          {
            asarNameWithAddress = asarNameWithAddress + System.Environment.NewLine;
            asarNameWithAddress = asarNameWithAddress + asar.Subdistrict;
            asarNameWithAddress = asarNameWithAddress + ", " + asar.District;
          }
          _asars.Add(asarNameWithAddress);
        }
        ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _asars);
        _asarList.Adapter = adapter;
        _asarList.ItemClick += asarList_ItemClick;
      }
    }
    private void asarList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
    {
      string selectedAsar = _asars[e.Position];
      string[] asarName = selectedAsar.Split('\n');
      AsarInfo asar = _asarInfoList.Where(w => w.AsarName == asarName[0]).FirstOrDefault();
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


