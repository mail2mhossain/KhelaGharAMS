using DryIoc;
using Prism.DryIoc;
using KhelagharMobileApps.Views;
using Xamarin.Forms;
using KhelagharMobileApps.Core.Services;

namespace KhelagharMobileApps
{
  public partial class App : PrismApplication
  {
    public App(IPlatformInitializer initializer = null) : base(initializer) { }

    protected override void OnInitialized()
    {
      InitializeComponent();

      NavigationService.NavigateAsync("NavigationPage/MainPage?title=Hello%20from%20Xamarin.Forms");
    }

    protected override void RegisterTypes()
    {
      Container.RegisterTypeForNavigation<NavigationPage>();
      Container.RegisterTypeForNavigation<MainPage>();
      Container.Register<IKgApiService, KgApiService>();
    }
  }
}
