using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ConsentForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client_consent_provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FinancialProviderId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_consent_provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "financial_providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financial_providers", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_consent_provider");

            migrationBuilder.DropTable(
                name: "financial_providers");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 2, 15, 18, 13, 745, DateTimeKind.Local).AddTicks(1357), new DateTime(2022, 11, 2, 15, 18, 13, 744, DateTimeKind.Local).AddTicks(2774), new DateTime(2022, 11, 2, 15, 18, 13, 745, DateTimeKind.Local).AddTicks(38) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(5548), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(5565) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7781), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7786) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7792), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7793) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7796), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7800), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7806), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7806) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7809), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 779, DateTimeKind.Local).AddTicks(9373), new DateTime(2022, 11, 2, 15, 18, 13, 779, DateTimeKind.Local).AddTicks(9387) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 780, DateTimeKind.Local).AddTicks(2130), new DateTime(2022, 11, 2, 15, 18, 13, 780, DateTimeKind.Local).AddTicks(2135) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 767, DateTimeKind.Local).AddTicks(6654), new DateTime(2022, 11, 2, 15, 18, 13, 767, DateTimeKind.Local).AddTicks(6669), "9807.E8GnpqvwDWYM77dydfX3sQ==.2MgaudgUFIAsLruR6gOY1Kxwds0PeG9pHANTUJrZDNo=" });
        }
    }
}
