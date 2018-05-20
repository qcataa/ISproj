using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ISproj.Data.Migrations
{
    public partial class CourseModelAddTeacherFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherCNP",
                table: "CourseModel");

            migrationBuilder.RenameColumn(
                name: "TeacherCNP",
                table: "CourseModel",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModel_TeacherCNP",
                table: "CourseModel",
                newName: "IX_CourseModel_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherId",
                table: "CourseModel",
                column: "TeacherId",
                principalTable: "TeacherViewModel",
                principalColumn: "CNP",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherId",
                table: "CourseModel");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "CourseModel",
                newName: "TeacherCNP");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModel_TeacherId",
                table: "CourseModel",
                newName: "IX_CourseModel_TeacherCNP");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModel_TeacherViewModel_TeacherCNP",
                table: "CourseModel",
                column: "TeacherCNP",
                principalTable: "TeacherViewModel",
                principalColumn: "CNP",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
