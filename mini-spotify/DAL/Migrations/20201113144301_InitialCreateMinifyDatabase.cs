using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mini_spotify.DAL.Migrations
{
    public partial class InitialCreateMinifyDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Streams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streams_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Duration", "Genre", "Name", "Path" },
                values: new object[,]
                {
                    { new Guid("aa5ab627-3b64-4c22-9cc3-cca5fd57c896"), 5, "Classic", "Titanic", "." },
                    { new Guid("52f5aab5-e89e-497c-94a9-01b2961e5f27"), 4, "Rap", "Low(feat. T-Pain", "." }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PassWord", "UserName" },
                values: new object[,]
                {
                    { new Guid("aa5ab627-3b64-5d22-8cc3-cca5fd57c896"), "s1140207@student.windesheim.nl", "Ronald", "Haan", "tA1EvGT0VPo6QDf+dPQmyyEv/KD5WaXaz721lSIAgImosSUv", "1140207" },
                    { new Guid("aa5ab653-3b62-5e22-5cc3-cca5fd57c846"), "s1121300@student.windesheim.nl", "Ali", "Alkhalil", "xDpX3fbWOO4pkXV0FcNJMVhiyo3z83XdjWajl1FgGtl3Em7r", "1121300" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Streams_SongId",
                table: "Streams",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Streams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
