using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QEnergy.Services.Persistence.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Projects");
        }
    }
}
