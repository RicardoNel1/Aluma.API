using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ExplicitlySetSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "fna_assets_exempt_from_cgt",
                newName: "fna_assets_exempt_from_cgt",
                newSchema: "dbo");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "fna_assets_exempt_from_cgt",
                schema: "dbo",
                newName: "fna_assets_exempt_from_cgt");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 25, 16, 40, 25, 760, DateTimeKind.Local).AddTicks(6844), new DateTime(2022, 5, 25, 16, 40, 25, 759, DateTimeKind.Local).AddTicks(484), new DateTime(2022, 5, 25, 16, 40, 25, 760, DateTimeKind.Local).AddTicks(4731) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 779, DateTimeKind.Local).AddTicks(9331), new DateTime(2022, 5, 25, 16, 40, 25, 779, DateTimeKind.Local).AddTicks(9414) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4481), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4491) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4501), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4503) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4509), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4516), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4517) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4575), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4577) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4587), new DateTime(2022, 5, 25, 16, 40, 25, 780, DateTimeKind.Local).AddTicks(4588) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 830, DateTimeKind.Local).AddTicks(4004), new DateTime(2022, 5, 25, 16, 40, 25, 830, DateTimeKind.Local).AddTicks(4069) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 831, DateTimeKind.Local).AddTicks(163), new DateTime(2022, 5, 25, 16, 40, 25, 831, DateTimeKind.Local).AddTicks(179) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 25, 16, 40, 25, 808, DateTimeKind.Local).AddTicks(6436), new DateTime(2022, 5, 25, 16, 40, 25, 808, DateTimeKind.Local).AddTicks(6486), "9454.2ANADFJ2vtJxQqC/5IzK+g==.Z1HztcmARudhTjjC4FDUDrZmSXLlS+LafV2YHMSAhvc=" });
        }
    }
}
