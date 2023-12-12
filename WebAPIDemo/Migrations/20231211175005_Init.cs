using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shirts",
                columns: table => new
                {
                    ShirtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shirts", x => x.ShirtId);
                });

            migrationBuilder.InsertData(
                table: "Shirts",
                columns: new[] { "ShirtId", "Brand", "Color", "Gender", "Price", "Size" },
                values: new object[,]
                {
                    { 1, "My Brand", "Blue", "Man", 30.0, 10 },
                    { 2, "My Brand1", "Blue1", "Man1", 130.0, 110 },
                    { 3, "My Brand2", "Blue2", "Man2", 230.0, 210 },
                    { 4, "My Brand3", "Blue3", "Man3", 330.0, 310 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shirts");
        }
    }
}
