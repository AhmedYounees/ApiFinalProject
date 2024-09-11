using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiFinalProject.Migrations
{
    /// <inheritdoc />
    public partial class addimageCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Reviews",
                newName: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "PosterImage",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterImage",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Reviews",
                newName: "Description");
        }
    }
}
