using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mohammed_hazim.Migrations
{
    /// <inheritdoc />
    public partial class FirstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OwnerName = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerSurename = table.Column<string>(type: "TEXT", nullable: false),
                    CarPlateNum = table.Column<string>(type: "TEXT", nullable: false),
                    CarBrandName = table.Column<string>(type: "TEXT", nullable: false),
                    CarModel = table.Column<int>(type: "INTEGER", nullable: false),
                    ChassisNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
