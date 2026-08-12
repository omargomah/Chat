using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Chat.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture_Id",
                table: "AspNetUsers",
                type: "VarChar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture_Url",
                table: "AspNetUsers",
                type: "VarChar(2048)",
                maxLength: 2048,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Picture_Url",
                table: "AspNetUsers");
        }
    }
}
