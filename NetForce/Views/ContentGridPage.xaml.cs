using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using NetForce.ViewModels;

namespace NetForce.Views;

public sealed partial class ContentGridPage : Page
{
    public ContentGridViewModel ViewModel
    {
        get;
    }

    public ContentGridPage()
    {
        ViewModel = App.GetService<ContentGridViewModel>();
        InitializeComponent();
    }
}
