using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Db.Migrations
{
    public partial class FirmTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "Vouchers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "GeneralLedger",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirmId",
                table: "ChartOfAccounts",
                type: "int",
                nullable: true);

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
                    CreatedBy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.FirmId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_FirmId",
                table: "Vouchers",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralLedger_FirmId",
                table: "GeneralLedger",
                column: "FirmId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccounts_FirmId",
                table: "ChartOfAccounts",
                column: "FirmId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartOfAccounts_Firm_FirmId",
                table: "ChartOfAccounts",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "FirmId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralLedger_Firm_FirmId",
                table: "GeneralLedger",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "FirmId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Firm_FirmId",
                table: "Vouchers",
                column: "FirmId",
                principalTable: "Firm",
                principalColumn: "FirmId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartOfAccounts_Firm_FirmId",
                table: "ChartOfAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GeneralLedger_Firm_FirmId",
                table: "GeneralLedger");

            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Firm_FirmId",
                table: "Vouchers");

            migrationBuilder.DropTable(
                name: "Firm");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_FirmId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_GeneralLedger_FirmId",
                table: "GeneralLedger");

            migrationBuilder.DropIndex(
                name: "IX_ChartOfAccounts_FirmId",
                table: "ChartOfAccounts");

            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "GeneralLedger");

            migrationBuilder.DropColumn(
                name: "FirmId",
                table: "ChartOfAccounts");
        }
    }
}
