using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheeflienWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesCreateFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeAlergies");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeAlergies",
                columns: table => new
                {
                    AlergiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeAlergies", x => new { x.AlergiesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_RecipeAlergies_Alergies_AlergiesId",
                        column: x => x.AlergiesId,
                        principalTable: "Alergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeAlergies_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAlergies_RecipesId",
                table: "RecipeAlergies",
                column: "RecipesId");
        }
    }
}
