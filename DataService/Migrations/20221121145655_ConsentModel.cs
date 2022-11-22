using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ConsentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_consent_provider_clients_ClientId",
                table: "client_consent_provider");

            migrationBuilder.DropIndex(
                name: "IX_client_consent_provider_ClientId",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "ConsentVersion",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "client_consent_provider");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "client_consent_provider",
                newName: "ClientConsentId");

            migrationBuilder.CreateTable(
                name: "client_consent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_consent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_consent_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 21, 16, 56, 54, 368, DateTimeKind.Local).AddTicks(808), new DateTime(2022, 11, 21, 16, 56, 54, 367, DateTimeKind.Local).AddTicks(545), new DateTime(2022, 11, 21, 16, 56, 54, 367, DateTimeKind.Local).AddTicks(9983) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 375, DateTimeKind.Local).AddTicks(8953), new DateTime(2022, 11, 21, 16, 56, 54, 375, DateTimeKind.Local).AddTicks(8965) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1602), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1607) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1613), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1614) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1617), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1617) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1620), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1621) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1626), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1627) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1629), new DateTime(2022, 11, 21, 16, 56, 54, 376, DateTimeKind.Local).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 404, DateTimeKind.Local).AddTicks(1061), new DateTime(2022, 11, 21, 16, 56, 54, 404, DateTimeKind.Local).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 404, DateTimeKind.Local).AddTicks(4327), new DateTime(2022, 11, 21, 16, 56, 54, 404, DateTimeKind.Local).AddTicks(4332) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 21, 16, 56, 54, 392, DateTimeKind.Local).AddTicks(2777), new DateTime(2022, 11, 21, 16, 56, 54, 392, DateTimeKind.Local).AddTicks(2793), "9100.c1Ih0VTMuoYYwHxpmisrMA==.o9z+9Z/8eEuSU7CcvD4ear9upfLtYC08pMvtyVNdcIY=" });

            migrationBuilder.CreateIndex(
                name: "IX_client_consent_provider_ClientConsentId",
                table: "client_consent_provider",
                column: "ClientConsentId");

            migrationBuilder.CreateIndex(
                name: "IX_client_consent_ClientId",
                table: "client_consent",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_consent_provider_client_consent_ClientConsentId",
                table: "client_consent_provider",
                column: "ClientConsentId",
                principalTable: "client_consent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_consent_provider_client_consent_ClientConsentId",
                table: "client_consent_provider");

            migrationBuilder.DropTable(
                name: "client_consent");

            migrationBuilder.DropIndex(
                name: "IX_client_consent_provider_ClientConsentId",
                table: "client_consent_provider");

            migrationBuilder.RenameColumn(
                name: "ClientConsentId",
                table: "client_consent_provider",
                newName: "ModifiedBy");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "client_consent_provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConsentVersion",
                table: "client_consent_provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "client_consent_provider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "client_consent_provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "client_consent_provider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 18, 12, 35, 50, 738, DateTimeKind.Local).AddTicks(7131), new DateTime(2022, 11, 18, 12, 35, 50, 737, DateTimeKind.Local).AddTicks(7746), new DateTime(2022, 11, 18, 12, 35, 50, 738, DateTimeKind.Local).AddTicks(6291) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 745, DateTimeKind.Local).AddTicks(7999), new DateTime(2022, 11, 18, 12, 35, 50, 745, DateTimeKind.Local).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(239), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(245) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(251), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(252) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(255), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(256) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(258), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(259) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(264), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(265) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(267), new DateTime(2022, 11, 18, 12, 35, 50, 746, DateTimeKind.Local).AddTicks(268) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 772, DateTimeKind.Local).AddTicks(5028), new DateTime(2022, 11, 18, 12, 35, 50, 772, DateTimeKind.Local).AddTicks(5042) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 772, DateTimeKind.Local).AddTicks(7897), new DateTime(2022, 11, 18, 12, 35, 50, 772, DateTimeKind.Local).AddTicks(7902) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 18, 12, 35, 50, 760, DateTimeKind.Local).AddTicks(7182), new DateTime(2022, 11, 18, 12, 35, 50, 760, DateTimeKind.Local).AddTicks(7196), "9741.Gn64vHIiRp3mjK6sKWqWrQ==.Eo4pAUg1D7fI+LMb/lVAnvAjLyoRE/wYKCbV/yeQZZQ=" });

            migrationBuilder.CreateIndex(
                name: "IX_client_consent_provider_ClientId",
                table: "client_consent_provider",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_consent_provider_clients_ClientId",
                table: "client_consent_provider",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
