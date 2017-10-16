using KhelagharMobileApps.Core.Models;
using KhelagharMobileApps.Core.Services;
using Plugin.Connectivity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KhelagharMobileApps.ViewModels
{
  public class MainPageViewModel : BindableBase, INavigationAware
  {
    private string _textToSearch = "";
    //private string _queryUrl = "Asar?name=আনন্দ";
    private string _queryUrl = "Asar?name=";
    private ObservableCollection<AsarInfo> _asarList;
    private int _asarCount;
    private readonly IKgApiService _apiService;
    private readonly INavigationService _navigationService;
    public DelegateCommand SearchCommand { get; set; }
    private DelegateCommand<ItemTappedEventArgs> _goToDetailPage = null;
    public MainPageViewModel(IKgApiService apiService, INavigationService navigationService)
    {
      _apiService = apiService;
      _navigationService = navigationService;
      SearchCommand = new DelegateCommand(Search);
    }
    private async void Search()
    {
      if (!CrossConnectivity.Current.IsConnected)
      {
        return;
      }
      IList<AsarInfo> asars = await _apiService.GetAsars(_queryUrl + _textToSearch);
      Asars = new ObservableCollection<AsarInfo>(asars);
      AsarCount = asars.Count;
    }
    public DelegateCommand<ItemTappedEventArgs> GoToDetailPage
    {
      get
      {
        if (_goToDetailPage == null)
        {
          _goToDetailPage = new DelegateCommand<ItemTappedEventArgs>(async selected =>
          {
            NavigationParameters param = new NavigationParameters();
            param.Add("show", selected.Item);
            await _navigationService.NavigateAsync("DetailPage", param);
          });
        }
        return _goToDetailPage;
      }
    }
    public string TextToSearch
    {
      get { return _textToSearch; }
      set { SetProperty(ref _textToSearch, value); }
    }
    public ObservableCollection<AsarInfo> Asars
    {
      get { return _asarList; }
      set { SetProperty(ref _asarList, value); }
    }
    public int AsarCount
    {
      get { return _asarCount; }
      set { SetProperty(ref _asarCount, value); }
    }
    public void OnNavigatedFrom(NavigationParameters parameters)
    {

    }
    public void OnNavigatingTo(NavigationParameters parameters)
    {
      
    }
    public void OnNavigatedTo(NavigationParameters parameters)
    {
      //await Task.Delay(1);
      //IList<AsarInfo> asars = await _apiService.GetAsars(_queryUrl);
      //Asars = new ObservableCollection<AsarInfo>(asars);
    }
  }
}
