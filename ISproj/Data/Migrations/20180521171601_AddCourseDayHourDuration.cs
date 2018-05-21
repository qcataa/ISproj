using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ISproj.Data.Migrations
{
    public partial class AddCourseDayHourDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "CourseModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "CourseModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "CourseModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "CourseModel");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "CourseModel");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "CourseModel");
        }
    }
}
