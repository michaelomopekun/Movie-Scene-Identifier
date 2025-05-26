using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSceneIdentifierBackend.Migrations
{
    public partial class UpdateMoviesIdentity_and_uploadedClip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoviesIdentified",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ImdbId = table.Column<string>(type: "text", nullable: false),
                    Confidence = table.Column<float>(type: "real", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Year = table.Column<string>(type: "text", nullable: false),
                    Released = table.Column<string>(type: "text", nullable: false),
                    Runtime = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Actors = table.Column<string>(type: "text", nullable: false),
                    Plot = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Poster = table.Column<string>(type: "text", nullable: false),
                    imdbRating = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UploadedClipId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesIdentified", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadedClips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CloudinaryFilePath = table.Column<string>(type: "text", nullable: false),
                    CloudinaryFileName = table.Column<string>(type: "text", nullable: false),
                    CloudinaryFileType = table.Column<string>(type: "text", nullable: false),
                    CloudinaryFileSize = table.Column<string>(type: "text", nullable: false),
                    CloudinaryPublicId = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MovieIdentifiedId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedClips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedClips_MoviesIdentified_MovieIdentifiedId",
                        column: x => x.MovieIdentifiedId,
                        principalTable: "MoviesIdentified",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadedClips_MovieIdentifiedId",
                table: "UploadedClips",
                column: "MovieIdentifiedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadedClips");

            migrationBuilder.DropTable(
                name: "MoviesIdentified");
        }
    }
}
