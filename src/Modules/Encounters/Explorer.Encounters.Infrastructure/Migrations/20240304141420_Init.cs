using System.Collections.Generic;
using Explorer.Encounters.Core.Domain.Encounter;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Explorer.Encounters.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "encounters");

            migrationBuilder.CreateTable(
                name: "Encounters",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Radius = table.Column<double>(type: "double precision", nullable: false),
                    XpReward = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Instances = table.Column<List<EncounterInstance>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TouristProgress",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Xp = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristProgress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HiddenLocationEncounters",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PictureLongitude = table.Column<double>(type: "double precision", nullable: false),
                    PictureLatitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiddenLocationEncounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HiddenLocationEncounters_Encounters_Id",
                        column: x => x.Id,
                        principalSchema: "encounters",
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyPointEncounter",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    KeyPointId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPointEncounter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyPointEncounter_Encounters_Id",
                        column: x => x.Id,
                        principalSchema: "encounters",
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MiscEncounters",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ChallengeDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiscEncounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MiscEncounters_Encounters_Id",
                        column: x => x.Id,
                        principalSchema: "encounters",
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialEncounters",
                schema: "encounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PeopleNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialEncounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialEncounters_Encounters_Id",
                        column: x => x.Id,
                        principalSchema: "encounters",
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HiddenLocationEncounters",
                schema: "encounters");

            migrationBuilder.DropTable(
                name: "KeyPointEncounter",
                schema: "encounters");

            migrationBuilder.DropTable(
                name: "MiscEncounters",
                schema: "encounters");

            migrationBuilder.DropTable(
                name: "SocialEncounters",
                schema: "encounters");

            migrationBuilder.DropTable(
                name: "TouristProgress",
                schema: "encounters");

            migrationBuilder.DropTable(
                name: "Encounters",
                schema: "encounters");
        }
    }
}
