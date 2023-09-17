using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinesApi.Migrations
{
    /// <inheritdoc />
    public partial class createdAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnershipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnershipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counties_Provinces_ProvinceRefId",
                        column: x => x.ProvinceRefId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnershipTypeRefId = table.Column<int>(type: "int", nullable: false),
                    ProvinceRefId = table.Column<int>(type: "int", nullable: false),
                    CountyRefId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeoghraphicPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvestmentAmount = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<short>(type: "smallint", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    MineTypeRefId = table.Column<int>(type: "int", nullable: false),
                    StatusRefId = table.Column<int>(type: "int", nullable: false),
                    EmploymentCommitment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mines_Counties_CountyRefId",
                        column: x => x.CountyRefId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mines_MineTypes_MineTypeRefId",
                        column: x => x.MineTypeRefId,
                        principalTable: "MineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mines_OwnershipTypes_OwnershipTypeRefId",
                        column: x => x.OwnershipTypeRefId,
                        principalTable: "OwnershipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mines_Provinces_ProvinceRefId",
                        column: x => x.ProvinceRefId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mines_Statuses_StatusRefId",
                        column: x => x.StatusRefId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    NumberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumTypeRefId = table.Column<int>(type: "int", nullable: false),
                    MineRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.NumberId);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Mines_MineRefId",
                        column: x => x.MineRefId,
                        principalTable: "Mines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhoneNumTypes_PhoneNumTypeRefId",
                        column: x => x.PhoneNumTypeRefId,
                        principalTable: "PhoneNumTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Counties_ProvinceRefId",
                table: "Counties",
                column: "ProvinceRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mines_CountyRefId",
                table: "Mines",
                column: "CountyRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mines_MineTypeRefId",
                table: "Mines",
                column: "MineTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mines_OwnershipTypeRefId",
                table: "Mines",
                column: "OwnershipTypeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mines_ProvinceRefId",
                table: "Mines",
                column: "ProvinceRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Mines_StatusRefId",
                table: "Mines",
                column: "StatusRefId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_MineRefId",
                table: "PhoneNumbers",
                column: "MineRefId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PhoneNumTypeRefId",
                table: "PhoneNumbers",
                column: "PhoneNumTypeRefId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Mines");

            migrationBuilder.DropTable(
                name: "PhoneNumTypes");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "MineTypes");

            migrationBuilder.DropTable(
                name: "OwnershipTypes");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
