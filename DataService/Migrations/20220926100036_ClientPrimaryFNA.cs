using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ClientPrimaryFNA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryFNA",
                table: "clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 9, 26, 12, 0, 35, 513, DateTimeKind.Local).AddTicks(5317), new DateTime(2022, 9, 26, 12, 0, 35, 512, DateTimeKind.Local).AddTicks(4680), new DateTime(2022, 9, 26, 12, 0, 35, 513, DateTimeKind.Local).AddTicks(3994) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(2050), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(2069) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5818), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5826) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5837), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5838) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5842), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5843) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5847), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5856), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5857) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5861), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5862) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 553, DateTimeKind.Local).AddTicks(5949), new DateTime(2022, 9, 26, 12, 0, 35, 553, DateTimeKind.Local).AddTicks(5967) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 554, DateTimeKind.Local).AddTicks(572), new DateTime(2022, 9, 26, 12, 0, 35, 554, DateTimeKind.Local).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 540, DateTimeKind.Local).AddTicks(6639), new DateTime(2022, 9, 26, 12, 0, 35, 540, DateTimeKind.Local).AddTicks(6654), "9660./IHzk7ra+NsZEC1ESI/ypQ==.pSGe5DoNOdkV5/NhanzEVJKAEw87Q+CD34+qdGc5H8U=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryFNA",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 9, 22, 14, 14, 46, 907, DateTimeKind.Local).AddTicks(2333), new DateTime(2022, 9, 22, 14, 14, 46, 906, DateTimeKind.Local).AddTicks(4961), new DateTime(2022, 9, 22, 14, 14, 46, 907, DateTimeKind.Local).AddTicks(1541) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 914, DateTimeKind.Local).AddTicks(8119), new DateTime(2022, 9, 22, 14, 14, 46, 914, DateTimeKind.Local).AddTicks(8136) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(353), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(359) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(364), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(368), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(369) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(372), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(372) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(378), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(381), new DateTime(2022, 9, 22, 14, 14, 46, 915, DateTimeKind.Local).AddTicks(382) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 941, DateTimeKind.Local).AddTicks(8326), new DateTime(2022, 9, 22, 14, 14, 46, 941, DateTimeKind.Local).AddTicks(8342) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 942, DateTimeKind.Local).AddTicks(1135), new DateTime(2022, 9, 22, 14, 14, 46, 942, DateTimeKind.Local).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 9, 22, 14, 14, 46, 929, DateTimeKind.Local).AddTicks(1975), new DateTime(2022, 9, 22, 14, 14, 46, 929, DateTimeKind.Local).AddTicks(1991), "9735.lrUQJKAtl4UI0dJDpqlisA==.L4P3+lu0rDf1maLpoS3M4ws22RnBW7JnCNk2qkDp8KA=" });
        }
    }
}
