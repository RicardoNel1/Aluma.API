using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class TotalCapitalAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CapitalNeeds",
                table: "fna_retirement_planning",
                newName: "TotalCapitalNeeds");

            migrationBuilder.AddColumn<double>(
                name: "TotalCapitalAvailable",
                table: "fna_retirement_planning",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCapitalAvailable",
                table: "fna_retirement_planning");

            migrationBuilder.RenameColumn(
                name: "TotalCapitalNeeds",
                table: "fna_retirement_planning",
                newName: "CapitalNeeds");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 30, 13, 40, 58, 346, DateTimeKind.Local).AddTicks(6317), new DateTime(2022, 5, 30, 13, 40, 58, 345, DateTimeKind.Local).AddTicks(6007), new DateTime(2022, 5, 30, 13, 40, 58, 346, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(6981), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(6999) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8931), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8938) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8943), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8944) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8947), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8947) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8949), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8955), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8955) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8958), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8958) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(6254), new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(6269) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(9803), new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 367, DateTimeKind.Local).AddTicks(1925), new DateTime(2022, 5, 30, 13, 40, 58, 367, DateTimeKind.Local).AddTicks(1941), "9766.w1JWlG0lU0CItOlYd6jTvQ==.FvJkiOKpMTYE+dnbLvkv07ppvHUV3AOHkjqS1j2YY+k=" });
        }
    }
}
