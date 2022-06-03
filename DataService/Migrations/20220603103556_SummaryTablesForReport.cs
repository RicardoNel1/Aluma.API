using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataService.Migrations
{
    public partial class SummaryTablesForReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Needs_GrossAnnualSalary",
                table: "fna_providing_on_dread_disease",
                newName: "TotalDreadDisease");

            migrationBuilder.AddColumn<double>(
                name: "IncomeNeedsTotal",
                table: "fna_retirement_planning",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Needs_GrossAnnualSalaryMultiple",
                table: "fna_providing_on_dread_disease",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Needs_GrossAnnualSalaryTotal",
                table: "fna_providing_on_dread_disease",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "RetirementAge",
                table: "fna_assumptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "CurrentNetIncome",
                table: "fna_assumptions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LifeExpentancy",
                table: "fna_assumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsAfterRetirement",
                table: "fna_assumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsTillLifeExpentancy",
                table: "fna_assumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearsTillRetirement",
                table: "fna_assumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "fna_summary_assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    TotalAssetsAttractingCGT = table.Column<double>(type: "float", nullable: false),
                    TotalAssetsExcemptCGT = table.Column<double>(type: "float", nullable: false),
                    TotalLiquidAssets = table.Column<double>(type: "float", nullable: false),
                    TotalAccrual = table.Column<double>(type: "float", nullable: false),
                    TotalLiabilities = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_summary_assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_summary_assets_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_summary_insurance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    TotalToSpouse = table.Column<double>(type: "float", nullable: false),
                    TotalToThirdParty = table.Column<double>(type: "float", nullable: false),
                    TotalToLiquidity = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_summary_insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_summary_insurance_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_summary_providing_death",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    TotalNeeds = table.Column<double>(type: "float", nullable: false),
                    TotalAvailable = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_summary_providing_death", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_summary_providing_death_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_summary_providing_disability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    TotalIncomeNeed = table.Column<double>(type: "float", nullable: false),
                    TotalNeeds = table.Column<double>(type: "float", nullable: false),
                    TotalAvailable = table.Column<double>(type: "float", nullable: false),
                    TotalExistingShortTermIncome = table.Column<double>(type: "float", nullable: false),
                    TotalExistingLongTermIncome = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_summary_providing_disability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_summary_providing_disability_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fna_summary_retirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FNAId = table.Column<int>(type: "int", nullable: false),
                    TotalPensionFund = table.Column<double>(type: "float", nullable: false),
                    TotalPreservation = table.Column<double>(type: "float", nullable: false),
                    TotalNeeds = table.Column<double>(type: "float", nullable: false),
                    TotalAvailable = table.Column<double>(type: "float", nullable: false),
                    SavingsRequiredPremium = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fna_summary_retirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fna_summary_retirement_client_fna_FNAId",
                        column: x => x.FNAId,
                        principalTable: "client_fna",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 6, 3, 12, 35, 54, 250, DateTimeKind.Local).AddTicks(2751), new DateTime(2022, 6, 3, 12, 35, 54, 248, DateTimeKind.Local).AddTicks(4519), new DateTime(2022, 6, 3, 12, 35, 54, 250, DateTimeKind.Local).AddTicks(696) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 269, DateTimeKind.Local).AddTicks(9228), new DateTime(2022, 6, 3, 12, 35, 54, 269, DateTimeKind.Local).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4561), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4577) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4589), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4591) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4596), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4598) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4604), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4606) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4622), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4631), new DateTime(2022, 6, 3, 12, 35, 54, 270, DateTimeKind.Local).AddTicks(4633) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 324, DateTimeKind.Local).AddTicks(8909), new DateTime(2022, 6, 3, 12, 35, 54, 324, DateTimeKind.Local).AddTicks(8956) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 325, DateTimeKind.Local).AddTicks(3835), new DateTime(2022, 6, 3, 12, 35, 54, 325, DateTimeKind.Local).AddTicks(3843) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 6, 3, 12, 35, 54, 304, DateTimeKind.Local).AddTicks(9979), new DateTime(2022, 6, 3, 12, 35, 54, 305, DateTimeKind.Local).AddTicks(19), "9047.TvxOXROXKTW9colVBzDiNA==.0SansH9IuLHNIK/uRge8Hswy5vD1kxnIF1tSVq9Buh8=" });

            migrationBuilder.CreateIndex(
                name: "IX_fna_summary_assets_FNAId",
                table: "fna_summary_assets",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_summary_insurance_FNAId",
                table: "fna_summary_insurance",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_summary_providing_death_FNAId",
                table: "fna_summary_providing_death",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_summary_providing_disability_FNAId",
                table: "fna_summary_providing_disability",
                column: "FNAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_fna_summary_retirement_FNAId",
                table: "fna_summary_retirement",
                column: "FNAId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fna_summary_assets");

            migrationBuilder.DropTable(
                name: "fna_summary_insurance");

            migrationBuilder.DropTable(
                name: "fna_summary_providing_death");

            migrationBuilder.DropTable(
                name: "fna_summary_providing_disability");

            migrationBuilder.DropTable(
                name: "fna_summary_retirement");

            migrationBuilder.DropColumn(
                name: "IncomeNeedsTotal",
                table: "fna_retirement_planning");

            migrationBuilder.DropColumn(
                name: "Needs_GrossAnnualSalaryMultiple",
                table: "fna_providing_on_dread_disease");

            migrationBuilder.DropColumn(
                name: "Needs_GrossAnnualSalaryTotal",
                table: "fna_providing_on_dread_disease");

            migrationBuilder.DropColumn(
                name: "CurrentNetIncome",
                table: "fna_assumptions");

            migrationBuilder.DropColumn(
                name: "LifeExpentancy",
                table: "fna_assumptions");

            migrationBuilder.DropColumn(
                name: "YearsAfterRetirement",
                table: "fna_assumptions");

            migrationBuilder.DropColumn(
                name: "YearsTillLifeExpentancy",
                table: "fna_assumptions");

            migrationBuilder.DropColumn(
                name: "YearsTillRetirement",
                table: "fna_assumptions");

            migrationBuilder.RenameColumn(
                name: "TotalDreadDisease",
                table: "fna_providing_on_dread_disease",
                newName: "Needs_GrossAnnualSalary");

            migrationBuilder.AlterColumn<double>(
                name: "RetirementAge",
                table: "fna_assumptions",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "advisors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppointmentDate", "Created", "Modified" },
                values: new object[] { new DateTime(2021, 5, 30, 13, 40, 58, 346, DateTimeKind.Local).AddTicks(6317), new DateTime(2022, 5, 30, 13, 40, 58, 345, DateTimeKind.Local).AddTicks(6007), new DateTime(2022, 5, 30, 13, 40, 58, 346, DateTimeKind.Local).AddTicks(5097) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(6981), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(6999) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8931), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8938) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8943), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8944) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8947), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8947) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8949), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8955), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8955) });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8958), new DateTime(2022, 5, 30, 13, 40, 58, 353, DateTimeKind.Local).AddTicks(8958) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(6254), new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(6269) });

            migrationBuilder.UpdateData(
                table: "user_addresses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(9803), new DateTime(2022, 5, 30, 13, 40, 58, 377, DateTimeKind.Local).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified", "Password" },
                values: new object[] { new DateTime(2022, 5, 30, 13, 40, 58, 367, DateTimeKind.Local).AddTicks(1925), new DateTime(2022, 5, 30, 13, 40, 58, 367, DateTimeKind.Local).AddTicks(1941), "9766.w1JWlG0lU0CItOlYd6jTvQ==.FvJkiOKpMTYE+dnbLvkv07ppvHUV3AOHkjqS1j2YY+k=" });
        }
    }
}
