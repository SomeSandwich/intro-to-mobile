using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitDB_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsLock = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AvatarPath = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Legit = table.Column<double>(type: "double precision", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Participations_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MediaPath = table.Column<string[]>(type: "text[]", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsSold = table.Column<bool>(type: "boolean", nullable: false),
                    IsHide = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserShare = table.Column<List<int>>(type: "integer[]", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    JwtId = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Carts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.PostId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RatingAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rates_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    ReportAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Icon" },
                values: new object[,]
                {
                    { 1, "Áo", "icons/" },
                    { 2, "Quần", "icons/" },
                    { 3, "Váy", "icons/" },
                    { 4, "Áo khoác", "icons/" },
                    { 5, "Mũ", "icons/" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AvatarPath", "Email", "Legit", "Name", "PasswordHash", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { 1, "245 Micah Turnpike, Gerholdmouth, Maldives", "F6eByvDeQ5MlVr", "hieucckha@gmail.com", -1.0, "Hiếu Nguyễn", "1000:votIdhp03Qf24mib6VVGzh6sRM/ZqewG:j/Cjdq8GKsd2lwt/olWCtpfUMtk=", "4798366092", 0 },
                    { 2, "310 Kuvalis Points, Port Autumntown, Seychelles", "yH_rHI5emLoaxj", "Korey79@gmail.com", -1.0, "Breana Ledner", "1000:6eKiXobdVRRL3lWMtOqKS1Xm4AL8iZNc:a9YBudc1BvDNXUd1Gvn1rxyOsys=", "0563469575", 0 },
                    { 3, "4649 Kathryn View, New Clarabelle, Niger", "UkmH7xfQ7o-3tY", "Kattie.Raynor@yahoo.com", -1.0, "Kaelyn McKenzie", "1000:eAhnRaWYn5fFAxYROvkdms8e9ZPasgQA:O8HibmA84g+rzW+lFaR5ZP/Q2Jo=", "5277917543", 0 },
                    { 4, "14047 Wilfrid Locks, East Baron, Albania", "D7aD6H8Cvc3SUU", "Maxime30@hotmail.com", -1.0, "Walter Stoltenberg", "1000:6k/SX1UOGvo7l1Q7wNoj1XVWYp5bqKI6:pW7ptRTqXk5CtIweniCFyzgEcDo=", "1861302485", 0 },
                    { 5, "97748 Zemlak Place, Wilkinsonburgh, Jersey", "ha50CMvp9KlVrc", "Keara_Schmeler13@gmail.com", -1.0, "Vena Nader", "1000:B8OIEyAN6YIR/8GFVISCW8S3CL0mjJGE:b8gaNxiXkvUboebz+bng8IFTnUw=", "7193601726", 0 },
                    { 6, "399 Rashad Stravenue, Lake Hal, Burkina Faso", "RzKj4nt5aX_tfe", "Arthur_Hane@yahoo.com", -1.0, "Queen Kessler", "1000:4XQrP6V+cDPtkWdKl9ptuqpgw/ZNBaI+:pMvnXGkJ11qWFpntTXrrVenicK4=", "6361208106", 0 },
                    { 7, "60606 Jessica Rue, Hilariobury, Chad", "F6eByvDeQ5MlVr", "Scotty_Braun@hotmail.com", -1.0, "Lacey Feil", "1000:bDxrnb/dn2BXlL7XGJ+X4qqMtffN6yV0:uCOmOHL3MRT8Kgg/hb3LyC2Oodg=", "1449384229", 0 },
                    { 8, "2972 Emmerich Manors, West Cassie, Saudi Arabia", "ha50CMvp9KlVrc", "Brandon20@yahoo.com", -1.0, "Layla Kris", "1000:OUMChDtTu8Ee6nvTl+Usmex+VHMM3uG5:Sgjknrvn76HkOyGKsZkkxhF0CE0=", "2362583396", 0 },
                    { 9, "22315 Genevieve Street, Kreigerberg, Faroe Islands", "F6eByvDeQ5MlVr", "Floy.OKon53@hotmail.com", -1.0, "Stella Yundt", "1000:BhPYBU9Hn3iC3YA8F4WxwnmtnURWfHRA:v0xudg2U4brVmdBhRStp62azW2c=", "5310064421", 0 },
                    { 10, "215 Idell Unions, Schmidttown, Latvia", "D7aD6H8Cvc3SUU", "Axel83@hotmail.com", -1.0, "Rossie Kulas", "1000:86jFHBAxasgGwGqZHXhE5SuojJ6nUY7p:QwE8SCzps1pXPjISDl8pOKtWGp0=", "0466795852", 0 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Caption", "CategoryId", "CreatedDate", "Description", "IsDeleted", "IsHide", "IsSold", "MediaPath", "Price", "UpdatedDate", "UserId", "UserShare" },
                values: new object[,]
                {
                    { 1, "Fugit alias voluptas sunt doloremque dolor veritatis dolorum eligendi enim ut modi est rem eligendi facilis dolores fugit eaque et.", 4, new DateTime(2023, 4, 21, 13, 11, 39, 923, DateTimeKind.Local).AddTicks(4750), "Sed cumque sit temporibus. Animi similique architecto cumque enim laboriosam exercitationem occaecati. Est a est debitis repudiandae qui tenetur. Aut minus accusamus officia nesciunt ab sint ut omnis. Molestiae quae qui aut non voluptas exercitationem amet velit. Vero alias distinctio corporis officiis qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 260527, new DateTime(2023, 4, 21, 13, 11, 39, 931, DateTimeKind.Local).AddTicks(5410), 1, new List<int> { 2, 3, 4, 5 } },
                    { 2, "Accusantium ut in voluptatem optio eos sunt molestias consectetur.", 4, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(490), "Ullam qui quae sit maxime vero. Laboriosam vero reprehenderit sed hic commodi vel maiores enim eos. Deserunt dicta voluptatem mollitia id iusto quas doloremque rerum accusamus. Velit aut inventore. Ullam nulla repellat autem. Amet asperiores aut nobis molestiae. Rem rem ad vitae id officiis soluta tempora.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 147880, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(500), 1, new List<int> { 5, 4, 3 } },
                    { 3, "Eligendi qui laudantium omnis nulla aspernatur.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(710), "Est corporis in assumenda odio dolores. Atque optio earum sit. Laudantium quis quas. Et provident animi totam qui soluta aperiam modi. In voluptatem distinctio labore quam ea. Voluptatem dicta quia est nobis illo. Minus nobis odit aut quis minima molestias.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 183959, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(710), 1, new List<int>() },
                    { 4, "Nam ipsa quia sit vel aperiam consequatur aliquam aut provident ut qui voluptas maiores autem ullam quia et.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(930), "Vero perspiciatis nihil ex dolor unde quia mollitia laudantium. Et id enim doloremque non molestiae pariatur minima minima. Impedit et nesciunt quasi voluptatem et ea ratione. Animi quia ut qui itaque ut. Quos at alias omnis fuga.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 87717, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(930), 1, new List<int> { 5 } },
                    { 5, "Possimus atque et deleniti minima provident qui reprehenderit omnis tempore soluta.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1130), "Dolores et sunt quas tenetur eveniet aperiam. Et maxime sapiente iure et. Facere ut sunt nulla minus nemo molestias provident provident numquam. Harum in occaecati sint. Est quod quam omnis itaque quaerat. Et autem consequatur ipsa rerum quibusdam neque. Aut quae similique.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 275440, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1140), 1, new List<int> { 5, 3, 4, 2 } },
                    { 6, "Facere dolores consectetur consequatur eveniet incidunt cum qui sapiente est ut numquam reprehenderit aliquid impedit magnam error sed voluptas.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1420), "Quaerat et beatae et qui culpa voluptas voluptatem. Omnis quasi voluptates. Neque nostrum quos aut hic quia numquam voluptates. Nisi id optio ut. Possimus temporibus quos aut saepe architecto inventore. Sequi quia incidunt. Quo est animi aliquam non quisquam sed autem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 240957, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1420), 2, new List<int> { 5, 1, 3 } },
                    { 7, "Sunt illum consequatur aut natus molestiae ex iste voluptatem reiciendis et inventore.", 2, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1560), "Et fuga iusto eum quas temporibus. Quos aut ex animi consequatur sit omnis est. Aut rem dolores. Molestias in minima qui harum dolore.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 174897, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1560), 2, new List<int> { 4, 3, 5, 1 } },
                    { 8, "Dolores excepturi ad laboriosam sit nulla.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1700), "Asperiores placeat et harum blanditiis quia reprehenderit aut sed natus. Rerum aspernatur eos. Fuga impedit qui. Excepturi tempora quos quam. A facere dolores ipsam illo eveniet voluptate. Rerum delectus ab.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 298856, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1700), 2, new List<int> { 1, 3, 4 } },
                    { 9, "Asperiores explicabo explicabo et deleniti ab deserunt et quam enim molestias.", 2, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1880), "Enim aut voluptatem voluptas. Aut et inventore perferendis autem. Delectus distinctio atque et voluptatem. Voluptate aut pariatur quis molestiae eum ullam ea quo. Sed laudantium doloremque qui. Dolores architecto nam itaque quisquam minus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 142358, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1880), 2, new List<int>() },
                    { 10, "Vitae ipsa nobis cum eveniet ut aut vitae id doloribus deleniti.", 4, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2030), "Doloremque quas occaecati fuga et illum. Autem beatae facilis quisquam ad ut doloremque quia nulla. Dolores excepturi voluptatibus numquam totam est. Et dolores et facere harum aut aliquid beatae ipsa.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 257152, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2030), 3, new List<int> { 4, 2, 1, 5 } },
                    { 11, "Occaecati ea quae amet enim autem laboriosam nisi ad nisi atque praesentium assumenda.", 5, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2270), "Facilis inventore aut nemo et enim eum dignissimos incidunt. Nemo laudantium enim. Repellat aut dolor corrupti quis dolore illum suscipit eius. Beatae in magnam et dolor quod illo. Ut quibusdam dolorem rerum. Totam repellendus est consequuntur eius corrupti dolore animi quam. Eaque dolorum optio provident est qui eum laborum dolorum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 228033, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2270), 3, new List<int> { 5, 4, 2, 1 } },
                    { 12, "Soluta ut inventore autem qui non iusto nobis.", 3, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2450), "Repellat omnis laudantium qui. Accusantium reiciendis commodi voluptatibus rerum dolorem nam officia aut ad. Aut nemo fuga maiores labore quo explicabo. Eaque dolor quidem quos architecto non occaecati vel explicabo. Amet illum voluptatem quod quis doloremque et hic. Aspernatur aut et quibusdam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 197367, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2450), 3, new List<int> { 2, 4 } },
                    { 13, "Voluptatibus laudantium natus ea repudiandae sapiente corrupti perferendis sit sed ducimus.", 3, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2640), "Provident et laborum. Sunt qui tempora id tempora labore officia et aut qui. Quo nesciunt magni. Quasi et sit et sed delectus qui. Quaerat rem laboriosam. Dolores nulla quo natus error explicabo rem pariatur. Doloremque nesciunt commodi at officiis et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 238399, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2640), 4, new List<int> { 1, 5, 2, 3 } },
                    { 14, "Sed ipsam accusamus modi voluptatem quia voluptatem vitae dolorem nam eligendi ipsa dicta quis nisi ipsa rem.", 4, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2830), "Porro sed et id doloribus voluptatibus eius atque. Ut nam unde voluptatum est est. Dolorem repellat et qui cum. Autem officiis qui nihil cumque vitae mollitia. Optio magni totam soluta illum tempore ipsa debitis eos.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 269241, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2830), 4, new List<int> { 1, 2, 3, 5 } },
                    { 15, "Voluptates quibusdam est quos temporibus odit nobis animi quo est modi illum ea sed culpa cumque et quia quae debitis.", 2, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3020), "Minus sequi quisquam fuga rerum quod deleniti impedit quia. Tenetur aliquid omnis ad commodi ut omnis dolores. Enim ea quia et eum consectetur. Velit omnis itaque et. Et tempora magnam et et rerum illo qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 142846, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3020), 4, new List<int> { 2 } },
                    { 16, "Rem rerum temporibus quibusdam molestiae sunt.", 3, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3220), "Qui labore fuga laboriosam unde et quia. Quam consequatur quia laudantium temporibus et laboriosam consectetur repudiandae. Non voluptatem accusamus a quasi commodi inventore odit impedit. Rerum ut dignissimos mollitia facere. Eos quisquam sit nisi officiis et voluptatum. Consequatur in excepturi qui porro expedita.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 148022, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3220), 5, new List<int> { 2, 3 } },
                    { 17, "Quos ullam est amet aut veritatis vel ipsam consequatur nesciunt aut rerum.", 3, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3440), "Saepe qui porro eum rerum reprehenderit officiis eligendi saepe ut. Veritatis iste et voluptatibus consequuntur voluptatibus labore beatae quia. Dolores dolores minima deserunt mollitia modi quas. Et dolores voluptatibus. Corrupti ut ea quo voluptas delectus fugit omnis. Impedit facere in. Reiciendis soluta repellendus suscipit adipisci enim fuga quod doloribus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 188421, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3440), 5, new List<int>() },
                    { 18, "Atque sit et atque dolorum consequatur officiis ducimus amet et in ratione nostrum eligendi placeat quisquam nemo officia et.", 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3630), "Adipisci in iste. Quos odit et repellat dolorem suscipit consequatur sapiente reprehenderit. Ut occaecati et illo iste. Nisi illum laborum minima odit magni aut odit sint voluptatem. Iusto voluptates corporis reiciendis. Sequi eum deleniti et sint.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 196463, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3630), 5, new List<int> { 3 } },
                    { 19, "Saepe omnis facere accusamus vitae sunt sint officia quidem.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1130), "Libero fugiat rerum aspernatur. Ea cupiditate neque sapiente quasi blanditiis nam voluptas nesciunt. Fuga veritatis soluta voluptatibus rerum. Voluptatem voluptate voluptas quam veritatis id at.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 256772, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1180), 1, new List<int> { 2, 6, 4, 7 } },
                    { 20, "Qui libero beatae fugiat esse et quibusdam et qui labore alias nihil officiis.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1480), "Quibusdam dolores provident ipsa consequatur aliquam voluptatum. Id illum repellendus est harum consequuntur voluptatem ratione ut rem. Incidunt totam officia est a rerum aut assumenda. Repellat sequi commodi minima quia vel natus. Non veniam quas explicabo sunt assumenda rerum. Non qui vel. Eum aut in iste totam tempore deserunt ducimus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 115808, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1480), 1, new List<int> { 9 } },
                    { 21, "Animi cupiditate quisquam quis est aspernatur eius corporis corporis modi velit molestiae voluptas consequatur minima libero.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1720), "Autem non illum eveniet amet qui non dolorum voluptatem voluptatibus. Assumenda dicta ipsa corrupti voluptas quidem. Est illum rerum ullam. Et ea repellendus id reiciendis quaerat placeat quis sint ex. Odit facere qui eaque ab aut harum quia eligendi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 227070, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1720), 1, new List<int> { 9, 5, 8 } },
                    { 22, "Illum voluptatum voluptatem qui rerum dolorem est sit.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1940), "Et officiis a assumenda doloribus perferendis voluptas. Tenetur aut est et numquam minus necessitatibus reiciendis laudantium. Provident quod sed vel nisi eligendi harum. Assumenda et placeat voluptas quas rerum. Corporis facere aperiam aut. Iste unde ut laboriosam. Incidunt doloribus harum quas qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 162889, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1940), 1, new List<int> { 3, 7, 10, 5 } },
                    { 23, "Fugit non voluptatem facere officia blanditiis saepe reprehenderit quia illo dolor et numquam temporibus non eos consectetur animi vel voluptas.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2190), "Ex dolorum ut consequatur aut ad voluptas voluptas ipsa. Voluptas consectetur nemo. Dolorum cupiditate at et explicabo sed qui saepe voluptatem cum. Aspernatur aut et laborum veritatis reprehenderit ea nemo eum qui. A et maiores perspiciatis voluptatem illum commodi et. Ratione est deleniti et tenetur eius molestias.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 298003, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2190), 2, new List<int> { 6 } },
                    { 24, "Minus consequatur officiis quos nihil labore in amet omnis est ut qui inventore velit veritatis iure fugit fugit qui.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2400), "Ipsa temporibus tempore molestiae. Deleniti est quia consequatur molestias quaerat. Consequatur nobis asperiores occaecati aut non aut. Numquam cumque id dolore dolores aut facilis quos impedit in. Quia eos autem. Ipsa quod praesentium aut nesciunt.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 125956, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2400), 2, new List<int> { 5, 9 } },
                    { 25, "Facilis molestias rem et tempora cumque et et commodi sit.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2560), "Quo et magni exercitationem veniam. Labore ipsum impedit rerum similique. Animi quaerat qui eum consequatur. Harum sit optio beatae. Facere debitis harum explicabo sed facere dolorum. Saepe eveniet voluptatem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 50818, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2560), 2, new List<int>() },
                    { 26, "Dicta deserunt in ut cum quam qui placeat.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2740), "Adipisci consequatur id quisquam quae. Possimus nisi et nihil natus et vitae. Delectus facilis accusamus unde itaque odit. Nihil et delectus fuga dolores est placeat. Sit enim optio rem modi fugiat.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 171104, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2740), 2, new List<int> { 7, 9 } },
                    { 27, "Mollitia rem doloribus provident est error quis iste nihil sint quasi iure provident ut at soluta.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2900), "Sit eius dolor omnis consequatur. Corporis natus rerum sit autem autem et veritatis totam. Accusamus numquam amet. Aliquam tempora officia totam maiores deleniti adipisci architecto.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 239948, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2900), 3, new List<int> { 5, 8, 7 } },
                    { 28, "Dicta occaecati est ad ipsum aut.", 2, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3080), "Accusantium tempora assumenda iure totam voluptates aut. Nemo ullam dolorem enim tempora aut suscipit. Voluptatem maiores rerum. Vero facilis voluptas est et est quia unde. Omnis ullam voluptatem natus molestias deserunt incidunt ea. Ut enim dicta sit fugiat.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 250411, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3080), 3, new List<int>() },
                    { 29, "Sunt eum et id in voluptatem quos corporis reiciendis veniam voluptatem non minima nostrum suscipit.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3260), "Et ab temporibus aperiam aperiam. Eius nemo eos culpa voluptate veritatis vitae est sit natus. Vero occaecati impedit magni voluptas est harum doloremque. Qui sint eum et ipsam. Ut eligendi aut consequatur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 225834, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3260), 3, new List<int> { 6, 8 } },
                    { 30, "Ut illum ipsam provident ullam repellat quia iste qui aut iste qui eos.", 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3450), "Architecto consectetur nihil est veniam modi commodi. Vitae est eos saepe eligendi reiciendis optio asperiores aut. Veniam laboriosam ipsum a. Repellat accusamus rerum magni odio ad unde iusto.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 159944, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3450), 4, new List<int> { 3, 6, 8 } },
                    { 31, "Nisi distinctio debitis suscipit libero itaque quas aut numquam porro et maiores.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3630), "Nostrum quae eum ipsam deserunt aut. Nam nulla et exercitationem sequi ut et nesciunt. In debitis quia nobis a ipsum omnis dolorem. Voluptas id quia. In aut molestias inventore. Dolore dolorem dolores blanditiis ipsum architecto asperiores.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 134131, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3630), 4, new List<int> { 2, 8, 5 } },
                    { 32, "Accusantium amet nisi officiis esse doloribus qui dolorem.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3810), "Enim culpa ducimus. Saepe quibusdam sit ad. Distinctio laborum id. Ut dolorem sint nostrum unde dicta nostrum quas voluptas. Nesciunt ut voluptate neque possimus qui quos et repudiandae sit. Quo aliquid architecto illo laborum laudantium iure rerum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 75847, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3810), 4, new List<int> { 2, 1 } },
                    { 33, "Molestias nulla autem quia nostrum qui et in blanditiis nihil commodi dolore.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4000), "Aut tempora ut totam deserunt perspiciatis ut laudantium nemo dignissimos. Quia molestiae ipsum rem dolores qui optio autem. Atque non sapiente ratione enim alias numquam. Dolor non fugiat consequatur commodi soluta. Voluptas sit minima atque aliquid nisi. Quia enim minima inventore fuga.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 233009, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4000), 4, new List<int> { 3 } },
                    { 34, "Perspiciatis commodi sit est et consequatur necessitatibus voluptatum explicabo perferendis aut quo et officia distinctio quam iure eveniet est totam.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4220), "Ab ut quasi et. Aut doloribus quibusdam ut corporis mollitia accusamus excepturi. Sed quaerat sunt voluptatem et enim. Consequatur quidem consequuntur deserunt sunt ea aliquid debitis consequatur est. Quo enim nesciunt est. Sapiente doloribus provident aut voluptas perspiciatis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 289749, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4220), 5, new List<int> { 4, 7 } },
                    { 35, "Et est fugiat deserunt hic illo quaerat et.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4440), "Et at voluptas dolorem provident ut accusamus iste. Neque sit aut dolor id beatae delectus natus commodi sed. Sed beatae dolores qui dolor officia magni tempore recusandae. Id cum totam sed dolorem. Incidunt cupiditate est vitae veniam ullam est velit laborum. Ipsum magnam et dignissimos et facilis velit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 227934, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4440), 5, new List<int>() },
                    { 36, "Impedit rerum et tempore mollitia consequatur porro.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4600), "Voluptatem consequuntur dolore eaque dolorum fuga. Et qui consequatur repellendus natus tempora amet porro animi. Qui id est facere ex illum nam. Autem nostrum ea. Hic impedit id sit et necessitatibus voluptas voluptatibus iusto modi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 160230, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4600), 5, new List<int> { 7 } },
                    { 37, "Vero in hic qui rerum vel et qui molestiae.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4860), "Reiciendis laudantium molestias. Quibusdam aut facere mollitia ut nostrum dolores ea. Hic optio laborum necessitatibus et libero dolorum. Consequatur numquam quasi voluptatem est temporibus eveniet quia. Amet praesentium quae impedit. Quia laudantium occaecati est voluptatum adipisci rem sapiente at doloribus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 51415, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4860), 6, new List<int> { 9, 8 } },
                    { 38, "Unde odit vero cumque sapiente ex ut consequuntur.", 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5030), "Consequuntur cupiditate eveniet nihil vel autem. Quos deleniti fuga quaerat recusandae quis enim enim accusamus. Et voluptatibus dolore atque voluptates. Ea aliquam et eum maiores eligendi provident vitae nesciunt. Omnis veniam deserunt.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 122248, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5030), 6, new List<int> { 7, 4 } },
                    { 39, "Eum voluptatibus quidem qui cumque nihil.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5250), "Eligendi ut id recusandae. Consectetur minus quia. Omnis et aliquam vitae quae illum facilis est. Praesentium nostrum autem non vel ut occaecati laudantium laboriosam. Aut consectetur omnis eos aliquam sit aut. Ratione autem itaque et facilis officia at incidunt provident. Vel atque labore labore commodi impedit veniam saepe placeat sint.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 247853, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5250), 6, new List<int> { 7, 1 } },
                    { 40, "Quam modi ducimus deleniti voluptas et ab non harum eveniet quibusdam ratione.", 2, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5460), "Incidunt dolores culpa animi error. Et provident illum et unde mollitia. Magnam accusantium ea dolor molestias reprehenderit ut nihil sint. Tenetur inventore ratione qui iusto non vel. Voluptatem aspernatur fugit soluta et ad excepturi. Sunt nam rerum amet assumenda. Tenetur cumque voluptates ut dolorum nulla.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 162384, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5460), 6, new List<int>() },
                    { 41, "Voluptatibus magni voluptas hic itaque qui aliquid rerum odio dolorem totam voluptatibus a eum facere magni.", 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5630), "Sed id accusamus expedita quis maiores non modi. Et corporis molestiae et magnam dolor. Quibusdam velit vitae. Doloribus est aspernatur sed ipsam ut quia optio.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 160532, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5630), 6, new List<int> { 1, 9, 5, 4 } },
                    { 42, "Et provident odio aspernatur tenetur ut vero distinctio optio qui non quasi sunt adipisci.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5840), "Nihil tenetur ad maxime et ea dolore. Rerum laboriosam nam consequatur nihil pariatur. Animi incidunt est. Aut distinctio reiciendis. Saepe ad sint quod est esse magni minima suscipit. Possimus reiciendis eaque ea sunt enim blanditiis dolor.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 231434, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5840), 7, new List<int> { 4, 2, 6 } },
                    { 43, "Dolor et et voluptatibus et.", 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6000), "Quia qui rerum eos inventore. Nostrum est quasi delectus veniam earum. Doloribus dolor fugit est laboriosam enim consequatur aliquam. Hic quia facere beatae. Doloribus aliquid fuga. Placeat quos officia modi quam officiis velit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 204329, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6000), 7, new List<int>() },
                    { 44, "Illum recusandae quis rerum rerum corporis iure praesentium cum cum quasi.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6180), "Vitae ea est. Doloribus vel quia veniam. Recusandae eum reprehenderit quas. Voluptas unde consequatur sunt nemo quo at et. Qui repudiandae facere nihil sed expedita alias. Totam est reprehenderit laboriosam quas autem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 266305, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6180), 7, new List<int>() },
                    { 45, "Magnam vitae doloribus sit voluptatem ullam voluptas dolores sint eius rem expedita.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6320), "Eum natus mollitia est inventore aut sunt. Sunt dolorum ea voluptas quisquam itaque. Ut consequatur est. Recusandae inventore distinctio. Voluptatem eaque doloribus modi dolor placeat aut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 107809, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6320), 8, new List<int> { 4 } },
                    { 46, "Rem esse culpa alias dolores aperiam ducimus sit quo qui veniam cumque quos optio ut illum.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6510), "Animi consectetur quas voluptatem nulla perspiciatis est et praesentium non. Incidunt et vel eum. Quo vero voluptatum deserunt omnis esse ratione neque ut. Alias corporis sit nulla nostrum. Debitis voluptates id deleniti consequatur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 236004, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6510), 8, new List<int> { 9, 4, 2 } },
                    { 47, "Occaecati eum voluptatem deserunt consequuntur suscipit porro modi autem est pariatur et.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6700), "Nemo voluptas maiores repellendus eveniet voluptatum eum ex deleniti qui. Iste hic est et reprehenderit. Est laudantium fuga quia non. Aut repudiandae et. Quos et ut unde. Quia recusandae reprehenderit veniam ut. Id sapiente qui molestias aperiam qui voluptas reprehenderit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 224610, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6700), 8, new List<int>() },
                    { 48, "Aut mollitia molestiae et rerum molestias molestiae.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6900), "Repellendus autem praesentium. In dignissimos sit dolorem quasi voluptas molestias quos. Reprehenderit totam ad perspiciatis maiores. Omnis recusandae numquam voluptates nobis enim eaque vero quis. Ut sapiente iure optio rem adipisci perspiciatis dolor. Magni omnis in quaerat voluptas et aut nobis debitis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 204891, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6900), 8, new List<int> { 6, 9, 5 } },
                    { 49, "Fugit quasi praesentium esse dolorem corrupti non ut sit.", 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7110), "Provident eius natus expedita delectus. Dignissimos vitae illo nisi est ullam explicabo soluta minima. Et tempora illum et similique laborum. Nobis alias possimus ut error non. Architecto voluptatibus molestiae vero voluptas cupiditate. Voluptas porro voluptate laborum natus et quo rerum. Est nam laudantium deleniti numquam harum velit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 192723, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7110), 8, new List<int>() },
                    { 50, "Nobis commodi aut enim veniam non porro sed.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7270), "Aperiam similique porro quia placeat quis quisquam. Voluptatem ea fugiat et voluptas. Fugit vel et aut odio vero rerum. Velit ea et corporis at animi quas esse. Aut ut culpa et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 160706, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7270), 9, new List<int> { 10, 3, 8, 7 } },
                    { 51, "Nesciunt qui aperiam laboriosam nihil tenetur alias tenetur aut quaerat ut veritatis qui.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7420), "Dolore exercitationem sunt rerum et. Facilis mollitia neque eos soluta. Aut accusamus assumenda. Et amet dolor nesciunt magni quasi quam quam ut est.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 79834, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7420), 9, new List<int> { 10, 7 } },
                    { 52, "Voluptatem in officiis explicabo maiores excepturi officiis omnis magnam.", 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7580), "Placeat hic necessitatibus quod voluptas. Suscipit qui magni corrupti eveniet. Ad dolore omnis sapiente et non et. Nobis suscipit unde ratione minima blanditiis temporibus delectus quasi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 54842, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7580), 9, new List<int>() },
                    { 53, "Alias fugit nisi veritatis qui similique et et.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7780), "Aut molestiae nemo. Illo at totam earum minima itaque adipisci eos unde. Voluptatem officiis omnis et quod. Quod libero mollitia corrupti. Ut eos impedit quia nihil cupiditate animi quas velit ducimus. Dolorem voluptatibus labore quod ipsum maxime voluptatibus. Laudantium laborum voluptatibus qui dolor aut iusto amet.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 239864, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7780), 10, new List<int> { 4, 9, 7, 3 } },
                    { 54, "Enim ea dolores est quasi inventore iure incidunt est nulla nam repudiandae.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7980), "Qui voluptatibus fuga. Vitae tenetur a aperiam natus natus iusto sit et sequi. Dolor consequatur omnis enim sit corrupti reprehenderit. Sed minima nisi et. A ut corrupti quibusdam aut dignissimos quisquam. Atque aut sapiente.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 144049, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7980), 10, new List<int>() },
                    { 55, "Nemo praesentium soluta et nihil aut excepturi ex.", 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8200), "Eos ab voluptatibus provident atque sint vel nisi recusandae ut. Reprehenderit magnam facere. Accusantium voluptatem iusto atque aut aliquid velit earum. Veritatis non molestias sed libero omnis nesciunt amet. Blanditiis et fugit voluptate odit. Minima sint quia. Dolorum sit consequatur voluptatem perferendis sint aliquid esse.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 271270, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8200), 10, new List<int>() },
                    { 56, "Dolore omnis ipsam omnis saepe ipsum ut optio quidem deleniti eum vitae ab amet.", 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8380), "Ut consequatur quidem et laudantium voluptates blanditiis omnis quia quia. Aliquid vel voluptates explicabo. Dolorum consequatur eligendi fugiat et voluptatum non dolorem voluptates. Quam molestias voluptatem quod ut. Esse ut accusamus dolor doloremque non in. Porro maiores voluptatem repudiandae.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 251040, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8380), 10, new List<int> { 8 } }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "PostId", "Rating", "RatingAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Amet qui tempore et sint voluptas. Aut porro fuga expedita.", 1, 5, new DateTime(2023, 4, 21, 13, 11, 39, 931, DateTimeKind.Local).AddTicks(9040), 3 },
                    { 2, "Voluptate eos delectus assumenda velit eaque possimus voluptates et. Eligendi dolore quas voluptas.", 2, 7, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(500), 3 },
                    { 3, "Et placeat rerum quis et rem distinctio error qui ut. Dolore provident dolore omnis earum.", 3, 5, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(710), 4 },
                    { 4, "Est sit modi et et minus. Quaerat eius atque velit voluptatibus blanditiis esse.", 4, 7, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(930), 5 },
                    { 5, "A iusto ea ipsum quis. Accusantium nulla omnis reprehenderit qui fuga et dignissimos.", 5, 5, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1140), 2 },
                    { 6, "Provident non eum. Deleniti ratione sed sunt excepturi.", 6, 10, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1420), 4 },
                    { 7, "Est praesentium quasi at. Vero sed perferendis.", 7, 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1560), 3 },
                    { 8, "Doloribus explicabo excepturi laudantium maiores. Quo quos doloribus quod sequi numquam aperiam sunt.", 8, 10, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1700), 3 },
                    { 9, "Est quaerat illum. Aut sit repellendus quod beatae.", 9, 5, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(1890), 1 },
                    { 10, "Cupiditate nostrum ea quibusdam non aut. Deleniti nihil et accusamus aut quia ratione optio ullam.", 10, 4, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2040), 1 },
                    { 11, "Quia expedita incidunt. Eveniet debitis ex.", 11, 1, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2270), 5 },
                    { 12, "Non tenetur veniam eos laborum odit. Quis et dolor cupiditate repudiandae quas occaecati nemo.", 12, 4, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2450), 2 },
                    { 13, "Minima hic est beatae id et sapiente ut. Sit eveniet et corporis.", 13, 8, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2640), 5 },
                    { 14, "Molestiae in ipsam dicta qui nihil deserunt. Perspiciatis accusantium et aut eius enim reprehenderit sequi pariatur impedit.", 14, 5, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(2840), 2 },
                    { 15, "Fugiat odio consequatur unde. Maiores et reiciendis veniam nesciunt vero reiciendis porro tempore ut.", 15, 8, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3030), 3 },
                    { 16, "Dolorem iure et iusto harum. Porro est fugiat perferendis sed sapiente.", 16, 3, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3220), 3 },
                    { 17, "Maxime atque harum dolor aut voluptas a voluptas. Aut dolores ut omnis iste tempora ut et provident provident.", 17, 7, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3440), 2 },
                    { 18, "Cupiditate commodi repudiandae. Quia incidunt rerum accusamus vero quis.", 18, 9, new DateTime(2023, 4, 21, 13, 11, 39, 932, DateTimeKind.Local).AddTicks(3640), 1 },
                    { 19, "Aut velit et assumenda molestiae. Itaque quo doloremque.", 19, 6, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1190), 10 },
                    { 20, "Distinctio iusto nesciunt sit expedita neque. Ullam architecto consectetur eaque expedita itaque nesciunt cum fugit et.", 20, 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1480), 10 },
                    { 21, "Ipsa dignissimos rerum ut voluptatem nemo qui. Pariatur necessitatibus quidem perspiciatis.", 21, 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1720), 8 },
                    { 22, "Delectus quia perspiciatis et quia. Ipsam beatae ad consequatur laborum dolor.", 22, 7, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(1940), 3 },
                    { 23, "Ipsum quis nihil pariatur voluptatem sit et. Velit aspernatur facere ut.", 23, 9, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2190), 1 },
                    { 24, "Aliquam ut ullam et. Dolores laboriosam qui veniam ad voluptatem.", 24, 10, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2410), 4 },
                    { 25, "Et itaque eos minima fugiat non et soluta error. Unde esse quia facilis mollitia eveniet alias accusantium quia.", 25, 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2560), 10 },
                    { 26, "Commodi quia blanditiis qui rerum quis. Quos saepe sit ducimus numquam et omnis illo dolor.", 26, 2, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2740), 1 },
                    { 27, "Dolor maxime fugit soluta beatae. Saepe natus nemo sunt adipisci veritatis sapiente officia.", 27, 8, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(2900), 7 },
                    { 28, "In quasi dolorum occaecati magni molestiae. Ut dolorum repellendus minima velit sed alias atque maxime.", 28, 6, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3090), 4 },
                    { 29, "Voluptas sint et dolorem enim hic a aperiam nihil. Perferendis officia delectus at rerum vitae eaque est deserunt.", 29, 9, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3260), 2 },
                    { 30, "Sed fugiat modi impedit facilis ab velit rerum. Voluptatibus quaerat fugiat velit voluptatem voluptatem.", 30, 2, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3450), 9 },
                    { 31, "Eius et alias omnis labore ut autem pariatur iure assumenda. Et enim nisi.", 31, 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3630), 6 },
                    { 32, "Reiciendis perspiciatis quas officiis. Qui praesentium corporis iusto ea illo et.", 32, 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(3820), 10 },
                    { 33, "In rem deleniti aut eligendi. Harum debitis et eum iusto est corporis.", 33, 9, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4010), 6 },
                    { 34, "Ut nihil ratione voluptas accusantium quis ipsa quam aliquam tempore. Dignissimos incidunt dolorem ea.", 34, 7, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4230), 3 },
                    { 35, "Qui et esse nostrum. Ea quaerat consequuntur itaque et dicta sequi accusamus optio.", 35, 10, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4440), 6 },
                    { 36, "Sint qui repudiandae consectetur quia qui eum omnis impedit. Hic maiores voluptate nisi est et.", 36, 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4600), 1 },
                    { 37, "Nihil nesciunt doloremque est consectetur impedit illo deleniti. Eligendi quia molestiae non sint sint ratione quis aliquam ex.", 37, 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(4860), 9 },
                    { 38, "Aut et veniam consequatur non aut assumenda vitae voluptatem. Ipsum nihil et consequatur placeat placeat modi.", 38, 6, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5030), 10 },
                    { 39, "Eius sed et illum accusantium facere ut est id possimus. Error beatae alias.", 39, 9, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5250), 3 },
                    { 40, "Maiores quaerat exercitationem qui in aut. Placeat eum reiciendis consequuntur voluptatem aut sit.", 40, 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5470), 8 },
                    { 41, "Earum sed blanditiis nam natus consequatur itaque voluptas id quo. Ipsa suscipit commodi non eos atque minima voluptas sed qui.", 41, 8, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5630), 1 },
                    { 42, "Possimus libero eveniet iure fuga fuga nemo quam libero tenetur. Id id distinctio dolorem illum iure est.", 42, 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(5840), 5 },
                    { 43, "Possimus commodi accusamus quos odio odit. Repudiandae sit quibusdam sunt ea alias cumque ea.", 43, 7, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6000), 5 },
                    { 44, "Cum eaque error. Et dicta aut.", 44, 1, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6190), 1 },
                    { 45, "Labore quas doloribus aliquam sed est optio corporis et quod. Eos ut fugit.", 45, 10, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6320), 10 },
                    { 46, "Fugiat a et eum non. Accusamus quia animi deserunt ratione illum nobis aspernatur.", 46, 8, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6510), 9 },
                    { 47, "Et suscipit laborum autem est dolores accusantium expedita vitae accusamus. Aperiam illum iusto.", 47, 3, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6700), 5 },
                    { 48, "Quisquam quae sit illo et molestiae omnis ut ad. Sunt fuga magnam consectetur soluta fugiat consequatur unde quibusdam.", 48, 7, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(6900), 6 },
                    { 49, "Ipsa dolorum tempora animi. Distinctio ut dicta ut odio.", 49, 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7110), 9 },
                    { 50, "Reprehenderit et magnam est quis in voluptatem quam id. Ut non ut laborum architecto voluptatibus.", 50, 2, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7270), 7 },
                    { 51, "Ad et natus distinctio quia quisquam aut. Ut pariatur inventore accusamus minus aut eum labore fugit aut.", 51, 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7420), 5 },
                    { 52, "Quae ut beatae ex nam dolores. Voluptatem voluptatem beatae qui ipsa corrupti et cumque ad quidem.", 52, 6, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7580), 7 },
                    { 53, "Excepturi sequi sed qui dolores sed neque quaerat. Tempore non in culpa soluta maiores quisquam et quo.", 53, 4, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7780), 3 },
                    { 54, "Et vero aut possimus et odio repellat. Quia impedit esse tempore.", 54, 6, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(7980), 1 },
                    { 55, "Porro unde repudiandae iste. Rerum iste animi quas facere.", 55, 5, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8200), 4 },
                    { 56, "Quia quia placeat aut consequuntur quis. Enim eum molestias reiciendis neque quasi libero eius.", 56, 8, new DateTime(2023, 4, 21, 13, 11, 40, 79, DateTimeKind.Local).AddTicks(8380), 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PostId",
                table: "Carts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PostId",
                table: "OrderDetails",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SellerId",
                table: "Orders",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_UserId",
                table: "Participations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rates_PostId",
                table: "Rates",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PostId",
                table: "Reports",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
