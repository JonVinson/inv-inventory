using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_data.Migrations
{
    /// <inheritdoc />
    public partial class INV19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Manufacturers",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Manufacturers",
                newName: "Phone");
        }
    }
}
