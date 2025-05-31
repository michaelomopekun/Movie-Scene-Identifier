using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSceneIdentifierBackend.Migrations
{
    public partial class addTop_KtomovieIdentifiedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Top_K",
                table: "MoviesIdentified",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Top_K",
                table: "MoviesIdentified");
        }
    }
}
