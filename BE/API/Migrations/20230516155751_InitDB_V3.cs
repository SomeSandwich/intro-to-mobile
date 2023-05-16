using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitDB_V3 : Migration
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
                    Money = table.Column<double>(type: "double precision", nullable: false),
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
                columns: new[] { "Id", "Address", "AvatarPath", "Email", "Legit", "Money", "Name", "PasswordHash", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { 1, "546 Giovanni Vista, Turnerport, Bahrain", "F6eByvDeQ5MlVr", "hieucckha@gmail.com", -1.0, 0.0, "Hiếu Nguyễn", "1000:4kNP6sZVLX7Vx9KEJmt5q3fw2J6nBeIr:Td0uNIP1j4c94QG69342a7wzQmA=", "6613102487", 0 },
                    { 2, "741 Goldner Expressway, Brekkeborough, Sweden", "F6eByvDeQ5MlVr", "Tatyana.Carter@gmail.com", -1.0, 0.0, "Kenny Harris", "1000:ZYxRbEsQWWX/LFV+AnwlBfnN82Hq9w/C:65i6uqQa/xRBFkZ1Lq0RDoKX+uw=", "9678044286", 0 },
                    { 3, "1459 Tania Stravenue, Hillaryland, Bermuda", "RzKj4nt5aX_tfe", "Jewell.Koelpin48@gmail.com", -1.0, 0.0, "Jenifer Schuster", "1000:O3yec5EBeN347vkV8sbWWcIkg5DhC8DY:fSR/zZHeXltAgQpl8yhhY10b0Ic=", "8116704314", 0 },
                    { 4, "301 Riley River, Humbertobury, Antigua and Barbuda", "RzKj4nt5aX_tfe", "Myrtle42@yahoo.com", -1.0, 0.0, "Judd Harris", "1000:7slfme6PNuPgboKAL3hPAXztrlFX4wOK:v2nFTFONsxd14oYVSTtO964BTFU=", "3003155080", 0 },
                    { 5, "3065 Hamill Stream, New Jay, Palestinian Territory", "UkmH7xfQ7o-3tY", "Carroll42@yahoo.com", -1.0, 0.0, "Gust Wilkinson", "1000:bHU1dJ7+Rmykq5+8HSYQZ3dE29jghF9o:62P6rBGgsMKGHAgHhPMwnb+igfQ=", "9485308350", 0 },
                    { 6, "7148 Gleichner Lights, Port Lizziechester, Argentina", "yH_rHI5emLoaxj", "Kiera84@yahoo.com", -1.0, 0.0, "Kelsie Wehner", "1000:PIHTbvJCx08QOLRgaW4KetTbncBl1uAx:lMvWTrBSV4f/7h9mI8adNksjb/U=", "9610345575", 0 },
                    { 7, "076 Torp Valleys, North Lupe, Madagascar", "D7aD6H8Cvc3SUU", "Dejon.Mayert@yahoo.com", -1.0, 0.0, "Hallie Torphy", "1000:ymwCDpQT4QSel9gtWNwwIKGgvAdwPZkl:P84yWO1AB6JnQzY3dZ/bp446NnA=", "2730974440", 0 },
                    { 8, "443 Jenifer Islands, Aracelytown, Macao", "RzKj4nt5aX_tfe", "Catalina_Purdy@gmail.com", -1.0, 0.0, "Bennie Hartmann", "1000:3Ve21SeYEYzGngFUbYVrk9WJmVwiqzy/:Hs2oxtyFwkqGbtiG3Khn3pTH1Lo=", "8391708803", 0 },
                    { 9, "19441 Hyman Drives, North Julien, Gabon", "F6eByvDeQ5MlVr", "Amir26@gmail.com", -1.0, 0.0, "Kaitlyn Barton", "1000:mqvvzIt1QfP3XLyo0LRB8RUTcniY1/Ji:7+120akEEyNJp65Z5uej1xV59a0=", "5290782923", 0 },
                    { 10, "8536 O'Hara Mission, New Berryport, Panama", "yH_rHI5emLoaxj", "Ardella70@gmail.com", -1.0, 0.0, "Misty Smitham", "1000:ZX8OrGBGzu5fe3wI7XQrWT0RbRvIGnlW:10D2a+Iggg7c/FzGOIsX1uo9dIo=", "5587622160", 0 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Caption", "CategoryId", "CreatedDate", "Description", "IsDeleted", "IsHide", "IsSold", "MediaPath", "Price", "UpdatedDate", "UserId", "UserShare" },
                values: new object[,]
                {
                    { 1, "Fugit ut commodi repellat est itaque quia ut rem qui magnam.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 808, DateTimeKind.Local).AddTicks(4000), "Rerum ratione id sit. Quibusdam odit dolores voluptatem rerum. Qui quasi earum et et repudiandae nam voluptatem ut quasi. Ex autem veritatis et ut impedit esse et. Optio cumque numquam eum perspiciatis recusandae.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 271627, new DateTime(2023, 5, 16, 22, 57, 50, 818, DateTimeKind.Local).AddTicks(8330), 1, new List<int> { 4, 5 } },
                    { 2, "Corporis pariatur libero earum ipsa unde eaque quisquam tempore voluptas.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3300), "Officia earum nobis est quae. Molestiae animi et. Et suscipit quae doloremque nemo ut non tempore. Exercitationem illo consequatur. Tempora ut dolore inventore tempore nobis voluptatem vitae velit omnis. Aut pariatur excepturi error dolore quis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 247848, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3310), 1, new List<int>() },
                    { 3, "Qui ea tempore nemo a quibusdam et totam vitae facere atque rerum dolorem dicta delectus iure laudantium architecto praesentium.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3580), "Consectetur iste qui dignissimos sit in molestiae temporibus doloribus nemo. Ut harum illum quis quae labore quo. Numquam asperiores sed ipsam praesentium quos ut ea. Iure dolore sed enim. Quas minima dolor sint culpa reprehenderit aut vel rerum excepturi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 144416, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3580), 1, new List<int> { 3 } },
                    { 4, "Quasi sed est sed voluptas consectetur eos est sit dolorum ut dolorem.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3820), "Repellendus sed itaque omnis eos voluptas et. Sint dignissimos sed tenetur. Suscipit non nulla aut neque ad voluptas sint et voluptas. Perspiciatis eveniet et sed. Nulla quibusdam nulla ullam ullam eos non quasi cumque. Ipsam voluptas sint.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 168839, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3820), 2, new List<int> { 5, 3, 4, 1 } },
                    { 5, "Voluptate repellat facilis quo non qui.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3980), "Molestiae minima accusamus soluta vitae aliquam delectus excepturi pariatur praesentium. Velit non rerum omnis nesciunt facere eum vel. Debitis in consequuntur est suscipit et incidunt natus. Est cum sed officiis est dolores qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 173696, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3980), 2, new List<int> { 5, 1, 3, 4 } },
                    { 6, "Aut placeat ex non et.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4110), "Maxime porro perferendis porro voluptatibus corrupti. Minima qui odit. Neque id sunt ut et in consequuntur minus officiis. Qui voluptates incidunt quidem hic nulla nihil doloremque. Consequatur error nesciunt voluptate voluptatum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 136471, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4110), 2, new List<int> { 5 } },
                    { 7, "Iusto ut accusamus dolorem quidem minima nesciunt eaque illo est voluptatem porro.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4310), "Nostrum fuga aut consequuntur deleniti sunt. Eaque non hic similique minima odit id dolores velit et. Molestiae temporibus doloribus consectetur dolore quas voluptatem voluptatibus iusto. Enim culpa dolorum tenetur dicta consequatur. In rerum ab voluptate ad possimus necessitatibus aut consequatur rerum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 65020, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4310), 2, new List<int> { 4, 3 } },
                    { 8, "Est perspiciatis fugit voluptas necessitatibus exercitationem.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4450), "Fugit ea et modi ipsa eligendi. Ipsum nemo doloremque ut in. Eum alias et qui et iusto. Quia laudantium laboriosam nisi eius perferendis iste deleniti sint.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 162132, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4450), 3, new List<int> { 2, 5, 4 } },
                    { 9, "Molestias voluptatem porro ea aut omnis quis aspernatur ut accusamus quaerat rerum repellat non tempora deleniti.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4660), "Amet minima porro enim saepe ipsam. Minima est eveniet enim iste fugiat qui quis ducimus est. Perspiciatis id minus quidem officia quia est. Hic pariatur repellendus aperiam quia magni deleniti ut nostrum architecto. Voluptate quis animi est impedit ut quia similique. Voluptas magni quam doloremque ea officia quae iste.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 71021, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4660), 3, new List<int>() },
                    { 10, "Necessitatibus deleniti similique quis excepturi saepe voluptates quam aspernatur aut accusantium quo dolor aut quo molestiae animi.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4850), "Sed optio sint ipsa. Dolores neque expedita est voluptatem laborum autem omnis reprehenderit. Rerum beatae mollitia eligendi ut temporibus quis maxime quod. Rerum sequi quia veritatis cupiditate excepturi minus ipsum. Ipsa explicabo omnis officiis doloremque velit error voluptates inventore commodi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 174135, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4860), 3, new List<int>() },
                    { 11, "Culpa sunt qui ullam pariatur et ad ab distinctio voluptatem sit corporis at.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5050), "Fugit facere error voluptatem. Illo aut dicta quod. Ex accusantium aspernatur quis voluptates voluptatem ipsam veritatis earum ut. Quisquam aspernatur vitae totam. Suscipit magni quas corrupti cumque sunt veritatis illo quos rem. Assumenda dignissimos maxime impedit magni molestiae ipsum qui id quos. Natus est possimus occaecati.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 81068, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5050), 3, new List<int> { 5, 4, 1, 2 } },
                    { 12, "Ratione sed cum blanditiis sit vero voluptatem qui harum debitis tempore inventore a at ullam non ut nesciunt praesentium.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5240), "Et ut quis amet corporis quos nesciunt porro. Soluta voluptatum quos est rem. Odio sit quia voluptates a neque laudantium impedit. Dolorum eaque nemo consequatur sed dolores velit minima eligendi. Quis saepe ut qui laboriosam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 155097, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5240), 4, new List<int> { 3, 5, 2 } },
                    { 13, "Sed et beatae tempora fugit id.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5360), "Maxime et aut error unde molestiae cumque. Enim quam quas. Rem commodi et eos error alias. Id dolores fuga tempora sit fugit qui in.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 225689, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5360), 4, new List<int>() },
                    { 14, "Hic dolorem eum officia laudantium.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5540), "Vero laborum ea fuga sunt odit atque voluptas quis odit. Nam et aperiam inventore commodi culpa maxime hic. Quidem perspiciatis aut illum necessitatibus quis odit cum quod. Illo eligendi eum autem blanditiis aut tempora dignissimos. Ut nihil consequatur ut non. Voluptas similique impedit doloremque.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 289735, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5540), 4, new List<int>() },
                    { 15, "Minus vel veniam tenetur dolorum dolor quis ex atque id sapiente at aut odio.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5700), "Dolor qui architecto ipsa accusantium eos suscipit quidem ipsam mollitia. Minus dolorum architecto fugit dolores voluptas ut accusamus ullam nemo. Rerum nisi veritatis velit velit tenetur labore. Dolorem quae ex tempore sed rerum quo nobis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 117237, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5710), 4, new List<int> { 1, 3, 5, 2 } },
                    { 16, "Assumenda deserunt qui ipsam dolores expedita nihil quis.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5870), "Aperiam sit cumque aspernatur in perferendis ut molestiae quia. Qui necessitatibus modi iure esse quo. Nostrum est ut nihil ullam asperiores enim et sit. Molestiae rerum sunt atque consequatur a et molestiae et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 141810, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5870), 4, new List<int> { 1 } },
                    { 17, "Commodi deleniti ut nostrum fugiat sint dolor sint pariatur vel ut quia.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6080), "Provident saepe doloremque. Enim et deleniti molestiae. Omnis et qui veritatis et dolor ut ullam dolore officia. Voluptatibus aut adipisci nihil quia possimus velit. Dicta et ullam quis veniam voluptates rerum voluptas autem non. Ipsa nesciunt pariatur consequuntur dolor corporis omnis tenetur sed. Eos tempora adipisci.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 291742, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6080), 5, new List<int> { 1, 3, 2, 4 } },
                    { 18, "Aspernatur eos quo consequatur voluptas sunt deleniti ex sint quos at dolore ut dolor et unde nulla repellat in autem.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6240), "Sed et eum omnis aut ipsam et et. Enim quos qui corporis dignissimos perferendis laborum. Maxime quas eos aut non sunt facilis modi. Est dolor maiores sapiente.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 64818, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6240), 5, new List<int> { 3, 4, 2, 1 } },
                    { 19, "Cupiditate odio laborum nam provident.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6470), "Suscipit nesciunt iure qui cumque aut harum quod. Earum impedit iste nulla adipisci officia labore sit temporibus quos. Modi velit aut dolorum perspiciatis voluptas debitis. Ut impedit est minima id maiores est magnam. Eos voluptate deserunt quod. Ut omnis similique voluptatem quis quis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 89241, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6470), 5, new List<int> { 2, 4, 3 } },
                    { 20, "Tempore et quos officiis non quo voluptas officiis similique repellat ab eligendi tempora quasi cupiditate culpa dolor.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6610), "Fugiat ut dolores. Harum cupiditate excepturi enim nisi dolorem consequatur. Voluptas et eum. Reiciendis doloribus sunt voluptatem perferendis provident incidunt laboriosam. Suscipit nisi repudiandae magni.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 76835, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6610), 5, new List<int> { 2, 4, 1 } },
                    { 21, "Excepturi in ratione consequuntur voluptas eaque sunt rerum amet autem et.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6250), "Et rerum aut est. Debitis nam nemo voluptatem. Eius minima consectetur commodi quas voluptatem officia fugit exercitationem qui. Voluptatem nostrum dolorem explicabo necessitatibus non aliquam non eos est. Hic ab non qui id sed. Sed ex rem minus exercitationem dolor quia id earum accusantium. Quia totam nihil vel eveniet.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 177006, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6290), 1, new List<int> { 4, 8 } },
                    { 22, "A ducimus ut et nobis aut omnis sit qui placeat vel id ipsa non facere dolorem dolor vel ducimus.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6540), "Dolorum consectetur qui porro. Quis nam illum aliquid autem. Accusantium eaque ea et iure qui sapiente corporis. Nostrum nam a eveniet dignissimos aut dignissimos debitis aliquid aut. Natus voluptatum dolorem est est. Earum enim laudantium modi et distinctio.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 196305, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6540), 1, new List<int> { 10 } },
                    { 23, "Deleniti aut atque totam consequuntur sit quam quia tempore fugiat est unde provident recusandae et velit totam.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6700), "Voluptatum non totam. Aspernatur dolore eum assumenda qui non aliquam. Animi molestias harum culpa eveniet. Aspernatur quod sed.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 143340, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6700), 1, new List<int> { 2, 4 } },
                    { 24, "Rerum magnam dicta totam quaerat ullam voluptatibus accusamus beatae culpa aliquam facere dicta atque a doloribus debitis qui sapiente omnis.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6860), "Nisi quod placeat. Earum dolorem ut fuga aut vel labore. Veniam dolorem similique. Mollitia ea laudantium voluptatem magnam at iste quia.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 166850, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6860), 1, new List<int> { 6, 9, 10, 2 } },
                    { 25, "Ullam accusantium ad quis ut enim et corporis.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7110), "Velit at architecto reprehenderit omnis. Sed accusantium soluta distinctio non omnis. Consequatur vero sed voluptatem quam quis. Delectus officia et porro fuga. Non numquam distinctio saepe at. Ullam nihil nulla voluptatem repellendus placeat omnis autem saepe. Quia est sint et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 168152, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7110), 2, new List<int> { 4, 7, 6 } },
                    { 26, "Eum similique qui enim quia autem quidem quia.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7250), "Sed et iure sapiente quos et amet eum qui. Sequi dolor sint facere et. Eum et atque voluptas maxime. Laborum tempore similique voluptatem voluptates.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 141003, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7250), 2, new List<int> { 9, 10, 7 } },
                    { 27, "Nostrum ipsa et ut rerum voluptas.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7410), "Vero temporibus illum dolorem architecto culpa. Quos velit totam occaecati repellat neque magnam vero qui ad. Dolorem nesciunt quibusdam cumque est in nisi corporis. Autem dolorem consequatur fuga quas corrupti qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 266599, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7410), 2, new List<int> { 8 } },
                    { 28, "Consequatur et laudantium excepturi voluptatum consequatur totam omnis.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7580), "Eum iure unde nulla provident officiis quia quasi id sit. Velit aspernatur quasi exercitationem nihil ad fugiat delectus molestiae. Fugiat qui quis fugit. Doloremque eaque sed consequatur quia dolorem alias. Animi unde nostrum culpa beatae quo voluptatibus quae. Ea praesentium dolor et sequi autem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 266946, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7580), 2, new List<int> { 5, 3 } },
                    { 29, "Sed quia nemo perspiciatis aut sunt architecto aliquid quaerat aperiam recusandae.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7740), "Ut debitis occaecati vel. Iure eligendi maiores voluptatibus. Odit corrupti atque quis eum rerum illo aperiam dolore et. Velit blanditiis neque. Impedit distinctio dolore fuga repellat quo velit iste.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 134304, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7740), 3, new List<int> { 6, 5 } },
                    { 30, "Itaque quis deleniti minus dolore consectetur et deserunt ut aut.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7950), "Doloribus est deserunt mollitia alias ab eos. Explicabo et laboriosam cum. Accusantium cumque voluptatem tempora nobis. Necessitatibus dolorum sit maxime sit voluptatem dolore. Soluta incidunt reiciendis ut ullam est. Doloribus iste est magni nisi velit omnis autem. Aperiam eligendi autem porro modi quia.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 237481, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7950), 3, new List<int> { 7, 10, 6 } },
                    { 31, "Sed fugit perferendis omnis tempora vero voluptatibus aspernatur ullam consequatur eum aut tempora qui officia ut modi ipsum.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9100), "Deserunt eos in error nobis tempore consequatur ipsam quibusdam ea. Natus consequatur autem ratione expedita. Consequatur veritatis ipsum quod a neque aperiam vitae. Architecto aut et aut id et velit. Qui eligendi dolorem natus et possimus in iusto error laudantium. Eum qui perferendis eum molestiae nesciunt est dolores voluptatibus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 126623, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9100), 3, new List<int> { 7, 10, 6 } },
                    { 32, "Enim et quo vitae ut maiores doloremque sit qui quae.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9320), "Sunt omnis deserunt et enim vero saepe rerum quam dolores. Nihil autem et repellat nihil. Est doloremque esse in dolore. Nemo nihil quas aut cupiditate architecto voluptate. A natus quaerat maiores illum omnis. Pariatur id hic rerum qui ut. Repellendus mollitia soluta voluptate mollitia ipsa provident ratione.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 135863, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9320), 3, new List<int> { 5, 7 } },
                    { 33, "Suscipit officiis in ratione quae ut ratione est libero laboriosam placeat.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9510), "Aut est aliquid in vel molestiae veritatis. Odio veritatis et. Sit in mollitia et eum corporis quibusdam deserunt. Excepturi laudantium odit. Aut non laborum voluptatem debitis et optio labore id accusantium. Voluptas aspernatur quod et et. In exercitationem vel deleniti illum est omnis sunt voluptas quod.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 53356, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9520), 3, new List<int> { 6, 5, 4, 8 } },
                    { 34, "Sit tenetur enim in officia ipsum quasi vel quia soluta eveniet soluta tenetur excepturi quisquam nobis non fuga.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(30), "Ducimus facere inventore odit aut ut dolor unde illo maxime. Labore distinctio vel fugiat amet dolorem nihil. Vero est nam ut debitis aut est. Dolores nostrum rerum. Dolor illo non sed aut in recusandae ex et. Dolore et dolorem cum et animi est omnis ut voluptates.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 190636, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(60), 4, new List<int> { 8, 10, 7 } },
                    { 35, "Sit alias eveniet natus non eveniet enim unde et pariatur cumque magnam qui odio officiis neque.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(950), "Enim suscipit rem debitis illo est. Dolore commodi delectus soluta. Commodi magni sed eos eum. Quaerat tempore aut eos laborum beatae unde eum omnis. Quidem ut odit voluptatem ut aut iure.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 207975, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(960), 4, new List<int> { 9, 6, 7, 5 } },
                    { 36, "Enim aut tempore quam recusandae nobis et qui voluptates delectus aut atque.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1160), "Deleniti odit quia tempore dolor sunt dolores sit cum. Et occaecati in. Provident enim hic ut autem amet earum. Sed repudiandae mollitia animi voluptas et provident tempora voluptate adipisci. Quam velit sed. Sit dolores sit. Qui consequuntur doloribus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 277950, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1170), 4, new List<int>() },
                    { 37, "Est delectus ea optio iusto ipsa explicabo magni non.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1370), "Reprehenderit voluptas ab culpa autem iusto dolor voluptas velit. Cumque vero voluptatum natus ut consectetur fugit eaque accusamus porro. Ipsa sed ratione ea voluptatum perspiciatis. Maxime debitis deserunt optio consequatur vel aut qui. Ea corporis asperiores dignissimos in nisi. Dicta sit sit aut quia id dicta aut totam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 184639, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1370), 4, new List<int> { 9, 7 } },
                    { 38, "Magnam commodi quia error neque aut ut ex minima cupiditate dignissimos veritatis omnis.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1580), "Quidem aspernatur maiores velit quo quas. Est saepe velit ex architecto. Odio molestiae debitis sit fuga quod et sit beatae occaecati. Aut et ut. Aut sint et voluptas in mollitia sint sed et. Dignissimos doloribus saepe odio quia soluta. Ut aut dignissimos cum rerum sed numquam non omnis eum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 83719, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1580), 5, new List<int> { 3, 7 } },
                    { 39, "Omnis reiciendis officiis voluptatem dolor possimus porro.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1740), "A dolorem accusantium. Nihil eligendi aut reprehenderit odit ea quia possimus. Aut excepturi rerum est. Beatae laborum dolorem deserunt aut in et velit fugiat facere. Ut qui aut tempora.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 181419, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1740), 5, new List<int> { 1, 9 } },
                    { 40, "Dicta omnis voluptatem rerum est aliquam fugit nostrum.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1950), "Minima et unde velit corporis modi. Accusamus ut quaerat illum dolorem. Velit maiores et occaecati nesciunt saepe ex officia aliquam. Ea enim fugiat qui officiis at magni consequatur ut. Dolores quam assumenda eveniet. Officiis rerum dolore inventore qui accusamus sit ex eveniet repellat. Laboriosam voluptatem voluptatibus omnis ut nemo voluptas ab aspernatur adipisci.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 193016, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1950), 5, new List<int> { 10, 3, 8, 2 } },
                    { 41, "Quibusdam explicabo mollitia modi voluptate consequatur voluptates dolores aperiam ut dolore eos.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2120), "Totam non maiores illo quod laboriosam dolorum placeat impedit. Et distinctio omnis et corporis. Expedita dolorem vel. Dolorem dignissimos nesciunt dolorem. Voluptatibus et illum. Quaerat consequatur cumque fuga rerum unde eaque laborum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 272993, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2120), 5, new List<int> { 1, 7 } },
                    { 42, "Quia eaque fuga commodi quidem voluptatem quae ipsum saepe et accusantium dolorum qui.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2250), "Assumenda ab numquam. Quo et voluptates voluptatem eveniet veniam odit. Ut est eos in quae beatae. Ullam voluptatibus dolore sint rerum expedita qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 217360, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2250), 6, new List<int> { 7 } },
                    { 43, "Ut earum aliquam necessitatibus excepturi enim dolores natus incidunt dolores ut adipisci amet.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2490), "Qui quas perspiciatis. Neque laborum provident optio consequatur ut commodi neque exercitationem. Voluptas quia earum qui dicta est molestiae quis. Dolore ad perferendis facilis doloremque odit. Architecto ipsa qui asperiores quia voluptas cum dolor culpa officiis. Accusantium sunt nisi ipsa assumenda quia autem perferendis autem tempora. Ut illo expedita quia ipsum est omnis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 282891, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2490), 6, new List<int> { 1, 10 } },
                    { 44, "Dolor distinctio sunt quia libero saepe.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2680), "Voluptas et ad optio nostrum provident ut. Rerum dolorum vel at harum deleniti amet vitae accusamus. Qui ut laboriosam aut. Distinctio cum modi voluptatem qui consequatur porro saepe natus soluta. A autem sit. Quo dolor quae ipsam ad omnis veniam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 283842, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2680), 6, new List<int>() },
                    { 45, "Reiciendis dolore omnis illum recusandae dolore.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2830), "Facilis in enim. Ipsum expedita similique. Mollitia ut non magni fugit. Eos occaecati ipsum rerum sit rerum. Porro sunt rem maiores et fugit non. Deserunt repudiandae fugit provident rem omnis amet.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 288629, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2830), 7, new List<int> { 5, 6, 1, 9 } },
                    { 46, "Est ex quia veniam odio quaerat quia.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2990), "Aliquid est commodi error sed et est consectetur. Necessitatibus sed consequatur vitae id labore. Voluptates ex dolore numquam necessitatibus eos. Omnis mollitia maiores provident est maiores velit. Neque aut aut laboriosam consectetur aperiam id ut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 109274, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2990), 7, new List<int> { 5, 6 } },
                    { 47, "Sed rem officia iusto voluptatum voluptatum architecto.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3160), "Illum qui accusantium sunt excepturi mollitia voluptatem sequi et odio. Ut quis recusandae voluptas. Voluptatem sit laboriosam eos odit ullam perspiciatis enim ipsa. Sapiente ea dolore suscipit veritatis eligendi illum minus. Sed perspiciatis corporis fuga. Aliquam nostrum et quod rerum enim esse distinctio.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 159454, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3160), 7, new List<int> { 1, 9, 6 } },
                    { 48, "Mollitia commodi optio iure nisi corrupti repellat ex sit nulla atque labore iste.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3330), "Ipsa impedit veniam et enim deleniti. Illo ut voluptatem alias voluptatem optio est. Nobis quia et quo. Consequatur saepe aspernatur. Aperiam sint sed rem perferendis. Explicabo quis earum est ut ab dignissimos.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 233384, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3330), 7, new List<int> { 3 } },
                    { 49, "Non cumque sed officia repellendus dolorem in quibusdam.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3500), "Amet nulla et debitis quos quia commodi. Maxime dolores ut ipsam ut. Quos rerum et fuga est laudantium facilis unde. Est sint ratione sit saepe consequatur et. Velit dolor sit eveniet magnam occaecati velit iure voluptatem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 94601, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3500), 8, new List<int> { 7, 10 } },
                    { 50, "Assumenda aspernatur et rerum sint libero voluptatum voluptas repellat quia maiores saepe ut unde molestiae.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3660), "Alias nihil fugiat quia vel sint ipsam. Iusto porro nihil dolores. Quae enim eligendi doloremque. Qui et a quos optio et in laborum amet.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 214207, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3670), 8, new List<int> { 7, 1 } },
                    { 51, "Aperiam sed culpa earum aut maxime similique possimus optio officiis ut commodi laboriosam molestiae rerum.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3810), "Magnam velit sequi molestias porro ut voluptatem sit. Asperiores cum quia consequatur est. Hic assumenda fugiat et. Enim eaque omnis ut blanditiis aut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 69968, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3810), 8, new List<int> { 5, 3, 4 } },
                    { 52, "Iusto aut doloribus ipsa et voluptate molestias.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3950), "Eaque labore autem cumque. Aut deleniti est. Quaerat ut laudantium hic quia. Sapiente facere velit soluta rerum animi possimus aliquid consequatur officiis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 147382, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3950), 9, new List<int> { 7, 4 } },
                    { 53, "At expedita rerum fuga ut expedita reprehenderit.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4080), "Saepe aspernatur facere illum vel consectetur. Ut harum deserunt ut eius qui aperiam eum et iure. Possimus quos temporibus amet placeat aliquid voluptatem inventore ad. Doloremque sunt alias aut nemo quibusdam et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 231009, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4080), 9, new List<int>() },
                    { 54, "Rerum qui unde nobis a minima dolorem sint.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4310), "Rerum odio expedita doloremque. Natus quam et et dicta est laboriosam. Nemo voluptatibus dolorem aut cum nemo. Debitis deleniti voluptas est numquam atque numquam possimus facilis exercitationem. Atque fugiat quam dolore at. Dolorum ad quia. Aut molestias fuga consequatur amet libero voluptatem.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 143513, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4310), 9, new List<int>() },
                    { 55, "Quos molestiae iusto quisquam porro aut.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4450), "Reprehenderit veniam nobis eos. Autem quis et id velit facere aliquam fuga. Et quibusdam est laudantium cum sint quis ratione molestiae qui. Molestiae vel dolorem ut ut voluptas. Modi iste nulla aut beatae. Ipsam quis vel.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 210871, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4450), 9, new List<int> { 10, 1 } },
                    { 56, "Omnis sint consequuntur ab odit hic quaerat tenetur et sapiente iusto aut vel sed commodi pariatur blanditiis.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4760), "Ut voluptatem vero reiciendis est. Voluptatibus ipsam assumenda nobis voluptatem itaque qui sunt. Quia quis cum aliquid et ut sint sunt debitis. Provident exercitationem aspernatur quaerat dolorum error repellendus aliquid. Recusandae magni nobis commodi reprehenderit eum consequuntur. Enim perferendis non iste praesentium praesentium ad voluptas ipsa architecto.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 293539, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4770), 9, new List<int>() },
                    { 57, "Fuga illum molestiae id sit hic possimus incidunt voluptatem excepturi itaque dolor aliquam.", 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4960), "Accusamus nemo cupiditate tenetur. Eligendi voluptatibus omnis consequatur aut maiores. In qui explicabo doloremque eum incidunt ratione aut sed voluptates. Dolor sit eligendi modi. Beatae minima eum omnis. A sed sed veritatis sed sint ab aperiam adipisci.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 129563, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4960), 10, new List<int> { 6 } },
                    { 58, "Dolor eligendi voluptatem ut consequatur animi.", 4, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5140), "Ut deleniti beatae voluptatibus est rem ratione nam dicta accusantium. Cumque reprehenderit autem nihil rerum aperiam sit ad. Ducimus autem voluptatibus aut impedit. Velit dolores earum quam quibusdam voluptate. Velit voluptatem consequatur. Quia consequuntur est itaque possimus blanditiis molestiae asperiores voluptatem. Officia veniam hic.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 55728, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5140), 10, new List<int> { 4, 6 } },
                    { 59, "Nulla et ea reprehenderit cumque ut cupiditate consequatur officiis iste corrupti illum modi assumenda.", 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5360), "Vel culpa ipsum. At eum aliquam enim qui velit. Quibusdam incidunt aut voluptates excepturi quod reiciendis minima omnis. Aut qui id commodi fugiat vitae quo aperiam eos. Aliquid accusantium quia mollitia et totam. At reiciendis incidunt sed laborum nemo molestias. Ut neque alias voluptatem quia officia ut animi inventore.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 85016, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5360), 10, new List<int>() },
                    { 60, "Esse veritatis exercitationem repellat doloribus officia labore.", 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5480), "Nobis ut itaque rem dolores quas vel vel cum inventore. Hic est quis nesciunt et. Nam facilis rem quia rerum repellendus. Voluptate facilis dolores rem dicta.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 219367, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5480), 10, new List<int> { 9, 2, 4 } },
                    { 61, "Eaque possimus itaque ratione odio quo aut assumenda et et sint veniam.", 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5660), "Error molestiae fugit pariatur dicta nemo aliquid expedita. Dicta repellat adipisci aliquid nostrum doloremque ad. Adipisci ea ducimus. Quae commodi qui molestias omnis. Numquam qui labore at molestias. Omnis non quas at.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 181488, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5660), 10, new List<int> { 5, 7, 9, 4 } }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "PostId", "Rating", "RatingAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Omnis in ex nihil et inventore. Vitae et ex doloremque.", 1, 1, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(1930), 2 },
                    { 2, "Voluptatum voluptatem perferendis molestiae consequuntur quas quia aperiam. Aperiam in magnam atque repellat eos qui animi.", 2, 6, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3310), 4 },
                    { 3, "Vel eaque eaque itaque tempora consequatur id officiis nostrum doloribus. Dicta suscipit esse aut et animi.", 3, 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3580), 4 },
                    { 4, "Dolor nam molestiae earum reprehenderit. Rem dolores animi tempore.", 4, 8, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3830), 4 },
                    { 5, "Voluptas ut et necessitatibus quasi. Dolorem eveniet consequuntur officia.", 5, 6, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(3980), 4 },
                    { 6, "Esse nemo velit quia molestiae modi. Optio qui numquam dolores nihil repellendus.", 6, 5, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4120), 3 },
                    { 7, "Unde et omnis provident odio dolores nam fugiat eius itaque. Voluptas sint libero maiores a.", 7, 3, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4310), 5 },
                    { 8, "Cupiditate est voluptates ipsam necessitatibus sapiente. Dolores cupiditate voluptas aut deserunt ratione ipsa tempore et.", 8, 1, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4450), 4 },
                    { 9, "Harum exercitationem maiores. Voluptas iure magni similique voluptatibus vitae doloribus ea.", 9, 3, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4670), 5 },
                    { 10, "Inventore voluptate accusamus animi vel aliquam molestias exercitationem pariatur. Quis voluptate inventore.", 10, 9, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(4860), 5 },
                    { 11, "Id incidunt molestias molestiae et omnis repellat fugit. Mollitia ea est reprehenderit voluptatum voluptates.", 11, 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5050), 2 },
                    { 12, "Quia iusto optio rerum dolorem eum blanditiis neque. Aut ipsum doloremque quia qui.", 12, 10, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5240), 2 },
                    { 13, "Repellat ipsam repudiandae et quis illo. Officiis vitae nobis officia et libero.", 13, 8, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5360), 3 },
                    { 14, "Sed molestiae facere rerum omnis laboriosam odio commodi. Blanditiis atque consectetur quas harum soluta et.", 14, 9, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5540), 3 },
                    { 15, "Rerum maiores placeat fugit. Reiciendis aut eos assumenda incidunt repudiandae necessitatibus.", 15, 4, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5710), 2 },
                    { 16, "Totam dolor voluptas est distinctio enim sunt nulla. Eligendi animi maiores iste et aspernatur.", 16, 9, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(5870), 5 },
                    { 17, "Inventore aspernatur ratione voluptates. Dolorem ab corrupti dolor rerum molestias occaecati nam error.", 17, 2, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6080), 2 },
                    { 18, "Est quo est vel est est sit culpa et. Facilis dolorum nihil.", 18, 8, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6240), 4 },
                    { 19, "Sequi amet sint. Ut quas ducimus iusto.", 19, 1, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6480), 1 },
                    { 20, "Vitae iure doloribus labore quisquam eveniet et voluptatum. Tempora ratione voluptate nam ut quia.", 20, 8, new DateTime(2023, 5, 16, 22, 57, 50, 819, DateTimeKind.Local).AddTicks(6610), 2 },
                    { 21, "Accusamus minima cumque officia itaque. Ut velit fugiat.", 21, 7, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6300), 4 },
                    { 22, "Voluptates iste nulla consectetur minus ut tenetur eos voluptates. Repellendus at et consectetur et consectetur quia sequi.", 22, 1, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6540), 3 },
                    { 23, "Et quia rerum accusantium qui laborum sint ducimus incidunt. Nulla facere voluptatem minus quia aut.", 23, 10, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6710), 7 },
                    { 24, "Nam sint tempora sed laborum. Nisi vel doloremque molestias sit esse sit.", 24, 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(6860), 10 },
                    { 25, "Atque repellat nam soluta maxime non veniam quo qui. Rem omnis tempore qui illum debitis.", 25, 1, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7120), 3 },
                    { 26, "Omnis earum voluptatem omnis id consequatur. Autem non eum voluptatum odio.", 26, 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7260), 4 },
                    { 27, "Quo at ut cumque dolore. Et quod fugit praesentium sint nihil adipisci.", 27, 10, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7410), 6 },
                    { 28, "Ea rerum voluptas sit. Ut id odit accusamus similique harum.", 28, 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7590), 9 },
                    { 29, "Et earum accusantium distinctio perferendis porro numquam temporibus perferendis est. Fuga iste est rerum sequi.", 29, 7, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7750), 2 },
                    { 30, "Quia voluptatem aperiam nemo aut voluptates. Perspiciatis voluptas laboriosam voluptate accusantium.", 30, 5, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(7950), 6 },
                    { 31, "Ratione quos minus non. Tempora nesciunt aut blanditiis eveniet perspiciatis.", 31, 7, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9100), 8 },
                    { 32, "Qui est magnam autem rerum fugit nostrum quaerat. Non assumenda fugit rerum qui.", 32, 2, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9320), 8 },
                    { 33, "Quaerat magni sed ab. Dolor quasi porro consequatur.", 33, 1, new DateTime(2023, 5, 16, 22, 57, 50, 996, DateTimeKind.Local).AddTicks(9520), 10 },
                    { 34, "Sapiente error voluptates officiis cum. Sint asperiores qui velit temporibus autem sequi eos.", 34, 10, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(140), 2 },
                    { 35, "Debitis temporibus autem aut dolore laborum reiciendis. Expedita quis sed.", 35, 10, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(960), 5 },
                    { 36, "Mollitia quo eius veniam non. Blanditiis unde repudiandae quaerat officia.", 36, 7, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1170), 6 },
                    { 37, "Voluptatem accusamus ad et perferendis dolorem. Debitis eligendi perferendis nesciunt ea veritatis voluptas sapiente incidunt rerum.", 37, 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1370), 5 },
                    { 38, "Voluptate qui nobis animi voluptatibus omnis sit ratione sed et. Magni facere inventore pariatur aut non aliquam iure.", 38, 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1580), 3 },
                    { 39, "Molestiae quia aspernatur consequatur optio dignissimos officiis sed. Quis similique fugit natus voluptatem rerum explicabo qui voluptatum voluptates.", 39, 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1750), 2 },
                    { 40, "Repudiandae dolor quia amet. Consectetur nihil adipisci accusamus nemo in ut.", 40, 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(1960), 6 },
                    { 41, "Consectetur nihil sed pariatur voluptatem. Recusandae aut qui dolorum dolores perspiciatis hic.", 41, 9, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2130), 6 },
                    { 42, "Neque eum eos molestias ut ipsa debitis velit provident. Officia voluptas aut laboriosam ipsum sit.", 42, 8, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2260), 5 },
                    { 43, "Aspernatur cum quaerat ullam quia quasi doloremque saepe quia. Nobis autem non maxime dolores minima corrupti et.", 43, 10, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2500), 1 },
                    { 44, "Est delectus amet voluptate consequatur nesciunt quas dolorem a quo. Unde voluptas non corrupti.", 44, 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2680), 1 },
                    { 45, "Quo provident quia hic libero. Repellat explicabo unde ad sequi veritatis.", 45, 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(2830), 3 },
                    { 46, "Voluptatem nobis sit vel. Ea enim omnis illo ut.", 46, 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3000), 8 },
                    { 47, "Et est earum similique voluptatem. Sequi placeat sunt maxime nesciunt quo quae qui.", 47, 6, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3160), 6 },
                    { 48, "Alias id deserunt velit est ea cumque. Magni provident omnis minus error eius quo eum soluta illo.", 48, 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3340), 8 },
                    { 49, "Consequatur rerum consequatur reiciendis dolores nisi incidunt eius ut facilis. Quae quia ullam aliquid reprehenderit ipsum omnis sit architecto.", 49, 6, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3500), 7 },
                    { 50, "Iste illum assumenda voluptates nihil amet. Cumque quidem omnis fugiat aut magnam et ut non.", 50, 1, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3670), 5 },
                    { 51, "Ipsa qui ut voluptatibus sit pariatur sit qui voluptatem exercitationem. Dignissimos aut tempora voluptatum.", 51, 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3810), 1 },
                    { 52, "Voluptatibus error blanditiis ea voluptates. Repudiandae quibusdam ea.", 52, 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(3960), 7 },
                    { 53, "Dolor omnis qui officia molestias est eveniet eaque. Necessitatibus ut doloremque.", 53, 5, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4090), 4 },
                    { 54, "Sunt aspernatur iure. Eligendi quia aliquid rerum repudiandae sint.", 54, 7, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4310), 2 },
                    { 55, "Rerum natus amet qui voluptatem. Qui qui incidunt natus aut deserunt sint et.", 55, 10, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4460), 8 },
                    { 56, "Dicta aut nemo neque consectetur illum hic. Quam nostrum iure rerum enim vitae similique.", 56, 9, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4770), 4 },
                    { 57, "Nostrum quidem modi consequatur quibusdam asperiores qui ab. Nulla est sit dolor deserunt sit.", 57, 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(4960), 6 },
                    { 58, "Fugit non aut voluptates et quibusdam enim doloremque quisquam voluptatem. Fugit omnis doloremque pariatur enim facere.", 58, 3, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5140), 6 },
                    { 59, "Voluptatum vitae modi veniam. Culpa ullam distinctio reiciendis.", 59, 7, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5360), 1 },
                    { 60, "Et et inventore incidunt saepe. Vel et vitae deleniti enim facere aliquid doloremque.", 60, 7, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5480), 7 },
                    { 61, "Sunt sit dolorum blanditiis voluptas quis. Sint aspernatur vitae expedita.", 61, 2, new DateTime(2023, 5, 16, 22, 57, 50, 997, DateTimeKind.Local).AddTicks(5660), 3 }
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
