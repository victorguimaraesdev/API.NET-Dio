using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.NET.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "adm@teste.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "ZzjZy@example.com");
        }
    }
}
