using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASP.Data.Migrations
{
    public partial class UpdateData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e52ab90c-d308-4d9e-b40e-83a0ff493650");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d6ac927-7c67-4b3a-ba7d-80872a69b2fb", "AQAAAAEAACcQAAAAEHevrq8J1SRe5rYz8vMT62IFv1bwtec4w6A+M365z2wEG0UfzRn9zG4wyyVt4mG04A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 23, 15, 35, 19, 318, DateTimeKind.Local).AddTicks(1181));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fb4fa78e-7ecb-477d-a94b-69898e182c63");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed321e92-d7e2-44d9-b550-8eaf96c6f536", "AQAAAAEAACcQAAAAEN4lP7t4zNv7OJeEU3dnUL362je59/bHCAJwr4oy3bvd7C5eypNsmIfxq/4coV4kUQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 23, 9, 16, 58, 434, DateTimeKind.Local).AddTicks(2017));
        }
    }
}
