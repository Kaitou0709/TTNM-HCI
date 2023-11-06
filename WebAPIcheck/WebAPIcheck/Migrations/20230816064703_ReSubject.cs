using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIcheck.Migrations
{
    /// <inheritdoc />
    public partial class ReSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeeSubjects",
                columns: table => new
                {
                    IdFee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fee = table.Column<int>(type: "int", nullable: false),
                    IdSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeSubjects", x => x.IdFee);
                    table.ForeignKey(
                        name: "FK_FeeSubjects_Subjects_IdSubject",
                        column: x => x.IdSubject,
                        principalTable: "Subjects",
                        principalColumn: "IdSubject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReSubjects",
                columns: table => new
                {
                    IdReSubject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSubject = table.Column<int>(type: "int", nullable: false),
                    IdStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReSubjects", x => x.IdReSubject);
                    table.ForeignKey(
                        name: "FK_ReSubjects_Students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Students",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReSubjects_SubjectClasses_IdSubject",
                        column: x => x.IdSubject,
                        principalTable: "SubjectClasses",
                        principalColumn: "IdClass",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamSubjects",
                columns: table => new
                {
                    IdExam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStartTime = table.Column<int>(type: "int", nullable: false),
                    IdEndTime = table.Column<int>(type: "int", nullable: false),
                    IdReSubject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSubjects", x => x.IdExam);
                    table.ForeignKey(
                        name: "FK_ExamSubjects_ReSubjects_IdReSubject",
                        column: x => x.IdReSubject,
                        principalTable: "ReSubjects",
                        principalColumn: "IdReSubject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    IdScore = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReSubject = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.IdScore);
                    table.ForeignKey(
                        name: "FK_Scores_ReSubjects_IdReSubject",
                        column: x => x.IdReSubject,
                        principalTable: "ReSubjects",
                        principalColumn: "IdReSubject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSubjects_IdReSubject",
                table: "ExamSubjects",
                column: "IdReSubject");

            migrationBuilder.CreateIndex(
                name: "IX_FeeSubjects_IdSubject",
                table: "FeeSubjects",
                column: "IdSubject");

            migrationBuilder.CreateIndex(
                name: "IX_ReSubjects_IdStudent",
                table: "ReSubjects",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_ReSubjects_IdSubject",
                table: "ReSubjects",
                column: "IdSubject");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_IdReSubject",
                table: "Scores",
                column: "IdReSubject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamSubjects");

            migrationBuilder.DropTable(
                name: "FeeSubjects");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "ReSubjects");
        }
    }
}
