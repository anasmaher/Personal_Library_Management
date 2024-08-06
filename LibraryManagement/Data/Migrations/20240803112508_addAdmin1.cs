using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[UserRoles] (UserId, RoleId) SELECT 'aaa06b01-bb3e-4702-8062-c13c811c5467', Id FROM [Security].[Role]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId = 'aaa06b01-bb3e-4702-8062-c13c811c5467'");
        }
    }
}
