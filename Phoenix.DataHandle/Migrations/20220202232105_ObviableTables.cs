using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoenix.DataHandle.Migrations
{
    public partial class ObviableTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "School",
                type: "datetimeoffset(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Schedule",
                type: "datetimeoffset(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Course",
                type: "datetimeoffset(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Classroom",
                type: "datetimeoffset(0)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObviatedAt",
                table: "School");

            migrationBuilder.DropColumn(
                name: "ObviatedAt",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "ObviatedAt",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ObviatedAt",
                table: "Classroom");

            migrationBuilder.DropColumn(
                name: "ObviatedAt",
                table: "AspNetUsers");
        }
    }
}
