using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mns_api.Migrations
{
    /// <inheritdoc />
    public partial class AddInStockResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinStockQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinStockQuantity",
                table: "Products");
        }
    }
}
