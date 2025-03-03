using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zircon_Hobbies.Migrations
{
    /// <inheritdoc />
    public partial class AddProdComp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Production_CompanyId",
                table: "Gunpla",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Production_Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gunpla_Production_CompanyId",
                table: "Gunpla",
                column: "Production_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gunpla_Production_Company_Production_CompanyId",
                table: "Gunpla",
                column: "Production_CompanyId",
                principalTable: "Production_Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gunpla_Production_Company_Production_CompanyId",
                table: "Gunpla");

            migrationBuilder.DropTable(
                name: "Production_Company");

            migrationBuilder.DropIndex(
                name: "IX_Gunpla_Production_CompanyId",
                table: "Gunpla");

            migrationBuilder.DropColumn(
                name: "Production_CompanyId",
                table: "Gunpla");
        }
    }
}
