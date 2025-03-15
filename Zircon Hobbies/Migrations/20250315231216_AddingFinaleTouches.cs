using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zircon_Hobbies.Migrations
{
    /// <inheritdoc />
    public partial class AddingFinaleTouches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gunpla_Production_Company_Production_CompanyId",
                table: "Gunpla");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_Gunpla_Production_CompanyId",
                table: "Gunpla");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Production_CompanyId",
                table: "Gunpla");

            migrationBuilder.CreateIndex(
                name: "IX_Gunpla_ProductionCompanyId",
                table: "Gunpla",
                column: "ProductionCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gunpla_Production_Company_ProductionCompanyId",
                table: "Gunpla",
                column: "ProductionCompanyId",
                principalTable: "Production_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gunpla_Production_Company_ProductionCompanyId",
                table: "Gunpla");

            migrationBuilder.DropIndex(
                name: "IX_Gunpla_ProductionCompanyId",
                table: "Gunpla");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Production_CompanyId",
                table: "Gunpla",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCart_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id");
        }
    }
}
