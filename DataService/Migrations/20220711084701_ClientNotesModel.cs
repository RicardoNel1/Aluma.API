using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ClientNotesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client_notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", nullable: true),
                    NoteBody = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_notes_clients_ClientId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_notes");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 7, 5, 16, 19, 5, 458, DateTimeKind.Local).AddTicks(2930), new DateTime(2022, 7, 5, 16, 19, 5, 457, DateTimeKind.Local).AddTicks(5705), new DateTime(2022, 7, 5, 16, 19, 5, 458, DateTimeKind.Local).AddTicks(2015) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(347), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(354) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2427), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2431) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2437), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2438) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2441), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2441) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2444), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2444) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2450), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2453), new DateTime(2022, 7, 5, 16, 19, 5, 465, DateTimeKind.Local).AddTicks(2453) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 488, DateTimeKind.Local).AddTicks(9469), new DateTime(2022, 7, 5, 16, 19, 5, 488, DateTimeKind.Local).AddTicks(9482) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 489, DateTimeKind.Local).AddTicks(2235), new DateTime(2022, 7, 5, 16, 19, 5, 489, DateTimeKind.Local).AddTicks(2239) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 7, 5, 16, 19, 5, 477, DateTimeKind.Local).AddTicks(9113), new DateTime(2022, 7, 5, 16, 19, 5, 477, DateTimeKind.Local).AddTicks(9122), "9289.OFg84coKX6vwA/Hha7chKw==./fPv3M2Fw6vglD3T35hBoYVSF6cAuQxgFs2Q9zMC2PU=" });
        }
    }
}
