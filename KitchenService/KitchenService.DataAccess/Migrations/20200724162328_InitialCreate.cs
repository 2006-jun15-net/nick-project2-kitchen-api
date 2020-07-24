using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KitchenService.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FridgeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FridgeItems_People_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FridgeItems",
                columns: new[] { "Id", "ExpirationDate", "Name", "OwnerId" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2020, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "cheese", null });

            migrationBuilder.InsertData(
                table: "FridgeItems",
                columns: new[] { "Id", "ExpirationDate", "Name", "OwnerId" },
                values: new object[] { 2, new DateTimeOffset(new DateTime(2020, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "steak", null });

            migrationBuilder.InsertData(
                table: "FridgeItems",
                columns: new[] { "Id", "ExpirationDate", "Name", "OwnerId" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2020, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -5, 0, 0, 0)), "salmon", null });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeItems_OwnerId",
                table: "FridgeItems",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeItems");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
