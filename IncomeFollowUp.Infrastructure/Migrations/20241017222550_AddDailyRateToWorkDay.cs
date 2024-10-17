using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncomeFollowUp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyRateToWorkDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyRate",
                table: "WorkDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyRate",
                table: "WorkDays");
        }
    }
}
