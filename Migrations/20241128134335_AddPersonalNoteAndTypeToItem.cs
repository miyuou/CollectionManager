using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCollection_0._1.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonalNoteAndTypeToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonalNote",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalNote",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Item");
        }
    }
}
