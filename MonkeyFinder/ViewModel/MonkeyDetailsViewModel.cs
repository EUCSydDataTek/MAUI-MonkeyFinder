using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "MyMonkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey monkey;
}
