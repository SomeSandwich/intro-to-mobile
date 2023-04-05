using Api.Context.Entities;
using API.Utils;
using Bogus;

namespace Api.Context.GenerateData;

public static class FakerGenerating
{
    private static int s_userId = 1;
    public static List<User> Users { get; set; } = new List<User>();

    public static List<Category> Categories { get; set; } = new List<Category>()
    {
        new Category { Id = 1, Description = "Áo", Icon = "icons/" },
        new Category { Id = 2, Description = "Quần", Icon = "icons/" },
        new Category { Id = 3, Description = "Váy", Icon = "icons/" },
        new Category { Id = 4, Description = "Áo khoác", Icon = "icons/" },
        new Category { Id = 5, Description = "Mũ", Icon = "icons/" }
    };

    private static int s_rateId = 1;
    public static List<Rate> Rates { get; set; } = new List<Rate>();

    private static int s_postId = 1;
    public static List<Post> Posts { get; set; } = new List<Post>();

    private static Faker f;

    public static void Init()
    {
        f = new Faker();

        GenUser(5);
        GenPost();

        Console.WriteLine($"UserCount: {Users.Count}");
        Console.WriteLine($"CategoryCount: {Categories.Count}");
        Console.WriteLine($"PostCount: {Posts.Count}");
        Console.WriteLine($"RateCount: {Rates.Count}");
    }

    private static void GenUser(int count)
    {
        for (int i = 0; i < count; i++, s_userId++)
        {
            Console.WriteLine($"Begin Gen user:{s_userId}");

            var user = new User
            {
                Id = s_userId,
                Name = (s_userId == 1) ? "Hiếu Nguyễn" : f.Name.FullName(),
                Email = (s_userId == 1) ? "hieucckha@gmail.com" : f.Internet.Email(),
                PhoneNumber = f.Phone.PhoneNumber("##########"),
                PasswordHash = "123".HashPassword(),
                Address = f.Address.FullAddress()
            };

            Users.Add(user);

            // var postCount = f.Random.Number(3, 5);
            // GenPost(s_userId, postCount);

            Console.WriteLine($"Gen Success user:{user.Id} - {user.Name}");
        }
    }

    private static void GenPost()
    {
        Console.WriteLine($"In GenRate: PostCount: {Posts.Count}");
        Console.WriteLine($"In GenRate: RateCount: {Rates.Count}");

        foreach (var user in Users)
        {
            var postCount = f.Random.Number(3, 5);

            for (int i = 0; i < postCount; i++, s_postId++)
            {
                Console.WriteLine($"Begin Gen User{user.Id} - PostIndex:{i}");

                var post = new Post
                {
                    Id = s_postId,
                    Price = f.Random.Number(50_000, 300_000),
                    Caption = f.Lorem.Sentence(5, 15),
                    Description = f.Lorem.Paragraph(4),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsSold = true,
                    // RateId = GenRate(user.Id, s_postId),
                    UserId = user.Id,
                    CategoryId = f.PickRandom<Category>(Categories).Id
                };

                Posts.Add(post);

                var rateId = GenRate(user.Id, s_postId);

                Console.WriteLine($"Gen Success User{user.Id} - PostIndex:{i}");

                Console.WriteLine($"IP GenRate: PostCount: {Posts.Count}");
                Console.WriteLine($"IP GenRate: RateCount: {Rates.Count}");
            }
        }
    }

    private static int GenRate(int userId, int postId)
    {
        int ratingUserId;
        do
        {
            ratingUserId = f.PickRandom<User>(Users).Id;
        } while (ratingUserId == userId);

        var rate = new Rate
        {
            Id = s_rateId++,
            RatingAt = DateTime.Now,
            Comment = f.Lorem.Sentences(2, " "),
            UserId = ratingUserId,
            PostId = postId
        };

        Rates.Add(rate);

        Console.WriteLine($"IN GenRate: PostCount: {Posts.Count}");
        Console.WriteLine(
            $"IN GenRate: RateCount: {Rates.Count} - {rate.Id} - {rate.Comment} - {rate.UserId} - {rate.PostId}");

        return s_rateId;
    }
}
