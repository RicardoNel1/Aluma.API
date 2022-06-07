using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class RetirementPlanning_Add_IncomeAvailableTotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IncomeAvailableTotal",
                table: "fna_retirement_planning",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 7, 8, 4, 43, 863, DateTimeKind.Local).AddTicks(6180), new DateTime(2022, 6, 7, 8, 4, 43, 862, DateTimeKind.Local).AddTicks(8838), new DateTime(2022, 6, 7, 8, 4, 43, 863, DateTimeKind.Local).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(5773), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8058), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8062) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8068), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8069) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8071), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8072) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8075), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8076) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8081), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8082) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8084), new DateTime(2022, 6, 7, 8, 4, 43, 870, DateTimeKind.Local).AddTicks(8085) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(3945), new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(3964) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(7470), new DateTime(2022, 6, 7, 8, 4, 43, 897, DateTimeKind.Local).AddTicks(7477) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 7, 8, 4, 43, 885, DateTimeKind.Local).AddTicks(7539), new DateTime(2022, 6, 7, 8, 4, 43, 885, DateTimeKind.Local).AddTicks(7555), "9205.mJ+WxOE1h1DgBU98Q5ZrWg==.b62aXP5jz32ZPkOt5Idm3iSOg0T4tgztQwSYK4GEtPg=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeAvailableTotal",
                table: "fna_retirement_planning");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 6, 12, 20, 59, 667, DateTimeKind.Local).AddTicks(716), new DateTime(2022, 6, 6, 12, 20, 59, 666, DateTimeKind.Local).AddTicks(6975), new DateTime(2022, 6, 6, 12, 20, 59, 666, DateTimeKind.Local).AddTicks(9902) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(4298), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(4309) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6369), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6373) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6378), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6379) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6382), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6385), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6386) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6390), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6391) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6393), new DateTime(2022, 6, 6, 12, 20, 59, 673, DateTimeKind.Local).AddTicks(6394) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(5486), new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(5503) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(8179), new DateTime(2022, 6, 6, 12, 20, 59, 697, DateTimeKind.Local).AddTicks(8183) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 6, 12, 20, 59, 686, DateTimeKind.Local).AddTicks(6857), new DateTime(2022, 6, 6, 12, 20, 59, 686, DateTimeKind.Local).AddTicks(6883), "9437.V9lpRki3Nai1m5gEgVwYqQ==.RBNhSVMcGqy1AzRRWVUp6Ya3Zun0yXv1Bnf9Y+O0w2Q=" });
        }
    }
}
