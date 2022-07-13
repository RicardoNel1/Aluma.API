using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ClientNotesModelUAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_client_notes_clients_ClientId",
                table: "client_notes");

            migrationBuilder.DropIndex(
                name: "IX_client_notes_ClientId",
                table: "client_notes");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 12, 13, 51, 26, 546, DateTimeKind.Local).AddTicks(4174), new DateTime(2022, 7, 12, 13, 51, 26, 545, DateTimeKind.Local).AddTicks(6263), new DateTime(2022, 7, 12, 13, 51, 26, 546, DateTimeKind.Local).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(3750), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(3757) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5876), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5892), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5893) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5896), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5897) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5899), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5906), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5906) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5909), new DateTime(2022, 7, 12, 13, 51, 26, 553, DateTimeKind.Local).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(4168), new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(4183) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(7192), new DateTime(2022, 7, 12, 13, 51, 26, 578, DateTimeKind.Local).AddTicks(7197) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 12, 13, 51, 26, 566, DateTimeKind.Local).AddTicks(9172), new DateTime(2022, 7, 12, 13, 51, 26, 566, DateTimeKind.Local).AddTicks(9182), "9464.ARuFivLHaLl2lB/nJA8Mhg==.XlvJVI5V7RIzp3mneFAoOMMUmmmZBmOv+zTmoD8Xd4Y=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 11, 10, 47, 0, 982, DateTimeKind.Local).AddTicks(5), new DateTime(2022, 7, 11, 10, 47, 0, 981, DateTimeKind.Local).AddTicks(2453), new DateTime(2022, 7, 11, 10, 47, 0, 981, DateTimeKind.Local).AddTicks(9087) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(3115), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(3129) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5353), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5358) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5364), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5365) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5367), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5368) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5371), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5371) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5376), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5377) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5380), new DateTime(2022, 7, 11, 10, 47, 0, 989, DateTimeKind.Local).AddTicks(5381) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 1, 14, DateTimeKind.Local).AddTicks(4793), new DateTime(2022, 7, 11, 10, 47, 1, 14, DateTimeKind.Local).AddTicks(4806) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 1, 14, DateTimeKind.Local).AddTicks(7676), new DateTime(2022, 7, 11, 10, 47, 1, 14, DateTimeKind.Local).AddTicks(7681) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 11, 10, 47, 1, 2, DateTimeKind.Local).AddTicks(7769), new DateTime(2022, 7, 11, 10, 47, 1, 2, DateTimeKind.Local).AddTicks(7781), "9592.gqQfOCvnp4gGaUuzaPK2oA==.6YxU8kJIehAEOfhqEwqlAG5o9atvVsr3eA0kDMMjsAU=" });

            migrationBuilder.CreateIndex(
                name: "IX_client_notes_ClientId",
                table: "client_notes",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_client_notes_clients_ClientId",
                table: "client_notes",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
