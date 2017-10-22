using KhelagharMobileApps.Core.Models;
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
    public DelegateCommand CallCommand { get; set; }
    public AsarInfo SelectedAsar
    {
      get { return _selectedAsar; }
      set { SetProperty(ref _selectedAsar, value); }
    }
    public DetailPageViewModel()
    {
      CallCommand = new DelegateCommand(MakeACall);
    }
    private void MakeACall()
    {
      _selectedAsar.SecretaryMobileNo = "+8801713032885";
    }
    public void OnNavigatedFrom(NavigationParameters parameters)
    {
    }
    public void OnNavigatedTo(NavigationParameters parameters)
    {
      SelectedAsar = parameters["show"] as AsarInfo;
    }
    public void OnNavigatingTo(NavigationParameters parameters)
    {
      
    }
  }
}
