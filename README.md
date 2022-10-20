
### Create a Monkey Service

We are ready to create a method that will retrieve the monkey data from the internet. We will first implement this with a simple HTTP request using HttpClient. We will do this inside of our `MonkeyService.cs` file that is located in the `Services` folder.

1. Inside of the `MonkeyService.cs`, let's add a new method to get all Monkeys:

    ```csharp
    List<Monkey> monkeyList = new ();
    public async Task<List<Monkey>> GetMonkeys()
    {
        return monkeyList;
    }
    ```

    Right now, the method simply creates a new list of Monkeys and returns it. We can now fill in the method use `HttpClient` to pull down a json file, parse it, cache it, and return it.

1. Let's get access to an `HttpClient` by added into the contructor for the `MonkeyService`.

    ```csharp
     HttpClient httpClient;
    public MonkeyService()
    {
        this.httpClient = new HttpClient();
    }
    ```

    .NET MAUI includes dependency injection similar to ASP.NET Core. We will register this service and dependencies soon.

1. Let's check to see if we have any monkeys in the list and return it if so by filling in the `GetMonkeys` method:

    ```csharp
    if (monkeyList?.Count > 0)
        return monkeyList;
    ```

1. We can use the `HttpClient` to make a web request and parse it using the built in `System.Text.Json` deserialization.

    ```csharp
    var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");

    if (response.IsSuccessStatusCode)
    {
        monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
    }
    
    return monkeyList;
    ```

1. Add the following using directive at the top of the file to access the `ReadFromJsonAsync` extension method:

    ```csharp
    using System.Net.Http.Json;
    ```

