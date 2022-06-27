using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class ExtendVarCharMaxExceptionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisagreeReason",
                table: "client_risk_profile",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdvisorNotes",
                table: "client_risk_profile",
                type: "varchar(MAX)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 27, 13, 40, 40, 241, DateTimeKind.Local).AddTicks(6872), new DateTime(2022, 6, 27, 13, 40, 40, 239, DateTimeKind.Local).AddTicks(8123), new DateTime(2022, 6, 27, 13, 40, 40, 241, DateTimeKind.Local).AddTicks(4236) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 264, DateTimeKind.Local).AddTicks(5293), new DateTime(2022, 6, 27, 13, 40, 40, 264, DateTimeKind.Local).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1289), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1320), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1323) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1331), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1333) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1341), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1343) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1361), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1363) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1371), new DateTime(2022, 6, 27, 13, 40, 40, 265, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 346, DateTimeKind.Local).AddTicks(5981), new DateTime(2022, 6, 27, 13, 40, 40, 346, DateTimeKind.Local).AddTicks(6045) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 347, DateTimeKind.Local).AddTicks(3583), new DateTime(2022, 6, 27, 13, 40, 40, 347, DateTimeKind.Local).AddTicks(3614) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 27, 13, 40, 40, 309, DateTimeKind.Local).AddTicks(7328), new DateTime(2022, 6, 27, 13, 40, 40, 309, DateTimeKind.Local).AddTicks(7374), "9757.BDWPSt9f/OqftHW+zb2Bnw==.34gJQv9JCT1DuoBPppJ660VZrzcLiQur+9ehnHqmlF4=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisagreeReason",
                table: "client_risk_profile",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdvisorNotes",
                table: "client_risk_profile",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 22, 12, 32, 47, 43, DateTimeKind.Local).AddTicks(336), new DateTime(2022, 6, 22, 12, 32, 47, 42, DateTimeKind.Local).AddTicks(2316), new DateTime(2022, 6, 22, 12, 32, 47, 42, DateTimeKind.Local).AddTicks(9461) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 49, DateTimeKind.Local).AddTicks(9132), new DateTime(2022, 6, 22, 12, 32, 47, 49, DateTimeKind.Local).AddTicks(9143) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1168), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1172) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1177), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1178) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1180), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1181) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1184), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1184) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1190), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1190) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1193), new DateTime(2022, 6, 22, 12, 32, 47, 50, DateTimeKind.Local).AddTicks(1194) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 73, DateTimeKind.Local).AddTicks(9826), new DateTime(2022, 6, 22, 12, 32, 47, 73, DateTimeKind.Local).AddTicks(9844) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 74, DateTimeKind.Local).AddTicks(2743), new DateTime(2022, 6, 22, 12, 32, 47, 74, DateTimeKind.Local).AddTicks(2749) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 22, 12, 32, 47, 62, DateTimeKind.Local).AddTicks(9470), new DateTime(2022, 6, 22, 12, 32, 47, 62, DateTimeKind.Local).AddTicks(9486), "9382.sgKiFz77QYfg2BDOIyr7rQ==.pEN83zYomZ5/rlLRoNPwD0Ntgxndv4RWm1OYF/CvlYI=" });
        }
    }
}
