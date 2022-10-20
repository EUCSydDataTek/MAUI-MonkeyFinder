# 1.TapGesture_PullToRefresh_

### Automatisk load af objekter når MainPage vises
Når man navigerer til MainPage bliver OnAppearing() metoden automatisk eksekveret. Herfra kan man eksekvere `viewModel.GetMonkeysCommand`:

```csharp
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
```

&nbsp;

### Pull-to-Refresh
Her oprettes et `RefreshView` udenfor `CollectionView`. Dette `RefreshView` skal binde `Command` til en Command, der kan levere friske data, samt binde
`IsRefreshing` til en property som man kan sætte til `false` når data er hentet. Den sætter den selv til `true` når man laver *Pull-to-Refresh*:

```xml
<RefreshView
    Grid.ColumnSpan="2"
    Command="{Binding GetMonkeysCommand}"
    IsRefreshing="{Binding IsRefreshing}">

    <CollectionView ItemsSource="{Binding Monkeys}">

        <CollectionView.ItemTemplate>
            ...
        </CollectionView>

</RefreshView>
```

&nbsp;

### Tap-and-Go to MonkeyDetails
Her benyttes en `TapGestureRecognizer`, som f.eks. kan bo i `<Frame>`-elementet:

```xml
<Frame.GestureRecognizers>
    <TapGestureRecognizer 
    Command="{Binding Source={RelativeSource AncestorType={x:Type viewmodel:MonkeysViewModel}}, Path=GoToDetailsCommand}"
    CommandParameter="{Binding .}"/>
</Frame.GestureRecognizers>
```

I MonkeysViewModel findes `GoToDetailsCommand`, som sørger for at navigere til DetailsPage:

```csharp
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
```

I `MonkeyDetailsViewModel` er klassen forberedt til at modtage et `Monkey`-objekt vha. *Shells QueryString navigation*. Der er oprettet
en `Monkey` property, som objektet kan afleveres til. Herefter er det en smal sag at binde View'et til denne property.

```csharp
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
```