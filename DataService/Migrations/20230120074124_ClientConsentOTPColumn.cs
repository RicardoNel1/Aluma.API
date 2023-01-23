using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ClientConsentOTPColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OtpVerified",
                table: "client_consent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2022, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(8922), new DateTime(2023, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(1446), new DateTime(2023, 1, 20, 9, 41, 23, 268, DateTimeKind.Local).AddTicks(8155) });

            migrationBuilder.InsertData(
                table: "financial_providers",
                columns: new[] { "Id", "Code", "Created", "CreatedBy", "Modified", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "ABSA", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(8491), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(8506), 0, "Absa Life" },
                    { 22, "AMAS", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9442), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9443), 0, "Astute Medical Aid Service" },
                    { 21, "SLME", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9439), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9440), 0, "Sanlam Employee Benefit" },
                    { 20, "MOME", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9436), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9437), 0, "Momentum Employee Benefit" },
                    { 19, "FMI", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9433), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9434), 0, "FMI" },
                    { 18, "STLB", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9430), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9431), 0, "Stanlib" },
                    { 16, "SLM", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9424), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9425), 0, "Sanlam" },
                    { 15, "SET", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9421), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9422), 0, "Sanlam Collective Investments" },
                    { 14, "PPS", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9418), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9418), 0, "PPS" },
                    { 13, "OUT", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9415), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9416), 0, "Old Mutual Unit Trusts" },
                    { 17, "SLMNA", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9426), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9427), 0, "Sanlam Namibia" },
                    { 11, "OMGP", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9409), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9410), 0, "Galaxy Portfolio Services" },
                    { 2, "AG", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9373), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9377), 0, "Allan Gray" },
                    { 12, "OMU", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9412), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9413), 0, "Old Mutual" },
                    { 3, "ALT", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9382), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9383), 0, "Altrisk" },
                    { 5, "DSI", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9388), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9389), 0, "Discovery Invest" },
                    { 6, "DSL", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9393), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9394), 0, "Discovery Life" },
                    { 4, "CHT", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9385), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9386), 0, "Liberty Active" },
                    { 8, "MOM", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9399), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9400), 0, "Momentum" },
                    { 9, "MOMW", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9402), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9403), 0, "Momentum Wealth" },
                    { 10, "NGL", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9406), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9407), 0, "Nedgroup Life" },
                    { 7, "LIB", new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9396), 0, new DateTime(2023, 1, 20, 9, 41, 23, 287, DateTimeKind.Local).AddTicks(9397), 0, "Liberty Life" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "financial_providers",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DropColumn(
                name: "OtpVerified",
                table: "client_consent");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 11, 24, 16, 49, 11, 390, DateTimeKind.Local).AddTicks(8228), new DateTime(2022, 11, 24, 16, 49, 11, 389, DateTimeKind.Local).AddTicks(8430), new DateTime(2022, 11, 24, 16, 49, 11, 390, DateTimeKind.Local).AddTicks(7415) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(7140), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(7156) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9329), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9334) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9339), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9343), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9347), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9348) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9353), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9354) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9357), new DateTime(2022, 11, 24, 16, 49, 11, 398, DateTimeKind.Local).AddTicks(9358) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(5040), new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(5058) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(7918), new DateTime(2022, 11, 24, 16, 49, 11, 426, DateTimeKind.Local).AddTicks(7922) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 11, 24, 16, 49, 11, 414, DateTimeKind.Local).AddTicks(3846), new DateTime(2022, 11, 24, 16, 49, 11, 414, DateTimeKind.Local).AddTicks(3868), "9394.YCB4KPgMwc0vOYaT++ZiFg==.ZKVFBU+x4UrQsID5JJ8jwltV2NRF3cHjLQz33B2yatQ=" });
        }
    }
}
