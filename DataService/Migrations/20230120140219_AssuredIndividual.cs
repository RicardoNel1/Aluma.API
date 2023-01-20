using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class AssuredIndividual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beneficiary",
                table: "fna_insurance",
                type: "varchar(100)",
                nullable: true);

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
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(1879), new DateTime(2023, 1, 20, 16, 2, 18, 260, DateTimeKind.Local).AddTicks(1895) });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beneficiary",
                table: "fna_insurance");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2022, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(8922), new DateTime(2023, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(1446), new DateTime(2023, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(8155) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(8491), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(8506) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9373), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9377) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9382), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9383) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9385), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9386) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9388), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9389) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9393), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9394) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9396), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9399), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9400) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9402), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9403) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9406), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9407) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9409), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9412), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9413) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9415), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9416) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9418), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9418) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9421), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9422) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9424), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9425) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9426), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9427) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9430), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9431) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9433), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9434) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9436), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9437) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9439), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9440) });

            migrationBuilder.UpdateData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9442), new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9443) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(2038), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(2052) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4097), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4107), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4108) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4111), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4112) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4114), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4115) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4120), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4121) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4124), new DateTime(2023, 1, 20, 9, 41, 23, 276, DateTimeKind.Local).AddTicks(4124) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 303, DateTimeKind.Local).AddTicks(4097), new DateTime(2023, 1, 20, 9, 41, 23, 303, DateTimeKind.Local).AddTicks(4112) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 303, DateTimeKind.Local).AddTicks(7647), new DateTime(2023, 1, 20, 9, 41, 23, 303, DateTimeKind.Local).AddTicks(7652) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2023, 1, 20, 9, 41, 23, 291, DateTimeKind.Local).AddTicks(1763), new DateTime(2023, 1, 20, 9, 41, 23, 291, DateTimeKind.Local).AddTicks(1772), "9688.99T7a/a/ylOQJ2OWDywQtg==.b+D8Mi8wtF1lAcKSvwRHnziU05WDxg35DcAxVYZ64pw=" });
        }
    }
}
