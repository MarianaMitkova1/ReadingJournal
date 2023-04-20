using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingJournal.Migrations
{
    public partial class UpdateBookModel_AddIsDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");
        }
    }
}
