using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.DataHandle.Migrations
{
    public partial class V3_alpha4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "CreatedApplicationType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "BotFeedback",
                newName: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneCountryCode",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8)
                .OldAnnotation("Relational:ColumnOrder", 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "BotFeedback",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneCountryCode",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<int>(
                name: "CreatedApplicationType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
