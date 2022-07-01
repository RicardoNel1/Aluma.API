using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class LinkCapitalGainsAndTaxLumpsumToFNA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 12, 44, 51, 419, DateTimeKind.Local).AddTicks(6845), new DateTime(2022, 6, 3, 12, 44, 51, 418, DateTimeKind.Local).AddTicks(398), new DateTime(2022, 6, 3, 12, 44, 51, 419, DateTimeKind.Local).AddTicks(4942) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 438, DateTimeKind.Local).AddTicks(4235), new DateTime(2022, 6, 3, 12, 44, 51, 438, DateTimeKind.Local).AddTicks(4314) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(932), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(951) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(984), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(988) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(997), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1000) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1014), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1017) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1042), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1045) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1054), new DateTime(2022, 6, 3, 12, 44, 51, 439, DateTimeKind.Local).AddTicks(1056) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 518, DateTimeKind.Local).AddTicks(8582), new DateTime(2022, 6, 3, 12, 44, 51, 518, DateTimeKind.Local).AddTicks(8638) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 519, DateTimeKind.Local).AddTicks(6314), new DateTime(2022, 6, 3, 12, 44, 51, 519, DateTimeKind.Local).AddTicks(6337) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 44, 51, 486, DateTimeKind.Local).AddTicks(9093), new DateTime(2022, 6, 3, 12, 44, 51, 486, DateTimeKind.Local).AddTicks(9137), "9826.YSZFKih2rzp2je3T5nb++g==.WD9a/pSh2g0kNsdxEJN3lVrQk51KXRCc59E/jOZx7Do=" });

            migrationBuilder.CreateIndex(
                name: "IX_fna_tax_lumpsum_FnaId",
                table: "fna_tax_lumpsum",
                column: "FnaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_capital_gains_tax_FnaId",
                table: "fna_capital_gains_tax",
                column: "FnaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_fna_capital_gains_tax_client_fna_FnaId",
                table: "fna_capital_gains_tax",
                column: "FnaId",
                principalTable: "client_fna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fna_tax_lumpsum_client_fna_FnaId",
                table: "fna_tax_lumpsum",
                column: "FnaId",
                principalTable: "client_fna",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fna_capital_gains_tax_client_fna_FnaId",
                table: "fna_capital_gains_tax");

            migrationBuilder.DropForeignKey(
                name: "FK_fna_tax_lumpsum_client_fna_FnaId",
                table: "fna_tax_lumpsum");

            migrationBuilder.DropIndex(
                name: "IX_fna_tax_lumpsum_FnaId",
                table: "fna_tax_lumpsum");

            migrationBuilder.DropIndex(
                name: "IX_fna_capital_gains_tax_FnaId",
                table: "fna_capital_gains_tax");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 12, 35, 54, 250, DateTimeKind.Local).AddTicks(2751), new DateTime(2022, 6, 3, 12, 35, 54, 248, DateTimeKind.Local).AddTicks(4519), new DateTime(2022, 6, 3, 12, 35, 54, 250, DateTimeKind.Local).AddTicks(696) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 269, DateTimeKind.Local).AddTicks(9228), new DateTime(2022, 6, 3, 12, 35, 54, 269, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4561), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4577) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4589), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4591) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4596), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4598) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4604), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4606) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4622), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4631), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4633) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 324, DateTimeKind.Local).AddTicks(8909), new DateTime(2022, 6, 3, 12, 35, 54, 324, DateTimeKind.Local).AddTicks(8956) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 325, DateTimeKind.Local).AddTicks(3835), new DateTime(2022, 6, 3, 12, 35, 54, 325, DateTimeKind.Local).AddTicks(3843) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 304, DateTimeKind.Local).AddTicks(9979), new DateTime(2022, 6, 3, 12, 35, 54, 305, DateTimeKind.Local).AddTicks(19), "9047.TvxOXROXKTW9colVBzDiNA==.0SansH9IuLHNIK/uRge8Hswy5vD1kxnIF1tSVq9Buh8=" });
        }
    }
}
