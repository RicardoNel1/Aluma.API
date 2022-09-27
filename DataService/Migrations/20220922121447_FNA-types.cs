using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class FNAtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FNAType",
                table: "client_fna",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PrimaryPortfolio",
                table: "client_fna",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 9, 22, 14, 14, 46, 907, DateTimeKind.Local).AddTicks(2333), new DateTime(2022, 9, 22, 14, 14, 46, 906, DateTimeKind.Local).AddTicks(4961), new DateTime(2022, 9, 22, 14, 14, 46, 907, DateTimeKind.Local).AddTicks(1541) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 914, DateTimeKind.Local).AddTicks(8119), new DateTime(2022, 9, 22, 14, 14, 46, 914, DateTimeKind.Local).AddTicks(8136) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(353), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(359) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(364), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(368), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(369) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(372), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(372) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(378), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(381), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(382) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 941, DateTimeKind.Local).AddTicks(8326), new DateTime(2022, 9, 22, 14, 14, 46, 941, DateTimeKind.Local).AddTicks(8342) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 942, DateTimeKind.Local).AddTicks(1135), new DateTime(2022, 9, 22, 14, 14, 46, 942, DateTimeKind.Local).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 929, DateTimeKind.Local).AddTicks(1975), new DateTime(2022, 9, 22, 14, 14, 46, 929, DateTimeKind.Local).AddTicks(1991), "9735.lrUQJKAtl4UI0dJDpqlisA==.L4P3+lu0rDf1maLpoS3M4ws22RnBW7JnCNk2qkDp8KA=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FNAType",
                table: "client_fna");

            migrationBuilder.DropColumn(
                name: "PrimaryPortfolio",
                table: "client_fna");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 8, 25, 10, 28, 54, 184, DateTimeKind.Local).AddTicks(991), new DateTime(2022, 8, 25, 10, 28, 54, 183, DateTimeKind.Local).AddTicks(106), new DateTime(2022, 8, 25, 10, 28, 54, 183, DateTimeKind.Local).AddTicks(9747) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(5871), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(5882) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9294), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9302) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9311), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9312) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9318), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9324), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9325) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9333), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9335) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9339), new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 234, DateTimeKind.Local), new DateTime(2022, 8, 25, 10, 28, 54, 234, DateTimeKind.Local).AddTicks(15) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 234, DateTimeKind.Local).AddTicks(4536), new DateTime(2022, 8, 25, 10, 28, 54, 234, DateTimeKind.Local).AddTicks(4545) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 215, DateTimeKind.Local).AddTicks(6861), new DateTime(2022, 8, 25, 10, 28, 54, 215, DateTimeKind.Local).AddTicks(6879), "9765.sP+3e6Qc+8Cstb8hsvxLPA==.cLYD1iKWgKJW0B5szFvqsEVN/U7cdfaFfGwOejE91DY=" });
        }
    }
}
