using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class RemoveFierdOnEconomy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fna_economy_client_fna_FNAId",
                table: "fna_economy");

            migrationBuilder.DropIndex(
                name: "IX_fna_economy_FNAId",
                table: "fna_economy");

            migrationBuilder.DropColumn(
                name: "FNAId",
                table: "fna_economy");

            migrationBuilder.DropColumn(
                name: "InvestmentReturnRate",
                table: "fna_economy");

            migrationBuilder.AddColumn<int>(
                name: "EconomyVariablesId",
                table: "client_fna",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 9, 15, 2, 40, 400, DateTimeKind.Local).AddTicks(3282), new DateTime(2022, 6, 9, 15, 2, 40, 399, DateTimeKind.Local).AddTicks(6370), new DateTime(2022, 6, 9, 15, 2, 40, 400, DateTimeKind.Local).AddTicks(2499) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 406, DateTimeKind.Local).AddTicks(8023), new DateTime(2022, 6, 9, 15, 2, 40, 406, DateTimeKind.Local).AddTicks(8034) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(348), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(352) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(358), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(359) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(362), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(363) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(365), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(366) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(371), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(372) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(374), new DateTime(2022, 6, 9, 15, 2, 40, 407, DateTimeKind.Local).AddTicks(375) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 432, DateTimeKind.Local).AddTicks(3115), new DateTime(2022, 6, 9, 15, 2, 40, 432, DateTimeKind.Local).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 432, DateTimeKind.Local).AddTicks(5842), new DateTime(2022, 6, 9, 15, 2, 40, 432, DateTimeKind.Local).AddTicks(5846) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 9, 15, 2, 40, 420, DateTimeKind.Local).AddTicks(6457), new DateTime(2022, 6, 9, 15, 2, 40, 420, DateTimeKind.Local).AddTicks(6473), "9557.CYCz4MuxvYRl2zJtinbWIA==.mdBbz+rnZd01xTGYS21gZREJRt8wGIf28Lq7qKUZobQ=" });

            migrationBuilder.CreateIndex(
                name: "IX_client_fna_EconomyVariablesId",
                table: "client_fna",
                column: "EconomyVariablesId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_fna_fna_economy_EconomyVariablesId",
                table: "client_fna",
                column: "EconomyVariablesId",
                principalTable: "fna_economy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_fna_fna_economy_EconomyVariablesId",
                table: "client_fna");

            migrationBuilder.DropIndex(
                name: "IX_client_fna_EconomyVariablesId",
                table: "client_fna");

            migrationBuilder.DropColumn(
                name: "EconomyVariablesId",
                table: "client_fna");

            migrationBuilder.AddColumn<int>(
                name: "FNAId",
                table: "fna_economy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "InvestmentReturnRate",
                table: "fna_economy",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 7, 8, 4, 43, 863, DateTimeKind.Local).AddTicks(6180), new DateTime(2022, 6, 7, 8, 4, 43, 862, DateTimeKind.Local).AddTicks(8838), new DateTime(2022, 6, 7, 8, 4, 43, 863, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(5773), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8058), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8062) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8068), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8069) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8071), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8072) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8075), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8076) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8081), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8082) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8084), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8085) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(3945), new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(3964) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(7470), new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(7477) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 885, DateTimeKind.Local).AddTicks(7539), new DateTime(2022, 6, 7, 8, 4, 43, 885, DateTimeKind.Local).AddTicks(7555), "9205.mJ+WxOE1h1DgBU98Q5ZrWg==.b62aXP5jz32ZPkOt5Idm3iSOg0T4tgztQwSYK4GEtPg=" });

            migrationBuilder.CreateIndex(
                name: "IX_fna_economy_FNAId",
                table: "fna_economy",
                column: "FNAId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_fna_economy_client_fna_FNAId",
                table: "fna_economy",
                column: "FNAId",
                principalTable: "client_fna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
