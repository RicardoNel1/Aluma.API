using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ConsentModelDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_consent_clients_ClientId",
                table: "client_consent");

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

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "client_consent_provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_client_consent_clients_ClientId",
                table: "client_consent",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_consent_clients_ClientId",
                table: "client_consent");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "client_consent_provider");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "client_consent_provider");

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

            migrationBuilder.AddForeignKey(
                name: "FK_client_consent_clients_ClientId",
                table: "client_consent",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id");
        }
    }
}
