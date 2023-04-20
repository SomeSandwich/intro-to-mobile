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
                    { 1, "1850 Fannie Harbors, Tatumton, Timor-Leste", null, "hieucckha@gmail.com", -1.0, "Hiếu Nguyễn", "1000:e1iReyjHgPAC3pAmVKr/CFzKY/EiHzXf:iPb/Wat9nodlkpm6Yy/FxmmgPlc=", "4616467866", 0 },
                    { 2, "29751 Koss Groves, Lake Micheal, Togo", null, "Mathew0@gmail.com", -1.0, "Maurice Swift", "1000:690t69zwGSQJqeVcrVzrg2deOG8WlO6v:wndGtm/JHoNdajUf3q/rPoWAyWI=", "0530895044", 0 },
                    { 3, "93824 Kirlin Canyon, South Arielview, Benin", null, "Juston63@hotmail.com", -1.0, "Jonatan Jacobi", "1000:4D01ugmra1wdQolEIu93XghKGLC3qUl7:15iyQcev+Bh48omH2MNSNyqAflQ=", "1145834539", 0 },
                    { 4, "61803 Hyatt View, South Providencimouth, South Africa", null, "Briana.Tillman@gmail.com", -1.0, "Erika Harber", "1000:wFX0fP4/JZ31EXTbUx/jwpjMs9cGwydB:2L20xhd4m9+X9sd2uLcbCMkh+20=", "8560542540", 0 },
                    { 5, "69471 Paolo Harbors, South Justonbury, Lesotho", null, "Celestine5@hotmail.com", -1.0, "Frida Stracke", "1000:IHqrxuBTkqquNcg5Ug2+EyhImUEbAtT3:WgxDqyobWKnFQtyjAajGsrlQQjk=", "9838995346", 0 },
                    { 6, "97636 Rowan Club, New Thaliaport, Cuba", null, "Jett.Will95@yahoo.com", -1.0, "Jan Kris", "1000:aHVsFcGxiYzIpsbNWEkfz93HHdxz+oGP:+BpYukNo4OqZJlmD4t7DUzSZXBM=", "4178235415", 0 },
                    { 7, "030 Jarrell Run, New Maximilianborough, Saint Barthelemy", null, "Charlotte27@hotmail.com", -1.0, "Theo Klocko", "1000:Ua+8YqTEh9Guyr0/3AkZcuXBgmxW/tmT:ZN1MF/1YKGRhMQnYoYsusqFEgHM=", "4959255912", 0 },
                    { 8, "1648 Ritchie Port, Port Floyd, Colombia", null, "Erika_Greenfelder@gmail.com", -1.0, "Lolita Runolfsdottir", "1000:39i2uBAZoKFLm8jna06DG6nPKxizR6Da:qL5NhMJogrVe1y28dulyf/K3+M0=", "9908359970", 0 },
                    { 9, "799 Skiles Prairie, West Adalbertochester, Sao Tome and Principe", null, "Colby11@hotmail.com", -1.0, "Jacinto Batz", "1000:h1PVnoGGwL0xZyiBiQrqSnRfSjlWulVt:EM9PEY3xz8Va/rnba/CTO6nJFEU=", "8409682370", 0 },
                    { 10, "114 Joanne Crossroad, East Leonardport, Central African Republic", null, "Moises.Flatley@yahoo.com", -1.0, "Emmanuelle Jaskolski", "1000:lyODTXgnw9sslBfBhl7lgZ4Yd9rsycZL:TJ+PaokIn7rnQomC1w7bvjGH998=", "7046794073", 0 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Caption", "CategoryId", "CreatedDate", "Description", "IsDeleted", "IsHide", "IsSold", "MediaPath", "Price", "UpdatedDate", "UserId", "UserShare" },
                values: new object[,]
                {
                    { 1, "Aperiam voluptate tempore omnis eos.", 1, new DateTime(2023, 4, 16, 5, 38, 55, 961, DateTimeKind.Local).AddTicks(9130), "Repudiandae optio voluptas magni est saepe explicabo velit odio. Est harum expedita id repudiandae debitis odio beatae quis. Facilis neque quia dolor provident. Perspiciatis voluptates non facilis rerum. Nulla ipsa quos pariatur vel. Nihil velit quis earum reprehenderit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 50687, new DateTime(2023, 4, 16, 5, 38, 55, 969, DateTimeKind.Local).AddTicks(5150), 1, new List<int>() },
                    { 2, "Voluptatem mollitia dolorum ab placeat eaque reiciendis qui molestias optio non.", 3, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(610), "Magnam qui assumenda est eos sit distinctio est non. Quae ipsa sit fugiat. Vero eos fugiat sint. Facilis consectetur corrupti labore atque soluta. Fugit soluta est eveniet consectetur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 291713, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(620), 1, new List<int> { 5 } },
                    { 3, "Et maxime beatae adipisci quisquam molestiae ullam doloribus commodi doloremque nisi.", 4, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(820), "Dolore ut eos facere corporis qui corrupti sit quis dolorem. Accusamus quia sed necessitatibus et libero explicabo reprehenderit consequatur ratione. Facilis dolor quod dolorem iure eum a aliquam. Qui sit illo cum quo voluptates voluptatem voluptatem illo veniam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 75705, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(820), 1, new List<int> { 2 } },
                    { 4, "Quidem et culpa facilis delectus id in omnis impedit repellat earum aut et impedit voluptas autem repellendus.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6370), "Ut ea et suscipit qui. Laboriosam sint error. Mollitia et voluptas. Maiores esse iusto qui autem officia aut cupiditate. Qui omnis suscipit illum officiis sit. Non velit quod ratione sapiente aliquid. Voluptas maxime molestiae magnam quam magni rerum quia.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 60004, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6370), 2, new List<int> { 5, 4, 1 } },
                    { 5, "Aliquid sit perferendis facere ratione.", 2, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6560), "Provident repellat minima rerum alias eius quisquam eligendi placeat. Sit voluptatem est sed nostrum. Quis et ea. Fugit ipsam debitis quo vel. Eveniet quos consequatur voluptas odit ut deleniti quia. Corrupti maiores id eius soluta et quidem atque. Tenetur ea non impedit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 84302, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6560), 2, new List<int> { 3 } },
                    { 6, "Consectetur ut et odio debitis et molestias nobis enim est eos molestiae dolorem ad sunt rem alias laboriosam velit velit.", 2, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6730), "Qui dolor ipsa minus tempore totam id ipsum dolorum. Quia voluptatum dolore sed eaque voluptatem beatae rerum. Qui et sint atque. Exercitationem est perferendis eius minima error. Est et voluptas nisi quia qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 63798, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6730), 2, new List<int> { 1 } },
                    { 7, "Sequi voluptate ea quo soluta ipsam molestiae voluptatem nam ea eaque vel facere accusantium.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6890), "Harum dignissimos dolorum dolorum ut error. Provident enim iusto sed consequuntur eligendi. Tempora amet rerum et. Quis enim maiores mollitia ratione. Fuga cumque ad et in nobis qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 214654, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6890), 2, new List<int> { 5 } },
                    { 8, "Consequatur consequuntur nisi nihil dolorum sunt quos corrupti velit tempore non et eum corrupti totam magnam sint consequatur.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7100), "Asperiores voluptatem asperiores odio molestiae et nemo non. Id aperiam omnis ut voluptate sed culpa esse quia. Pariatur dolores vero qui numquam sint. Dolore sunt eligendi vel blanditiis ipsam qui. Laborum iure maiores impedit dolorem ut sint facilis. Quis dolore magni animi et asperiores non iste. Blanditiis consequatur in sint occaecati qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 111628, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7100), 2, new List<int> { 3, 5, 4, 1 } },
                    { 9, "Consequatur perspiciatis exercitationem est nesciunt eos perspiciatis in et fuga enim debitis inventore eos repellat voluptate corrupti quidem.", 3, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7250), "Voluptates explicabo temporibus nostrum culpa et vel laudantium. Voluptas dolore id nostrum aut ea. Sequi unde ea atque ut. Doloribus qui voluptatem ipsum. Qui consectetur ab soluta quisquam. Perferendis sunt ut maiores quasi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 193491, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7250), 3, new List<int>() },
                    { 10, "Eligendi reiciendis officiis rerum animi.", 3, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7410), "Amet nobis minus incidunt qui omnis excepturi illum fuga. Ex cum dolorem autem distinctio exercitationem aut dolorum ipsam. Animi sapiente ad cumque velit harum. Et praesentium iusto asperiores perferendis modi omnis nihil. Ut veritatis voluptates vel quia totam vero ipsam rerum voluptates. Amet cupiditate sit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 127534, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7410), 3, new List<int> { 5, 1 } },
                    { 11, "Eaque nihil nihil unde quod voluptatem nihil ratione delectus a qui rerum autem beatae rerum hic et aperiam id.", 2, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7560), "Et voluptatem ullam fugit expedita asperiores dignissimos natus voluptatem. Aut nam ut facere animi ea. Explicabo soluta dignissimos at pariatur iure minus reiciendis. Quo mollitia ut consectetur iure qui in amet deleniti et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 96375, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7560), 3, new List<int> { 5, 1, 2 } },
                    { 12, "Alias iure ex et nesciunt quidem doloribus perferendis commodi voluptatum laborum dolores consequuntur impedit impedit et et omnis.", 4, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7700), "Sunt ut saepe. Aut totam et illum eos quos eaque quos saepe. Quia quos rerum facere eligendi quos. Repellat aut cum possimus harum nisi.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 57632, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7700), 3, new List<int> { 2, 4, 1 } },
                    { 13, "Voluptatem ullam et eligendi doloremque officia rerum nihil quas ab aspernatur laudantium quis dolorem alias vel enim consequatur ipsum.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7830), "Et enim consequatur nam quas. Iusto voluptatem ea. Placeat nihil id sit fugit ut. Enim blanditiis repellat dolor tempore id ut totam eaque pariatur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 140965, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7840), 3, new List<int>() },
                    { 14, "Modi sit hic quia cum sapiente nihil molestias assumenda id ut accusamus sunt laboriosam et excepturi dolores et est.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8020), "Laborum debitis eum similique ex vitae maiores fugiat inventore nemo. Blanditiis temporibus magnam. Tempore modi perferendis dolorem incidunt facilis veniam. Ab sunt quae quia distinctio excepturi assumenda incidunt veritatis. Officiis harum ipsa labore nostrum minus dignissimos voluptatibus amet. Dolore ex omnis natus molestias nesciunt magni consequatur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 205514, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8020), 4, new List<int>() },
                    { 15, "Ut veritatis ut libero nihil est.", 5, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8170), "Vel quae sunt perferendis distinctio voluptatem ut nobis. Earum officiis repellendus. Voluptates iste suscipit exercitationem velit consequatur nam necessitatibus. Ut nulla tempora porro illo ut. Corporis repellat velit laudantium fugiat et fugit praesentium numquam. Quis eos velit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 53386, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8170), 4, new List<int> { 2, 3 } },
                    { 16, "Nulla neque voluptatem quisquam eius minima expedita inventore aperiam ipsum provident nostrum voluptatum eum odio omnis.", 3, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8310), "Natus eveniet consectetur placeat incidunt quo occaecati vel. Aut sit provident nobis quibusdam aliquam eos. Est omnis illo et in voluptas provident et est. Qui distinctio cupiditate corporis magni cumque similique accusantium.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 196814, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8320), 4, new List<int> { 2, 3, 1, 5 } },
                    { 17, "Id odio aut ducimus reiciendis et.", 4, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8520), "Cum minus nulla provident qui quidem iusto nostrum. Minus excepturi cumque sint et ut sit. Et dolor magni ut aperiam quo est. Accusamus omnis deserunt maiores voluptas libero possimus sint veritatis. Aut quo aut sapiente ipsam ex. Omnis officia impedit animi. Sint occaecati quos officia et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 231353, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8520), 5, new List<int> { 3, 2, 1, 4 } },
                    { 18, "Eveniet assumenda voluptas incidunt qui quibusdam in et ad nam consequatur quam et magnam sed omnis doloribus deserunt quisquam et.", 2, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8650), "Aut libero aut. Delectus velit pariatur dicta. Enim occaecati delectus. Dolor repellat consequatur repellat omnis sequi. Soluta harum iusto illo magnam suscipit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 245853, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8650), 5, new List<int> { 2 } },
                    { 19, "Fugit consequatur necessitatibus autem qui libero.", 2, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8760), "Voluptatem laborum iure et qui qui unde eveniet. Aut magni totam suscipit. Dolore dolores aut hic nostrum eveniet. Et quas consectetur ratione voluptates est. Aut tempora quo.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 113589, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8760), 5, new List<int> { 4 } },
                    { 20, "Dolore nihil est placeat et.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4510), "Et aliquid neque et labore quo magni. Dolores reiciendis amet exercitationem accusamus perferendis ea vel. Laudantium in magnam inventore. Totam eius quia quas quidem ea occaecati eum consequatur. Dolores et fugiat magni et suscipit necessitatibus odio vero molestiae. Aut et aperiam quia rem rerum non assumenda quos aperiam. Rerum sed dolores officiis qui est repudiandae asperiores.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 133831, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4560), 1, new List<int> { 5, 4, 8 } },
                    { 21, "Minus quos eveniet molestias cupiditate odit sint.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4770), "Accusantium fugit sed sint et dolor. Nihil est dolorem nobis. Voluptatibus et et. Tempore ducimus labore aut quia aut nostrum. Repellat quibusdam recusandae aperiam ut odit consequatur voluptatibus. Ab a ut non libero ipsa.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 109128, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4770), 1, new List<int> { 2, 8, 5 } },
                    { 22, "Eius officiis cupiditate qui eius.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4940), "Ea in minima incidunt reprehenderit voluptatem sit ut sit. Vitae doloribus nihil earum. Eaque similique facere voluptas consequatur in. Laudantium ipsam odio. Commodi et aut culpa. Est beatae similique ut dolorem molestiae aliquam ut numquam. Illum aperiam earum doloremque voluptas dicta vel eaque suscipit cum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 168786, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4940), 1, new List<int> { 5 } },
                    { 23, "Natus quas et aut libero blanditiis maxime id qui aut consequatur non quidem consectetur eum rem soluta deserunt repudiandae.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5130), "Optio sed voluptate totam recusandae iure repellendus cupiditate aperiam doloribus. Voluptas repudiandae enim a dolor eveniet iusto provident deleniti error. Similique eveniet numquam temporibus et enim laborum dolorum. Iste tempora laborum aut modi tenetur officia vitae aliquam consequatur.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 139856, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5130), 1, new List<int> { 5 } },
                    { 24, "Qui culpa sed ipsum necessitatibus quidem hic est architecto.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5300), "Aut exercitationem voluptatum eaque at. Omnis officiis ducimus. Ut quasi adipisci sed aut aliquam quia tenetur. Ducimus in provident dolorem deserunt quia quis est. Iusto blanditiis facilis consequatur exercitationem consequatur dolores voluptas. Quia delectus ea. Et cum velit.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 213456, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5300), 1, new List<int> { 10, 4, 7, 6 } },
                    { 25, "Non blanditiis voluptatibus pariatur perferendis aut.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5410), "Enim est inventore ipsam saepe. Ipsa quam facilis dolor blanditiis consequuntur sit vel. Aut id rem et. Aut non sed similique aut ut similique.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 52186, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5410), 2, new List<int> { 9, 3, 8 } },
                    { 26, "Sed sunt aut fugit nesciunt aut molestiae tenetur earum.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5540), "Non eos tempora praesentium mollitia hic dicta autem. Vero voluptates nisi inventore. Id quos accusantium velit iusto. Laudantium dicta ut odio deserunt distinctio est modi. Ipsa est dolores nobis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 83069, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5540), 2, new List<int>() },
                    { 27, "Ut iusto soluta natus distinctio voluptatum autem tempore.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5700), "Itaque aut sit ea est voluptatem dolorum facere praesentium cumque. Reiciendis ut aliquam. Est voluptates dicta ratione voluptate doloremque delectus. Nulla molestias corrupti fugiat dolore autem illo.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 107589, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5710), 2, new List<int> { 7 } },
                    { 28, "Rerum ut odit quo sit maxime.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5880), "Cumque rerum voluptatibus quas atque ut officiis ut. Et et voluptas dolorum dolorum aut necessitatibus. Sed ut suscipit dolore vel aut praesentium. Ut iure quidem. Iure aliquid quam et qui assumenda ut. Dolor et pariatur velit autem et placeat. Adipisci nesciunt aliquid sapiente sint nihil non.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 201436, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5890), 2, new List<int>() },
                    { 29, "Iure temporibus sunt rerum rerum aliquam in vel deleniti unde et ut magnam sapiente qui dolores est libero velit est.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6040), "Fuga accusamus veritatis voluptatum a omnis dolorum dolorum. Eius est quidem ratione sapiente sit debitis distinctio consequatur. Sapiente omnis accusamus aut minus accusamus ab dolorum autem. Libero tenetur ut quisquam non. Et pariatur nostrum tempora.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 273219, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6040), 2, new List<int> { 9, 7 } },
                    { 30, "Inventore autem dolorem aliquam ab delectus eaque eius fugit non cupiditate ea et id ut inventore.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6210), "Qui in ad. Sequi amet nihil ex voluptatem suscipit voluptatem. Atque cum nemo voluptatem ipsa. Placeat odio molestiae cumque. Ullam cumque debitis. Est qui qui doloribus vitae rerum ut cupiditate nemo. A voluptatem reiciendis autem ut dolorem id consectetur perferendis nesciunt.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 149493, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6210), 3, new List<int> { 6 } },
                    { 31, "Placeat quia iure animi in ut omnis autem tempore rerum modi debitis voluptatibus dicta.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6340), "Rem explicabo facere sunt et quos qui rem velit. Tempore aliquam exercitationem. Debitis aut mollitia dolorem velit est. Iusto reiciendis ut reiciendis enim temporibus. Cumque aspernatur sint harum odit quis doloribus vel.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 254859, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6340), 3, new List<int>() },
                    { 32, "Consequatur vero et consequatur unde modi porro quam.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6510), "Id sequi iure esse doloremque et esse. Eligendi quia ab dolore ut culpa et ea facere omnis. Ad eius quas laboriosam laborum. Vel sit est beatae non dolores iusto. Rerum qui nihil molestiae quisquam ex odio accusantium impedit et. Cumque rerum dolor aliquam dolorem et quam quos itaque.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 102242, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6510), 3, new List<int> { 2, 8, 6, 10 } },
                    { 33, "Blanditiis perferendis quia vel consequuntur.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6660), "Ea non ipsa. Occaecati earum assumenda corporis sed. Tempora praesentium sed quibusdam fugit perspiciatis aliquam fugit harum omnis. Aut autem nihil quo ut animi sed repudiandae et. Numquam perferendis est natus repellat dolores voluptatibus autem. Aut eaque cumque. Hic fugit ut ipsam consequatur vel dignissimos.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 97183, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6660), 3, new List<int> { 6, 4, 2 } },
                    { 34, "At reprehenderit fugit vero dolores et rerum quibusdam et tenetur.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6820), "At facere nisi molestiae voluptatibus consectetur repellendus. Vel quia quia sunt quaerat porro rerum numquam. Cupiditate numquam a consequatur voluptate et. Necessitatibus aut voluptate quaerat. Delectus atque sapiente quis asperiores amet sapiente. Deserunt facilis sed omnis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 288462, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6820), 4, new List<int> { 6, 2, 1, 3 } },
                    { 35, "Quaerat sit est voluptate maxime nihil voluptatem qui officia.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6960), "Asperiores consequatur ab et eius. Tempore neque aliquam aperiam non nihil aut. Vel ullam aperiam molestiae quae facilis ipsum laudantium. Rerum expedita suscipit quia omnis. Voluptas accusamus omnis qui.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 174167, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6960), 4, new List<int> { 10, 2, 7 } },
                    { 36, "Incidunt animi culpa minus est.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7090), "Maxime quia repellat sequi perspiciatis quo rem ullam. Voluptas eos et enim deleniti sit qui voluptas rerum id. Officia eum nostrum quaerat aliquam distinctio asperiores eos molestiae quia. Maxime quod quod rerum dolores velit rerum vitae.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 67519, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7090), 4, new List<int> { 8, 7, 1 } },
                    { 37, "Facere totam deserunt alias fugit amet soluta.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7220), "Perspiciatis tenetur assumenda nihil vel modi vitae. Ex enim totam architecto dignissimos deserunt unde. Nesciunt eos ut hic tempora. Beatae nobis eveniet. Sequi optio eum sequi qui qui aliquam ducimus illum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 264785, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7220), 4, new List<int> { 6, 1 } },
                    { 38, "In amet optio optio ut.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7330), "Odio et quaerat. A eius atque velit voluptates. Molestiae rem voluptatibus consequatur. Tenetur omnis qui libero ea odit voluptas velit explicabo nihil. Rerum provident suscipit maiores nobis placeat nulla sed facere nulla.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 126957, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7330), 4, new List<int> { 9, 2 } },
                    { 39, "Assumenda dolores voluptatem nesciunt dolor alias.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7440), "Consequatur dolore sint nihil quae quae sit. Aliquam laboriosam rerum ad facere eum. Aut atque sint mollitia quia. Esse esse voluptate ab inventore et maxime harum sunt quo.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 234734, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7440), 5, new List<int> { 2 } },
                    { 40, "Quibusdam animi culpa et dolorem assumenda eos sit qui id sit.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7590), "Id ea unde voluptatum quidem impedit sequi et et. Veritatis tempore non deserunt saepe amet tempora pariatur est. Eum necessitatibus delectus quidem rerum quos est laboriosam. Aut similique assumenda in culpa dolor. Est laboriosam accusantium in.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 119163, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7590), 5, new List<int> { 3, 1, 7 } },
                    { 41, "Tempore at et omnis maiores et eveniet omnis ipsam officiis adipisci sequi delectus amet occaecati.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7730), "Officia pariatur voluptatem non dignissimos. Id ut expedita et saepe nobis ipsum voluptatem. Quia quia corrupti voluptatem atque. Consequatur quidem aut autem earum. Delectus voluptate aut omnis labore accusantium. Maiores placeat quod unde commodi vel.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 265752, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7730), 5, new List<int> { 1 } },
                    { 42, "Perspiciatis doloribus omnis ut enim voluptate a consequuntur consequatur aut eum possimus praesentium.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7920), "Laudantium ea voluptates commodi repudiandae iste iste occaecati est. Dolore inventore et qui dolor quo impedit qui quis tempora. Minus excepturi voluptatem ut non consequatur perferendis consequatur. Est et rem autem tenetur. Dignissimos tenetur voluptate beatae odio vel. Voluptatem ipsa dolores impedit esse tempora.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 110658, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7920), 5, new List<int> { 2, 9 } },
                    { 43, "Necessitatibus quia quae voluptate aliquid cupiditate.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8060), "Neque et iste qui laboriosam quis tenetur minus aspernatur. Eius sapiente assumenda. Eos corrupti veritatis qui error repellendus commodi. Fugiat asperiores omnis reiciendis. Non eos dolorem. Reiciendis magnam eum aperiam soluta laudantium illo.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 298242, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8060), 5, new List<int> { 8, 7, 9, 4 } },
                    { 44, "Dolor eligendi ut velit voluptas aut aliquid totam dolor exercitationem possimus qui ut.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8170), "Ipsa saepe in voluptates at laborum rerum ex sed sit. Tenetur aut sint deleniti. Earum totam maxime ea minima. Molestiae praesentium consequuntur omnis perspiciatis quibusdam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 147172, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8180), 6, new List<int> { 1 } },
                    { 45, "Explicabo nisi distinctio aut corrupti odio maiores necessitatibus deserunt nam dicta earum nesciunt vel accusamus totam nam.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8330), "Aliquid explicabo fugit odio aliquam maxime vel eum. Reprehenderit incidunt placeat ut exercitationem totam atque. Et ratione mollitia voluptatem. Illum est expedita voluptas necessitatibus earum sunt. Facere facilis pariatur illum necessitatibus ut libero numquam cupiditate voluptas.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 200421, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8330), 6, new List<int> { 5, 3 } },
                    { 46, "Dolorum nesciunt eos omnis ut quis excepturi.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8460), "Provident non sed vitae odit quia dignissimos voluptatem ad quo. A possimus minima laborum veniam eveniet dicta. Nostrum voluptatem incidunt numquam omnis aut necessitatibus qui dolor voluptatem. Assumenda qui blanditiis dicta odit ipsam.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 126128, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8460), 6, new List<int>() },
                    { 47, "Deserunt soluta quia aliquid rerum ut.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8620), "Enim blanditiis maiores porro vel ducimus libero adipisci. Rem non et. Aut asperiores dolor ipsam a. Ducimus est omnis et aut aut beatae exercitationem at. Quae iure omnis nostrum est doloremque repellendus minus qui. Impedit deserunt dolores enim aut rerum. Ducimus assumenda et reiciendis quo ea et.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 183739, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8620), 6, new List<int> { 7, 3 } },
                    { 48, "Error molestias totam perferendis qui quidem facilis dolores molestias quidem deserunt molestias ipsam enim.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8750), "Voluptatibus molestiae eos voluptatibus est non. Eum earum et. Dolorum fuga enim molestias. Reiciendis inventore facilis aut ut sed. Est non aut aut mollitia est repudiandae suscipit reiciendis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 82696, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8750), 6, new List<int> { 1, 2 } },
                    { 49, "Tenetur quia unde iure incidunt voluptate expedita sunt id eos labore est neque impedit voluptatem ullam recusandae sequi et.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8960), "Voluptatem ducimus illo animi iure quis doloribus rerum atque. Qui ullam sunt vel et explicabo sit et. Est excepturi cum qui id aut voluptatem ut. Animi culpa necessitatibus esse. Exercitationem nostrum quo est quo non et labore ad. Voluptas velit cupiditate et non esse aut voluptas. Possimus reiciendis minus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 260834, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8960), 7, new List<int> { 8, 3, 2 } },
                    { 50, "Quis quo tempore eaque qui minima maxime in molestias voluptatum sed quam natus commodi rerum tempore in tempore.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9120), "Quia ut mollitia et nisi ea. Rerum quidem earum. Quis odio est placeat quae commodi. Qui id illo et harum sunt esse dolores assumenda. Dignissimos doloribus aut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 154488, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9120), 7, new List<int> { 1, 3, 6, 4 } },
                    { 51, "Ut nemo beatae in provident amet occaecati commodi id consequuntur ut et et.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9280), "Reiciendis id est alias. Distinctio ad adipisci. Eos magnam et vero incidunt sequi ab minima voluptas odio. Itaque nemo deserunt culpa enim qui deleniti aut aut consequatur. Neque quidem magnam repudiandae qui nostrum. Est distinctio totam. Adipisci asperiores velit est delectus quo.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 202706, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9280), 7, new List<int>() },
                    { 52, "Libero est nemo reprehenderit praesentium ea et ut omnis vel voluptatem et nostrum officiis omnis ipsam deserunt amet quibusdam est.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9420), "Et nesciunt commodi ducimus. Itaque enim neque similique. Repudiandae minima beatae aperiam non laborum. Quasi alias sit delectus dolor.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 220239, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9420), 8, new List<int> { 5, 6 } },
                    { 53, "Neque dolore nihil placeat et autem eveniet est suscipit qui aliquam ab voluptas nulla dolorum quaerat aut facere.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9580), "Recusandae et minus qui laudantium maxime. Voluptatem id blanditiis deleniti soluta aut ut. Laborum id eos. Aut enim possimus. Odit quasi quae nihil qui. Quo expedita et praesentium sit facilis ea ut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 230599, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9580), 8, new List<int> { 6, 7, 9 } },
                    { 54, "Rerum aliquid fuga quia exercitationem repellendus odio cumque.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9730), "Voluptatem vel non et quaerat est molestiae et dignissimos. Quo occaecati deleniti veritatis laudantium impedit labore temporibus modi. Rerum ut non doloremque rerum qui voluptatem dolore aut sint. Enim ad mollitia et aut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 274735, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9730), 8, new List<int> { 5, 1, 3 } },
                    { 55, "Et placeat quas deleniti ut eum vel repellendus molestias ut et vero quos iusto quae voluptas dolore.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9850), "Sed itaque voluptas. Voluptatibus et placeat officia quia. Odit et aut eligendi voluptate nisi cum reprehenderit. Error et sunt beatae rerum voluptas autem rerum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 145726, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9850), 8, new List<int> { 4, 1, 6, 10 } },
                    { 56, "Quo soluta ut aut et dicta a.", 2, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local), "Ut numquam quibusdam consequatur et doloribus. Qui sed qui maiores odio delectus ducimus dolores. In cum rerum cum cupiditate quos. Blanditiis molestiae facilis.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 218967, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local), 8, new List<int>() },
                    { 57, "Voluptatibus voluptatem ipsum praesentium quia modi recusandae voluptas eius dignissimos rem ea dolor nulla dignissimos accusamus.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(140), "Sunt est asperiores provident occaecati voluptatibus quia incidunt. Necessitatibus qui totam voluptas rerum. Eveniet non accusamus placeat eum voluptates et. Et quis exercitationem. Dolorem voluptatem fugit perferendis vitae delectus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 60956, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(140), 9, new List<int> { 1, 3 } },
                    { 58, "Similique perspiciatis rem velit itaque ut hic.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(300), "Voluptatem sequi aut et. Ipsam molestiae sunt qui labore nostrum quisquam enim qui odio. Aut et dignissimos sed modi ea qui ut. Similique quia eos doloremque. Veniam quisquam eligendi et harum delectus facilis exercitationem. Et magni aliquam. Voluptas consequatur minus.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 267174, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(300), 9, new List<int>() },
                    { 59, "Esse dolorem rerum voluptate veritatis corporis.", 3, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(440), "Quasi et non. Ab molestias blanditiis odio quo quae qui iure nam. Reiciendis et omnis non culpa inventore explicabo. Nihil optio et dolor. Voluptates ex occaecati ipsa. Sed et aut nulla vel voluptatem asperiores ut quia. Molestiae ea dicta sed.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 84736, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(440), 9, new List<int> { 4, 1, 10 } },
                    { 60, "Dolor aut tenetur minima quasi quam qui porro ullam rerum nihil consequuntur tempore illum veniam ex.", 4, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(640), "Enim vero tempora. Quas nisi sapiente pariatur dolores explicabo dolor illo earum. Doloremque omnis voluptates eos dolor consectetur iure. Illum esse amet minima amet hic. Asperiores ut error sit reiciendis laborum eveniet quas eaque molestiae. Quam est ab in consequatur rem numquam voluptatem itaque. Dicta nesciunt explicabo rerum ducimus nesciunt quia veritatis impedit provident.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 209908, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(640), 9, new List<int> { 3 } },
                    { 61, "Minus suscipit tempore exercitationem sint.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(790), "Sapiente ut et voluptates magnam vel iste maiores dolorem. Voluptas saepe soluta vero. Beatae eum quaerat. Neque voluptatem accusamus nulla dolores. Eaque vel ipsam voluptatem consequatur est dolor voluptatem. Distinctio id eligendi ut rerum non velit vel.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 207385, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(790), 9, new List<int>() },
                    { 62, "In adipisci similique delectus officia molestias eos quae quo eveniet ducimus cupiditate sit reiciendis quidem blanditiis enim at.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(990), "Est ad sed quibusdam quos corporis sunt id pariatur quis. Dicta est facere consectetur modi harum quibusdam illo ipsam. Nemo voluptate aspernatur laudantium est hic aperiam iusto. Voluptatibus et alias a nesciunt et qui ducimus odit qui. Voluptas ipsa hic in qui vitae aut neque quis. Ipsam qui provident pariatur assumenda sint voluptas eaque eaque tenetur. Facere illo ut laboriosam sint sit magni.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 65393, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(990), 10, new List<int> { 4, 2, 8 } },
                    { 63, "Debitis maxime consequatur officia et suscipit tenetur vel sit.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1170), "Neque accusamus voluptatibus tempora qui quia accusantium. Voluptatibus quia eum est earum dignissimos. Quaerat omnis perspiciatis explicabo rerum aspernatur necessitatibus earum. Rerum amet ut doloribus nihil. Iste eum officiis ab enim accusamus dicta rerum inventore sit. Illo non corrupti ut. Nisi id vitae dignissimos adipisci officia aliquid omnis tempora.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 112118, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1170), 10, new List<int>() },
                    { 64, "Quas animi atque porro maiores voluptatem perferendis dolor quibusdam rerum soluta ut amet nihil est aut et ratione ipsa dolor.", 1, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1320), "Fugiat earum qui illo quisquam et vitae temporibus magnam. Aperiam recusandae commodi et quia veritatis repellat. Veniam harum nobis omnis ratione aut dolorum ratione sapiente commodi. Quam ab excepturi eum eum ex et quae aut aut.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 90261, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1320), 10, new List<int> { 4, 2, 6 } },
                    { 65, "Accusamus voluptatem fuga porro et quia voluptatem dolorem aliquam consequatur.", 5, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1460), "Nam modi fuga ea corrupti. Asperiores sapiente pariatur est. Dolores quod et rerum deleniti et animi quidem. Inventore aliquid esse assumenda beatae accusantium vel debitis. Ut non aut enim ut. Velit voluptatem reiciendis quidem sed dolorem accusantium reiciendis iure dolorum.", false, false, true, new[] { "post/8_C1_FMCcqxIlA", "post/PNHb-tbQIUWjV6" }, 102914, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1460), 10, new List<int>() }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "PostId", "Rating", "RatingAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Sed est odit vero est voluptatem ad esse accusamus ut. Voluptas excepturi aut consectetur sint.", 1, 0, new DateTime(2023, 4, 16, 5, 38, 55, 969, DateTimeKind.Local).AddTicks(9620), 2 },
                    { 2, "Dolorem eos eos. Rerum earum ratione velit et repellat et aut rerum consequuntur.", 2, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(620), 4 },
                    { 3, "Suscipit quidem est suscipit enim ab rerum officiis et quaerat. Voluptatem assumenda omnis corrupti velit officia.", 3, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(820), 3 },
                    { 4, "Ut dolores voluptates deserunt dignissimos aspernatur. Architecto aut sed harum ipsum cumque.", 4, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6380), 4 },
                    { 5, "Pariatur odit quod officia culpa voluptas eligendi. Aperiam quas soluta eligendi molestiae eaque repudiandae nulla atque et.", 5, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6570), 1 },
                    { 6, "Consectetur id ullam repudiandae eos ut vel dolores maiores illo. Corrupti accusamus quae aut sed voluptatem et quae.", 6, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6730), 3 },
                    { 7, "Natus sunt et quo voluptatem aut. Et doloremque non non voluptatem officia qui quas qui voluptas.", 7, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(6890), 3 },
                    { 8, "Et animi incidunt cupiditate tenetur dolores vel. Eum ea dolor veniam.", 8, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7110), 3 },
                    { 9, "Dolorum culpa est ut cupiditate. Explicabo qui provident non.", 9, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7250), 5 },
                    { 10, "Possimus minus deleniti adipisci quod. Assumenda at qui cumque.", 10, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7410), 5 },
                    { 11, "Qui voluptas tenetur nobis a. Deserunt eius explicabo omnis earum ad qui quia.", 11, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7560), 4 },
                    { 12, "Qui voluptatem sed nesciunt et dolor ut nemo sed. Recusandae in iusto voluptatem similique dolor.", 12, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7700), 5 },
                    { 13, "Omnis aut accusamus consequatur magnam id eos rerum. Unde magni et.", 13, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(7840), 5 },
                    { 14, "Dolore et voluptas maiores fugiat. Non vel autem minus tempora ut numquam.", 14, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8020), 5 },
                    { 15, "Incidunt repellendus culpa ut suscipit impedit asperiores. Accusantium ut incidunt omnis dolores provident.", 15, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8180), 1 },
                    { 16, "Dolor repellendus deleniti vero ut. Est corrupti asperiores temporibus.", 16, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8320), 3 },
                    { 17, "Possimus harum perferendis incidunt cupiditate consequuntur ipsa voluptatibus. Sapiente aut quia cupiditate.", 17, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8520), 2 },
                    { 18, "Aperiam perspiciatis consequatur. Esse enim beatae itaque ducimus.", 18, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8650), 4 },
                    { 19, "Consequuntur et veniam error aut libero est. A fuga est quos dolore.", 19, 0, new DateTime(2023, 4, 16, 5, 38, 55, 970, DateTimeKind.Local).AddTicks(8760), 4 },
                    { 20, "Sequi dolorem voluptatem sunt perspiciatis velit et sunt quia iusto. Pariatur qui praesentium.", 20, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4580), 4 },
                    { 21, "Et optio eius recusandae ducimus pariatur atque. Quae facilis vero omnis at ut.", 21, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4770), 10 },
                    { 22, "Iusto sed voluptatibus deserunt beatae consequuntur hic quo minima. Expedita nulla quis.", 22, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(4950), 3 },
                    { 23, "Voluptatem minima est a aut. Et iste laudantium et provident commodi veritatis.", 23, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5130), 8 },
                    { 24, "Debitis aut aspernatur eligendi. Aut sequi id et id non omnis qui eos.", 24, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5300), 5 },
                    { 25, "Saepe aperiam ea nemo nostrum doloremque commodi odit alias et. Amet aut pariatur quam maiores quia.", 25, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5410), 8 },
                    { 26, "Quia quo similique necessitatibus. Qui nulla aut nisi minima voluptatem dolorem voluptate.", 26, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5540), 4 },
                    { 27, "Eos eos atque rerum amet quibusdam dolores eligendi delectus. Animi est ab ut sed.", 27, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5710), 5 },
                    { 28, "Fugit qui facilis voluptatem quia qui recusandae earum reiciendis impedit. Mollitia voluptates asperiores aliquam nobis.", 28, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(5890), 3 },
                    { 29, "Deserunt voluptates consectetur deserunt illum. Id eaque a dolor totam.", 29, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6040), 5 },
                    { 30, "Laboriosam debitis praesentium saepe qui. Explicabo animi vel tempora et.", 30, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6220), 9 },
                    { 31, "Facere rem provident consequatur. Consectetur quia optio eius ut.", 31, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6350), 5 },
                    { 32, "Culpa fuga voluptatem et. Doloremque et perferendis iure tempora quas esse quia in est.", 32, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6510), 5 },
                    { 33, "Saepe veritatis saepe. Est eos aperiam amet facilis hic porro velit.", 33, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6670), 7 },
                    { 34, "Officia veniam non vitae excepturi vel. Eos odit architecto accusantium corporis debitis delectus fuga incidunt minus.", 34, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6820), 1 },
                    { 35, "Et ea itaque asperiores rem ipsa repudiandae rerum ut. Placeat est molestias et ullam ut voluptas voluptas magnam.", 35, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(6960), 6 },
                    { 36, "Exercitationem veritatis non nobis. Veritatis magni et ipsam itaque quia omnis non.", 36, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7100), 6 },
                    { 37, "Optio voluptatem labore distinctio. Unde esse incidunt.", 37, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7230), 8 },
                    { 38, "Alias aut voluptate cupiditate qui qui. Inventore corporis necessitatibus placeat.", 38, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7330), 1 },
                    { 39, "Necessitatibus dolores sint. Ab et modi inventore velit voluptatibus ut fuga.", 39, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7440), 9 },
                    { 40, "Vel possimus alias porro atque. Consequuntur labore rem vero quibusdam aut.", 40, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7590), 8 },
                    { 41, "Eaque accusantium voluptas laudantium illum officia magnam ut. Harum quasi perspiciatis sequi velit rerum omnis nulla soluta beatae.", 41, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7730), 2 },
                    { 42, "Mollitia numquam odio aut ipsam dolore repellat eaque. Et nulla tempore sint sed.", 42, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(7920), 9 },
                    { 43, "Rerum neque odio laborum deleniti sunt id quas. Maiores quaerat asperiores asperiores.", 43, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8060), 7 },
                    { 44, "Ullam et et qui animi sed necessitatibus. Et reprehenderit eum fugit.", 44, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8180), 4 },
                    { 45, "Rerum et quod minima. Quisquam eligendi minus et omnis est voluptas sunt reiciendis occaecati.", 45, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8340), 3 },
                    { 46, "Eius occaecati doloremque itaque. Dicta aut laborum beatae.", 46, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8460), 8 },
                    { 47, "Accusantium magni nesciunt quis quia aut sed. Eum inventore reprehenderit ut.", 47, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8630), 2 },
                    { 48, "Corrupti quo sit molestiae reprehenderit adipisci beatae. Accusantium sunt dolorum delectus iusto molestiae quae voluptas accusamus.", 48, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8750), 3 },
                    { 49, "Autem enim fugiat at vero mollitia mollitia non culpa itaque. Dolores pariatur dolor ut labore in ipsum reprehenderit.", 49, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(8960), 2 },
                    { 50, "Sunt nisi voluptatem animi. Esse consequatur sed tenetur et sunt et.", 50, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9120), 2 },
                    { 51, "Error doloribus odit velit magni repudiandae rerum. Dicta ratione incidunt odio reprehenderit laborum error rerum.", 51, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9280), 9 },
                    { 52, "Sit facilis assumenda omnis nemo non vel iure et. Pariatur consectetur est voluptatibus animi excepturi accusamus maiores et porro.", 52, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9420), 4 },
                    { 53, "Laboriosam tenetur voluptatem qui accusamus sint nesciunt dolor quia ab. Occaecati delectus repudiandae officiis repudiandae exercitationem est.", 53, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9580), 10 },
                    { 54, "Nostrum labore quia aliquam. Debitis aliquid laudantium voluptas eaque deserunt.", 54, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9730), 9 },
                    { 55, "Ut qui qui optio ut tempora tempore ad ut sed. Vero blanditiis nostrum.", 55, 0, new DateTime(2023, 4, 16, 5, 38, 56, 104, DateTimeKind.Local).AddTicks(9850), 1 },
                    { 56, "Fugit eos quis placeat quod adipisci sunt omnis omnis numquam. Sunt consequatur quo vel perspiciatis doloremque quia.", 56, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local), 2 },
                    { 57, "Harum soluta aperiam suscipit eos harum soluta aliquam ullam. Sunt est odio veritatis blanditiis.", 57, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(140), 7 },
                    { 58, "Numquam quas rem reprehenderit libero. Dolorem corrupti aut deserunt quis.", 58, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(300), 8 },
                    { 59, "Voluptatem dolores pariatur deleniti tenetur modi sit sit totam in. Vero architecto itaque quia ipsum nisi consequuntur.", 59, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(440), 8 },
                    { 60, "Nihil nisi ipsam aut eum iure dicta voluptates nihil voluptates. Qui ut nihil praesentium blanditiis eaque.", 60, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(650), 10 },
                    { 61, "Qui quaerat quia animi. Expedita nesciunt ut et est sint.", 61, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(790), 6 },
                    { 62, "Rerum illo soluta et odit laudantium maiores. Omnis et itaque laborum eveniet officiis est optio quae.", 62, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1000), 1 },
                    { 63, "Assumenda magni est quaerat quis. Voluptatibus consequuntur est.", 63, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1170), 2 },
                    { 64, "Ullam est aut eius. Repellat et nesciunt.", 64, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1330), 5 },
                    { 65, "Laudantium quod totam magnam. Facere velit iste totam vero quia voluptates vero illum.", 65, 0, new DateTime(2023, 4, 16, 5, 38, 56, 105, DateTimeKind.Local).AddTicks(1460), 3 }
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
