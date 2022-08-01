using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class EmailIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 8, 1, 16, 18, 23, 668, DateTimeKind.Local).AddTicks(1791), new DateTime(2022, 8, 1, 16, 18, 23, 667, DateTimeKind.Local).AddTicks(3848), new DateTime(2022, 8, 1, 16, 18, 23, 668, DateTimeKind.Local).AddTicks(980) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(2560), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(2575) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4771), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4777) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4783), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4784) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4787), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4788) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4790), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4791) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4796), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4797) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4800), new DateTime(2022, 8, 1, 16, 18, 23, 675, DateTimeKind.Local).AddTicks(4801) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 703, DateTimeKind.Local).AddTicks(5516), new DateTime(2022, 8, 1, 16, 18, 23, 703, DateTimeKind.Local).AddTicks(5534) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 703, DateTimeKind.Local).AddTicks(8443), new DateTime(2022, 8, 1, 16, 18, 23, 703, DateTimeKind.Local).AddTicks(8447) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 8, 1, 16, 18, 23, 691, DateTimeKind.Local).AddTicks(9412), new DateTime(2022, 8, 1, 16, 18, 23, 691, DateTimeKind.Local).AddTicks(9426), "9142.Z03VYn6rvbcwjgIhShh8lQ==.JPmqTM5XDzqlnp7ecwKm1DVYTMSQLh9UZxuGCiRqj4w=" });

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
