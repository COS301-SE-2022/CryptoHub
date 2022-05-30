using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserFollower",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FollowId = table.Column<int>(type: "int", nullable: false),
                    FollowDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollower", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserFollower_FollowerId",
                        column: x => x.FollowId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_UserFollower_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Firstname", "Lastname", "Password", "Username" },
                values: new object[] { 1, "johndoe@gmail.com", "john", "doe", "1234", "john" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Firstname", "Lastname", "Password", "Username" },
                values: new object[] { 2, "elonmusk@gmail.com", "elon", "musk", "1234", "elon" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Firstname", "Lastname", "Password", "Username" },
                values: new object[] { 3, "billgates@gmail.com", "bill", "gates", "windows", "bill" });

            migrationBuilder.InsertData(
                table: "Post",
                columns: new[] { "PostId", "Post", "UserId" },
                values: new object[,]
                {
                    { 1, "Crypto awesome", 1 },
                    { 2, "Crypto Amazing", 1 },
                    { 3, "cool site", 2 },
                    { 4, "send money", 3 },
                    { 5, "please", 3 }
                });

            migrationBuilder.InsertData(
                table: "UserFollower",
                columns: new[] { "id", "FollowDate", "FollowId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 24, 20, 26, 54, 245, DateTimeKind.Local).AddTicks(6064), 2, 1 },
                    { 2, new DateTime(2022, 5, 24, 20, 26, 54, 245, DateTimeKind.Local).AddTicks(6075), 3, 1 },
                    { 3, new DateTime(2022, 5, 24, 20, 26, 54, 245, DateTimeKind.Local).AddTicks(6076), 2, 3 },
                    { 4, new DateTime(2022, 5, 24, 20, 26, 54, 245, DateTimeKind.Local).AddTicks(6077), 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowId",
                table: "UserFollower",
                column: "FollowId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_UserId",
                table: "UserFollower",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "UserFollower");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
