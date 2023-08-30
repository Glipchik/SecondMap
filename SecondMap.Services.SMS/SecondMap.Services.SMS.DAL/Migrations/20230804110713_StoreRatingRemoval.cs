using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondMap.Services.StoreManagementService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class StoreRatingRemoval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Stores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Stores",
                type: "integer",
                nullable: true);
        }
    }
}
