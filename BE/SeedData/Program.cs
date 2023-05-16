// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using RestSharp;
using RestSharp.Authenticators;

// Prepare data
using var reader = new StreamReader("Clothes.csv");
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

var records = csv.GetRecords<Post>();

var preparePosts = new List<List<Post>>
{
    new(),
    new(),
    new(),
    new(),
    new()
};

var index = 0;
foreach (var post in records)
{
    Console.WriteLine($"ID: {post.Id}");
    Console.WriteLine($"Caption: {post.Caption}");
    Console.WriteLine($"Price: {post.Price}");
    Console.WriteLine($"Description: {post.Description}");
    Console.WriteLine($"MediaPath: {post.MediaPath}");
    Console.WriteLine($"CategoryId: {post.CategoryId}");
    Console.WriteLine();

    preparePosts.ElementAt(index).Add(post);

    index = index == preparePosts.Count - 1 ? 0 : ++index;
}


var credentials = new List<LoginReq>
{
    new() { Email = "hieucckha@gmail.com", Password = "123" },
    new() { Email = "baokyo002@gmail.com", Password = "123456789" },
    new() { Email = "truongtrongkhanh@gmail.com", Password = "123456" },
    new() { Email = "test1@gmail.com", Password = "123" },
    new() { Email = "test2@gmail.com", Password = "123" }
};

index = 0;
foreach (var posts in preparePosts)
{
    const string API_LINK = "https://localhost:7240/api/v1";

    // Post Client
    var unauthorizedOption = new RestClientOptions(API_LINK);
    var unauthorizedClient = new RestClient(unauthorizedOption);

    var loginReqCall = new RestRequest("auth/login")
        .AddJsonBody(credentials.ElementAt(index));
    var response = await unauthorizedClient.PostAsync<LoginRes>(loginReqCall);

    var token = response?.Token;

    var option = new RestClientOptions(API_LINK) { Authenticator = new JwtAuthenticator(token) };
    var client = new RestClient(option);

    foreach (var post in posts)
    {
        var uploadPostCall = new RestRequest("post")
            .AddParameter("CategoryId", post.CategoryId)
            .AddParameter("Price", post.Price)
            .AddParameter("Caption", post.Caption)
            .AddParameter("Description", post.Description)
            .AddFile("MediaFiles", $"img/{post.MediaPath}");

        var res = await client.PostAsync<SuccessRes>(uploadPostCall);
        Console.WriteLine($"ID: {post.Id}: {res.Message}");
    }

    index++;
}

public class Post
{
    public int Id { get; set; }
    public string Caption { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string MediaPath { get; set; }
    public int CategoryId { get; set; }
}

public class LoginReq
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginRes
{
    public string Token { get; set; }
}

public class SuccessRes
{
    public string Message { get; set; }
}
