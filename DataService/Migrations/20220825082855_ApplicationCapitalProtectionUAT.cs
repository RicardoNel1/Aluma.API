using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ApplicationCapitalProtectionUAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Created", "IsActive", "Modified" },
                values: new object[] { new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9339), true, new DateTime(2022, 8, 25, 10, 28, 54, 194, DateTimeKind.Local).AddTicks(9340) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 8, 22, 17, 15, 57, 998, DateTimeKind.Local).AddTicks(634), new DateTime(2022, 8, 22, 17, 15, 57, 997, DateTimeKind.Local).AddTicks(2961), new DateTime(2022, 8, 22, 17, 15, 57, 997, DateTimeKind.Local).AddTicks(9842) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(2185), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(2203) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5873), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5881) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5891), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5892) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5897), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5898) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5902), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5903) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5910), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5912) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "IsActive", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5916), false, new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5917) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 38, DateTimeKind.Local).AddTicks(5096), new DateTime(2022, 8, 22, 17, 15, 58, 38, DateTimeKind.Local).AddTicks(5118) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 38, DateTimeKind.Local).AddTicks(8054), new DateTime(2022, 8, 22, 17, 15, 58, 38, DateTimeKind.Local).AddTicks(8058) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 26, DateTimeKind.Local).AddTicks(1380), new DateTime(2022, 8, 22, 17, 15, 58, 26, DateTimeKind.Local).AddTicks(1398), "9427.XYDhskYgblHAsmkVLaMbYQ==.TVrUgd3XTBX2z7KrP7t1QOASdrFSUMO9FeEN50FXyRU=" });
        }
    }
}
