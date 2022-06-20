using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class InvestmentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fna_investments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Value = table.Column<double>(type: "float", nullable: false),
                    AttractingCGT = table.Column<bool>(type: "bit", nullable: false),
                    AllocateTo = table.Column<int>(type: "int", nullable: false),
                    Contribution = table.Column<double>(type: "float", nullable: false),
                    Escalating = table.Column<double>(type: "float", nullable: false),
                    InvestmentType = table.Column<string>(type: "varchar(100)", nullable: true),
                    Bonds = table.Column<double>(type: "float", nullable: false),
                    Equity = table.Column<double>(type: "float", nullable: false),
                    Property = table.Column<double>(type: "float", nullable: false),
                    OffshoreBonds = table.Column<double>(type: "float", nullable: false),
                    OffshoreEquity = table.Column<double>(type: "float", nullable: false),
                    OffshoreProperty = table.Column<double>(type: "float", nullable: false),
                    PrivateEquity = table.Column<double>(type: "float", nullable: false),
                    Cash = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_investments_client_fna_FNAId",
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
                values: new object[] { new DateTime(2021, 6, 20, 14, 25, 6, 489, DateTimeKind.Local).AddTicks(528), new DateTime(2022, 6, 20, 14, 25, 6, 488, DateTimeKind.Local).AddTicks(2795), new DateTime(2022, 6, 20, 14, 25, 6, 488, DateTimeKind.Local).AddTicks(9733) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(2297), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(2312) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4457), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4462) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4468), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4469) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4472), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4473) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4475), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4476) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4481), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4482) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4485), new DateTime(2022, 6, 20, 14, 25, 6, 498, DateTimeKind.Local).AddTicks(4486) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(886), new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(909) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(3934), new DateTime(2022, 6, 20, 14, 25, 6, 524, DateTimeKind.Local).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 20, 14, 25, 6, 512, DateTimeKind.Local).AddTicks(1902), new DateTime(2022, 6, 20, 14, 25, 6, 512, DateTimeKind.Local).AddTicks(1926), "9332.0iXLwSlvbegqWo+NLLzITA==.2dh+KNpGFyCxQUiPmbUHtXJJxz8PeyxBH6Z1P1uCXSw=" });

            migrationBuilder.CreateIndex(
                name: "IX_fna_investments_FNAId",
                table: "fna_investments",
                column: "FNAId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fna_investments");

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
    }
}
