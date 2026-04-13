using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class CreateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fore_Names = table.Column<string>(type: "text", nullable: false),
                    Sur_Names = table.Column<string>(type: "text", nullable: false),
                    DataOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Mobile_phone = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Address_2 = table.Column<string>(type: "text", nullable: false),
                    Post_Code = table.Column<string>(type: "text", nullable: false),
                    Email_Home = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
