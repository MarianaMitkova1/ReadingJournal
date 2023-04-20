using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingJournal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    PictureURL = table.Column<string>(nullable: true),
                    IsReaded = table.Column<bool>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
