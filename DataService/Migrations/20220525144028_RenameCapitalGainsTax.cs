using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class RenameCapitalGainsTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_capital_gains_tax",
                table: "capital_gains_tax");

            migrationBuilder.RenameTable(
                name: "capital_gains_tax",
                newName: "fna_capital_gains_tax");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fna_capital_gains_tax",
                table: "fna_capital_gains_tax",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fna_capital_gains_tax",
                table: "fna_capital_gains_tax");

            migrationBuilder.RenameTable(
                name: "fna_capital_gains_tax",
                newName: "capital_gains_tax");

            migrationBuilder.AddPrimaryKey(
                name: "PK_capital_gains_tax",
                table: "capital_gains_tax",
                column: "Id");

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
    }
}
