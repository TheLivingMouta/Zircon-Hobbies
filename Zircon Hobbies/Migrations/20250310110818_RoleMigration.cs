using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zircon_Hobbies.Migrations
{
    /// <inheritdoc />
    public partial class RoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "scale",
                table: "Gunpla",
                newName: "Scale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Scale",
                table: "Gunpla",
                newName: "scale");
        }
    }
}
