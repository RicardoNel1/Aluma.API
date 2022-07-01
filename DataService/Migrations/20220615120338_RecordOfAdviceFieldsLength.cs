using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class RecordOfAdviceFieldsLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaterialInformation",
                table: "application_roa",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "application_roa",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 15, 14, 3, 35, 982, DateTimeKind.Local).AddTicks(9654), new DateTime(2022, 6, 15, 14, 3, 35, 981, DateTimeKind.Local).AddTicks(3851), new DateTime(2022, 6, 15, 14, 3, 35, 982, DateTimeKind.Local).AddTicks(8187) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 4, DateTimeKind.Local).AddTicks(8554), new DateTime(2022, 6, 15, 14, 3, 36, 4, DateTimeKind.Local).AddTicks(8605) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2470), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2484) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2494), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2499), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2501) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2505), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2506) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2520), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2521) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2525), new DateTime(2022, 6, 15, 14, 3, 36, 5, DateTimeKind.Local).AddTicks(2526) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 60, DateTimeKind.Local).AddTicks(8413), new DateTime(2022, 6, 15, 14, 3, 36, 60, DateTimeKind.Local).AddTicks(8459) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 61, DateTimeKind.Local).AddTicks(3409), new DateTime(2022, 6, 15, 14, 3, 36, 61, DateTimeKind.Local).AddTicks(3424) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 15, 14, 3, 36, 37, DateTimeKind.Local).AddTicks(7821), new DateTime(2022, 6, 15, 14, 3, 36, 37, DateTimeKind.Local).AddTicks(7885), "9956.p/5zBqO3n1ozkx05YVjUhw==.F+Pk5enn/JP3em0qIRHXDJ+xfXiuju1RM0GLblxfx2M=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaterialInformation",
                table: "application_roa",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Introduction",
                table: "application_roa",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 13, 12, 11, 24, 45, DateTimeKind.Local).AddTicks(1593), new DateTime(2022, 6, 13, 12, 11, 24, 44, DateTimeKind.Local).AddTicks(3717), new DateTime(2022, 6, 13, 12, 11, 24, 45, DateTimeKind.Local).AddTicks(772) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(5239), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7548), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7553) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7559), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7563), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7564) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7566), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7567) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7578), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7579) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7582), new DateTime(2022, 6, 13, 12, 11, 24, 52, DateTimeKind.Local).AddTicks(7583) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 78, DateTimeKind.Local).AddTicks(5413), new DateTime(2022, 6, 13, 12, 11, 24, 78, DateTimeKind.Local).AddTicks(5432) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 78, DateTimeKind.Local).AddTicks(8387), new DateTime(2022, 6, 13, 12, 11, 24, 78, DateTimeKind.Local).AddTicks(8392) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 13, 12, 11, 24, 66, DateTimeKind.Local).AddTicks(2034), new DateTime(2022, 6, 13, 12, 11, 24, 66, DateTimeKind.Local).AddTicks(2048), "9647.4a7XvlAlT8YUawy6GgCDew==.calKY6UfB72jcIJom6iT5j6M0xDlnv5/5YEUjr/OQyA=" });
        }
    }
}
