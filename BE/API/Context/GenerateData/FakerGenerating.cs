using Api.Context.Entities;
using API.Utils;
using Bogus;

namespace Api.Context.GenerateData;

public static class FakerGenerating
{
    private static int s_userId = 1;
    public static List<User> Users { get; set; } = new();

    public static List<Category> Categories { get; set; } = new()
    {
        new Category { Id = 1, Description = "Áo", Icon = "icons/" },
        new Category { Id = 2, Description = "Quần", Icon = "icons/" },
        new Category { Id = 3, Description = "Váy", Icon = "icons/" },
        new Category { Id = 4, Description = "Áo khoác", Icon = "icons/" },
        new Category { Id = 5, Description = "Mũ", Icon = "icons/" }
    };

    private static List<string> Avatars { get; set; } = new List<string>()
    {
        "D7aD6H8Cvc3SUU",
        "F6eByvDeQ5MlVr",
        "ha50CMvp9KlVrc",
        "RzKj4nt5aX_tfe",
        "UkmH7xfQ7o-3tY",
        "yH_rHI5emLoaxj"
    };

    private static int s_rateId = 1;
    public static List<Rate> Rates { get; set; } = new();

    private static int s_postId = 1;
    public static List<Post> Posts { get; set; } = new();

    private static Faker f;

    public static void Init()
    {
        f = new Faker();

        GenUser(5);
        GenPost();
    }

    private static void GenUser(int count)
    {
        for (int i = 0; i < count; i++, s_userId++)
        {
            var user = new User
            {
                Id = s_userId,
                Name = (s_userId == 1) ? "Hiếu Nguyễn" : f.Name.FullName(),
                Email = (s_userId == 1) ? "hieucckha@gmail.com" : f.Internet.Email(),
                AvatarPath = f.PickRandom(Avatars),
                PhoneNumber = f.Phone.PhoneNumber("##########"),
                PasswordHash = "123".HashPassword(),
                Address = f.Address.FullAddress()
            };

            Users.Add(user);

            Console.WriteLine($"Gen Success user:{user.Id} - {user.Name}");
        }
    }

    private static void GenPost()
    {
        foreach (var user in Users)
        {
            var postCount = f.Random.Number(3, 5);

            for (int i = 0; i < postCount; i++, s_postId++)
            {
                List<int> userShare = new();
                int nShare = f.Random.Number(0, 4);
                while (userShare.Count != nShare)
                {
                    int sharer = f.PickRandom(Users).Id;
                    if (sharer == user.Id || userShare.Contains(sharer))
                        continue;

                    userShare.Add(sharer);
                }


                var post = new Post
                {
                    Id = s_postId,
                    Price = f.Random.Number(50_000, 300_000),
                    Caption = f.Lorem.Sentence(5, 15),
                    Description = f.Lorem.Paragraph(4),
                    MediaPath = new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" },
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsSold = true,
                    UserId = user.Id,
                    CategoryId = f.PickRandom(Categories).Id,
                    UserShare = userShare
                };

                Posts.Add(post);

                var rateId = GenRate(user.Id, s_postId);
            }
        }
    }

    private static int GenRate(int userId, int postId)
    {
        int ratingUserId;
        do
        {
            ratingUserId = f.PickRandom(Users).Id;
        } while (ratingUserId == userId);

        var rate = new Rate
        {
            Id = s_rateId++,
            RatingAt = DateTime.Now.ToUniversalTime(),
            Rating = f.Random.Number(1, 10),
            Comment = f.Lorem.Sentences(2, " "),
            UserId = ratingUserId,
            PostId = postId
        };

        Rates.Add(rate);

        return s_rateId;
    }
}
