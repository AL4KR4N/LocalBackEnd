using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace monchotradeBackend.Data.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class AddExchangeSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Notes" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(723), "Jeans for headphones" });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "InitiatorProductId", "InitiatorUserId", "Notes", "ReceiverProductId", "ReceiverUserId" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(727), 2, 1, "T-Shirt for sneakers", 5, 3 });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "Id", "CreatedAt", "InitiatorProductId", "InitiatorUserId", "Notes", "ReceiverProductId", "ReceiverUserId", "RejectionReason", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(731), 3, 2, "Headphones for charger", 7, 4, null, "Pending", null },
                    { 4, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(734), 4, 2, "Speaker for shirt", 9, 5, "Item condition not as expected", "Rejected", null },
                    { 5, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(737), 5, 3, "Sneakers for laptop stand", 11, 6, null, "Accepted", null },
                    { 6, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(740), 6, 3, "Running shoes for watch", 13, 7, null, "Pending", null },
                    { 7, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(743), 7, 4, "Charger for jacket", 15, 8, "Found another exchange", "Rejected", null },
                    { 8, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(747), 8, 4, "Power bank for tablet", 17, 9, null, "Pending", null },
                    { 9, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(750), 9, 5, "Formal shirt for yoga mat", 19, 10, null, "Accepted", null },
                    { 10, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(753), 10, 5, "Dress pants for desk organizer", 21, 11, null, "Pending", null },
                    { 11, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(757), 11, 6, "Laptop stand for vase", 23, 12, "Size doesn't match description", "Rejected", null },
                    { 12, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(760), 12, 6, "Mouse for gaming mouse", 25, 13, null, "Pending", null },
                    { 13, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(763), 13, 7, "Digital watch for wall art", 27, 14, null, "Accepted", null },
                    { 14, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(767), 14, 7, "Fitness tracker for thermostat", 29, 15, null, "Pending", null },
                    { 15, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(770), 15, 8, "Jacket for sunglasses", 31, 16, "Item no longer available", "Rejected", null },
                    { 16, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(773), 16, 8, "Scarf for hoodie", 33, 17, null, "Pending", null },
                    { 17, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(776), 17, 9, "Tablet for portable charger", 35, 18, null, "Accepted", null },
                    { 18, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(779), 18, 9, "Tablet case for cookware", 37, 19, null, "Pending", null },
                    { 19, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(782), 19, 10, "Yoga mat for pants", 39, 20, "Changed my mind", "Rejected", null },
                    { 20, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(785), 20, 10, "Dumbbells for jeans", 1, 1, null, "Pending", null },
                    { 21, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(788), 21, 11, "Desk organizer for headphones", 3, 2, null, "Pending", null },
                    { 22, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(792), 22, 11, "Notepad for sneakers", 5, 3, null, "Accepted", null },
                    { 23, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(795), 23, 12, "Vase for charger", 7, 4, "Distance too far for exchange", "Rejected", null },
                    { 24, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(798), 24, 12, "Pillow for shirt", 9, 5, null, "Pending", null },
                    { 25, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(801), 25, 13, "Gaming mouse for laptop stand", 11, 6, null, "Accepted", null },
                    { 26, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(804), 26, 13, "Keyboard for watch", 13, 7, null, "Pending", null },
                    { 27, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(808), 27, 14, "Wall art for jacket", 15, 8, "Item value mismatch", "Rejected", null },
                    { 28, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(888), 28, 14, "Lamp for tablet", 17, 9, null, "Pending", null },
                    { 29, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(892), 29, 15, "Thermostat for yoga mat", 19, 10, null, "Accepted", null },
                    { 30, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(895), 30, 15, "Air purifier for organizer", 21, 11, null, "Pending", null },
                    { 31, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(898), 31, 16, "Sunglasses for vase", 23, 12, "Communication issues", "Rejected", null },
                    { 32, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(901), 32, 16, "Wallet for gaming mouse", 25, 13, null, "Pending", null },
                    { 33, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(904), 33, 17, "Hoodie for wall art", 27, 14, null, "Accepted", null },
                    { 34, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(907), 34, 17, "Cap for thermostat", 29, 15, null, "Pending", null },
                    { 35, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(911), 35, 18, "Portable charger for sunglasses", 31, 16, "Schedule conflicts for meetup", "Rejected", null },
                    { 36, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(914), 36, 18, "USB cables for hoodie", 33, 17, null, "Pending", null },
                    { 37, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(917), 37, 19, "Cookware for charger", 35, 18, null, "Accepted", null },
                    { 38, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(920), 38, 19, "Knife set for jeans", 1, 1, null, "Pending", null },
                    { 39, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(923), 39, 20, "Pants for headphones", 3, 2, "Product quality concerns", "Rejected", null },
                    { 40, new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(926), 40, 20, "Sweater for sneakers", 5, 3, null, "Pending", null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(74));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(79));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(250));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(254));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(471));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(476));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(196));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(479));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(483));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(203));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(206));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(161));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(167));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(257));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(493));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(486));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(489));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(294));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(263));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(301));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(304));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(269));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(272));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(214));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(171));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(186));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(279));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(282));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(291));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(190));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 14, 10, 50, 36, 523, DateTimeKind.Local).AddTicks(193));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$pWU8w/2CsgnLF4X0c4hRyeye22RBgelCiTXTJySi1G/v9Ii7G44i.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$5Wh38j0Wfs9FUAIBaSZreeoBe.t7mnPzPfsuoG6FaOE7yBCOljY6m");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$Ap47oQ4bqlEMb7O/27rCSOnMvEy0j75KuP26kudEabfmCZLjBZJh6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$woLtTTaJ2jeald5cmc1QhufeIUdWAPhYgwwe5l7fgMUDYCprI7wSO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$Scuf1pNXCsl/5HHvWE6iy.qsLpXrx/VsMuJXaKjKwE3n7k4yylxk.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$0aR39aKAo50JfN3gctofCOviIqwHxxfHpy5RYfdZhSTlkWnHSsKHy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$E/gtpZWR9eneepZ2N.RZ8uU1pPdEgDIThwWu0z1SWphA/l8wKZ37W");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$sgJgQuhxosD6dVq14fEhZu/OLzqVMegCLah5QTNXclfArIMik57N6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$al8ubhvQgGtzuamz0bJyx.SrcAlFEsP1KnW375rBVxrVOjUAHSp1m");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$AHiQFYH2gxYShoYGvYiN9uQK6a4GkqyhOcepavzukcfjPl6wADyKy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$Otrdi7.pTg7pkZpUcT/LzuBaLW4c7hvY2G.FMIxLUZc1ndUis1kd6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$KHdLV.FJyGMXmhZvBOf7lO7TO784Cmh7ODPytDhNnIVAonKPUcJv6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "PasswordHash",
                value: "$2a$11$I9yloXUGwiZOi4Xe0yDHju4YJOm.BvcphZWJC/.h4oT/HEKWDFVce");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "PasswordHash",
                value: "$2a$11$NlZ6RHRci9N8yoYT936E7.OlTBLioWKDtCYiwePRJlrbkNpwAVeEi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "PasswordHash",
                value: "$2a$11$MWsIH7/ty2YQOCoh888lU.dAU2M63arO2QaZfRr3/jz1dy/.xSmSW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16,
                column: "PasswordHash",
                value: "$2a$11$9KmQ6Y757O9PVllhIf3S4Ooyz9FXBAfu.v4VyLRrWx7FapuBkiRWS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17,
                column: "PasswordHash",
                value: "$2a$11$U37HY6gUSSOtLo.wiD1hVe98xTmE/4jCLF2ZEptZ9Wm44VuIfpLBi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18,
                column: "PasswordHash",
                value: "$2a$11$dveQzUq9WgmHdAwI15dNPOicJe1F4Neu0NQM3Vj90rk7M4TEs0Xxu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19,
                column: "PasswordHash",
                value: "$2a$11$DBD877hoVbdmSQYZspVeTu4Yb.5Vj/i6jUDaCLGpinu2j6dPeFQAu");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20,
                column: "PasswordHash",
                value: "$2a$11$n11/iNpDNxjun4mZ/bSw.uvEZ4PmDF/o4OSG5DgdlGGEJVJDBKNc2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Notes" },
                values: new object[] { new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7844), "Interested in exchanging my jeans for your headphones" });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "InitiatorProductId", "InitiatorUserId", "Notes", "ReceiverProductId", "ReceiverUserId" },
                values: new object[] { new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7848), 5, 3, "Would like to exchange my sneakers for your charger", 7, 4 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7386));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7390));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7540));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7589));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7592));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7498));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7446));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7597));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7505));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7464));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7471));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7546));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7579));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7581));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7554));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7556));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7559));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7515));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7537));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7473));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7489));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7566));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7568));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7576));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7492));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2024, 11, 11, 12, 1, 31, 571, DateTimeKind.Local).AddTicks(7495));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$gfjCx6sW1juZn/T0phsPneKoF6g3BPE3aaiT3E5zxF9OZ822EaIBW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$sZCxd5XUmOFdinB4cyb3AuD5s4jFgo/WZS3bX1WEsDLIJMdFLm8dW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$I9cSA2WJ17GhBJsMqLJ.0OZ7SdW5okSPaSxyQnufpaxJs6AnbsxHK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "$2a$11$Sh3Jht/c2UkF.3eLqcNH5OrvoSNWbmvZGemVwmsJ7iyA3ObbFrHre");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$4gMfTnTAWgR3efctsd1bc.bH.upngOnXDKUmjOxnGKW5DrtGoXKEy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$36LVQx3mFA.YTikwy8GUhudLAjUDvhjk3wgIamBm13VodgZ92bVPS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$Hm2xAG7hgE0Ko1FjRnwyxOGQ8NLAlWSdWaXDI1TiKo2kwjRh9Jp0S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$H0N3JVTnTMtftLV95vhKWeiFYKN/AmNMjx0Or5KsEo3cttXyYNTDm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$0N81239rFtLFro8YApjxzer/5IVqkc/7JI.rUJXV4g1gvpoFRTbTi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$gHxkFQCybVW.eElflV5KHe43Tzx4T4D3cCXYgQgMy6jZL81aez7lG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$0m6mVPUqNDr1Bg/w9A7l6u54XJ2Hi.5nKPgo9sr8GwfajVOwmP.Zm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$VAQ.CNefYApKa.bN6z.QieDsIfCgZCXk4A5ub9LSxeUfIXhhmkyze");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13,
                column: "PasswordHash",
                value: "$2a$11$rbaAJgJBxGoUm1tK7R9AF.B1CTQgmrbWl99te4ekBCEyp1a6tLEBi");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14,
                column: "PasswordHash",
                value: "$2a$11$vsZ5TF45MVPsw1Vzlounre/ngHMR6D.62GL18SBGzYCSy5Sqhrw06");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15,
                column: "PasswordHash",
                value: "$2a$11$XaeV3IlwqaOoA9r54ivFkOlYMZ80K4Wcw.4xsRmOqiZ5ze6ICco6K");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16,
                column: "PasswordHash",
                value: "$2a$11$inAloJfwMfpBJq9KpA/y7eqBXzYfKbSF/VC8DTY3PZLpypc7U.epy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17,
                column: "PasswordHash",
                value: "$2a$11$83rIGAmCEPQpB38FKW8kuOpnSSMQSGBk8NFQ.cCamth9o.BrUZJ96");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18,
                column: "PasswordHash",
                value: "$2a$11$fiZcfa7l80y1aHItrcu5QOFap2HlUpsfovJbY7vwZpC7OnJ9VCMWC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19,
                column: "PasswordHash",
                value: "$2a$11$0hsN2yS1YJ2OV8c0ch0XXu26xhGeMnFZOS80Br2thgJs9NRF7YZva");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 20,
                column: "PasswordHash",
                value: "$2a$11$onx2AIgKZYaCfirUsf.uFuC0ozGTvK2Evuc0YCJMIEaF.Tyl/mrpG");
        }
    }
}
