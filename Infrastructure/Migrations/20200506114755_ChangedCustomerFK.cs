using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangedCustomerFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Customers_AspNetUsers_UserId",
                "Customers");

            migrationBuilder.DropIndex(
                "IX_Customers_UserId",
                "Customers");

            migrationBuilder.AlterColumn<string>(
                "UserId",
                "Customers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddColumn<int>(
                "CustomerId",
                "AspNetUsers",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_CustomerId",
                "AspNetUsers",
                "CustomerId");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_Customers_CustomerId",
                "AspNetUsers",
                "CustomerId",
                "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_Customers_CustomerId",
                "AspNetUsers");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_CustomerId",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "CustomerId",
                "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                "UserId",
                "Customers",
                "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                "IX_Customers_UserId",
                "Customers",
                "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                "FK_Customers_AspNetUsers_UserId",
                "Customers",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}