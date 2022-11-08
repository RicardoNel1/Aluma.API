using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class isIdVerified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isIdVerified",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 2, 15, 18, 13, 745, DateTimeKind.Local).AddTicks(1357), new DateTime(2022, 11, 2, 15, 18, 13, 744, DateTimeKind.Local).AddTicks(2774), new DateTime(2022, 11, 2, 15, 18, 13, 745, DateTimeKind.Local).AddTicks(38) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(5548), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(5565) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7781), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7786) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7792), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7793) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7796), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7800), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7806), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7806) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7809), new DateTime(2022, 11, 2, 15, 18, 13, 752, DateTimeKind.Local).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 779, DateTimeKind.Local).AddTicks(9373), new DateTime(2022, 11, 2, 15, 18, 13, 779, DateTimeKind.Local).AddTicks(9387) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 780, DateTimeKind.Local).AddTicks(2130), new DateTime(2022, 11, 2, 15, 18, 13, 780, DateTimeKind.Local).AddTicks(2135) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 2, 15, 18, 13, 767, DateTimeKind.Local).AddTicks(6654), new DateTime(2022, 11, 2, 15, 18, 13, 767, DateTimeKind.Local).AddTicks(6669), "9807.E8GnpqvwDWYM77dydfX3sQ==.2MgaudgUFIAsLruR6gOY1Kxwds0PeG9pHANTUJrZDNo=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isIdVerified",
                table: "users");

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
        }
    }
}
