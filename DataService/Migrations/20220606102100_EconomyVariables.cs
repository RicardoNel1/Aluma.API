using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class EconomyVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fna_economy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    InflationRate = table.Column<double>(type: "float", nullable: false),
                    InvestmentReturnRate = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_economy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_economy_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 6, 12, 20, 59, 667, DateTimeKind.Local).AddTicks(716), new DateTime(2022, 6, 6, 12, 20, 59, 666, DateTimeKind.Local).AddTicks(6975), new DateTime(2022, 6, 6, 12, 20, 59, 666, DateTimeKind.Local).AddTicks(9902) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(4298), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(4309) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6369), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6373) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6378), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6379) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6382), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6385), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6386) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6390), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6391) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6393), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6394) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(5486), new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(5503) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(8179), new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 686, DateTimeKind.Local).AddTicks(6857), new DateTime(2022, 6, 6, 12, 20, 59, 686, DateTimeKind.Local).AddTicks(6883), "9437.V9lpRki3Nai1m5gEgVwYqQ==.RBNhSVMcGqy1AzRRWVUp6Ya3Zun0yXv1Bnf9Y+O0w2Q=" });

            migrationBuilder.CreateIndex(
                name: "IX_fna_economy_FNAId",
                table: "fna_economy",
                column: "FNAId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fna_economy");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(9837), new DateTime(2022, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(3346), new DateTime(2022, 6, 3, 23, 6, 26, 308, DateTimeKind.Local).AddTicks(9123) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(1459), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(1471) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3454), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3464), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3465) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3467), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3468) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3471), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3471) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3476), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3476) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3479), new DateTime(2022, 6, 3, 23, 6, 26, 315, DateTimeKind.Local).AddTicks(3479) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(4361), new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(4379) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(6855), new DateTime(2022, 6, 3, 23, 6, 26, 338, DateTimeKind.Local).AddTicks(6859) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 23, 6, 26, 327, DateTimeKind.Local).AddTicks(7228), new DateTime(2022, 6, 3, 23, 6, 26, 327, DateTimeKind.Local).AddTicks(7249), "9615.wxS0yyrSGZStXlRXVxUjQg==.rwGVxmBvWMF1J+VqgvvWKU3zNjzRoHZ+qnEx391/Lrs=" });
        }
    }
}
