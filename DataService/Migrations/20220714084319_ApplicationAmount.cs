using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ApplicationAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationAmount",
                table: "applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationAmount",
                table: "applications");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 12, 13, 51, 26, 546, DateTimeKind.Local).AddTicks(4174), new DateTime(2022, 7, 12, 13, 51, 26, 545, DateTimeKind.Local).AddTicks(6263), new DateTime(2022, 7, 12, 13, 51, 26, 546, DateTimeKind.Local).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(3750), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(3757) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5876), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5892), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5893) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5896), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5897) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5899), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5906), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5906) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5909), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(4168), new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(4183) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(7192), new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(7197) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 566, DateTimeKind.Local).AddTicks(9172), new DateTime(2022, 7, 12, 13, 51, 26, 566, DateTimeKind.Local).AddTicks(9182), "9464.ARuFivLHaLl2lB/nJA8Mhg==.XlvJVI5V7RIzp3mneFAoOMMUmmmZBmOv+zTmoD8Xd4Y=" });
        }
    }
}
