using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace monchotradeBackend.Data.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SecondLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InitiatorUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitiatorProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RejectionReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exchanges_Products_InitiatorProductId",
                        column: x => x.InitiatorProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchanges_Products_ReceiverProductId",
                        column: x => x.ReceiverProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchanges_Users_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchanges_Users_ReceiverUserId",
                        column: x => x.ReceiverUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Clothing" },
                    { 2, "Accessories" },
                    { 3, "Electronics" },
                    { 4, "Kitchen" },
                    { 5, "Home Decor" },
                    { 6, "Footwear" },
                    { 7, "Office Supplies" },
                    { 8, "Fitness" },
                    { 9, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Country", "Email", "LastName", "Name", "PasswordHash", "PhoneNumber", "SecondLastName" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 15), "Mexico", "ana.martinezgomez@example.com", "Martinez", "Ana Luisa", "$2a$11$gfjCx6sW1juZn/T0phsPneKoF6g3BPE3aaiT3E5zxF9OZ822EaIBW", "+52 55 1234 5678", "Gomez" },
                    { 2, new DateOnly(1985, 8, 22), "Mexico", "carlos.gomezlopez@example.com", "Gomez", "Carlos", "$2a$11$sZCxd5XUmOFdinB4cyb3AuD5s4jFgo/WZS3bX1WEsDLIJMdFLm8dW", "+52 33 9876 5432", "Lopez" },
                    { 3, new DateOnly(1992, 3, 10), "Mexico", "miguel.torresramirez@example.com", "Torres", "Miguel", "$2a$11$I9cSA2WJ17GhBJsMqLJ.0OZ7SdW5okSPaSxyQnufpaxJs6AnbsxHK", "+52 81 5555 4444", "Ramirez" },
                    { 4, new DateOnly(1988, 11, 5), "Mexico", "laura.rodriguezfernandez@example.com", "Rodriguez", "Laura", "$2a$11$Sh3Jht/c2UkF.3eLqcNH5OrvoSNWbmvZGemVwmsJ7iyA3ObbFrHre", "+52 55 2222 3333", "Fernandez" },
                    { 5, new DateOnly(1995, 7, 18), "Mexico", "juan.sanchezmartinez@example.com", "Sanchez", "Juan Carlos", "$2a$11$4gMfTnTAWgR3efctsd1bc.bH.upngOnXDKUmjOxnGKW5DrtGoXKEy", "+52 33 7777 8888", "Martinez" },
                    { 6, new DateOnly(1987, 9, 30), "Mexico", "sofia.ramirezcastillo@example.com", "Ramirez", "Sofia", "$2a$11$36LVQx3mFA.YTikwy8GUhudLAjUDvhjk3wgIamBm13VodgZ92bVPS", "+52 81 4444 5555", "Castillo" },
                    { 7, new DateOnly(1993, 2, 14), "Mexico", "andres.lopezcastro@example.com", "Lopez", "Andres", "$2a$11$Hm2xAG7hgE0Ko1FjRnwyxOGQ8NLAlWSdWaXDI1TiKo2kwjRh9Jp0S", "+52 55 6666 7777", "Castro" },
                    { 8, new DateOnly(1991, 6, 25), "Mexico", "valentina.castroperez@example.com", "Castro", "Valentina", "$2a$11$H0N3JVTnTMtftLV95vhKWeiFYKN/AmNMjx0Or5KsEo3cttXyYNTDm", "+52 33 1111 2222", "Perez" },
                    { 9, new DateOnly(1986, 12, 7), "Mexico", "pedro.jimenezmorales@example.com", "Jimenez", "Pedro", "$2a$11$0N81239rFtLFro8YApjxzer/5IVqkc/7JI.rUJXV4g1gvpoFRTbTi", "+52 81 9999 0000", "Morales" },
                    { 10, new DateOnly(1994, 4, 12), "Mexico", "mariana.diazherrera@example.com", "Diaz", "Mariana", "$2a$11$gHxkFQCybVW.eElflV5KHe43Tzx4T4D3cCXYgQgMy6jZL81aez7lG", "+52 55 3333 4444", "Herrera" },
                    { 11, new DateOnly(1989, 10, 20), "Mexico", "javier.moralesjimenez@example.com", "Morales", "Javier", "$2a$11$0m6mVPUqNDr1Bg/w9A7l6u54XJ2Hi.5nKPgo9sr8GwfajVOwmP.Zm", "+52 33 5555 6666", "Jimenez" },
                    { 12, new DateOnly(1996, 1, 8), "Mexico", "gabriela.fernandeztorres@example.com", "Fernandez", "Gabriela", "$2a$11$VAQ.CNefYApKa.bN6z.QieDsIfCgZCXk4A5ub9LSxeUfIXhhmkyze", "+52 81 2222 3333", "Torres" },
                    { 13, new DateOnly(1984, 8, 16), "Mexico", "tomas.herreramendoza@example.com", "Herrera", "Tomas", "$2a$11$rbaAJgJBxGoUm1tK7R9AF.B1CTQgmrbWl99te4ekBCEyp1a6tLEBi", "+52 55 7777 8888", "Mendoza" },
                    { 14, new DateOnly(1997, 5, 3), "Mexico", "natalia.mendozarodriguez@example.com", "Mendoza", "Natalia", "$2a$11$vsZ5TF45MVPsw1Vzlounre/ngHMR6D.62GL18SBGzYCSy5Sqhrw06", "+52 33 4444 5555", "Rodriguez" },
                    { 15, new DateOnly(1983, 11, 22), "Mexico", "luis.ruizsanchez@example.com", "Ruiz", "Luis Fernando", "$2a$11$XaeV3IlwqaOoA9r54ivFkOlYMZ80K4Wcw.4xsRmOqiZ5ze6ICco6K", "+52 81 6666 7777", "Sanchez" },
                    { 16, new DateOnly(1990, 7, 7), "Mexico", "elena.gonzalezreyes@example.com", "Gonzalez", "Elena", "$2a$11$inAloJfwMfpBJq9KpA/y7eqBXzYfKbSF/VC8DTY3PZLpypc7U.epy", "+52 55 8888 9999", "Reyes" },
                    { 17, new DateOnly(1986, 3, 19), "Mexico", "ricardo.reyesgarcia@example.com", "Reyes", "Ricardo", "$2a$11$83rIGAmCEPQpB38FKW8kuOpnSSMQSGBk8NFQ.cCamth9o.BrUZJ96", "+52 33 0000 1111", "Garcia" },
                    { 18, new DateOnly(1993, 9, 14), "Mexico", "isabel.garciamoreno@example.com", "Garcia", "Isabel", "$2a$11$fiZcfa7l80y1aHItrcu5QOFap2HlUpsfovJbY7vwZpC7OnJ9VCMWC", "+52 81 3333 4444", "Moreno" },
                    { 19, new DateOnly(1988, 6, 30), "Mexico", "daniel.morenosilva@example.com", "Moreno", "Daniel", "$2a$11$0hsN2yS1YJ2OV8c0ch0XXu26xhGeMnFZOS80Br2thgJs9NRF7YZva", "+52 55 5555 6666", "Silva" },
                    { 20, new DateOnly(1995, 12, 5), "Mexico", "carmen.silvanavarro@example.com", "Silva", "Carmen", "$2a$11$onx2AIgKZYaCfirUsf.uFuC0ozGTvK2Evuc0YCJMIEaF.Tyl/mrpG", "+52 33 2222 3333", "Navarro" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsActive", "Name", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7386), "Classic blue jeans for everyday wear", true, "Men's Jeans", 10, null, 1 },
                    { 2, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7390), "Comfortable cotton t-shirt, perfect for casual outings", true, "Casual T-Shirt", 5, null, 1 },
                    { 3, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7540), "Noise-cancelling headphones with high-quality sound", true, "Wireless Headphones", 8, null, 2 },
                    { 4, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7543), "Portable speaker with long battery life", true, "Bluetooth Speaker", 12, null, 2 },
                    { 5, 6, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7589), "Comfortable and durable sports shoes", true, "Sports Sneakers", 7, null, 3 },
                    { 6, 6, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7592), "Lightweight running shoes for everyday exercise", true, "Running Shoes", 10, null, 3 },
                    { 7, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7498), "Fast-charging USB-C charger", true, "Smartphone Charger", 15, null, 4 },
                    { 8, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7502), "10,000mAh power bank for mobile devices", true, "Power Bank", 9, null, 4 },
                    { 9, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7393), "Stylish white formal shirt, perfect for office", true, "Formal Shirt", 11, null, 5 },
                    { 10, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7446), "Comfortable black dress pants for formal events", true, "Dress Pants", 6, null, 5 },
                    { 11, 7, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7595), "Ergonomic stand for laptops", true, "Laptop Stand", 8, null, 6 },
                    { 12, 7, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7597), "Compact wireless mouse for convenience", true, "Wireless Mouse", 13, null, 6 },
                    { 13, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7505), "Sporty digital watch with water resistance", true, "Digital Watch", 14, null, 7 },
                    { 14, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7507), "Tracks steps, calories, and more", true, "Fitness Tracker", 5, null, 7 },
                    { 15, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7464), "Stylish and warm winter jacket", true, "Men's Jacket", 4, null, 8 },
                    { 16, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7471), "Soft wool scarf for cold weather", true, "Wool Scarf", 7, null, 8 },
                    { 17, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7546), "10-inch tablet with high-resolution display", true, "Tablet", 6, null, 9 },
                    { 18, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7548), "Protective case for 10-inch tablet", true, "Tablet Case", 10, null, 9 },
                    { 19, 8, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7607), "Non-slip yoga mat for workouts", true, "Yoga Mat", 12, null, 10 },
                    { 20, 8, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7610), "Adjustable dumbbells for home workouts", true, "Dumbbells Set", 3, null, 10 },
                    { 21, 7, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7600), "Keeps your desk neat and organized", true, "Desk Organizer", 8, null, 11 },
                    { 22, 7, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7603), "Set of three notepads", true, "Notepad Set", 15, null, 11 },
                    { 23, 5, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7579), "Elegant ceramic vase for home decor", true, "Ceramic Vase", 7, null, 12 },
                    { 24, 5, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7581), "Soft pillow with a decorative cover", true, "Decorative Pillow", 10, null, 12 },
                    { 25, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7551), "High-precision gaming mouse", true, "Gaming Mouse", 6, null, 13 },
                    { 26, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7554), "Tactile mechanical keyboard", true, "Mechanical Keyboard", 5, null, 13 },
                    { 27, 5, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7584), "Modern abstract wall art", true, "Wall Art", 4, null, 14 },
                    { 28, 5, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7587), "Stylish table lamp with adjustable brightness", true, "Table Lamp", 8, null, 14 },
                    { 29, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7556), "Programmable smart thermostat", true, "Smart Thermostat", 5, null, 15 },
                    { 30, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7559), "Compact air purifier for small rooms", true, "Air Purifier", 3, null, 15 },
                    { 31, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7515), "Polarized sunglasses with UV protection", true, "Sunglasses", 10, null, 16 },
                    { 32, 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7537), "Classic leather wallet with multiple compartments", true, "Leather Wallet", 9, null, 16 },
                    { 33, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7473), "Comfortable cotton hoodie", true, "Cotton Hoodie", 8, null, 17 },
                    { 34, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7489), "Adjustable sports cap", true, "Sports Cap", 15, null, 17 },
                    { 35, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7562), "Compact charger for mobile devices", true, "Portable Charger", 14, null, 18 },
                    { 36, 3, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7566), "Set of 3 USB cables", true, "USB Cable Set", 12, null, 18 },
                    { 37, 4, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7568), "Non-stick cookware set", true, "Cookware Set", 6, null, 19 },
                    { 38, 4, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7576), "Stainless steel knives for every need", true, "Knife Set", 7, null, 19 },
                    { 39, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7492), "Comfortable everyday pants", true, "Pants", 6, null, 20 },
                    { 40, 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7495), "Warm sweater for winter", true, "Sweater", 15, null, 20 }
                });

            migrationBuilder.InsertData(
                table: "ProfileImages",
                columns: new[] { "Id", "Url", "UserId" },
                values: new object[,]
                {
                    { 1, "64d3f2dd-200a-49d4-b1ac-cb3541ba22f0.jpg", 1 },
                    { 2, "bae63af5-6084-465d-b4dc-b6549dee41ee.jpg", 2 }
                });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "Id", "CreatedAt", "InitiatorProductId", "InitiatorUserId", "Notes", "ReceiverProductId", "ReceiverUserId", "RejectionReason", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7844), 1, 1, "Interested in exchanging my jeans for your headphones", 3, 2, null, "Pending", null },
                    { 2, new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7848), 5, 3, "Would like to exchange my sneakers for your charger", 7, 4, null, "Accepted", null }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ProductId", "Url" },
                values: new object[,]
                {
                    { 3, 1, "2c2abcc8-d9de-4657-b2ea-9cb6317e35c1.jpg" },
                    { 4, 2, "d8440f61-bf9a-45f8-b18c-e9b7e2d35ba8.jpg" },
                    { 5, 3, "13268719-b95e-4ffa-af40-83b1c7a7a079.jpg" },
                    { 6, 4, "6c97ef84-b64c-4f70-8995-71f8889dd311.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_CreatedAt",
                table: "Exchanges",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_InitiatorProductId",
                table: "Exchanges",
                column: "InitiatorProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_InitiatorUserId_Status",
                table: "Exchanges",
                columns: new[] { "InitiatorUserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_ReceiverProductId",
                table: "Exchanges",
                column: "ReceiverProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_ReceiverUserId_Status",
                table: "Exchanges",
                columns: new[] { "ReceiverUserId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_Status",
                table: "Exchanges",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive",
                table: "Products",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
