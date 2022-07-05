using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class UpdateMedicalAid_20220705 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GapCover",
                table: "medical_aid",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "MaxAnnualSavings",
                table: "medical_aid",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "NetworkPlan",
                table: "medical_aid",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SavingsPlan",
                table: "medical_aid",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GapCover",
                table: "medical_aid");

            migrationBuilder.DropColumn(
                name: "MaxAnnualSavings",
                table: "medical_aid");

            migrationBuilder.DropColumn(
                name: "NetworkPlan",
                table: "medical_aid");

            migrationBuilder.DropColumn(
                name: "SavingsPlan",
                table: "medical_aid");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 27, 13, 40, 40, 241, DateTimeKind.Local).AddTicks(6872), new DateTime(2022, 6, 27, 13, 40, 40, 239, DateTimeKind.Local).AddTicks(8123), new DateTime(2022, 6, 27, 13, 40, 40, 241, DateTimeKind.Local).AddTicks(4236) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 264, DateTimeKind.Local).AddTicks(5293), new DateTime(2022, 6, 27, 13, 40, 40, 264, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1289), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1320), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1323) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1331), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1333) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1341), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1343) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1361), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1363) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1371), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 346, DateTimeKind.Local).AddTicks(5981), new DateTime(2022, 6, 27, 13, 40, 40, 346, DateTimeKind.Local).AddTicks(6045) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 347, DateTimeKind.Local).AddTicks(3583), new DateTime(2022, 6, 27, 13, 40, 40, 347, DateTimeKind.Local).AddTicks(3614) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 309, DateTimeKind.Local).AddTicks(7328), new DateTime(2022, 6, 27, 13, 40, 40, 309, DateTimeKind.Local).AddTicks(7374), "9757.BDWPSt9f/OqftHW+zb2Bnw==.34gJQv9JCT1DuoBPppJ660VZrzcLiQur+9ehnHqmlF4=" });
        }
    }
}
