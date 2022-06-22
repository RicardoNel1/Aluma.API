using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class MedicalAid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "medical_aid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "varchar(100)", nullable: true),
                    Type = table.Column<string>(type: "varchar(100)", nullable: true),
                    MedicalAidNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    MainMember = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfDependants = table.Column<int>(type: "int", nullable: false),
                    MonthlyPremium = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_aid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_aid_clients_ClientId",
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
                values: new object[] { new DateTime(2021, 6, 21, 14, 54, 53, 577, DateTimeKind.Local).AddTicks(946), new DateTime(2022, 6, 21, 14, 54, 53, 575, DateTimeKind.Local).AddTicks(1978), new DateTime(2022, 6, 21, 14, 54, 53, 576, DateTimeKind.Local).AddTicks(9494) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(2243), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(2265) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4566), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4575) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4581), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4582) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4585), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4586) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4589), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4595), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4596) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4598), new DateTime(2022, 6, 21, 14, 54, 53, 586, DateTimeKind.Local).AddTicks(4599) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 621, DateTimeKind.Local).AddTicks(9183), new DateTime(2022, 6, 21, 14, 54, 53, 621, DateTimeKind.Local).AddTicks(9202) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 622, DateTimeKind.Local).AddTicks(1966), new DateTime(2022, 6, 21, 14, 54, 53, 622, DateTimeKind.Local).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 21, 14, 54, 53, 604, DateTimeKind.Local).AddTicks(2893), new DateTime(2022, 6, 21, 14, 54, 53, 604, DateTimeKind.Local).AddTicks(2914), "9578.MBh8g2F0o91Qn/QNcmWEKA==.jkDLf3EfsmSVVb5IpoHxyuj78R1K6ijeomIPv59yCsM=" });

            migrationBuilder.CreateIndex(
                name: "IX_medical_aid_ClientId",
                table: "medical_aid",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "medical_aid");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 21, 9, 30, 2, 919, DateTimeKind.Local).AddTicks(4599), new DateTime(2022, 6, 21, 9, 30, 2, 918, DateTimeKind.Local).AddTicks(8016), new DateTime(2022, 6, 21, 9, 30, 2, 919, DateTimeKind.Local).AddTicks(3775) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(4981), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(4988) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6881), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6886) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6891), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6892) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6895), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6895) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6898), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6898) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6908), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6909) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6912), new DateTime(2022, 6, 21, 9, 30, 2, 925, DateTimeKind.Local).AddTicks(6912) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 947, DateTimeKind.Local).AddTicks(9370), new DateTime(2022, 6, 21, 9, 30, 2, 947, DateTimeKind.Local).AddTicks(9377) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 948, DateTimeKind.Local).AddTicks(1888), new DateTime(2022, 6, 21, 9, 30, 2, 948, DateTimeKind.Local).AddTicks(1892) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 21, 9, 30, 2, 937, DateTimeKind.Local).AddTicks(9400), new DateTime(2022, 6, 21, 9, 30, 2, 937, DateTimeKind.Local).AddTicks(9410), "9965.bHX6GFz/x96WsEMUeVGKPQ==.afzcB/Bo722k94lD26ewJB6QblaA/FcyjttY+LeX+SI=" });
        }
    }
}
