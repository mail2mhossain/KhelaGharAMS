using Acr.UserDialogs;
using KhelagharMobileApps.Core.Models;
using KhelagharMobileApps.Core.Services;
using Plugin.ExternalMaps;
using Plugin.Geolocator.Abstractions;
using Plugin.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KhelagharMobileApps.ViewModels
{
  public class DetailPageViewModel : BindableBase, INavigationAware
  {
    private AsarInfo _selectedAsar;
    private Position _position = null;
    private string _geoLocation = String.Empty;
    private string _committeeType = String.Empty;
    public DelegateCommand CallCommand { get; set; }
    public DelegateCommand NavigateTo { get; set; }
    public DelegateCommand UpdateLocationCommand { get; set; }
    public AsarInfo SelectedAsar
    {
      get { return _selectedAsar; }
      set { SetProperty(ref _selectedAsar, value);}
    }
    public string GeoLocation
    {
      get { return _geoLocation; }
      set { SetProperty(ref _geoLocation, value); }
    }
    public DetailPageViewModel()
    {
      CallCommand = new DelegateCommand(MakeACall);
      NavigateTo = new DelegateCommand(NavigateToMap);
      UpdateLocationCommand = new DelegateCommand(UpdateLocation);
    }

    private void UpdateLocation()
    {
      int id = _selectedAsar.AsarId;

    }

    private async void MakeACall()
    {
      // Make Phone Call
      var phoneDialer = CrossMessaging.Current.PhoneDialer;
      if (phoneDialer.CanMakePhoneCall)
      {
        if (_selectedAsar.SecretaryMobileNo != null)
          phoneDialer.MakePhoneCall(_selectedAsar.SecretaryMobileNo);
      }
      else
        await UserDialogs.Instance.AlertAsync("You do not have permission.");

      // Send Sms
      //var smsMessenger = CrossMessaging.Current.SmsMessenger;
      //if (smsMessenger.CanSendSms)
      //  smsMessenger.SendSms("+27213894839493", "Well hello there from Xam.Messaging.Plugin");

      //var emailMessenger = CrossMessaging.Current.EmailMessenger;
      //if (emailMessenger.CanSendEmail)
      //{
      //  // Send simple e-mail to single receiver without attachments, bcc, cc etc.
      //  emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

      //  // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc. 
      //  var email = new EmailMessageBuilder()
      //    .To("to.plugins@xamarin.com")
      //    .Cc("cc.plugins@xamarin.com")
      //    .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
      //    .Subject("Xamarin Messaging Plugin")
      //    .Body("Well hello there from Xam.Messaging.Plugin")
      //    .Build();

      //  emailMessenger.SendEmail(email);
      //}
    }
    private void NavigateToMap()
    {
      if(_position !=null)
        CrossExternalMaps.Current.NavigateTo("AplombTech", _position.Latitude + 1, _position.Longitude);
    }
    public void OnNavigatedFrom(NavigationParameters parameters)
    {
    }
    public void OnNavigatedTo(NavigationParameters parameters)
    {
      SelectedAsar = parameters["show"] as AsarInfo;
    }
    public async void OnNavigatingTo(NavigationParameters parameters)
    {
      LocationFinder finder = new LocationFinder();
      _position = await finder.GetCurrentLocation();
      if (_position != null)
        GeoLocation = "Lat-" + _position.Latitude + ", Lon-" + _position.Longitude;
    }
  }
}
