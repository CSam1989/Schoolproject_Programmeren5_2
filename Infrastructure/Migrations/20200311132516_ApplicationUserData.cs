using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ApplicationUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DeviceCodes");

            migrationBuilder.DropTable(
                "PersistedGrants");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                "LoginProvider",
                "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                "ProviderKey",
                "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                "LoginProvider",
                "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "AspNetUserTokens",
                "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "LoginProvider",
                "AspNetUserTokens",
                "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "ProviderKey",
                "AspNetUserLogins",
                "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "LoginProvider",
                "AspNetUserLogins",
                "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                "DeviceCodes",
                table => new
                {
                    UserCode = table.Column<string>("nvarchar(200)", maxLength: 200),
                    ClientId = table.Column<string>("nvarchar(200)", maxLength: 200),
                    CreationTime = table.Column<DateTime>("datetime2"),
                    Data = table.Column<string>("nvarchar(max)", maxLength: 50000),
                    DeviceCode = table.Column<string>("nvarchar(200)", maxLength: 200),
                    Expiration = table.Column<DateTime>("datetime2"),
                    SubjectId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_DeviceCodes", x => x.UserCode); });

            migrationBuilder.CreateTable(
                "PersistedGrants",
                table => new
                {
                    Key = table.Column<string>("nvarchar(200)", maxLength: 200),
                    ClientId = table.Column<string>("nvarchar(200)", maxLength: 200),
                    CreationTime = table.Column<DateTime>("datetime2"),
                    Data = table.Column<string>("nvarchar(max)", maxLength: 50000),
                    Expiration = table.Column<DateTime>("datetime2", nullable: true),
                    SubjectId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>("nvarchar(50)", maxLength: 50)
                },
                constraints: table => { table.PrimaryKey("PK_PersistedGrants", x => x.Key); });

            migrationBuilder.CreateIndex(
                "IX_DeviceCodes_DeviceCode",
                "DeviceCodes",
                "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_DeviceCodes_Expiration",
                "DeviceCodes",
                "Expiration");

            migrationBuilder.CreateIndex(
                "IX_PersistedGrants_Expiration",
                "PersistedGrants",
                "Expiration");

            migrationBuilder.CreateIndex(
                "IX_PersistedGrants_SubjectId_ClientId_Type",
                "PersistedGrants",
                new[] {"SubjectId", "ClientId", "Type"});
        }
    }
}