using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CHU_PolicyPlatform_1.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Gerent",
                columns: table => new
                {
                    GerentID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GPassword = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerent", x => x.GerentID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UPassword = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    ProposalID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PContent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GainsInfluential = table.Column<string>(name: "Gains&Influential", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Underways = table.Column<bool>(type: "bit", nullable: false),
                    Pass = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.ProposalID);
                    table.ForeignKey(
                        name: "FK_Proposal_Category",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToRepond",
                columns: table => new
                {
                    ProposalID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GerentID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RContent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToRepond", x => new { x.ProposalID, x.GerentID });
                    table.ForeignKey(
                        name: "FK_ToRepond_Gerent",
                        column: x => x.GerentID,
                        principalTable: "Gerent",
                        principalColumn: "GerentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToRepond_Proposal",
                        column: x => x.ProposalID,
                        principalTable: "Proposal",
                        principalColumn: "ProposalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProposalID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Crucial = table.Column<bool>(type: "bit", nullable: false),
                    VDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => new { x.UserID, x.ProposalID });
                    table.ForeignKey(
                        name: "FK_Vote_Proposal",
                        column: x => x.ProposalID,
                        principalTable: "Proposal",
                        principalColumn: "ProposalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_CategoryID",
                table: "Proposal",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_UserID",
                table: "Proposal",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ToRepond_GerentID",
                table: "ToRepond",
                column: "GerentID");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ProposalID",
                table: "Vote",
                column: "ProposalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToRepond");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Gerent");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
