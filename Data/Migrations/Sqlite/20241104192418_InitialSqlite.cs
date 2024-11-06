using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace monchotradeBackend.Data.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class InitialSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
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
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Country", "Email", "LastName", "Name", "PasswordHash", "PhoneNumber", "SecondLastName" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 15), "Mexico", "ana.martinezgomez@example.com", "Martinez", "Ana Luisa", "$2a$11$JkAbOFiMV7HbukMnlUN93uVR7r3X6pT/pAeidM4TTZlaIDe5SgkkW", "+52 55 1234 5678", "Gomez" },
                    { 2, new DateOnly(1985, 8, 22), "Mexico", "carlos.gomezlopez@example.com", "Gomez", "Carlos", "$2a$11$9OD3JJgf7KIicQpUxbEaDu4zuHAV1ibPvunolcqD8r5hx4XBB4HRC", "+52 33 9876 5432", "Lopez" },
                    { 3, new DateOnly(1992, 3, 10), "Mexico", "miguel.torresramirez@example.com", "Torres", "Miguel", "$2a$11$ilAvzBE/u.WdLhpfCsfYmu4gtIyWf3m.Y69JMuktP91GLhCQYzMR2", "+52 81 5555 4444", "Ramirez" },
                    { 4, new DateOnly(1988, 11, 5), "Mexico", "laura.rodriguezfernandez@example.com", "Rodriguez", "Laura", "$2a$11$Sq/OKy.RcxfqV1cG2OBinesYYJ3atEc3fTzyv339tuhXg6IKIlu8C", "+52 55 2222 3333", "Fernandez" },
                    { 5, new DateOnly(1995, 7, 18), "Mexico", "juan.sanchezmartinez@example.com", "Sanchez", "Juan Carlos", "$2a$11$VBAFqGUj0o5SVQMY/on9oepGFzwbmcYyfavu0yWqOARcDc.Xha9Ee", "+52 33 7777 8888", "Martinez" },
                    { 6, new DateOnly(1987, 9, 30), "Mexico", "sofia.ramirezcastillo@example.com", "Ramirez", "Sofia", "$2a$11$gw9nn3vgMDUnCN.hp3qWD.RU1cZUcQ5nAVoZVzypgCH0mxeojKbse", "+52 81 4444 5555", "Castillo" },
                    { 7, new DateOnly(1993, 2, 14), "Mexico", "andres.lopezcastro@example.com", "Lopez", "Andres", "$2a$11$GRpH/I9pC4KNM3bsOA1qIOgUkdDQOCbZdyU3n6vZcqcGbSmyXBCf.", "+52 55 6666 7777", "Castro" },
                    { 8, new DateOnly(1991, 6, 25), "Mexico", "valentina.castroperez@example.com", "Castro", "Valentina", "$2a$11$3z3FmxMUavgzGi8STV8v7uyUOaSq24nBlUZHg8b2LKre1.wJHQ5OS", "+52 33 1111 2222", "Perez" },
                    { 9, new DateOnly(1986, 12, 7), "Mexico", "pedro.jimenezmorales@example.com", "Jimenez", "Pedro", "$2a$11$H0XEmhsPNZAEgflxb/mkLuTx1Az2Cl7D/r29o2XPE1iaoHdMLZHMm", "+52 81 9999 0000", "Morales" },
                    { 10, new DateOnly(1994, 4, 12), "Mexico", "mariana.diazherrera@example.com", "Diaz", "Mariana", "$2a$11$sl/djQf13eH/xFFCPfhAk.91RkTWtiN9RLJRO7Mw.o1G/eymRCcte", "+52 55 3333 4444", "Herrera" },
                    { 11, new DateOnly(1989, 10, 20), "Mexico", "javier.moralesjimenez@example.com", "Morales", "Javier", "$2a$11$Uat/htxSghHKpOt.89OT2.rYOYxUw5SW6jGjMLGkGpkdXQIKNm6lK", "+52 33 5555 6666", "Jimenez" },
                    { 12, new DateOnly(1996, 1, 8), "Mexico", "gabriela.fernandeztorres@example.com", "Fernandez", "Gabriela", "$2a$11$Mkzc4WfZ.x7Yu8Ja6466du0M4eddyST9Ykr2RGwMipYaRaKpF914C", "+52 81 2222 3333", "Torres" },
                    { 13, new DateOnly(1984, 8, 16), "Mexico", "tomas.herreramendoza@example.com", "Herrera", "Tomas", "$2a$11$HaXBvM9UeurM4LlcR4zSFuysw4FqEJzv6DaWq1iA8YR0GW7tvm0D6", "+52 55 7777 8888", "Mendoza" },
                    { 14, new DateOnly(1997, 5, 3), "Mexico", "natalia.mendozarodriguez@example.com", "Mendoza", "Natalia", "$2a$11$pKQF4v5B0pUxHXvBDAjQ0.eU8kdZKc5qwf8BqgJ5SrrmE7uFZpBDi", "+52 33 4444 5555", "Rodriguez" },
                    { 15, new DateOnly(1983, 11, 22), "Mexico", "luis.ruizsanchez@example.com", "Ruiz", "Luis Fernando", "$2a$11$9WYa19luuGuv5nAZLJA6y.JD1wwb.uN1AznUcS.woRw2nS8wYoUra", "+52 81 6666 7777", "Sanchez" },
                    { 16, new DateOnly(1990, 7, 7), "Mexico", "elena.gonzalezreyes@example.com", "Gonzalez", "Elena", "$2a$11$0An4Q9tKM22f402sJ/033.vdsbWC00.FB3wd8g7LTJVCkgj210OM6", "+52 55 8888 9999", "Reyes" },
                    { 17, new DateOnly(1986, 3, 19), "Mexico", "ricardo.reyesgarcia@example.com", "Reyes", "Ricardo", "$2a$11$mF4ymiymmsk6ch2aLd/xcukf21JblCAJiwJ19cB2QLsg1.vcwFwXy", "+52 33 0000 1111", "Garcia" },
                    { 18, new DateOnly(1993, 9, 14), "Mexico", "isabel.garciamoreno@example.com", "Garcia", "Isabel", "$2a$11$Ooj7u6GC2DRxrucd7MkMbe5HHeornMihAW85TXHsBRM.SMoCF4r2i", "+52 81 3333 4444", "Moreno" },
                    { 19, new DateOnly(1988, 6, 30), "Mexico", "daniel.morenosilva@example.com", "Moreno", "Daniel", "$2a$11$RJiK4FOPskkLc8RDBkx9oeoB8DDkkhMzNFyf8oSoKsinoPzKhwtl2", "+52 55 5555 6666", "Silva" },
                    { 20, new DateOnly(1995, 12, 5), "Mexico", "carmen.silvanavarro@example.com", "Silva", "Carmen", "$2a$11$Zfeo2WDJ9OQcSTm2uG0KWeY/gwPrEqpS4l5jEeOXbJWQBcfKtXLI.", "+52 33 2222 3333", "Navarro" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "IsActive", "Name", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5665), "Classic blue jeans for everyday wear", true, "Men's Jeans", 10, null, 1 },
                    { 2, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5669), "Comfortable cotton t-shirt, perfect for casual outings", true, "Casual T-Shirt", 5, null, 1 },
                    { 3, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5672), "Noise-cancelling headphones with high-quality sound", true, "Wireless Headphones", 8, null, 2 },
                    { 4, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5714), "Portable speaker with long battery life", true, "Bluetooth Speaker", 12, null, 2 },
                    { 5, "Footwear", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5724), "Comfortable and durable sports shoes", true, "Sports Sneakers", 7, null, 3 },
                    { 6, "Footwear", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5730), "Lightweight running shoes for everyday exercise", true, "Running Shoes", 10, null, 3 },
                    { 7, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5733), "Fast-charging USB-C charger", true, "Smartphone Charger", 15, null, 4 },
                    { 8, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5753), "10,000mAh power bank for mobile devices", true, "Power Bank", 9, null, 4 },
                    { 9, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5756), "Stylish white formal shirt, perfect for office", true, "Formal Shirt", 11, null, 5 },
                    { 10, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5759), "Comfortable black dress pants for formal events", true, "Dress Pants", 6, null, 5 },
                    { 11, "Office Supplies", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5763), "Ergonomic stand for laptops", true, "Laptop Stand", 8, null, 6 },
                    { 12, "Office Supplies", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5766), "Compact wireless mouse for convenience", true, "Wireless Mouse", 13, null, 6 },
                    { 13, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5769), "Sporty digital watch with water resistance", true, "Digital Watch", 14, null, 7 },
                    { 14, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5772), "Tracks steps, calories, and more", true, "Fitness Tracker", 5, null, 7 },
                    { 15, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5779), "Stylish and warm winter jacket", true, "Men's Jacket", 4, null, 8 },
                    { 16, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5817), "Soft wool scarf for cold weather", true, "Wool Scarf", 7, null, 8 },
                    { 17, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5820), "10-inch tablet with high-resolution display", true, "Tablet", 6, null, 9 },
                    { 18, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5822), "Protective case for 10-inch tablet", true, "Tablet Case", 10, null, 9 },
                    { 19, "Fitness", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5825), "Non-slip yoga mat for workouts", true, "Yoga Mat", 12, null, 10 },
                    { 20, "Fitness", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5828), "Adjustable dumbbells for home workouts", true, "Dumbbells Set", 3, null, 10 },
                    { 21, "Office Supplies", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5831), "Keeps your desk neat and organized", true, "Desk Organizer", 8, null, 11 },
                    { 22, "Office Supplies", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5833), "Set of three notepads", true, "Notepad Set", 15, null, 11 },
                    { 23, "Home Decor", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5836), "Elegant ceramic vase for home decor", true, "Ceramic Vase", 7, null, 12 },
                    { 24, "Home Decor", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5839), "Soft pillow with a decorative cover", true, "Decorative Pillow", 10, null, 12 },
                    { 25, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5841), "High-precision gaming mouse", true, "Gaming Mouse", 6, null, 13 },
                    { 26, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5844), "Tactile mechanical keyboard", true, "Mechanical Keyboard", 5, null, 13 },
                    { 27, "Home Decor", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5847), "Modern abstract wall art", true, "Wall Art", 4, null, 14 },
                    { 28, "Home Decor", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5856), "Stylish table lamp with adjustable brightness", true, "Table Lamp", 8, null, 14 },
                    { 29, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(5859), "Programmable smart thermostat", true, "Smart Thermostat", 5, null, 15 },
                    { 30, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6047), "Compact air purifier for small rooms", true, "Air Purifier", 3, null, 15 },
                    { 31, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6051), "Polarized sunglasses with UV protection", true, "Sunglasses", 10, null, 16 },
                    { 32, "Accessories", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6054), "Classic leather wallet with multiple compartments", true, "Leather Wallet", 9, null, 16 },
                    { 33, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6057), "Comfortable cotton hoodie", true, "Cotton Hoodie", 8, null, 17 },
                    { 34, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6060), "Adjustable sports cap", true, "Sports Cap", 15, null, 17 },
                    { 35, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6063), "Compact charger for mobile devices", true, "Portable Charger", 14, null, 18 },
                    { 36, "Electronics", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6067), "Set of 3 USB cables", true, "USB Cable Set", 12, null, 18 },
                    { 37, "Kitchen", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6070), "Non-stick cookware set", true, "Cookware Set", 6, null, 19 },
                    { 38, "Kitchen", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6073), "Stainless steel knives for every need", true, "Knife Set", 7, null, 19 },
                    { 39, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6076), "Comfortable everyday pants", true, "Pants", 6, null, 20 },
                    { 40, "Clothing", new DateTime(2024, 11, 4, 11, 24, 18, 218, DateTimeKind.Local).AddTicks(6079), "Warm sweater for winter", true, "Sweater", 15, null, 20 }
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
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileImages_UserId",
                table: "ProfileImages",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
