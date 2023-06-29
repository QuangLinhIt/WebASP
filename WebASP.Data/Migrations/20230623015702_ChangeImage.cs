using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASP.Data.Migrations
{
    public partial class ChangeImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "97a447ee-261b-4891-b079-490d17fa63be");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "56a17015-c833-45d7-81cc-8f77b88e7f13", "AQAAAAEAACcQAAAAEJHAeKXoARgR6oB22qUz3DVDDDEQmgjrpaLHR7+uaMHOLFpmezhQy6SpXRRPxYhldA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 6, 23, 8, 57, 1, 677, DateTimeKind.Local).AddTicks(5586));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
    }
}
