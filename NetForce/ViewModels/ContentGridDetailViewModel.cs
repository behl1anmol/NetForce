using CommunityToolkit.Mvvm.ComponentModel;

using NetForce.Contracts.ViewModels;
using NetForce.Core.Contracts.Services;
using NetForce.Core.Models;
using NetForce.Core.Services;

namespace NetForce.ViewModels;

public class ContentGridDetailViewModel : ObservableRecipient, INavigationAware
{
    private TemplateModel? _templateItem;

    public TemplateModel? TemplateItem
    {
        get => _templateItem;
        set => SetProperty(ref _templateItem, value);
    }

    public ContentGridDetailViewModel()
    {
    }

    public void OnNavigatedTo(object parameter)
    {
        TemplateItem = parameter as TemplateModel;
    }

    public void OnNavigatedFrom()
    {
    }
}
