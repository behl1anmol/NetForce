using CommunityToolkit.Mvvm.ComponentModel;

using NetForce.Contracts.ViewModels;
using NetForce.Core.Contracts.Services;
using NetForce.Core.Models;
using NetForce.Core.Services;

namespace NetForce.ViewModels;

public class ContentGridDetailViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;
    private readonly IDataService _dataService;
    private readonly ICommandService _commandService;

    private TemplateModel? _templateItem;
    private SampleOrder? _item;

    public TemplateModel? TemplateItem
    {
        get => _templateItem;
        set => SetProperty(ref _templateItem, value);
    }

    public SampleOrder? Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    public ContentGridDetailViewModel(ISampleDataService sampleDataService
                                        , IDataService dataService,
                                ICommandService commandService)
    {
        _sampleDataService = sampleDataService;
        _dataService = dataService;
        _commandService = commandService;
    }

    public void OnNavigatedTo(object parameter)
    {
        TemplateItem = parameter as TemplateModel;
    }


    //public async void OnNavigatedTo(object parameter)
    //{
    //    if (parameter is long orderID)
    //    {
    //        var data = await _sampleDataService.GetContentGridDataAsync();
    //        Item = data.First(i => i.OrderID == orderID);
    //    }
    //}

    public void OnNavigatedFrom()
    {
    }
}
