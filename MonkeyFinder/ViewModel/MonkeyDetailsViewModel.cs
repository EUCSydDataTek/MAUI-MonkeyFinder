namespace MonkeyFinder.ViewModel;

[QueryProperty(nameof(Monkey), "MyMonkey")]
public class MonkeyDetailsViewModel : BaseViewModel
{
    Monkey monkey;
	public Monkey Monkey
	{
		get => monkey;
        set => SetProperty(ref monkey, value); 
    }
}
