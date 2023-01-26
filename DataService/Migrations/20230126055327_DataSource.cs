using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class DataSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataSource",
                table: "fna_retirement_pension_funds",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataSource",
                table: "fna_investments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataSource",
                table: "fna_insurance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2022, 1, 26, 7, 53, 26, 521, DateTimeKind.Local).AddTicks(9253), new DateTime(2023, 1, 26, 7, 53, 26, 521, DateTimeKind.Local).AddTicks(198), new DateTime(2023, 1, 26, 7, 53, 26, 521, DateTimeKind.Local).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(5717), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(5735) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6624), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6628) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6633), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6633) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6636), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6637) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6641), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6642) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6647), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6648) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6650), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6651) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6654), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6654) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6657), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6658) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6661), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6662) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6664), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6665) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6668), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6669) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6671), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6672) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6674), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6675) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6678), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6679) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6681), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6682) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6684), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6685) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6688), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6689) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6691), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6692) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6700), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6703), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6704) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6706), new DateTime(2023, 1, 26, 7, 53, 26, 541, DateTimeKind.Local).AddTicks(6707) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(6297), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(6314) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8482), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8488) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8494), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8495) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8498), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8499) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8502), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8503) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8508), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8509) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8511), new DateTime(2023, 1, 26, 7, 53, 26, 529, DateTimeKind.Local).AddTicks(8512) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 557, DateTimeKind.Local).AddTicks(2876), new DateTime(2023, 1, 26, 7, 53, 26, 557, DateTimeKind.Local).AddTicks(2895) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 557, DateTimeKind.Local).AddTicks(5666), new DateTime(2023, 1, 26, 7, 53, 26, 557, DateTimeKind.Local).AddTicks(5672) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2023, 1, 26, 7, 53, 26, 545, DateTimeKind.Local).AddTicks(205), new DateTime(2023, 1, 26, 7, 53, 26, 545, DateTimeKind.Local).AddTicks(219), "9277.55G81h8hrj3x2/H6JCTopQ==.ob9JDUJYfvjzygakuMZhMXc7Gqr07IGNbvtJ6p74TsM=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "fna_retirement_pension_funds");

            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "fna_investments");

            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "fna_insurance");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2022, 1, 21, 9, 50, 38, 619, DateTimeKind.Local).AddTicks(7110), new DateTime(2023, 1, 21, 9, 50, 38, 618, DateTimeKind.Local).AddTicks(9050), new DateTime(2023, 1, 21, 9, 50, 38, 619, DateTimeKind.Local).AddTicks(6341) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(7666), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(7684) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(8984), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(8989) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(8995), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(8996) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9007), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9008) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9011), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9012) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9018), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9019) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9023), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9024) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9027), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9028) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9031), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9032) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9036), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9037) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9040), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9041) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9044), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9045) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9048), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9049) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9051), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9052) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9055), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9056) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9059), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9063), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9064) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9068), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9069) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9072), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9073) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9076), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9077) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9080), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9081) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9084), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(9085) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 627, DateTimeKind.Local).AddTicks(8865), new DateTime(2023, 1, 21, 9, 50, 38, 627, DateTimeKind.Local).AddTicks(8882) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2431), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2438) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2445), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2447) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2450), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2451) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2455), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2456) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2462), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2464) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2467), new DateTime(2023, 1, 21, 9, 50, 38, 628, DateTimeKind.Local).AddTicks(2469) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 665, DateTimeKind.Local).AddTicks(9280), new DateTime(2023, 1, 21, 9, 50, 38, 665, DateTimeKind.Local).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 666, DateTimeKind.Local).AddTicks(3214), new DateTime(2023, 1, 21, 9, 50, 38, 666, DateTimeKind.Local).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 649, DateTimeKind.Local).AddTicks(1535), new DateTime(2023, 1, 21, 9, 50, 38, 649, DateTimeKind.Local).AddTicks(1548), "9660.hBukLlkY+3zhBd37w4jtjg==.vBayhSvVWxNaxNnFIuEt15TQ3+ozhvU2TYRptYmMWHA=" });
        }
    }
}
