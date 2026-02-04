using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRpraca.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "WMS",
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                schema: "WMS",
                table: "User",
                columns: new[] { "UserId", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "RoleId" },
                values: new object[] { 1, "admin@wms.local", "System", true, "Administrator", "$2a$11$1InRDSTNIMUJZXTBdGaM6uBwJtIb3wRSGywJak6t7eX.DG0O8sPl2", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "WMS",
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "WMS",
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1);
        }
    }
}
