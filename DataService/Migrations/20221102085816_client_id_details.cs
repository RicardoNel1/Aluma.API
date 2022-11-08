using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class client_id_details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client_id_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TraceId = table.Column<int>(type: "int", nullable: false),
                    IdNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    HomeAffairsIdNo = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdNoMatchStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdBookIssuedDate = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdType = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdCardInd = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdCardDate = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdBlocked = table.Column<string>(type: "varchar(100)", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Surname = table.Column<string>(type: "varchar(100)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(100)", nullable: true),
                    Citizenship = table.Column<string>(type: "varchar(100)", nullable: true),
                    CountryofBirth = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeceasedStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeceasedDate = table.Column<string>(type: "varchar(100)", nullable: true),
                    DeathPlace = table.Column<string>(type: "varchar(100)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "varchar(100)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "varchar(100)", nullable: true),
                    MarriageDate = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_id_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_id_details_clients_ClientId",
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
                values: new object[] { new DateTime(2021, 11, 2, 10, 58, 15, 817, DateTimeKind.Local).AddTicks(7432), new DateTime(2022, 11, 2, 10, 58, 15, 816, DateTimeKind.Local).AddTicks(9920), new DateTime(2022, 11, 2, 10, 58, 15, 817, DateTimeKind.Local).AddTicks(6656) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(4393), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(4401) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6411), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6415) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6421), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6422) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6424), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6425) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6428), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6429) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6434), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6435) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6438), new DateTime(2022, 11, 2, 10, 58, 15, 824, DateTimeKind.Local).AddTicks(6439) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 849, DateTimeKind.Local).AddTicks(7865), new DateTime(2022, 11, 2, 10, 58, 15, 849, DateTimeKind.Local).AddTicks(7882) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 850, DateTimeKind.Local).AddTicks(1035), new DateTime(2022, 11, 2, 10, 58, 15, 850, DateTimeKind.Local).AddTicks(1044) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 2, 10, 58, 15, 838, DateTimeKind.Local).AddTicks(5177), new DateTime(2022, 11, 2, 10, 58, 15, 838, DateTimeKind.Local).AddTicks(5190), "9065.F9v5PmbmIRMOP6j+TG5Faw==.4U/jB9Z876oqaNWaTICBQLs+Si1cJ4AO3kdmx4H3RLk=" });

            migrationBuilder.CreateIndex(
                name: "IX_client_id_details_ClientId",
                table: "client_id_details",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_id_details");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 9, 26, 12, 0, 35, 513, DateTimeKind.Local).AddTicks(5317), new DateTime(2022, 9, 26, 12, 0, 35, 512, DateTimeKind.Local).AddTicks(4680), new DateTime(2022, 9, 26, 12, 0, 35, 513, DateTimeKind.Local).AddTicks(3994) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(2050), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(2069) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5818), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5826) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5837), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5838) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5842), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5843) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5847), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5856), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5857) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5861), new DateTime(2022, 9, 26, 12, 0, 35, 524, DateTimeKind.Local).AddTicks(5862) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 553, DateTimeKind.Local).AddTicks(5949), new DateTime(2022, 9, 26, 12, 0, 35, 553, DateTimeKind.Local).AddTicks(5967) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 554, DateTimeKind.Local).AddTicks(572), new DateTime(2022, 9, 26, 12, 0, 35, 554, DateTimeKind.Local).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 9, 26, 12, 0, 35, 540, DateTimeKind.Local).AddTicks(6639), new DateTime(2022, 9, 26, 12, 0, 35, 540, DateTimeKind.Local).AddTicks(6654), "9660./IHzk7ra+NsZEC1ESI/ypQ==.pSGe5DoNOdkV5/NhanzEVJKAEw87Q+CD34+qdGc5H8U=" });
        }
    }
}
