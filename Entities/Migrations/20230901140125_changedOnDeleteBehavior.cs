using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinesApi.Migrations
{
    /// <inheritdoc />
    public partial class changedOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mines_MineTypes_MineTypeRefId",
                table: "Mines");

            migrationBuilder.DropForeignKey(
                name: "FK_Mines_OwnershipTypes_OwnershipTypeRefId",
                table: "Mines");

            migrationBuilder.DropForeignKey(
                name: "FK_Mines_Statuses_StatusRefId",
                table: "Mines");

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_MineTypes_MineTypeRefId",
                table: "Mines",
                column: "MineTypeRefId",
                principalTable: "MineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_OwnershipTypes_OwnershipTypeRefId",
                table: "Mines",
                column: "OwnershipTypeRefId",
                principalTable: "OwnershipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_Statuses_StatusRefId",
                table: "Mines",
                column: "StatusRefId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mines_MineTypes_MineTypeRefId",
                table: "Mines");

            migrationBuilder.DropForeignKey(
                name: "FK_Mines_OwnershipTypes_OwnershipTypeRefId",
                table: "Mines");

            migrationBuilder.DropForeignKey(
                name: "FK_Mines_Statuses_StatusRefId",
                table: "Mines");

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_MineTypes_MineTypeRefId",
                table: "Mines",
                column: "MineTypeRefId",
                principalTable: "MineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_OwnershipTypes_OwnershipTypeRefId",
                table: "Mines",
                column: "OwnershipTypeRefId",
                principalTable: "OwnershipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mines_Statuses_StatusRefId",
                table: "Mines",
                column: "StatusRefId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
