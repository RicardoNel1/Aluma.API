using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class FixTypoAssumptionsDTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearsTillLifeExpentancy",
                table: "fna_assumptions",
                newName: "YearsTillLifeExpectancy");

            migrationBuilder.RenameColumn(
                name: "LifeExpentancy",
                table: "fna_assumptions",
                newName: "LifeExpectancy");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearsTillLifeExpectancy",
                table: "fna_assumptions",
                newName: "YearsTillLifeExpentancy");

            migrationBuilder.RenameColumn(
                name: "LifeExpectancy",
                table: "fna_assumptions",
                newName: "LifeExpentancy");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 10, 27, 8, 417, DateTimeKind.Local).AddTicks(6488), new DateTime(2022, 6, 3, 10, 27, 8, 416, DateTimeKind.Local).AddTicks(9847), new DateTime(2022, 6, 3, 10, 27, 8, 417, DateTimeKind.Local).AddTicks(5625) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 423, DateTimeKind.Local).AddTicks(9717), new DateTime(2022, 6, 3, 10, 27, 8, 423, DateTimeKind.Local).AddTicks(9728) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1711), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1716) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1721), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1722) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1724), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1725) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1727), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1728) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1733), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1734) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1736), new DateTime(2022, 6, 3, 10, 27, 8, 424, DateTimeKind.Local).AddTicks(1737) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 446, DateTimeKind.Local).AddTicks(2037), new DateTime(2022, 6, 3, 10, 27, 8, 446, DateTimeKind.Local).AddTicks(2053) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 446, DateTimeKind.Local).AddTicks(4640), new DateTime(2022, 6, 3, 10, 27, 8, 446, DateTimeKind.Local).AddTicks(4645) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 10, 27, 8, 436, DateTimeKind.Local).AddTicks(2620), new DateTime(2022, 6, 3, 10, 27, 8, 436, DateTimeKind.Local).AddTicks(2637), "9261.SPTx5aSJj1zw3tYAnFQSVA==.8W3RZei2r8fE3W9n4q4KU67heI/hzsMq5S3H8KmQt94=" });
        }
    }
}
