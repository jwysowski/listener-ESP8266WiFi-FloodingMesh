using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace listener.Migrations
{
    public partial class UpdateListener : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NodeId",
                table: "Node",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "InManualMode",
                table: "Node",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TargetHumidity",
                table: "Node",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TargetTemperature",
                table: "Node",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Log",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "NodeId",
                table: "Log",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Log",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InManualMode",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "TargetHumidity",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "TargetTemperature",
                table: "Node");

            migrationBuilder.AlterColumn<string>(
                name: "NodeId",
                table: "Node",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Log",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NodeId",
                table: "Log",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Log",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
