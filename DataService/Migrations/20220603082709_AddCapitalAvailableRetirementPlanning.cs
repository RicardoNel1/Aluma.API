using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class AddCapitalAvailableRetirementPlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CapitalAvailable",
                table: "fna_retirement_planning",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalAvailable",
                table: "fna_retirement_planning");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 9, 39, 22, 664, DateTimeKind.Local).AddTicks(283), new DateTime(2022, 6, 3, 9, 39, 22, 663, DateTimeKind.Local).AddTicks(3189), new DateTime(2022, 6, 3, 9, 39, 22, 663, DateTimeKind.Local).AddTicks(9199) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(3250), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(3267) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5254), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5259) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5264), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5264) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5267), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5268) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5270), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5271) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5276), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5277) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5279), new DateTime(2022, 6, 3, 9, 39, 22, 670, DateTimeKind.Local).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 694, DateTimeKind.Local).AddTicks(8073), new DateTime(2022, 6, 3, 9, 39, 22, 694, DateTimeKind.Local).AddTicks(8091) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 695, DateTimeKind.Local).AddTicks(627), new DateTime(2022, 6, 3, 9, 39, 22, 695, DateTimeKind.Local).AddTicks(631) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 9, 39, 22, 683, DateTimeKind.Local).AddTicks(3024), new DateTime(2022, 6, 3, 9, 39, 22, 683, DateTimeKind.Local).AddTicks(3044), "9996.S54BcCjWVSsWH+oysCO2MA==.eUd6sJk9RaiL0RSuB9SBY+iES8mVxee0PZr/BcOX7LY=" });
        }
    }
}
