using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class Period : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "Budgets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "BudgetItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Days = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_PeriodId",
                table: "Budgets",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_PeriodId",
                table: "BudgetItems",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetItems_Periods_PeriodId",
                table: "BudgetItems",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Periods_PeriodId",
                table: "Budgets",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetItems_Periods_PeriodId",
                table: "BudgetItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Periods_PeriodId",
                table: "Budgets");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_PeriodId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_PeriodId",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "BudgetItems");
        }
    }
}
