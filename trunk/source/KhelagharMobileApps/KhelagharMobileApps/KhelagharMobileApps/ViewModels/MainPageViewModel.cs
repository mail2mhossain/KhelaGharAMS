using KhelagharMobileApps.Core.Models;
using KhelagharMobileApps.Core.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace KhelagharMobileApps.ViewModels
{
  public class MainPageViewModel : BindableBase, INavigationAware
  {
    private string _textToSearch = "";
    //private string _queryUrl = "Asar?name=আনন্দ";
    private string _queryUrl = "Asar?name=";
    private ObservableCollection<AsarInfo> _asarList;
    private readonly IKgApiService _apiService;
    public DelegateCommand SearchCommand { get; set; }
    public MainPageViewModel(IKgApiService apiService)
    {
      _apiService = apiService;
      SearchCommand = new DelegateCommand(Search);
    }
    private async void Search()
    {
      IList<AsarInfo> asars = await _apiService.GetAsars(_queryUrl+ _textToSearch);
      Asars = new ObservableCollection<AsarInfo>(asars);
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
    public void OnNavigatedFrom(NavigationParameters parameters)
    {

    }
    public void OnNavigatingTo(NavigationParameters parameters)
    {
      
    }
    public async void OnNavigatedTo(NavigationParameters parameters)
    {
      //await Task.Delay(1);
      //IList<AsarInfo> asars = await _apiService.GetAsars(_queryUrl);
      //Asars = new ObservableCollection<AsarInfo>(asars);
    }
  }
}
