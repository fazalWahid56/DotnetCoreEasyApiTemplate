using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreTemplate.App.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountNature",
                columns: table => new
                {
                    AccountNatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNature", x => x.AccountNatureId);
                });

            migrationBuilder.CreateTable(
                name: "Firm",
                columns: table => new
                {
                    FirmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.FirmId);
                });

            migrationBuilder.CreateTable(
                name: "VoucherTypes",
                columns: table => new
                {
                    VoucherTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTypes", x => x.VoucherTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Desciption = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Year = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VoucherTypeId = table.Column<int>(type: "int", nullable: false),
                    FirmId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherId);
                    table.ForeignKey(
                        name: "FK_Vouchers_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "FirmId");
                    table.ForeignKey(
                        name: "FK_Vouchers_VoucherTypes_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherTypes",
                        principalColumn: "VoucherTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTittle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AccountDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountNatureId = table.Column<int>(type: "int", nullable: false),
                    FirmId = table.Column<int>(type: "int", nullable: true),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_AccountNature_AccountNatureId",
                        column: x => x.AccountNatureId,
                        principalTable: "AccountNature",
                        principalColumn: "AccountNatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "FirmId");
                    table.ForeignKey(
                        name: "FK_ChartOfAccounts_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherId");
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedger",
                columns: table => new
                {
                    TransectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DrAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CrAmmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgainstAccountId = table.Column<int>(type: "int", nullable: true),
                    FromAccountId = table.Column<int>(type: "int", nullable: true),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    FirmId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedger", x => x.TransectionId);
                    table.ForeignKey(
                        name: "FK_GeneralLedger_ChartOfAccounts_AgainstAccountId",
                        column: x => x.AgainstAccountId,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_GeneralLedger_ChartOfAccounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "ChartOfAccounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_GeneralLedger_Firm_FirmId",
                        column: x => x.FirmId,
                        principalTable: "Firm",
                        principalColumn: "FirmId");
                    table.ForeignKey(
                        name: "FK_GeneralLedger_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_AccountNatureId",
                table: "ChartOfAccounts",
                column: "AccountNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_FirmId",
                table: "ChartOfAccounts",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_VoucherId",
                table: "ChartOfAccounts",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedger_AgainstAccountId",
                table: "GeneralLedger",
                column: "AgainstAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedger_FirmId",
                table: "GeneralLedger",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedger_FromAccountId",
                table: "GeneralLedger",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedger_VoucherId",
                table: "GeneralLedger",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_FirmId",
                table: "Vouchers",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_VoucherTypeId",
                table: "Vouchers",
                column: "VoucherTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralLedger");

            migrationBuilder.DropTable(
                name: "ChartOfAccounts");

            migrationBuilder.DropTable(
                name: "AccountNature");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "Firm");

            migrationBuilder.DropTable(
                name: "VoucherTypes");
        }
    }
}
