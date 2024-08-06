using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class addAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'aaa06b01-bb3e-4702-8062-c13c811c5467', N'anas.elhorigy@gmail.com', N'ANAS.ELHORIGY@GMAIL.COM', N'anas.elhorigy@gmail.com', N'ANAS.ELHORIGY@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEBvIp5o7EQTm/y8XiOaJcegHR8AohbaAGYtImeY1hAMKsExvczZqjyeFHh4s1MsOOg==', N'ND3EZFK4M36CWKZ3MGXVS5H4JPUVYJQ2', N'f268342e-081d-4c00-9260-6d3c24599235', NULL, 0, 0, NULL, 1, 0, N'Anas', N'Al-Horigy')\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[users] WHERE Id ='aaa06b01-bb3e-4702-8062-c13c811c5467' ");
        }
    }
}
