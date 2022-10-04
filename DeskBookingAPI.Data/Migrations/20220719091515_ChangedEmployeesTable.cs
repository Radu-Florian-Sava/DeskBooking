using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBookingAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "EmployeeRoleId",
                table: "Employees",
                type: "integer",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE public.\"Employees\" e SET \"EmployeeRoleId\"= " +
                "(SELECT er.\"Id\" FROM public.\"EmployeeRoles\" er" +
                "        WHERE LOWER(er.\"Name\")=LOWER(e.\"Role\") " +
                ");"
                );

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeRoleId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeRoleId",
                table: "Employees",
                column: "EmployeeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeRoles_EmployeeRoleId",
                table: "Employees",
                column: "EmployeeRoleId",
                principalTable: "EmployeeRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeRoles_EmployeeRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeRoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeRoleId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
