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
    private readonly IDataService _dataService;
    private readonly ICommandService _commandService;

    public ICommand ItemClickCommand { get; }

    public ObservableCollection<TemplateModel> TModelFiltered { get; } = new ObservableCollection<TemplateModel>();
    public ObservableCollection<TemplateModel> TModel { get; } = new ObservableCollection<TemplateModel>();

    public ObservableCollection<string> TagsFilterSet { get; } = new ObservableCollection<string>() {"All Tags"};

    public ObservableCollection<string> LanguageFilterSet { get; } = new ObservableCollection<string>() { "All Languages", "C#", "F#", "VB" };


    private int _tagsComboboxValue = 0;

    public int TagsComboboxValue
    {
        get => _tagsComboboxValue;
        set => SetProperty(ref _tagsComboboxValue, value);
    }

    private int _languageComboboxValue = 0;

    public int LanguageComboboxValue
    {
        get => _languageComboboxValue;
        set => SetProperty(ref _languageComboboxValue, value);
    }

    public ContentGridViewModel(INavigationService navigationService,
                                IDataService dataService,
                                ICommandService commandService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
        _commandService = commandService;

        ItemClickCommand = new RelayCommand<TemplateModel>(OnItemClick);
    }

    public async void OnNavigatedTo(object parameter)
    {
        TModel.Clear();
        TModelFiltered.Clear();
        TagsFilterSet.Clear();
        TagsFilterSet.Add("All Tags");
        var _templateTable = await _commandService.ExecuteCommandAsync("dotnet", null, "new", "--list");
        var data = await _dataService.GetTemplateGridDataAsync(_templateTable);
        
        foreach (var item in _dataService.TagsSet)
        {
            TagsFilterSet.Add(item);
        }

        foreach (var item in data)
        {
            TModel.Add(item);
        }
        FilteredView();
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

    public void FilteredView()
    {
        TModelFiltered.Clear();
        var tag = TagsFilterSet.ElementAt(TagsComboboxValue);
        var lang = LanguageFilterSet.ElementAt(LanguageComboboxValue);
        if (tag.Equals("All Tags") && lang.Equals("All Languages"))
        {
            foreach (var item in TModel)
            {
                TModelFiltered.Add(item);
            }
        }
        else if(!tag.Equals("All Tags") && lang.Equals("All Languages"))
        {
            foreach (var item in TModel)
            {
                if (item.Tags.Contains(tag))
                {
                    TModelFiltered.Add(item);
                }
            }
        }
        else if(!lang.Equals("All Languages") && tag.Equals("All Tags"))
        {
            foreach (var item in TModel)
            {
                if (item.Language.Contains(lang) && !TModelFiltered.Contains(item))
                {
                    TModelFiltered.Add(item);
                }
            }
        }
        else
        {
            foreach (var item in TModel)
            {
                if (item.Language.Contains(lang) && item.Tags.Contains(tag))
                {
                    TModelFiltered.Add(item);
                }
            }
        }
    }


}
