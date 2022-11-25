using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ProviderCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "financial_providers",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 24, 16, 49, 11, 390, DateTimeKind.Local).AddTicks(8228), new DateTime(2022, 11, 24, 16, 49, 11, 389, DateTimeKind.Local).AddTicks(8430), new DateTime(2022, 11, 24, 16, 49, 11, 390, DateTimeKind.Local).AddTicks(7415) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(7140), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(7156) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9329), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9334) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9339), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9343), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9347), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9348) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9353), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9354) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9357), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9358) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(5040), new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(5058) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(7918), new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(7922) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 414, DateTimeKind.Local).AddTicks(3846), new DateTime(2022, 11, 24, 16, 49, 11, 414, DateTimeKind.Local).AddTicks(3868), "9394.YCB4KPgMwc0vOYaT++ZiFg==.ZKVFBU+x4UrQsID5JJ8jwltV2NRF3cHjLQz33B2yatQ=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "financial_providers");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 23, 15, 21, 49, 582, DateTimeKind.Local).AddTicks(866), new DateTime(2022, 11, 23, 15, 21, 49, 581, DateTimeKind.Local).AddTicks(1930), new DateTime(2022, 11, 23, 15, 21, 49, 581, DateTimeKind.Local).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 589, DateTimeKind.Local).AddTicks(8132), new DateTime(2022, 11, 23, 15, 21, 49, 589, DateTimeKind.Local).AddTicks(8144) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(256), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(261) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(267), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(268) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(271), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(272) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(274), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(275) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(280), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(281) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(283), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(284) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(2950), new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(2972) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(5821), new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 606, DateTimeKind.Local).AddTicks(8428), new DateTime(2022, 11, 23, 15, 21, 49, 606, DateTimeKind.Local).AddTicks(8446), "9000.WskblVL+xOVSBSeU3o3dPg==.cDpyOCkoTUJFR4R1jU9RwIQMMZBjI69IiTbiyKOOHBs=" });
        }
    }
}
