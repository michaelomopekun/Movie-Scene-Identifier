using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSceneIdentifierBackend.Migrations
{
    public partial class UpdateMoviesIdentity_entity_added_column_for_cloudinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "UploadedClips",
                newName: "FileType");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryFileName",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryFilePath",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryFileSize",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryFileType",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryPublicId",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "UploadedClips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "UploadedClips",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryFileName",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "CloudinaryFilePath",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "CloudinaryFileSize",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "CloudinaryFileType",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "CloudinaryPublicId",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "UploadedClips");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "UploadedClips");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "UploadedClips",
                newName: "FilePath");
        }
    }
}
