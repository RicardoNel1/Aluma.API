using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ConsentVersionOnClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsentVersion",
                table: "client_consent_provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 16, 9, 45, 31, 771, DateTimeKind.Local).AddTicks(4828), new DateTime(2022, 11, 16, 9, 45, 31, 770, DateTimeKind.Local).AddTicks(4588), new DateTime(2022, 11, 16, 9, 45, 31, 771, DateTimeKind.Local).AddTicks(3975) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 778, DateTimeKind.Local).AddTicks(8349), new DateTime(2022, 11, 16, 9, 45, 31, 778, DateTimeKind.Local).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1170), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1177) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1182), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1183) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1186), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1187) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1190), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1191) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1196), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1197) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1200), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1201) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(1057), new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(1074) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(3890), new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(3894) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 794, DateTimeKind.Local).AddTicks(5273), new DateTime(2022, 11, 16, 9, 45, 31, 794, DateTimeKind.Local).AddTicks(5293), "9944.BMaqFVPPai6iYezrHG1cLQ==.8UJolLvtj8gHpRjNMZwHBBa9qrgrVnVidM3YeuQM3uI=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsentVersion",
                table: "client_consent_provider");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 11, 12, 19, 45, 754, DateTimeKind.Local).AddTicks(8030), new DateTime(2022, 11, 11, 12, 19, 45, 753, DateTimeKind.Local).AddTicks(8530), new DateTime(2022, 11, 11, 12, 19, 45, 754, DateTimeKind.Local).AddTicks(7190) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(133), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(145) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2336), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2341) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2347), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2348) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2351), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2352) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2354), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2355) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2360), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2361) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2364), new DateTime(2022, 11, 11, 12, 19, 45, 762, DateTimeKind.Local).AddTicks(2365) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 789, DateTimeKind.Local).AddTicks(8646), new DateTime(2022, 11, 11, 12, 19, 45, 789, DateTimeKind.Local).AddTicks(8669) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 790, DateTimeKind.Local).AddTicks(1652), new DateTime(2022, 11, 11, 12, 19, 45, 790, DateTimeKind.Local).AddTicks(1657) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 11, 12, 19, 45, 776, DateTimeKind.Local).AddTicks(8963), new DateTime(2022, 11, 11, 12, 19, 45, 776, DateTimeKind.Local).AddTicks(8981), "9960.ar3FaNn47YCNCHWQJJT89Q==.c51UbBGWyMnySiRIDwAYiS1hTBujf2bM8tqeT7Jzf3E=" });
        }
    }
}
