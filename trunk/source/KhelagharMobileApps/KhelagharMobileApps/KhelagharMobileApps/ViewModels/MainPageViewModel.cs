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
    private string _title;
    private string _queryUrl = "Asar?name=আনন্দ";
    private ObservableCollection<AsarInfo> _asarList;
    private readonly IKgApiService _apiService;
    public MainPageViewModel(IKgApiService apiService)
    {
      _apiService = apiService;
    }
    public ObservableCollection<AsarInfo> Asars
    {
      get { return _asarList; }
      set { SetProperty(ref _asarList, value); }
    }
    public string Title
    {
      get { return _title; }
      set { SetProperty(ref _title, value); }
    }
    public void OnNavigatedFrom(NavigationParameters parameters)
    {

    }
    public void OnNavigatingTo(NavigationParameters parameters)
    {
      
    }
    public void OnNavigatedTo(NavigationParameters parameters)
    {
      Task.Delay(1);
      IList<AsarInfo> asars = DataService.GetDataFromService(_queryUrl);
      if (parameters.ContainsKey("title"))
        Title = (string)parameters["title"] + " and Prism. Asars= " + asars.Count;
    }
  }
}
