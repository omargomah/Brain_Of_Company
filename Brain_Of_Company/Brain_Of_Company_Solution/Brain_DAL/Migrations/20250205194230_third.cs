using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brain_DAL.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Employees_SSN",
                table: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Employees_SSN",
                table: "Admin",
                column: "SSN",
                principalTable: "Employees",
                principalColumn: "SSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Employees_SSN",
                table: "Admin");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Employees_SSN",
                table: "Admin",
                column: "SSN",
                principalTable: "Employees",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
