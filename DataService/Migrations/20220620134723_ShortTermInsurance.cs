using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ShortTermInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortTermInsurance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "varchar(100)", nullable: true),
                    Type = table.Column<string>(type: "varchar(100)", nullable: true),
                    PolicyNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    MonthlyPremium = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortTermInsurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortTermInsurance_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 20, 15, 47, 22, 478, DateTimeKind.Local).AddTicks(7333), new DateTime(2022, 6, 20, 15, 47, 22, 478, DateTimeKind.Local).AddTicks(733), new DateTime(2022, 6, 20, 15, 47, 22, 478, DateTimeKind.Local).AddTicks(6609) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(975), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(988) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2919), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2923) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2929), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2932), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2933) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2935), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2936) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2941), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2942) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2944), new DateTime(2022, 6, 20, 15, 47, 22, 485, DateTimeKind.Local).AddTicks(2945) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 509, DateTimeKind.Local).AddTicks(1036), new DateTime(2022, 6, 20, 15, 47, 22, 509, DateTimeKind.Local).AddTicks(1052) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 509, DateTimeKind.Local).AddTicks(3647), new DateTime(2022, 6, 20, 15, 47, 22, 509, DateTimeKind.Local).AddTicks(3653) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 20, 15, 47, 22, 497, DateTimeKind.Local).AddTicks(6892), new DateTime(2022, 6, 20, 15, 47, 22, 497, DateTimeKind.Local).AddTicks(6905), "9227.y7SCC7apmhLG586d/nUdKw==.ZMR3NAwVwALVknV8dI8IGmHaneee1klVpv0ap+KzlIY=" });

            migrationBuilder.CreateIndex(
                name: "IX_ShortTermInsurance_ClientId",
                table: "ShortTermInsurance",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortTermInsurance");

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
