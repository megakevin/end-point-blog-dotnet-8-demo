using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleQuotes.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class VariousNullabilityChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_quotes_body_types_body_type_id",
                table: "quotes");

            migrationBuilder.DropForeignKey(
                name: "fk_quotes_sizes_size_id",
                table: "quotes");

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sizes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "size_id",
                table: "quotes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "body_type_id",
                table: "quotes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "feature_value",
                table: "quote_rules",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "feature_type",
                table: "quote_rules",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "models",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "year",
                table: "model_style_years",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "makes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "body_types",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "name" },
                values: new object[] { 4, "Full Size" });

            migrationBuilder.AddForeignKey(
                name: "fk_quotes_body_types_body_type_id",
                table: "quotes",
                column: "body_type_id",
                principalTable: "body_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_quotes_sizes_size_id",
                table: "quotes",
                column: "size_id",
                principalTable: "sizes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_quotes_body_types_body_type_id",
                table: "quotes");

            migrationBuilder.DropForeignKey(
                name: "fk_quotes_sizes_size_id",
                table: "quotes");

            migrationBuilder.DeleteData(
                table: "sizes",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "sizes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "size_id",
                table: "quotes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "body_type_id",
                table: "quotes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "feature_value",
                table: "quote_rules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "feature_type",
                table: "quote_rules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "models",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "year",
                table: "model_style_years",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "makes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "body_types",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "sizes",
                columns: new[] { "id", "name" },
                values: new object[] { 5, "Full Size" });

            migrationBuilder.AddForeignKey(
                name: "fk_quotes_body_types_body_type_id",
                table: "quotes",
                column: "body_type_id",
                principalTable: "body_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_quotes_sizes_size_id",
                table: "quotes",
                column: "size_id",
                principalTable: "sizes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
