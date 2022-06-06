using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class UpdateAssetSummaryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAssetsToEstate",
                table: "fna_summary_assets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 6, 19, 58, 28, 883, DateTimeKind.Local).AddTicks(4189), new DateTime(2022, 6, 6, 19, 58, 28, 882, DateTimeKind.Local).AddTicks(772), new DateTime(2022, 6, 6, 19, 58, 28, 883, DateTimeKind.Local).AddTicks(2622) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 896, DateTimeKind.Local).AddTicks(8407), new DateTime(2022, 6, 6, 19, 58, 28, 896, DateTimeKind.Local).AddTicks(8431) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1742), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1758), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1764), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1766) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1769), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1778), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1784), new DateTime(2022, 6, 6, 19, 58, 28, 897, DateTimeKind.Local).AddTicks(1785) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 937, DateTimeKind.Local).AddTicks(4037), new DateTime(2022, 6, 6, 19, 58, 28, 937, DateTimeKind.Local).AddTicks(4057) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 937, DateTimeKind.Local).AddTicks(8435), new DateTime(2022, 6, 6, 19, 58, 28, 937, DateTimeKind.Local).AddTicks(8443) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 6, 19, 58, 28, 918, DateTimeKind.Local).AddTicks(6587), new DateTime(2022, 6, 6, 19, 58, 28, 918, DateTimeKind.Local).AddTicks(6606), "9455.JdbdlsxyNSyP6BAUPA4TcA==.Lsb2NS2fU3mCU+Wa7/5nI8MMduKBtVATxE4/suFSt+s=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAssetsToEstate",
                table: "fna_summary_assets");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(9837), new DateTime(2022, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(3346), new DateTime(2022, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(9123) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(1459), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(1471) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3454), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3464), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3465) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3467), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3468) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3471), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3471) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3476), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3476) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3479), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3479) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(4361), new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(4379) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(6855), new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(6859) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 327, DateTimeKind.Local).AddTicks(7228), new DateTime(2022, 6, 3, 23, 6, 26, 327, DateTimeKind.Local).AddTicks(7249), "9615.wxS0yyrSGZStXlRXVxUjQg==.rwGVxmBvWMF1J+VqgvvWKU3zNjzRoHZ+qnEx391/Lrs=" });
        }
    }
}
