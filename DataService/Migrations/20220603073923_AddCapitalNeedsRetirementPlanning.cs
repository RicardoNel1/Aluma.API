using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class AddCapitalNeedsRetirementPlanning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CapitalNeeds",
                table: "fna_retirement_planning",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapitalNeeds",
                table: "fna_retirement_planning");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 2, 15, 11, 35, 242, DateTimeKind.Local).AddTicks(9458), new DateTime(2022, 6, 2, 15, 11, 35, 242, DateTimeKind.Local).AddTicks(2590), new DateTime(2022, 6, 2, 15, 11, 35, 242, DateTimeKind.Local).AddTicks(8569) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(4045), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(4066) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6215), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6223) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6229), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6233), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6234) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6236), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6237) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6243), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6244) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6246), new DateTime(2022, 6, 2, 15, 11, 35, 250, DateTimeKind.Local).AddTicks(6247) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 274, DateTimeKind.Local).AddTicks(2325), new DateTime(2022, 6, 2, 15, 11, 35, 274, DateTimeKind.Local).AddTicks(2345) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 274, DateTimeKind.Local).AddTicks(5571), new DateTime(2022, 6, 2, 15, 11, 35, 274, DateTimeKind.Local).AddTicks(5575) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 2, 15, 11, 35, 263, DateTimeKind.Local).AddTicks(9768), new DateTime(2022, 6, 2, 15, 11, 35, 263, DateTimeKind.Local).AddTicks(9786), "9224.Fc7Xu8cQQQNQJIbf1Bnj6w==.RViv7t/h2KG1P735n4iZgd/9q92rfZOaCgy02jTpEM4=" });
        }
    }
}
