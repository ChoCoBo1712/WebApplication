using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Artists",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Albums",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Artists",
                newName: "Picture");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Albums",
                newName: "Picture");
        }
    }
}
