using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSceneIdentifierBackend.Migrations
{
    public partial class movieIdentifiedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Confidence",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "ImdbId",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Plot",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Released",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Runtime",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "MoviesIdentified");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "MoviesIdentified");

            migrationBuilder.RenameColumn(
                name: "imdbRating",
                table: "MoviesIdentified",
                newName: "Payload");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "MoviesIdentified",
                newName: "imdbRating");

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Confidence",
                table: "MoviesIdentified",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImdbId",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Plot",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Released",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Runtime",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "MoviesIdentified",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
