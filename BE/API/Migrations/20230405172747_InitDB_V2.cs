using System;
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
                columns: new[] { "Id", "Address", "Email", "Legit", "Name", "PasswordHash", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { 1, "497 Stamm Fields, North Monserratchester, Kazakhstan", "hieucckha@gmail.com", -1.0, "Hiếu Nguyễn", "1000:L/M6l+BLtD1DDrZXn0a2yzqNofqtrl52:z3MXqLw0X0wX3UA5dMlYNPrwttw=", "0197313668", 0 },
                    { 2, "35668 Brekke Square, Averyshire, Mayotte", "Cade63@gmail.com", -1.0, "Jedidiah Goyette", "1000:BgXZIy3r2oeyuauzGZd88eQhyPXP+7+s:uRYKEk/+BLdDjzwOXA9TQpK1V6Y=", "3639204153", 0 },
                    { 3, "92853 Zachery Harbors, Gleasonfort, Hong Kong", "Agustina_Erdman@gmail.com", -1.0, "Destiney Cassin", "1000:UkXyg4O+pJuhhZTCV2qGC0k8Qh14fYnL:1RJ/CmRQ5HhidTOxF5um75W2XHU=", "7156547870", 0 },
                    { 4, "582 Block Common, Rooseveltmouth, Macedonia", "Juana.Dach85@hotmail.com", -1.0, "Mason Kunde", "1000:KeliJSeRyoUzgptuWsQsYvTT/ItDoYh+:e3UFivHBW+1F0G637pInakxwDAw=", "5646135754", 0 },
                    { 5, "2673 Baumbach Court, Wiegandchester, Pitcairn Islands", "Clair22@gmail.com", -1.0, "Lilla Wolff", "1000:g0N5E/8sDObPIRjVW4fyUjDHtYIec6m7:OIQMGn2XN3PUcHs7+lujMlzjdNA=", "2668370946", 0 },
                    { 6, "797 Adam Track, South Wilma, Austria", "Izaiah.Weber65@gmail.com", -1.0, "Emmanuelle Kerluke", "1000:eORBnOa8O0Wcc+h6IAFkSLqtzsaaVEvM:fPOMX8B0iCv2WCnTqClobJ8siGo=", "4598419531", 0 },
                    { 7, "6735 Ratke Ports, Arthurshire, Cocos (Keeling) Islands", "Bennett_Kessler91@gmail.com", -1.0, "Constantin Dietrich", "1000:K/BUMAeJOPLFMxkzFtT+jkQletAqDBj0:KMK4y7UFPfK1v5Nr69C4jkxfd44=", "1985140582", 0 },
                    { 8, "791 Leffler Plaza, Port Erik, Gibraltar", "Ebba.Wiegand@gmail.com", -1.0, "Sarina Stiedemann", "1000:oZDBSCRm1J3fivBPXdTFdEzaEPY6cpf/:YERFZX0TH8P3+7K708SyyKvu0mQ=", "0320901788", 0 },
                    { 9, "6585 Mueller Rapid, Louieville, Australia", "Thad_Haag98@gmail.com", -1.0, "Reynold Kreiger", "1000:Rq0fNelqYfeJGRR2bNeHSTgJwzktGFu8:t4AOkHyrH4AD77L9Y6Bg0AtLODU=", "2407790242", 0 },
                    { 10, "152 Jaiden Summit, Port Orionmouth, Rwanda", "Evangeline.Howell@yahoo.com", -1.0, "Earl Wyman", "1000:z47UgvVGTeUkQZ0no3O9kiDLaUHbLqIW:3N2qAb/mMRELgWaYOlyI5cG3bss=", "3581021763", 0 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Caption", "CategoryId", "CreatedDate", "Description", "IsDeleted", "IsHide", "IsSold", "MediaPath", "Price", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Omnis nulla fugiat sed quo sit cum.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 166, DateTimeKind.Local).AddTicks(710), "Inventore quaerat saepe expedita molestias quis nesciunt blanditiis rerum unde. Tempora libero voluptas illum vitae est omnis non sapiente qui. Id esse iure voluptatem. Non vel aut et. Voluptas explicabo rem libero molestiae deleniti est iusto eum sunt. Tempora et repellat unde fugit in alias quidem.", false, false, true, null, 275491, new DateTime(2023, 4, 6, 0, 27, 47, 174, DateTimeKind.Local).AddTicks(6310), 1 },
                    { 2, "Adipisci dolorum nihil minus culpa explicabo iste recusandae voluptatem molestias et molestias sed ut quisquam nulla.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4670), "Non veritatis est incidunt dolores voluptatem illo quo tempora. Consequatur debitis minus facere voluptate quae impedit. Deleniti blanditiis aut delectus quia quisquam. Molestias natus ab doloremque recusandae itaque reprehenderit molestiae.", false, false, true, null, 256342, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4670), 1 },
                    { 3, "Aliquam qui sed qui architecto veritatis nihil sed molestiae dolores.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4890), "Saepe sunt ea consequatur et voluptatum accusantium ea id. Illum molestias ut. Voluptatem quod commodi quas. Libero dolorem maiores nesciunt.", false, false, true, null, 281673, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4890), 1 },
                    { 4, "Ut iusto sit sunt molestias error vel velit.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5150), "Rerum illum sit. Iste sit pariatur omnis iste error voluptatem occaecati eos. Tempore dolorem saepe culpa qui voluptate vitae dignissimos illo. Voluptatum eos impedit sunt est. Consequatur quia est voluptas. Et vel modi. Sunt repellendus provident quae magni quidem.", false, false, true, null, 61653, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5160), 2 },
                    { 5, "Ipsum illo omnis aut deleniti qui veniam consequuntur enim eos et accusamus illum praesentium ut suscipit est totam est sed.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5360), "Qui iste error nulla veritatis labore aliquid. Iusto totam ut labore accusamus nobis et animi. Quia praesentium eveniet vitae hic dicta. Ex culpa nesciunt iusto. Et reprehenderit et.", false, false, true, null, 249275, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5360), 2 },
                    { 6, "Dolores doloribus culpa voluptatem laborum laboriosam voluptas.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6340), "Et et consequuntur consequatur. Eum corrupti maxime ratione non enim modi odit error aliquam. Quibusdam velit cupiditate dignissimos architecto. Ea ea eligendi assumenda debitis. Aut autem quod quo numquam saepe similique quisquam assumenda. Est ab soluta vero asperiores unde est quaerat in reprehenderit.", false, false, true, null, 96540, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6340), 2 },
                    { 7, "Veniam molestiae occaecati ratione velit laboriosam molestias dolorum magni soluta ipsam assumenda est aut.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6630), "Qui enim ipsum. Doloribus nostrum nobis expedita nam tenetur architecto sint. Explicabo illo ipsam labore temporibus neque sit est sit ut. Impedit voluptas omnis placeat esse. Nihil quis vel. Sapiente asperiores eos aut distinctio blanditiis. Ut ut id ratione velit.", false, false, true, null, 136992, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6630), 3 },
                    { 8, "Saepe nemo facilis magni tenetur officiis quis in ut vero quo laborum voluptatem minus ipsam ut.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6840), "Sed id maiores dolorem est dolor. Reprehenderit vel veritatis similique. Accusamus ex qui quia provident ipsa vel dolor deserunt. Hic expedita reiciendis. Dolores nesciunt et et.", false, false, true, null, 124795, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6840), 3 },
                    { 9, "Nihil cumque in minus dignissimos enim veritatis exercitationem est praesentium sit quam nesciunt doloremque ut et et.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(7050), "Et voluptas rerum quibusdam est. Veniam ex tenetur et similique ea. Molestias quis eos cum. Quos quae laudantium repudiandae sint dolor iure cumque dolores.", false, false, true, null, 147888, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(7050), 3 },
                    { 10, "Et laudantium aperiam eos et eum non ea optio autem asperiores ut et.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8030), "Voluptatem unde animi. Tempore et eveniet consequuntur. Reprehenderit ea ipsam qui dolorem sed repudiandae et non porro. Nulla itaque quas qui culpa alias.", false, false, true, null, 53796, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8030), 4 },
                    { 11, "Aut ipsa cumque voluptatibus laboriosam officiis nostrum quia laboriosam voluptas rem.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8230), "Qui nostrum reprehenderit quos atque. Aspernatur sit suscipit. Deserunt beatae alias. Doloribus sit eos ex enim. Quae quasi corrupti et beatae neque. Eveniet et quo quas quod voluptas quis commodi qui harum.", false, false, true, null, 93031, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8240), 4 },
                    { 12, "Aliquid a et soluta consectetur quis sint.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8410), "Est temporibus eos dolorem sunt tempore aspernatur in consequatur. Quae et esse eos non velit praesentium delectus et. Quasi autem sunt. Placeat temporibus exercitationem optio earum vel et dolores.", false, false, true, null, 249603, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8410), 4 },
                    { 13, "Ut qui quod quis aut aut voluptatem nobis eligendi corporis explicabo facilis repellat ut.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8610), "Et quia ad eaque temporibus neque eum. Omnis est ut aut. Cupiditate est nostrum ducimus. Recusandae animi et. Quam voluptates impedit fugit recusandae.", false, false, true, null, 209867, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8610), 4 },
                    { 14, "Aut omnis soluta eos sint rerum porro dolorem eos itaque molestias rem aut iure quos vel.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(9840), "Nisi molestiae omnis libero ab non. Praesentium eaque soluta enim ex. Hic deserunt et non rem nisi. Voluptatem a quis placeat commodi excepturi ullam. Quod accusamus qui consequatur reprehenderit. Sit consequuntur omnis voluptas est voluptas officia. Velit rem suscipit dicta autem necessitatibus aspernatur ut repellat.", false, false, true, null, 57394, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(9840), 4 },
                    { 15, "Vel molestiae animi aspernatur molestias ut recusandae corrupti recusandae quidem debitis accusantium commodi odio rerum adipisci.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(70), "Laudantium rerum animi magni neque placeat quae et fugiat. Eos vel repellendus. Autem saepe et explicabo beatae soluta nam inventore rerum. Aperiam error ad at perferendis et quisquam qui enim.", false, false, true, null, 223260, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(70), 5 },
                    { 16, "Ad ut mollitia praesentium deserunt perspiciatis eligendi libero.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(290), "Nesciunt consequatur porro omnis amet. Non qui ea numquam nam voluptates distinctio maiores et. Consequatur vel harum deserunt eos hic quia corporis. Reiciendis vel inventore aut libero. Ullam libero recusandae commodi eos. Itaque dolorem ad repellendus soluta voluptas neque repudiandae aut.", false, false, true, null, 185205, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(290), 5 },
                    { 17, "Nesciunt necessitatibus error esse soluta doloribus rem recusandae cum et voluptatem nisi omnis ad repellendus.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(1480), "Adipisci sed beatae nobis. Sunt tenetur nemo aut et unde quo nulla. Nam facilis hic animi et amet eos eaque. Repudiandae quis quasi ut praesentium numquam sint quis voluptas. Illo sed eos dolorum eveniet reiciendis recusandae voluptatem occaecati. Explicabo consequatur omnis consequuntur hic.", false, false, true, null, 231948, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(1480), 5 },
                    { 18, "Omnis sed doloremque qui nobis.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(3740), "Officiis reiciendis ipsam mollitia commodi velit aliquam magnam repellat. Libero facere consequatur voluptatem ipsum quasi qui labore beatae quae. Aut dolor iusto voluptas aut modi. Aut accusamus cumque eum et libero vel dolore.", false, false, true, null, 220589, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(3790), 1 },
                    { 19, "Enim nihil magni et aut quod repudiandae cupiditate quisquam iusto ex ipsa rem quia nostrum ut.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4120), "Voluptatum et dolorem est. Sed quis ut quia non omnis et et aut. Id voluptatem aut consequatur sed aperiam officiis praesentium ut. Laudantium recusandae illo accusantium qui. Voluptas odit maiores excepturi. Expedita similique totam similique mollitia repellendus commodi autem laborum cumque.", false, false, true, null, 85679, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4130), 1 },
                    { 20, "Ipsam ipsam dignissimos porro eos.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4340), "Ducimus error nostrum doloribus sit consequatur a numquam quas. Praesentium dignissimos quis incidunt totam voluptas laboriosam aut doloribus. Voluptate ab iusto voluptate in autem sapiente illo. Et ullam qui dolores aut non sit eum esse.", false, false, true, null, 65785, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4340), 1 },
                    { 21, "Animi quis quia numquam aperiam.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4530), "Est adipisci commodi necessitatibus harum et. Expedita ut est. Sit temporibus dolor voluptates repellendus architecto saepe itaque mollitia ratione. Modi beatae ea ut esse eveniet voluptas inventore. Architecto cum consectetur.", false, false, true, null, 79561, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4540), 1 },
                    { 22, "Repudiandae numquam nemo ut dolore sed blanditiis minima sed sint quia harum occaecati atque omnis.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4820), "Cupiditate qui necessitatibus excepturi non sed rerum. Porro et molestias. Ipsum quisquam dicta voluptates eaque. Tenetur et a perferendis sint adipisci earum et. Perspiciatis aliquam tempora nisi ea voluptas. Ab sint odio ratione iusto ullam et a ratione.", false, false, true, null, 159711, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4820), 2 },
                    { 23, "Veniam iure ipsam quis omnis.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5080), "Nobis molestias soluta odio consectetur dolor aut et incidunt. Et est nostrum ut. Voluptate nemo aspernatur dolorem sint ut quia non sit quae. Dignissimos aut nesciunt porro. Aut harum recusandae provident expedita est repellendus. Rem molestiae et praesentium aperiam reiciendis.", false, false, true, null, 250338, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5080), 2 },
                    { 24, "Architecto quasi repudiandae quas id impedit ut hic voluptas.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5770), "Blanditiis qui voluptate. Aliquam voluptas necessitatibus explicabo veniam sequi et iure. Unde ut consectetur necessitatibus. Est cupiditate doloribus distinctio minus voluptatem sequi. Voluptatem quis molestiae. Saepe laudantium qui delectus error perspiciatis voluptatum quam qui. Ipsam porro vel et alias consectetur suscipit.", false, false, true, null, 168217, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5770), 2 },
                    { 25, "Sunt culpa aut omnis voluptatem labore explicabo animi autem eveniet.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6050), "Molestiae ex eius ullam sint quia. Reprehenderit mollitia itaque aut suscipit cumque. Ullam vitae quisquam quo qui fuga est aut ipsam. Facilis consectetur est earum provident vel corrupti et. Consequatur consequuntur quisquam quia non consequatur temporibus. Temporibus provident eos unde.", false, false, true, null, 78871, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6060), 3 },
                    { 26, "Necessitatibus autem sunt non quia non eveniet aut soluta non omnis placeat et autem dolor.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6240), "Ut voluptas doloremque amet voluptate aspernatur mollitia. Cumque iusto voluptate. Voluptatem id sed dolores molestiae. Minus voluptatem ea esse aut.", false, false, true, null, 284453, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6240), 3 },
                    { 27, "At et fugiat rem nemo odit eos odit numquam excepturi molestiae optio ratione ratione doloremque magnam dolorem.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6500), "Voluptatibus perspiciatis blanditiis et. Cum sit et assumenda officia eius voluptas neque nisi. Est accusantium exercitationem corrupti unde quibusdam quia sed amet autem. Velit repudiandae eos sint sed minima illum quaerat. Blanditiis tempora nostrum voluptates. Eius velit ut similique similique amet beatae est suscipit. Aut vel quia distinctio eum aspernatur non.", false, false, true, null, 77239, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6500), 3 },
                    { 28, "Quis commodi cupiditate qui voluptatem odio nostrum.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7080), "Ipsum aut eum ea eos ab soluta voluptatem omnis. Consequatur beatae exercitationem eius dolor soluta fugit ut magnam. Aut sint ut error asperiores commodi non. Veniam aut deleniti dolorem eius voluptatibus omnis autem. Officia et quisquam nulla laboriosam eveniet alias non temporibus magni.", false, false, true, null, 85051, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7080), 3 },
                    { 29, "Quia numquam architecto magnam consequuntur voluptatem modi voluptate et ut voluptatem perferendis molestias consequuntur ratione at.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7280), "Earum enim ipsa aspernatur sit quia esse nostrum. Quia id nam ex suscipit maiores. Adipisci ut libero rem suscipit mollitia repellat voluptas. Quia veniam id accusantium voluptate sunt.", false, false, true, null, 165638, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7280), 3 },
                    { 30, "Et occaecati fugiat sint rerum exercitationem.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7480), "Numquam et modi minima mollitia sit et aut amet minima. Molestiae consequatur fugit laborum pariatur non. Dolorem doloremque voluptas soluta. Voluptatum hic quae nisi qui rerum. Magni est perferendis totam assumenda. Consequatur fugit voluptatem.", false, false, true, null, 78783, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7480), 4 },
                    { 31, "Rerum natus fuga neque et consequatur maiores nobis voluptas et voluptas neque distinctio ut voluptas corrupti qui.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8610), "Sint deleniti sed ea asperiores reprehenderit nostrum laudantium. Assumenda odio minima. Commodi earum corporis magnam expedita. Non quae in ut qui sit officia. Cumque blanditiis consequatur dicta fuga et nam eius. Et quidem expedita et.", false, false, true, null, 67107, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8620), 4 },
                    { 32, "Quia repudiandae eum soluta et rerum minus quo voluptatem et minima placeat.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8840), "Sit officia voluptate sed perspiciatis eum ea nihil sunt sed. Quia eveniet repudiandae quis est. Ea autem odit nemo distinctio ipsam non esse vitae et. Quibusdam et enim ullam voluptas veritatis saepe soluta optio.", false, false, true, null, 80673, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8840), 4 },
                    { 33, "Sit qui ex consequatur rerum corporis dicta officia qui tempore at repellat consectetur ut delectus.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9080), "Et cumque provident dolor. Eos accusantium ducimus placeat. Ea beatae et dolores quos doloremque sed aut at. Nihil delectus officia dolores in autem inventore. Omnis et id optio dolore qui enim. Velit et ut impedit est qui corporis quos deserunt consequatur.", false, false, true, null, 179702, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9080), 4 },
                    { 34, "Ab et voluptas voluptatibus blanditiis consequatur esse voluptatibus sapiente corrupti laboriosam molestiae.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9300), "Commodi saepe ipsa voluptas. Nostrum dolorum hic magnam. Placeat cupiditate quod omnis laborum. Molestiae exercitationem voluptas. Ut corrupti ut voluptas hic cum aliquid. Laudantium delectus inventore quam unde commodi autem neque. Et aut et enim.", false, false, true, null, 163037, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9300), 5 },
                    { 35, "Ipsa reprehenderit ullam quia architecto mollitia voluptas rerum.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(300), "Fugiat provident dolorem labore magnam. Tempora aliquid ab distinctio possimus assumenda soluta. Fugit harum exercitationem sed alias. Magnam quasi sequi aut et quo a aut. Adipisci nemo aliquam ut dicta et. Quia sint soluta at ut nesciunt iste possimus corporis. Nihil omnis earum sed quisquam eum.", false, false, true, null, 56623, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(300), 5 },
                    { 36, "Similique id cumque officiis earum placeat mollitia velit quo quas deleniti ullam consequatur corrupti consectetur voluptate rerum adipisci voluptatem earum.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(560), "Accusantium voluptatem numquam dolor quia laboriosam nemo iusto tempore quia. A sunt in dolor. Labore debitis officiis corporis est veritatis vitae enim. Quis voluptas praesentium eligendi nobis. Est consectetur quos aut officia illum incidunt excepturi magni sed. Veniam sed molestiae sint. Ut est qui.", false, false, true, null, 133850, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(560), 5 },
                    { 37, "Ad voluptas magni aut eos.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(740), "Ipsa iusto explicabo et perspiciatis dolores. Ab error dolores ullam blanditiis ex quia asperiores velit incidunt. Vitae error dolores et aut facere. Sed a tempora pariatur. Illo sunt ex est consequatur quaerat exercitationem.", false, false, true, null, 77122, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(740), 6 },
                    { 38, "Sunt autem laudantium enim vel earum magni eaque veritatis voluptatum omnis ut dicta qui ut voluptatibus qui adipisci voluptatem officia.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(990), "Est ut magni. Doloremque aut quo voluptas qui. Ab et et pariatur explicabo ut eos. Velit harum modi distinctio reprehenderit nemo quia qui nam. Officiis et at itaque. Vel magnam modi libero est voluptate harum qui et eligendi. Temporibus aspernatur et necessitatibus quia et nesciunt.", false, false, true, null, 109657, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(990), 6 },
                    { 39, "Velit est omnis quis doloribus soluta non unde minima iure rerum sunt mollitia sit voluptas rem inventore.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(1830), "Fugit neque dolores atque asperiores consequatur magnam esse voluptas. Expedita qui dolore. Sed libero hic. Harum et rerum odit provident quasi perspiciatis excepturi ipsum autem. Architecto tempore eveniet a repellendus exercitationem. Aspernatur vel quasi.", false, false, true, null, 179986, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(1830), 6 },
                    { 40, "Qui eos voluptates harum et est dolorum quo mollitia omnis mollitia soluta id quas.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2090), "Illo nobis aspernatur. Non quae voluptatem. Praesentium voluptate quia delectus similique reprehenderit quo maxime veniam distinctio. Quis nihil blanditiis quasi qui officia fugit sed. Sed dignissimos ut. Ad nesciunt omnis facilis voluptate in voluptatibus consequatur. Praesentium reiciendis cupiditate et accusamus qui vel.", false, false, true, null, 173529, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2090), 6 },
                    { 41, "Qui eos perferendis incidunt qui cum omnis sunt vitae.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2310), "Officiis nostrum facilis quisquam. Sint expedita nisi accusantium. Et labore veniam nemo quis saepe aut similique. Iure quis ducimus sunt rem. Ratione praesentium voluptatibus adipisci officiis odio aspernatur excepturi reiciendis eos. Aut sequi occaecati quia facilis ea aut.", false, false, true, null, 143967, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2310), 6 },
                    { 42, "Omnis necessitatibus aliquam id cupiditate perferendis eligendi voluptate qui laudantium ad nihil error.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3330), "Facere sit est quia alias minus. Sapiente impedit impedit id atque nihil sed. Omnis aut aut ut in consequatur odio dolore amet. Ex dicta tenetur quia. Soluta harum reiciendis voluptas aperiam magni qui. Doloremque placeat velit aut reiciendis.", false, false, true, null, 105352, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3330), 7 },
                    { 43, "Qui consequatur sed voluptatum repellendus doloribus quas.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3560), "Labore occaecati aspernatur architecto. Aut vel sit totam enim voluptatibus. Modi ipsa temporibus eos culpa rerum magni exercitationem. Nobis placeat unde vel distinctio quod officiis officiis ullam recusandae. Repudiandae unde deserunt autem consequatur quidem delectus. Ut rerum ea perspiciatis. Aut architecto et.", false, false, true, null, 196614, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3560), 7 },
                    { 44, "Delectus sunt ullam ducimus fugit voluptatem labore dolorum harum eaque reprehenderit et.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3790), "Repellendus aperiam aut et sit. Earum quis aspernatur. Rem beatae assumenda tempore eius nulla. Maiores nobis reprehenderit alias deserunt. Aperiam praesentium sint vero rerum exercitationem est beatae. Officiis est quidem tempore rem laboriosam. Architecto vitae minus corrupti.", false, false, true, null, 274223, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3790), 7 },
                    { 45, "Quis ea non dolores rerum aut vero magnam hic dolorum qui aut consectetur sed magnam.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(4020), "Eos eius explicabo exercitationem. Repellendus possimus ut omnis ut eligendi quo voluptatem culpa ducimus. Magnam quis commodi explicabo sequi in velit qui. Velit ducimus aut sapiente sit nostrum. Deleniti ipsam aliquam. Ipsa consequuntur fugit nesciunt aliquid assumenda. Maiores sed eum qui.", false, false, true, null, 183104, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(4020), 7 },
                    { 46, "Id rerum sunt natus debitis omnis et dolores voluptatem voluptate maxime a sit tenetur.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5010), "Laboriosam odio dolorem a voluptatem voluptatibus ipsam in eos doloribus. Cumque quo et iste quis vel porro laudantium. Est rerum aut reiciendis nam odit voluptatem. Et quis dolorum corrupti quis.", false, false, true, null, 64124, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5010), 8 },
                    { 47, "Voluptatem cumque quo ut impedit et corrupti voluptatem magnam et in.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5200), "Voluptas quidem sequi quia error sapiente occaecati fuga quia. Quo quis veniam laborum aut maxime ex voluptatem qui id. Vel suscipit velit minus ducimus sint quo recusandae ut. Eius dolorem laborum enim pariatur.", false, false, true, null, 161614, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5200), 8 },
                    { 48, "Dolore et minima neque est voluptas qui rerum consequatur quos voluptatem delectus alias excepturi autem eos.", 2, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5530), "Placeat expedita ab quisquam blanditiis recusandae sequi ratione ex in. Officiis pariatur deserunt ut quos saepe numquam. Optio ratione sapiente neque consectetur ut voluptas sed. Natus fugiat quia fugit rerum recusandae omnis et voluptates. Et laudantium eligendi laudantium molestiae quos sed quis est reiciendis. Impedit perferendis illum ullam optio modi corporis.", false, false, true, null, 131455, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5530), 8 },
                    { 49, "Consequuntur dolor nemo debitis non impedit non voluptate a voluptas eum et consequatur.", 4, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5690), "Repudiandae est perferendis eaque. Tenetur aspernatur itaque in repellendus. Nobis fuga non delectus. Omnis illo voluptatibus.", false, false, true, null, 75771, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5690), 9 },
                    { 50, "Et consequatur illo labore laborum.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6510), "Iure suscipit nemo culpa id suscipit et quis. Consequuntur aut iure ab molestiae fugiat magnam numquam consequatur. Est repellat non odit adipisci. Perferendis at quaerat ducimus sed qui et.", false, false, true, null, 248538, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6510), 9 },
                    { 51, "Voluptatem quod tempore molestiae in voluptates.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6710), "Saepe ex perspiciatis reiciendis sed reprehenderit molestiae. Itaque nisi dolor velit dolores doloremque officiis delectus fugiat. Exercitationem et consequatur sit et ut sed. Et ut neque voluptatum qui qui corrupti.", false, false, true, null, 248636, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6710), 9 },
                    { 52, "Dolorum blanditiis quos hic deserunt ut rem exercitationem distinctio iste quos et et.", 5, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6920), "Ut corporis autem. Et molestiae nihil et ut debitis. Reprehenderit hic dolores et nulla. Sint cum enim. Voluptas rerum minima recusandae.", false, false, true, null, 83360, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6920), 10 },
                    { 53, "Asperiores ut est hic laboriosam totam et.", 1, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8330), "Officiis repellendus vitae. Tempora velit unde id itaque. Delectus dolorem illo illum doloremque et eveniet enim. Perferendis et itaque laboriosam voluptatibus et sit deserunt. A ab tenetur atque harum ratione. Dolorum qui quaerat ab reiciendis nulla ab ut. Aut exercitationem placeat error et sit et velit et.", false, false, true, null, 61475, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8340), 10 },
                    { 54, "Et aspernatur at non voluptates fugiat qui vel omnis sint voluptatum qui.", 3, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8570), "Qui ipsum minima aut adipisci. Recusandae unde qui et odio temporibus necessitatibus consequuntur. Necessitatibus pariatur quae et voluptatem laboriosam quo sit. Reprehenderit sit praesentium maiores enim. Quia dolores fugiat quos. Id deleniti commodi necessitatibus ducimus sint aperiam ipsa qui soluta.", false, false, true, null, 83775, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8570), 10 }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "PostId", "Rating", "RatingAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Commodi alias quia molestias qui. Aut nisi quod accusamus error nemo voluptate nulla ut.", 1, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(2630), 4 },
                    { 2, "Reprehenderit ab officiis qui maxime porro in qui voluptatibus hic. Ad totam est veritatis eius laboriosam nostrum praesentium.", 2, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4680), 4 },
                    { 3, "Atque quia rem provident quia quos neque officia illo aliquid. Veritatis sit et quia et ullam.", 3, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(4900), 2 },
                    { 4, "Harum aut aliquam voluptates quisquam magni. Quia asperiores nobis corrupti.", 4, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5160), 1 },
                    { 5, "Animi alias ducimus. Quaerat molestiae voluptates modi odio facilis ea distinctio nemo rem.", 5, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(5370), 3 },
                    { 6, "At iste et voluptas quisquam. Exercitationem et qui similique culpa quas.", 6, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6350), 5 },
                    { 7, "Occaecati delectus totam ut ratione error labore assumenda illum enim. Temporibus fugit sunt nihil laborum iste maxime iusto et ipsam.", 7, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6630), 1 },
                    { 8, "Quisquam quisquam repellendus omnis expedita a odio rerum voluptas. Ea vero voluptatem ut temporibus fugiat laborum cupiditate aspernatur.", 8, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(6840), 2 },
                    { 9, "Dolore expedita fuga sapiente. Culpa nostrum aut ut voluptatem et ratione.", 9, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(7060), 4 },
                    { 10, "Velit rerum autem. Similique qui molestiae.", 10, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8030), 3 },
                    { 11, "Est iste minus fuga ut. Ipsam voluptatem earum voluptatem sed assumenda.", 11, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8240), 2 },
                    { 12, "Qui reprehenderit non. Recusandae reiciendis accusantium doloremque veniam saepe a.", 12, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8420), 5 },
                    { 13, "Non et fugiat minima vel et maxime omnis est adipisci. Autem facere aut enim dolore reprehenderit blanditiis.", 13, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(8610), 5 },
                    { 14, "Et voluptatem iste eos deserunt nihil nesciunt sit. Amet tenetur dolores optio sit at qui enim.", 14, 0, new DateTime(2023, 4, 6, 0, 27, 47, 175, DateTimeKind.Local).AddTicks(9840), 2 },
                    { 15, "Aspernatur eos pariatur iste omnis numquam dolorem quia enim. Qui ut alias non consequatur.", 15, 0, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(70), 1 },
                    { 16, "Ratione dolor qui. Perspiciatis fuga veritatis fugit sit.", 16, 0, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(300), 4 },
                    { 17, "Temporibus omnis quia officiis. Adipisci tempore sequi repudiandae quibusdam sed dolorum soluta laudantium aut.", 17, 0, new DateTime(2023, 4, 6, 0, 27, 47, 176, DateTimeKind.Local).AddTicks(1490), 4 },
                    { 18, "Voluptatem quia accusamus. Qui qui eligendi enim odit iure aut repudiandae eaque ipsa.", 18, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(3820), 10 },
                    { 19, "Voluptas iste tempore rerum ex sequi facilis. Animi totam id velit suscipit eos.", 19, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4130), 10 },
                    { 20, "Sit voluptatem quos est voluptates odio exercitationem. Dignissimos eligendi molestiae et autem odit et.", 20, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4340), 6 },
                    { 21, "Eligendi qui quaerat aut. Vel et sunt maxime maxime molestias.", 21, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4540), 2 },
                    { 22, "Sed nesciunt accusantium quo at magni aliquam rerum voluptas nisi. Ut fugiat quis et molestiae eum commodi id qui aut.", 22, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(4830), 5 },
                    { 23, "Aperiam voluptates commodi et consectetur aperiam veritatis quia deserunt. Ipsum consectetur rem aspernatur maiores.", 23, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5080), 6 },
                    { 24, "Voluptate et ipsam sunt autem quia similique accusantium omnis eum. Doloribus dolores facilis optio esse quas eveniet.", 24, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(5780), 8 },
                    { 25, "Ab eos quia asperiores et voluptatibus amet laudantium ab accusantium. Mollitia eum asperiores blanditiis harum.", 25, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6060), 10 },
                    { 26, "Quis iste facere rerum amet debitis quasi quia. Voluptate velit sed.", 26, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6240), 9 },
                    { 27, "Voluptas nostrum culpa suscipit rerum quisquam ea. Ipsum est qui pariatur ipsam dolores qui.", 27, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(6500), 4 },
                    { 28, "Sed vel sunt ad tempora dolor qui. Doloremque officiis in perferendis.", 28, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7080), 5 },
                    { 29, "Et magnam veniam. Quia totam error ipsam provident magnam inventore.", 29, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7280), 1 },
                    { 30, "Fugiat qui saepe. Sit delectus eos velit aliquid corporis et similique corrupti.", 30, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(7480), 10 },
                    { 31, "Repellat totam hic autem sunt accusantium dolorum. Saepe dolores voluptas voluptas occaecati.", 31, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8620), 10 },
                    { 32, "Qui dolor amet et accusamus autem ut ipsa aut. Aperiam nemo voluptates quam.", 32, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(8840), 2 },
                    { 33, "Possimus at amet fuga cumque nisi amet. Non a natus itaque doloribus autem corrupti explicabo numquam.", 33, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9090), 8 },
                    { 34, "Adipisci hic est minima similique sit at. Facilis est facere.", 34, 0, new DateTime(2023, 4, 6, 0, 27, 47, 321, DateTimeKind.Local).AddTicks(9300), 9 },
                    { 35, "Explicabo recusandae rerum aut illum. Voluptatem consectetur et et neque ut excepturi quod.", 35, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(310), 10 },
                    { 36, "Voluptatem pariatur vel numquam. Doloribus rem ut libero dicta rerum vel ipsa.", 36, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(560), 7 },
                    { 37, "Temporibus quia ut ad beatae beatae. Dolorem maxime expedita debitis a voluptatibus itaque ea.", 37, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(740), 4 },
                    { 38, "Eveniet qui tempore. Maxime reiciendis itaque est quibusdam.", 38, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(990), 4 },
                    { 39, "Dolor consequatur eveniet hic et blanditiis autem iusto incidunt. Velit harum molestiae voluptatum quos perferendis ducimus et reprehenderit.", 39, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(1830), 1 },
                    { 40, "Consequatur veritatis eligendi est harum porro et quidem et est. Quia illum dolores.", 40, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2090), 8 },
                    { 41, "Aperiam perspiciatis qui dolorum ab tempore provident in eligendi ratione. Voluptatem nam itaque.", 41, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(2310), 10 },
                    { 42, "Ullam neque eos rem. Quidem recusandae dolor repellendus nihil.", 42, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3340), 2 },
                    { 43, "Ratione explicabo perferendis libero suscipit vero reprehenderit adipisci molestiae. Placeat sit laborum eligendi id reprehenderit.", 43, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3560), 8 },
                    { 44, "Officia enim et ut asperiores itaque. Ea suscipit ad explicabo rem sed eius fugit ducimus.", 44, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(3790), 10 },
                    { 45, "Sunt qui illum quia commodi aliquam sit similique quaerat beatae. Aliquam id aut natus temporibus placeat perferendis.", 45, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(4020), 4 },
                    { 46, "Eaque qui eum. Beatae quia consequuntur nemo maiores aspernatur fuga quaerat autem tempora.", 46, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5020), 2 },
                    { 47, "Aspernatur sint sapiente in quis sit velit earum. Excepturi odio sit earum.", 47, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5200), 3 },
                    { 48, "Aspernatur fuga quia adipisci perspiciatis qui atque debitis. Perspiciatis ullam dolorum voluptatem.", 48, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5540), 9 },
                    { 49, "Eius soluta quisquam dolore. Illum optio consectetur enim quia laboriosam consequuntur.", 49, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(5690), 7 },
                    { 50, "Ut totam quos labore blanditiis dolores qui consectetur. Nostrum dolores eos consequatur.", 50, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6520), 10 },
                    { 51, "Sit soluta iusto qui distinctio animi aspernatur illo rem sapiente. Iure voluptatem nobis aut.", 51, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6710), 1 },
                    { 52, "Temporibus maiores repellat. Enim ut voluptatem fugiat.", 52, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(6920), 7 },
                    { 53, "Id distinctio ut numquam. Est alias dolore asperiores totam aut omnis.", 53, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8340), 8 },
                    { 54, "Unde cupiditate exercitationem recusandae ex. Consequatur soluta impedit doloremque.", 54, 0, new DateTime(2023, 4, 6, 0, 27, 47, 322, DateTimeKind.Local).AddTicks(8570), 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PostId",
                table: "Carts",
                column: "PostId");

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
