using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASP.Data.Migrations
{
    public partial class ChangeProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7d03b6f7-e675-416f-8b08-3a03d77b8c25");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c8a64770-9a16-409e-8d22-4975491e2a08", "AQAAAAEAACcQAAAAEEsoel+E6ECdO39jBwXqkoFNWCWwczJAXsO3oGo7Mi6+ClpQPORBYKXwmB1aIlbbmA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 22, 16, 11, 33, 727, DateTimeKind.Local).AddTicks(3554));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "82329770-fc6a-4cce-bb63-6f603318cd8f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d442b2f-902f-4bbe-b70e-e266152b3ca8", "AQAAAAEAACcQAAAAEBqXMSfzjr6A4VOo1QQSInekWSgIKt7YpmVcTljNoW432EsgYPjehhhqv9WhfPXLmA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 22, 16, 1, 14, 660, DateTimeKind.Local).AddTicks(3100));
        }
    }
}
