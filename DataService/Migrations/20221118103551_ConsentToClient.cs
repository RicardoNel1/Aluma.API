using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ConsentToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_consent_provider_clients_ClientId",
                table: "client_consent_provider");

            migrationBuilder.DropIndex(
                name: "IX_client_consent_provider_ClientId",
                table: "client_consent_provider");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 16, 9, 45, 31, 771, DateTimeKind.Local).AddTicks(4828), new DateTime(2022, 11, 16, 9, 45, 31, 770, DateTimeKind.Local).AddTicks(4588), new DateTime(2022, 11, 16, 9, 45, 31, 771, DateTimeKind.Local).AddTicks(3975) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 778, DateTimeKind.Local).AddTicks(8349), new DateTime(2022, 11, 16, 9, 45, 31, 778, DateTimeKind.Local).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1170), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1177) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1182), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1183) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1186), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1187) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1190), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1191) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1196), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1197) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1200), new DateTime(2022, 11, 16, 9, 45, 31, 779, DateTimeKind.Local).AddTicks(1201) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(1057), new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(1074) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(3890), new DateTime(2022, 11, 16, 9, 45, 31, 807, DateTimeKind.Local).AddTicks(3894) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 16, 9, 45, 31, 794, DateTimeKind.Local).AddTicks(5273), new DateTime(2022, 11, 16, 9, 45, 31, 794, DateTimeKind.Local).AddTicks(5293), "9944.BMaqFVPPai6iYezrHG1cLQ==.8UJolLvtj8gHpRjNMZwHBBa9qrgrVnVidM3YeuQM3uI=" });
        }
    }
}
