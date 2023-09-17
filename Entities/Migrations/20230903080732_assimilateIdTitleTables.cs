using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinesApi.Migrations
{
    /// <inheritdoc />
    public partial class assimilateIdTitleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Provinces",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Counties",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Provinces",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Counties",
                newName: "Name");
        }
    }
}
