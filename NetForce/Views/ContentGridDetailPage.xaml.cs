using CommunityToolkit.WinUI.UI.Animations;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using NetForce.Contracts.Services;
using NetForce.ViewModels;

namespace NetForce.Views;

public sealed partial class ContentGridDetailPage : Page
{
    public ContentGridDetailViewModel ViewModel
    {
        get;
    }

    public ContentGridDetailPage()
    {
        ViewModel = App.GetService<ContentGridDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.TemplateItem != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.TemplateItem);
            }
        }
    }
}
