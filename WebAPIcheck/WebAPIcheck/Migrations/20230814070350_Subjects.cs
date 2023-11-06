using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIcheck.Migrations
{
    /// <inheritdoc />
    public partial class Subjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonTime",
                columns: table => new
                {
                    IdTime = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTime", x => x.IdTime);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    IdSemester = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.IdSemester);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    IdSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFaclty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.IdSubject);
                    table.ForeignKey(
                        name: "FK_Subjects_Faculties_IdFaclty",
                        column: x => x.IdFaclty,
                        principalTable: "Faculties",
                        principalColumn: "IdFaculty",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    IdSchedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTimeStart = table.Column<int>(type: "int", nullable: false),
                    IdTimeEnd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_Schedules_LessonTime_IdTimeEnd",
                        column: x => x.IdTimeEnd,
                        principalTable: "LessonTime",
                        principalColumn: "IdTime",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Schedules_LessonTime_IdTimeStart",
                        column: x => x.IdTimeStart,
                        principalTable: "LessonTime",
                        principalColumn: "IdTime",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SubjectClasses",
                columns: table => new
                {
                    IdClass = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberClass = table.Column<int>(type: "int", nullable: false),
                    IdSubject = table.Column<int>(type: "int", nullable: false),
                    IdSemester = table.Column<int>(type: "int", nullable: false),
                    SemesterDateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SemesterDateEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectClasses", x => x.IdClass);
                    table.ForeignKey(
                        name: "FK_SubjectClasses_Semesters_IdSemester",
                        column: x => x.IdSemester,
                        principalTable: "Semesters",
                        principalColumn: "IdSemester",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectClasses_Subjects_IdSubject",
                        column: x => x.IdSubject,
                        principalTable: "Subjects",
                        principalColumn: "IdSubject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemestersSubject",
                columns: table => new
                {
                    IdSemesterSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubjectClass = table.Column<int>(type: "int", nullable: false),
                    IdSchedule = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemestersSubject", x => x.IdSemesterSubject);
                    table.ForeignKey(
                        name: "FK_SemestersSubject_Schedules_IdSchedule",
                        column: x => x.IdSchedule,
                        principalTable: "Schedules",
                        principalColumn: "IdSchedule",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemestersSubject_SubjectClasses_IdSubjectClass",
                        column: x => x.IdSubjectClass,
                        principalTable: "SubjectClasses",
                        principalColumn: "IdClass",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_IdTimeEnd",
                table: "Schedules",
                column: "IdTimeEnd");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_IdTimeStart",
                table: "Schedules",
                column: "IdTimeStart");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersSubject_IdSchedule",
                table: "SemestersSubject",
                column: "IdSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_SemestersSubject_IdSubjectClass",
                table: "SemestersSubject",
                column: "IdSubjectClass");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClasses_IdSemester",
                table: "SubjectClasses",
                column: "IdSemester");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClasses_IdSubject",
                table: "SubjectClasses",
                column: "IdSubject");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_IdFaclty",
                table: "Subjects",
                column: "IdFaclty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemestersSubject");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "SubjectClasses");

            migrationBuilder.DropTable(
                name: "LessonTime");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
