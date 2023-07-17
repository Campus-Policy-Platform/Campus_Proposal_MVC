using Microsoft.EntityFrameworkCore.Migrations;

namespace CHU_PolicyPlatform_1.Migrations
{
    public partial class ReviseCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryDay",
                table: "Proposal",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryReview",
                table: "Proposal",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryGerentReview",
                table: "Category",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryMaxDay",
                table: "Category",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryMinDay",
                table: "Category",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryDay",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "CategoryReview",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "CategoryGerentReview",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryMaxDay",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CategoryMinDay",
                table: "Category");
        }
    }
}
