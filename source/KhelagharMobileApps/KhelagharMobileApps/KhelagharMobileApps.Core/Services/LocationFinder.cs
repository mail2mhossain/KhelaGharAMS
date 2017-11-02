using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhelagharMobileApps.Core.Services
{
  public class LocationFinder
  {
    public async Task<Position> GetCurrentLocation()
    {
      Position position = null;
      try
      {
        var locator = CrossGeolocator.Current;
        locator.DesiredAccuracy = 100;

        position = await locator.GetLastKnownLocationAsync();

        if (position != null)
        {
          //got a cahched position, so let's use it.
          return position;
        }

        if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
        {
          //not available or enabled
          return position;
        }

        position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

      }
      catch (Exception ex)
      {
        //Display error as we have timed out or can't get location.
      }

      return position;
    }
    private async Task StartListening()
    {
      if (CrossGeolocator.Current.IsListening)
        return;

      await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

      CrossGeolocator.Current.PositionChanged += PositionChanged;
      CrossGeolocator.Current.PositionError += PositionError;
    }
    private void PositionChanged(object sender, PositionEventArgs e)
    {
      //If updating the UI, ensure you invoke on main thread
      var position = e.Position;
      

      //var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
      //output += "\n" + $"Time: {position.Timestamp}";
      //output += "\n" + $"Heading: {position.Heading}";
      //output += "\n" + $"Speed: {position.Speed}";
      //output += "\n" + $"Accuracy: {position.Accuracy}";
      //output += "\n" + $"Altitude: {position.Altitude}";
      //output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
      //Debug.WriteLine(output);
    }
    private void PositionError(object sender, PositionErrorEventArgs e)
    {
      //Debug.WriteLine(e.Error);
      //Handle event here for errors
    }
    private void StopListening()
    {
      if (!CrossGeolocator.Current.IsListening)
        return;

      CrossGeolocator.Current.StopListeningAsync();

      CrossGeolocator.Current.PositionChanged -= PositionChanged;
      CrossGeolocator.Current.PositionError -= PositionError;
    }
  }
}
