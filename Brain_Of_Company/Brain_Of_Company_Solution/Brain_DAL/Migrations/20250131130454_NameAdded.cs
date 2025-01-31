using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brain_DAL.Migrations
{
    /// <inheritdoc />
    public partial class NameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "Decimal(18,0)", nullable: false),
                    DateOfAdd = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DateOfDelete = table.Column<DateTime>(type: "DateTime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false),
                    SoldQuantities = table.Column<int>(type: "int", nullable: false),
                    RealQuantities = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfSale = table.Column<DateTime>(type: "DateTime", nullable: false),
                    Price = table.Column<decimal>(type: "Decimal(18,0)", nullable: false),
                    Isdeleted = table.Column<bool>(type: "BIT", nullable: false),
                    Quantities = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfDay = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IsAttended = table.Column<bool>(type: "BIT", nullable: false),
                    EmployeeSSN = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerSSN = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "varchar", nullable: false),
                    Location = table.Column<string>(type: "varchar", nullable: false),
                    MinimumDaysToAttendancePerMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DateOfHiring = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DateOfFiring = table.Column<DateTime>(type: "DateTime", nullable: false),
                    SalaryPerDay = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Isdeleted = table.Column<bool>(type: "BIT", nullable: false),
                    Phone = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    PercentageOfBonus = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Employees_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependent_Employee",
                columns: table => new
                {
                    DependentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeSSN = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependent_Employee", x => new { x.EmployeeSSN, x.DependentId });
                    table.ForeignKey(
                        name: "FK_Dependent_Employee_Dependent_DependentId",
                        column: x => x.DependentId,
                        principalTable: "Dependent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependent_Employee_Employees_EmployeeSSN",
                        column: x => x.EmployeeSSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeSSN",
                table: "Attendance",
                column: "EmployeeSSN");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerSSN",
                table: "Department",
                column: "ManagerSSN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dependent_Employee_DependentId",
                table: "Dependent_Employee",
                column: "DependentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ProductId",
                table: "Invoice",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employees_EmployeeSSN",
                table: "Attendance",
                column: "EmployeeSSN",
                principalTable: "Employees",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Employees_ManagerSSN",
                table: "Department",
                column: "ManagerSSN",
                principalTable: "Employees",
                principalColumn: "SSN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Employees_ManagerSSN",
                table: "Department");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Dependent_Employee");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Dependent");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
