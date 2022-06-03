using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class UpdateMaritalDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "client_marital_details",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "client_marital_details",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdNumber",
                table: "client_marital_details",
                type: "varchar(100)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "client_marital_details");

            migrationBuilder.DropColumn(
                name: "IdNumber",
                table: "client_marital_details");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "client_marital_details",
                newName: "FullName");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 25, 16, 58, 42, 495, DateTimeKind.Local).AddTicks(5821), new DateTime(2022, 5, 25, 16, 58, 42, 493, DateTimeKind.Local).AddTicks(207), new DateTime(2022, 5, 25, 16, 58, 42, 495, DateTimeKind.Local).AddTicks(3557) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 515, DateTimeKind.Local).AddTicks(6332), new DateTime(2022, 5, 25, 16, 58, 42, 515, DateTimeKind.Local).AddTicks(6404) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(955), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(975) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(985), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(987) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(991), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(993) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(996), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(998) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(1015), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(1016) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(1062), new DateTime(2022, 5, 25, 16, 58, 42, 516, DateTimeKind.Local).AddTicks(1063) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 596, DateTimeKind.Local).AddTicks(6225), new DateTime(2022, 5, 25, 16, 58, 42, 596, DateTimeKind.Local).AddTicks(6294) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 597, DateTimeKind.Local).AddTicks(1120), new DateTime(2022, 5, 25, 16, 58, 42, 597, DateTimeKind.Local).AddTicks(1127) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 58, 42, 562, DateTimeKind.Local).AddTicks(3041), new DateTime(2022, 5, 25, 16, 58, 42, 562, DateTimeKind.Local).AddTicks(3095), "9686.w03Z9MYxZRuFPMabQKrTow==.w3ewHlkE+LvSWicmSkE7V31gkW9h6ddUqG45lMxBzG4=" });
        }
    }
}
