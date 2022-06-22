using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class RenamedShortTermInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShortTermInsurance_clients_ClientId",
                table: "ShortTermInsurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShortTermInsurance",
                table: "ShortTermInsurance");

            migrationBuilder.RenameTable(
                name: "ShortTermInsurance",
                newName: "short_term_insurance");

            migrationBuilder.RenameIndex(
                name: "IX_ShortTermInsurance_ClientId",
                table: "short_term_insurance",
                newName: "IX_short_term_insurance_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_short_term_insurance",
                table: "short_term_insurance",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_short_term_insurance_clients_ClientId",
                table: "short_term_insurance",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_short_term_insurance_clients_ClientId",
                table: "short_term_insurance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_short_term_insurance",
                table: "short_term_insurance");

            migrationBuilder.RenameTable(
                name: "short_term_insurance",
                newName: "ShortTermInsurance");

            migrationBuilder.RenameIndex(
                name: "IX_short_term_insurance_ClientId",
                table: "ShortTermInsurance",
                newName: "IX_ShortTermInsurance_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShortTermInsurance",
                table: "ShortTermInsurance",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShortTermInsurance_clients_ClientId",
                table: "ShortTermInsurance",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
