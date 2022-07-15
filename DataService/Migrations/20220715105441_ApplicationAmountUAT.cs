using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ApplicationAmountUAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 14, 10, 43, 18, 730, DateTimeKind.Local).AddTicks(5417), new DateTime(2022, 7, 14, 10, 43, 18, 729, DateTimeKind.Local).AddTicks(8032), new DateTime(2022, 7, 14, 10, 43, 18, 730, DateTimeKind.Local).AddTicks(4644) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(2352), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4397), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4402) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4407), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4408) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4411), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4412) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4414), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4415) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4426), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4427) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4429), new DateTime(2022, 7, 14, 10, 43, 18, 737, DateTimeKind.Local).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 761, DateTimeKind.Local).AddTicks(6452), new DateTime(2022, 7, 14, 10, 43, 18, 761, DateTimeKind.Local).AddTicks(6463) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 761, DateTimeKind.Local).AddTicks(9105), new DateTime(2022, 7, 14, 10, 43, 18, 761, DateTimeKind.Local).AddTicks(9110) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 14, 10, 43, 18, 750, DateTimeKind.Local).AddTicks(6807), new DateTime(2022, 7, 14, 10, 43, 18, 750, DateTimeKind.Local).AddTicks(6820), "9130.0sMjfTFhwokgam8FE7mRfQ==.vrRpiVKg6PcLgX33+93oMTE5KRS0DiEjyQo8MmwRGz0=" });
        }
    }
}
