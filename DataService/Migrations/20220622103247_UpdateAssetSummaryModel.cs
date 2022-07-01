using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class UpdateAssetSummaryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalInvestmentsAttractingCGT",
                table: "fna_summary_estate",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalInvestmentsExemptCGT",
                table: "fna_summary_estate",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 22, 12, 32, 47, 43, DateTimeKind.Local).AddTicks(336), new DateTime(2022, 6, 22, 12, 32, 47, 42, DateTimeKind.Local).AddTicks(2316), new DateTime(2022, 6, 22, 12, 32, 47, 42, DateTimeKind.Local).AddTicks(9461) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 49, DateTimeKind.Local).AddTicks(9132), new DateTime(2022, 6, 22, 12, 32, 47, 49, DateTimeKind.Local).AddTicks(9143) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1168), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1172) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1177), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1178) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1180), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1181) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1184), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1184) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1190), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1190) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1193), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1194) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 73, DateTimeKind.Local).AddTicks(9826), new DateTime(2022, 6, 22, 12, 32, 47, 73, DateTimeKind.Local).AddTicks(9844) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 74, DateTimeKind.Local).AddTicks(2743), new DateTime(2022, 6, 22, 12, 32, 47, 74, DateTimeKind.Local).AddTicks(2749) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 62, DateTimeKind.Local).AddTicks(9470), new DateTime(2022, 6, 22, 12, 32, 47, 62, DateTimeKind.Local).AddTicks(9486), "9382.sgKiFz77QYfg2BDOIyr7rQ==.pEN83zYomZ5/rlLRoNPwD0Ntgxndv4RWm1OYF/CvlYI=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInvestmentsAttractingCGT",
                table: "fna_summary_estate");

            migrationBuilder.DropColumn(
                name: "TotalInvestmentsExemptCGT",
                table: "fna_summary_estate");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 20, 14, 25, 6, 489, DateTimeKind.Local).AddTicks(528), new DateTime(2022, 6, 20, 14, 25, 6, 488, DateTimeKind.Local).AddTicks(2795), new DateTime(2022, 6, 20, 14, 25, 6, 488, DateTimeKind.Local).AddTicks(9733) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(2297), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(2312) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4457), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4468), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4469) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4472), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4473) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4475), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4476) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4481), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4482) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4485), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4486) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(886), new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(909) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(3934), new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 512, DateTimeKind.Local).AddTicks(1902), new DateTime(2022, 6, 20, 14, 25, 6, 512, DateTimeKind.Local).AddTicks(1926), "9332.0iXLwSlvbegqWo+NLLzITA==.2dh+KNpGFyCxQUiPmbUHtXJJxz8PeyxBH6Z1P1uCXSw=" });
        }
    }
}
