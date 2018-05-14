using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ISproj.Data.Migrations
{
    public partial class AddCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherViewModelCNP",
                table: "CourseModel");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CourseModel",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TeacherViewModelCNP",
                table: "CourseModel",
                newName: "TeacherCNP");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModel_TeacherViewModelCNP",
                table: "CourseModel",
                newName: "IX_CourseModel_TeacherCNP");

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "CourseModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseModel",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherCNP",
                table: "CourseModel",
                column: "TeacherCNP",
                principalTable: "TeacherViewModel",
                principalColumn: "CNP",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherCNP",
                table: "CourseModel");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "CourseModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseModel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CourseModel",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TeacherCNP",
                table: "CourseModel",
                newName: "TeacherViewModelCNP");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModel_TeacherCNP",
                table: "CourseModel",
                newName: "IX_CourseModel_TeacherViewModelCNP");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherViewModelCNP",
                table: "CourseModel",
                column: "TeacherViewModelCNP",
                principalTable: "TeacherViewModel",
                principalColumn: "CNP",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
