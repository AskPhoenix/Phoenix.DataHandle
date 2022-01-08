using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoenix.DataHandle.Migrations
{
    public partial class RenameDeletablesToObviables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "School");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "School",
                newName: "ObviatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Schedule",
                newName: "ObviatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Course",
                newName: "ObviatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Classroom",
                newName: "ObviatedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "AspNetUsers",
                newName: "ObviatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsObviated",
                table: "School",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObviated",
                table: "Schedule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObviated",
                table: "Course",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObviated",
                table: "Classroom",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsObviated",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsObviated",
                table: "School");

            migrationBuilder.DropColumn(
                name: "IsObviated",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "IsObviated",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "IsObviated",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "IsObviated",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ObviatedAt",
                table: "School",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "ObviatedAt",
                table: "Schedule",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "ObviatedAt",
                table: "Course",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "ObviatedAt",
                table: "Classroom",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "ObviatedAt",
                table: "AspNetUsers",
                newName: "DeletedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "School",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Schedule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Classroom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
