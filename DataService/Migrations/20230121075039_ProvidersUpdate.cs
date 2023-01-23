using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ProvidersUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "Created", "Modified", "Name" },
                values: new object[] { new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(7666), new DateTime(2023, 1, 21, 9, 50, 38, 644, DateTimeKind.Local).AddTicks(7684), "ABSA Life" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2022, 1, 20, 16, 2, 18, 240, DateTimeKind.Local).AddTicks(7212), new DateTime(2023, 1, 20, 16, 2, 18, 239, DateTimeKind.Local).AddTicks(9503), new DateTime(2023, 1, 20, 16, 2, 18, 240, DateTimeKind.Local).AddTicks(6429) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Name" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(1879), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(1895), "Absa Life" });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2870), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2874) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2879), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2883), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2884) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2886), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2886) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2891), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2892) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2895), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2895) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2898), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2899) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2901), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2901) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2905), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2906) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2908), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2909) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2910), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2911) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2913), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2914) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2916), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2917) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2919), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2922), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2923) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2925), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2926) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2929), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2932), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2933) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2936), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2936) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2938), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2939) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2941), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(2942) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(1831), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(1843) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3871), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3876) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3882), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3883) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3885), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3886) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3888), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3889) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3894), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3895) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3898), new DateTime(2023, 1, 20, 16, 2, 18, 248, DateTimeKind.Local).AddTicks(3899) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 274, DateTimeKind.Local).AddTicks(6319), new DateTime(2023, 1, 20, 16, 2, 18, 274, DateTimeKind.Local).AddTicks(6337) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 274, DateTimeKind.Local).AddTicks(9106), new DateTime(2023, 1, 20, 16, 2, 18, 274, DateTimeKind.Local).AddTicks(9112) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 263, DateTimeKind.Local).AddTicks(4237), new DateTime(2023, 1, 20, 16, 2, 18, 263, DateTimeKind.Local).AddTicks(4248), "9003.QHB1ZzBX+1xNoluhYiftAQ==.bI+QHGPID6W4wScNF7rAdBYF8H8EKucvPdAy72+ZZEs=" });
        }
    }
}
