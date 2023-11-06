using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIcheck.Migrations
{
    /// <inheritdoc />
    public partial class Tuitionfee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tuitionfees",
                columns: table => new
                {
                    IdTutionFee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStudent = table.Column<int>(type: "int", nullable: false),
                    IdSemester = table.Column<int>(type: "int", nullable: false),
                    MoneyTuition = table.Column<int>(type: "int", nullable: false),
                    MoneyPaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuitionfees", x => x.IdTutionFee);
                    table.ForeignKey(
                        name: "FK_Tuitionfees_Semesters_IdSemester",
                        column: x => x.IdSemester,
                        principalTable: "Semesters",
                        principalColumn: "IdSemester",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tuitionfees_Students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Students",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tuitionfees_IdSemester",
                table: "Tuitionfees",
                column: "IdSemester");

            migrationBuilder.CreateIndex(
                name: "IX_Tuitionfees_IdStudent",
                table: "Tuitionfees",
                column: "IdStudent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tuitionfees");
        }
    }
}
