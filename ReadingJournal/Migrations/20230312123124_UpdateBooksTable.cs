using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingJournal.Migrations
{
    public partial class UpdateBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
