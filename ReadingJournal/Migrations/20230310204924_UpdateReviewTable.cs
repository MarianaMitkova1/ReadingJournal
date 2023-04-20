using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingJournal.Migrations
{
    public partial class UpdateReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Review",
                table: "Ratings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
