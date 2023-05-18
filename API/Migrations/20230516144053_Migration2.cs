using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupId",
                table: "CompanyServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServicesGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyServices_ServiceGroupId",
                table: "CompanyServices",
                column: "ServiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyServices_ServicesGroup_ServiceGroupId",
                table: "CompanyServices",
                column: "ServiceGroupId",
                principalTable: "ServicesGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyServices_ServicesGroup_ServiceGroupId",
                table: "CompanyServices");

            migrationBuilder.DropTable(
                name: "ServicesGroup");

            migrationBuilder.DropIndex(
                name: "IX_CompanyServices_ServiceGroupId",
                table: "CompanyServices");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "CompanyServices");
        }
    }
}
