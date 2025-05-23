using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSceneIdentifierBackend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadedClips",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedClips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviesIdentified",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ImdbId = table.Column<string>(type: "text", nullable: false),
                    SimilarityScore = table.Column<float>(type: "real", nullable: false),
                    MetadataPayload = table.Column<string>(type: "text", nullable: false),
                    UploadedClipId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesIdentified", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviesIdentified_UploadedClips_UploadedClipId",
                        column: x => x.UploadedClipId,
                        principalTable: "UploadedClips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesIdentified_UploadedClipId",
                table: "MoviesIdentified",
                column: "UploadedClipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesIdentified");

            migrationBuilder.DropTable(
                name: "UploadedClips");
        }
    }
}
