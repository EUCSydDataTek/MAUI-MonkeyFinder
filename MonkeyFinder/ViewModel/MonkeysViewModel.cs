using MonkeyFinder.Services;
using System.Windows.Input;

namespace MonkeyFinder.ViewModel;

public class MonkeysViewModel : BaseViewModel
{
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    private readonly MonkeyService monkeyService;
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
    }

    // New Property
    bool isRefreshing;
    public bool IsRefreshing 
    { 
        get => isRefreshing;
        set => SetProperty(ref isRefreshing, value);
    }

    private Command getMonkeysCommand;
    public ICommand GetMonkeysCommand => getMonkeysCommand ??= new Command(async () => await GetMonkeysAsync());

    async Task GetMonkeysAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            List<Monkey> monkeys = await monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                Monkeys.Clear();

            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;   // New
        }
    }


    private Command goToDetailsCommand;
    public ICommand GoToDetailsCommand => goToDetailsCommand ??= new Command<Monkey>(async (monkey) => await GoToDetails(monkey));
    async Task GoToDetails(Monkey monkey)
    {
        if (monkey == null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"MyMonkey", monkey }
        });
    }
}
