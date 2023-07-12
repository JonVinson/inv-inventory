using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace inventory_data.Migrations
{
    /// <inheritdoc />
    public partial class INV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManufacturerCode",
                table: "Products",
                newName: "ModelNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelNumber",
                table: "Products",
                newName: "ManufacturerCode");
        }
    }
}
