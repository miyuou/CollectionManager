using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection_0._1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveObsoleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SourceSite",
                table: "Item");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceSite",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
