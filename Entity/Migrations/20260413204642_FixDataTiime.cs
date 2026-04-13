using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class FixDataTiime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
    name: "DataOfBirth",
    table: "Employees",
    type: "timestamp without time zone",
    nullable: false,
    oldClrType: typeof(DateTime),
    oldType: "timestamp with time zone");
            migrationBuilder.AlterColumn<DateTime>(
    name: "StartDate",
    table: "Employees",
    type: "timestamp without time zone",
    nullable: false,
    oldClrType: typeof(DateTime),
    oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
