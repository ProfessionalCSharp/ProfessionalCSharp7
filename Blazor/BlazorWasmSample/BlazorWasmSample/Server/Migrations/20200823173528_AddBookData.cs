using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWasmSample.Server.Migrations
{
    public partial class AddBookData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "sample pub", "sample 1" },
                    { 2, "sample pub", "sample 2" },
                    { 3, "sample pub", "sample 3" },
                    { 4, "sample pub", "sample 4" },
                    { 5, "sample pub", "sample 5" },
                    { 6, "sample pub", "sample 6" },
                    { 7, "sample pub", "sample 7" },
                    { 8, "sample pub", "sample 8" },
                    { 9, "sample pub", "sample 9" },
                    { 10, "sample pub", "sample 10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);
        }
    }
}
