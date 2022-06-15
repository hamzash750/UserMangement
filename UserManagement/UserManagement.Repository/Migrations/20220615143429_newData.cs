using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagement.Repository.Migrations
{
    public partial class newData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "UserAddress",
                columns: new[] { "AddressId", "City", "Country", "PinCode", "State", "Street" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "12345", "USA State", "Test Street Address USA" },
                    { 2, "Muscat", "Oman", "445778", "Oman State", "Test Street Address Oman" },
                    { 3, "Dubai", "UAE", "98766", "UAE State", "Test Street Address UAE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AddressId", "Designation", "ImagePath", "JoiningDate", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Developer", "", new DateTime(2022, 6, 14, 19, 34, 28, 917, DateTimeKind.Local).AddTicks(9200), "John" },
                    { 2, 2, "Project Manger", "", new DateTime(2022, 6, 13, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(53), "Mark" },
                    { 3, 3, "Team Lead", "", new DateTime(2022, 6, 12, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(65), "Smith" },
                    { 4, 1, "Developer", "", new DateTime(2022, 5, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(66), "Emma" },
                    { 5, 2, "Developer", "", new DateTime(2022, 4, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(89), "Sophia" },
                    { 6, 3, "HR", "", new DateTime(2019, 6, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(91), "Olivia" },
                    { 7, 1, "Developer", "", new DateTime(2021, 6, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(101), "Liam" },
                    { 8, 2, "Developer", "", new DateTime(2022, 6, 13, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(103), "Benjamin" },
                    { 9, 3, "HR", "", new DateTime(2019, 6, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(104), "Elijah" },
                    { 10, 1, "Developer", "", new DateTime(2022, 5, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(106), "John" },
                    { 11, 2, "Developer", "", new DateTime(2022, 4, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(108), "Mark" },
                    { 12, 3, "Developer", "", new DateTime(2022, 3, 15, 19, 34, 28, 918, DateTimeKind.Local).AddTicks(109), "Smith" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
