using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatatzaakOfficeel.Migrations
{
    /// <inheritdoc />
    public partial class NinethMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orderitems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orderitems");
        }
    }
}
