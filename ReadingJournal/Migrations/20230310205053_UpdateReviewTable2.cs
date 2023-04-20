using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingJournal.Migrations
{
    public partial class UpdateReviewTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Reviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Ratings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");
        }
    }
}
