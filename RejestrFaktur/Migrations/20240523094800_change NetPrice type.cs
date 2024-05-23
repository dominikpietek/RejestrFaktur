using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RejestrFaktur.Migrations
{
    /// <inheritdoc />
    public partial class changeNetPricetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NetPrice",
                table: "ItemModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NetPrice",
                table: "ItemModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
