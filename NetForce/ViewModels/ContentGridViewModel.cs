using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NetForce.Contracts.Services;
using NetForce.Contracts.ViewModels;
using NetForce.Core.Contracts.Services;
using NetForce.Core.Models;

namespace NetForce.ViewModels;

public class ContentGridViewModel : ObservableRecipient, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;
    private readonly IDataService _dataService;
    private readonly ICommandService _commandService;

    public ICommand ItemClickCommand
    {
        get;
    }

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();
    public ObservableCollection<TemplateModel> TModel{ get; } = new ObservableCollection<TemplateModel>();
    public ContentGridViewModel(INavigationService navigationService, 
                                ISampleDataService sampleDataService,
                                IDataService dataService,
                                ICommandService commandService)
    {
        _navigationService = navigationService;
        _sampleDataService = sampleDataService;
        _dataService = dataService;
        _commandService = commandService;

        ItemClickCommand = new RelayCommand<TemplateModel>(OnItemClick);
    }

    public async void OnNavigatedTo(object parameter)
    {
        //Source.Clear();
        TModel.Clear();
        // TODO: Replace with real data.
        //var data = await _sampleDataService.GetContentGridDataAsync();
        var _templateTable = await _commandService.ExecuteCommandAsync("dotnet", null, "new", "--list");
        var data = await _dataService.GetTemplateGridDataAsync(_templateTable);
        foreach (var item in data)
        {
            TModel.Add(item);
            //Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void OnItemClick(TemplateModel? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(ContentGridDetailViewModel).FullName!, clickedItem);
        }
    }
    //private void OnItemClick(SampleOrder? clickedItem)
    //{
    //    if (clickedItem != null)
    //    {
    //        _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
    //        _navigationService.NavigateTo(typeof(ContentGridDetailViewModel).FullName!, clickedItem.OrderID);
    //    }
    //}
}
