using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ApplicationCapitalProtection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapitalProtection",
                table: "applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5916), new DateTime(2022, 8, 22, 17, 15, 58, 6, DateTimeKind.Local).AddTicks(5917) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalProtection",
                table: "applications");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 8, 1, 16, 29, 47, 183, DateTimeKind.Local).AddTicks(6908), new DateTime(2022, 8, 1, 16, 29, 47, 182, DateTimeKind.Local).AddTicks(8632), new DateTime(2022, 8, 1, 16, 29, 47, 183, DateTimeKind.Local).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 190, DateTimeKind.Local).AddTicks(7727), new DateTime(2022, 8, 1, 16, 29, 47, 190, DateTimeKind.Local).AddTicks(7745) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 190, DateTimeKind.Local).AddTicks(9999), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(3) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(9), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(13), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(14) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(17), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(18) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(23), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(24) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(27), new DateTime(2022, 8, 1, 16, 29, 47, 191, DateTimeKind.Local).AddTicks(28) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 221, DateTimeKind.Local).AddTicks(1585), new DateTime(2022, 8, 1, 16, 29, 47, 221, DateTimeKind.Local).AddTicks(1607) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 221, DateTimeKind.Local).AddTicks(4418), new DateTime(2022, 8, 1, 16, 29, 47, 221, DateTimeKind.Local).AddTicks(4423) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 29, 47, 208, DateTimeKind.Local).AddTicks(6092), new DateTime(2022, 8, 1, 16, 29, 47, 208, DateTimeKind.Local).AddTicks(6115), "9911.rwi55q+0Vg8tmQyz+eTHeA==.xy1SUCDmHoV6dA3ybrxP/EFjY7dJj7tnbUbHmv1Ht5I=" });
        }
    }
}
