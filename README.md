# 3.SelectionChangedCommand_PullToRefresh

Her benyttes CollectionView med `SelectionChangedCommand`. 
Virker dog ikke hvis der benyttes `Frame` i Templaten, men virker med `Border`:

```xml
<CollectionView ItemsSource="{Binding Monkeys}"
            SelectionMode="Single" 
            SelectionChangedCommand="{Binding GoToDetailsCommand}"
            SelectedItem="{Binding SelectedMonkey}" >
    <CollectionView.ItemTemplate>
        <DataTemplate x:DataType="model:Monkey">
            <Grid Padding="10">

                <Border HeightRequest="125" Style="{StaticResource CardView}">
                    <Grid Padding="0" ColumnDefinitions="125,*">
                        <Image Aspect="AspectFill" Source="{Binding Image}"
                            WidthRequest="125"
                            HeightRequest="125">
                        </Image>
                        <VerticalStackLayout Grid.Column="1" Padding="10">
                            <Label Style="{StaticResource LargeLabel}" Text="{Binding Name}" />
                            <Label Style="{StaticResource MediumLabel}" Text="{Binding Location}" />
                        </VerticalStackLayout>
                    </Grid>
                </Border>
            </Grid>
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

&nbsp;

Her kan man evt. stylen `CardView` i `App.xaml`, så det passer til `Border`:

```xml
<Style x:Key="CardView" TargetType="Border">
    <Setter Property="Stroke" Value="#DDDDDD"/>
    <Setter Property="StrokeThickness" Value="2"/>
    <Setter Property="Shadow"  Value="True"/>
    <Setter Property="Padding" Value="0"/>
    <Setter Property="Background" Value="{StaticResource LightBackground}"/>
    <Setter Property="StrokeShape" Value="RoundRectangle 10"/>
</Style>
```

&nbsp;

`GoToDetailsCommand` skal ikke tage imod noget object:

```csharp
async Task GoToDetails()
{
    await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
    {
        {"MyMonkey", SelectedMonkey }
    });
}
```

men benytter en property kaldet `SelectedMonkey`, som der blev bindet til i xaml-koden: `SelectedItem="{Binding SelectedMonkey}"`:

```csharp
public Monkey SelectedMonkey { get; set; }
```

