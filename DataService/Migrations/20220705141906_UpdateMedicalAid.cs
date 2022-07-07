using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class UpdateMedicalAid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 5, 16, 19, 5, 458, DateTimeKind.Local).AddTicks(2930), new DateTime(2022, 7, 5, 16, 19, 5, 457, DateTimeKind.Local).AddTicks(5705), new DateTime(2022, 7, 5, 16, 19, 5, 458, DateTimeKind.Local).AddTicks(2015) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(347), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(354) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2427), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2431) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2437), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2438) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2441), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2441) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2444), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2444) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2450), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2453), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2453) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 488, DateTimeKind.Local).AddTicks(9469), new DateTime(2022, 7, 5, 16, 19, 5, 488, DateTimeKind.Local).AddTicks(9482) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 489, DateTimeKind.Local).AddTicks(2235), new DateTime(2022, 7, 5, 16, 19, 5, 489, DateTimeKind.Local).AddTicks(2239) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 477, DateTimeKind.Local).AddTicks(9113), new DateTime(2022, 7, 5, 16, 19, 5, 477, DateTimeKind.Local).AddTicks(9122), "9289.OFg84coKX6vwA/Hha7chKw==./fPv3M2Fw6vglD3T35hBoYVSF6cAuQxgFs2Q9zMC2PU=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 5, 12, 41, 48, 206, DateTimeKind.Local).AddTicks(1641), new DateTime(2022, 7, 5, 12, 41, 48, 205, DateTimeKind.Local).AddTicks(3182), new DateTime(2022, 7, 5, 12, 41, 48, 206, DateTimeKind.Local).AddTicks(686) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(217), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(239) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2507), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2515) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2520), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2521) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2524), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2524) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2527), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2528) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2533), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2533) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2535), new DateTime(2022, 7, 5, 12, 41, 48, 214, DateTimeKind.Local).AddTicks(2536) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 241, DateTimeKind.Local).AddTicks(4723), new DateTime(2022, 7, 5, 12, 41, 48, 241, DateTimeKind.Local).AddTicks(4742) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 241, DateTimeKind.Local).AddTicks(7740), new DateTime(2022, 7, 5, 12, 41, 48, 241, DateTimeKind.Local).AddTicks(7747) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 5, 12, 41, 48, 230, DateTimeKind.Local).AddTicks(1582), new DateTime(2022, 7, 5, 12, 41, 48, 230, DateTimeKind.Local).AddTicks(1600), "9067.pZZ2K4yGrs0Y9tmHdr7RiQ==.cqOzXtmYjuLiwLn09tfaJyXaU7G9wNJqwumDad80wm4=" });
        }
    }
}
