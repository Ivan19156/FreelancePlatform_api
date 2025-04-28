using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Date", "Description", "ReceiverId", "SenderId", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 1000m, new DateTime(2025, 4, 27, 12, 49, 39, 328, DateTimeKind.Utc).AddTicks(2709), "Initial payment for project", 2, 1, "Payment", 1 },
                    { 2, 500m, new DateTime(2025, 4, 27, 12, 59, 39, 328, DateTimeKind.Utc).AddTicks(4220), "Refund for the project", 1, 2, "Refund", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
