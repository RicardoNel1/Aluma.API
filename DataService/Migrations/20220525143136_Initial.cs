using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 25, 16, 31, 34, 598, DateTimeKind.Local).AddTicks(5073), new DateTime(2022, 5, 25, 16, 31, 34, 597, DateTimeKind.Local).AddTicks(1175), new DateTime(2022, 5, 25, 16, 31, 34, 598, DateTimeKind.Local).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(1703), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(1759) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6668), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6686) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6699), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6706), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6708) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6716), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6717) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6744), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6746) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6752), new DateTime(2022, 5, 25, 16, 31, 34, 620, DateTimeKind.Local).AddTicks(6754) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 687, DateTimeKind.Local).AddTicks(1264), new DateTime(2022, 5, 25, 16, 31, 34, 687, DateTimeKind.Local).AddTicks(1337) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 687, DateTimeKind.Local).AddTicks(7539), new DateTime(2022, 5, 25, 16, 31, 34, 687, DateTimeKind.Local).AddTicks(7572) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 31, 34, 655, DateTimeKind.Local).AddTicks(7824), new DateTime(2022, 5, 25, 16, 31, 34, 655, DateTimeKind.Local).AddTicks(7872), "9298.OR1/F82U7l+S+f0tVreYEg==.Pc4kSAqcuE4DE4z67JtSoon0QpmoBbCysEgomoNBrQA=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(9580), new DateTime(2022, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(820), new DateTime(2022, 5, 23, 9, 21, 15, 37, DateTimeKind.Local).AddTicks(8773) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(6898), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(6906) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9027), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9031) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9037), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9038) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9041), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9042) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9045), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9046) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9051), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9051) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9054), new DateTime(2022, 5, 23, 9, 21, 15, 44, DateTimeKind.Local).AddTicks(9055) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(4862), new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(4883) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(7861), new DateTime(2022, 5, 23, 9, 21, 15, 70, DateTimeKind.Local).AddTicks(7866) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 23, 9, 21, 15, 58, DateTimeKind.Local).AddTicks(7260), new DateTime(2022, 5, 23, 9, 21, 15, 58, DateTimeKind.Local).AddTicks(7282), "9406.KUQoqM6LMz4opG7fNCY5KA==.zaSHlPGQoASmXFzJB4dQI2+XiJ59/gaDbxltaGzDzA8=" });
        }
    }
}
