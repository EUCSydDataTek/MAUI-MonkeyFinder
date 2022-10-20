namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
    MonkeysViewModel viewModel;
    public MainPage(MonkeysViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.GetMonkeysCommand.Execute(null);
    }
}

