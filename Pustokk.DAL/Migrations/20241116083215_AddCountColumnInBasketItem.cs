using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustokk.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCountColumnInBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "BasketItems");
        }
    }
}
