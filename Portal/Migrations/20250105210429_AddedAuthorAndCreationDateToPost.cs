using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorAndCreationDateToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                schema: "portal",
                table: "Posts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "portal",
                table: "Posts",
                newName: "PostID");

            migrationBuilder.AddColumn<string>(
                name: "AuthorID",
                schema: "portal",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                schema: "portal",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorID",
                schema: "portal",
                table: "Posts",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorID",
                schema: "portal",
                table: "Posts",
                column: "AuthorID",
                principalSchema: "portal",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorID",
                schema: "portal",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorID",
                schema: "portal",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                schema: "portal",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                schema: "portal",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Message",
                schema: "portal",
                table: "Posts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "PostID",
                schema: "portal",
                table: "Posts",
                newName: "Id");
        }
    }
}
