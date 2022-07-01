using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class AssetSummaryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fna_summary_assets_client_fna_FNAId",
                table: "fna_summary_assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fna_summary_assets",
                table: "fna_summary_assets");

            migrationBuilder.DropColumn(
                name: "TotalAccrual",
                table: "fna_summary_assets");

            migrationBuilder.RenameTable(
                name: "fna_summary_assets",
                newName: "fna_summary_estate");

            migrationBuilder.RenameIndex(
                name: "IX_fna_summary_assets_FNAId",
                table: "fna_summary_estate",
                newName: "IX_fna_summary_estate_FNAId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fna_summary_estate",
                table: "fna_summary_estate",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_fna_summary_estate_client_fna_FNAId",
                table: "fna_summary_estate",
                column: "FNAId",
                principalTable: "client_fna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fna_summary_estate_client_fna_FNAId",
                table: "fna_summary_estate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fna_summary_estate",
                table: "fna_summary_estate");

            migrationBuilder.RenameTable(
                name: "fna_summary_estate",
                newName: "fna_summary_assets");

            migrationBuilder.RenameIndex(
                name: "IX_fna_summary_estate_FNAId",
                table: "fna_summary_assets",
                newName: "IX_fna_summary_assets_FNAId");

            migrationBuilder.AddColumn<double>(
                name: "TotalAccrual",
                table: "fna_summary_assets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_fna_summary_assets",
                table: "fna_summary_assets",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_fna_summary_assets_client_fna_FNAId",
                table: "fna_summary_assets",
                column: "FNAId",
                principalTable: "client_fna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
