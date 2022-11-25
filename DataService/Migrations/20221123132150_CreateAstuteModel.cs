using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class CreateAstuteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advisor_astute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advisor_astute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_advisor_astute_advisors_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "advisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 23, 15, 21, 49, 582, DateTimeKind.Local).AddTicks(866), new DateTime(2022, 11, 23, 15, 21, 49, 581, DateTimeKind.Local).AddTicks(1930), new DateTime(2022, 11, 23, 15, 21, 49, 581, DateTimeKind.Local).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 589, DateTimeKind.Local).AddTicks(8132), new DateTime(2022, 11, 23, 15, 21, 49, 589, DateTimeKind.Local).AddTicks(8144) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(256), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(261) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(267), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(268) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(271), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(272) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(274), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(275) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(280), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(281) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(283), new DateTime(2022, 11, 23, 15, 21, 49, 590, DateTimeKind.Local).AddTicks(284) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(2950), new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(2972) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(5821), new DateTime(2022, 11, 23, 15, 21, 49, 619, DateTimeKind.Local).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 23, 15, 21, 49, 606, DateTimeKind.Local).AddTicks(8428), new DateTime(2022, 11, 23, 15, 21, 49, 606, DateTimeKind.Local).AddTicks(8446), "9000.WskblVL+xOVSBSeU3o3dPg==.cDpyOCkoTUJFR4R1jU9RwIQMMZBjI69IiTbiyKOOHBs=" });

            migrationBuilder.CreateIndex(
                name: "IX_advisor_astute_AdvisorId",
                table: "advisor_astute",
                column: "AdvisorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advisor_astute");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 21, 17, 51, 16, 218, DateTimeKind.Local).AddTicks(2384), new DateTime(2022, 11, 21, 17, 51, 16, 217, DateTimeKind.Local).AddTicks(1269), new DateTime(2022, 11, 21, 17, 51, 16, 218, DateTimeKind.Local).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 225, DateTimeKind.Local).AddTicks(7928), new DateTime(2022, 11, 21, 17, 51, 16, 225, DateTimeKind.Local).AddTicks(7939) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(275), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(285), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(287) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(289), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(293), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(294) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(299), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(302), new DateTime(2022, 11, 21, 17, 51, 16, 226, DateTimeKind.Local).AddTicks(303) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 258, DateTimeKind.Local).AddTicks(1419), new DateTime(2022, 11, 21, 17, 51, 16, 258, DateTimeKind.Local).AddTicks(1438) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 258, DateTimeKind.Local).AddTicks(7495), new DateTime(2022, 11, 21, 17, 51, 16, 258, DateTimeKind.Local).AddTicks(7509) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 21, 17, 51, 16, 244, DateTimeKind.Local).AddTicks(5295), new DateTime(2022, 11, 21, 17, 51, 16, 244, DateTimeKind.Local).AddTicks(5319), "9372.V69yERJm5/APLs8qIE8LmA==.qL3h+MD3bhijxs8708+zupYMyo9wfCSOWd7J6K1daRo=" });
        }
    }
}
