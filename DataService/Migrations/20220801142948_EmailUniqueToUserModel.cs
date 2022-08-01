using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class EmailUniqueToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                table: "users");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 15, 12, 54, 40, 875, DateTimeKind.Local).AddTicks(8256), new DateTime(2022, 7, 15, 12, 54, 40, 875, DateTimeKind.Local).AddTicks(849), new DateTime(2022, 7, 15, 12, 54, 40, 875, DateTimeKind.Local).AddTicks(7452) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(5444), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(5451) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7626), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7636), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7637) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7640), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7641) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7644), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7644) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7650), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7651) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7653), new DateTime(2022, 7, 15, 12, 54, 40, 882, DateTimeKind.Local).AddTicks(7654) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 907, DateTimeKind.Local).AddTicks(3129), new DateTime(2022, 7, 15, 12, 54, 40, 907, DateTimeKind.Local).AddTicks(3144) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 907, DateTimeKind.Local).AddTicks(5915), new DateTime(2022, 7, 15, 12, 54, 40, 907, DateTimeKind.Local).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 15, 12, 54, 40, 895, DateTimeKind.Local).AddTicks(9698), new DateTime(2022, 7, 15, 12, 54, 40, 895, DateTimeKind.Local).AddTicks(9711), "9271.8IjswpltrtTeXS8hrK8+oQ==.PNxua9kaz0/teneN7ytyB1cyrxNHxvdf8GIHaco6H/8=" });
        }
    }
}
