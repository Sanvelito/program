using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyServices_ServicesGroup_ServiceGroupId",
                table: "CompanyServices");

            migrationBuilder.DropIndex(
                name: "IX_CompanyServices_ServiceGroupId",
                table: "CompanyServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicesGroup",
                table: "ServicesGroup");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "CompanyServices");

            migrationBuilder.RenameTable(
                name: "ServicesGroup",
                newName: "ServiceGroups");

            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceGroups",
                table: "ServiceGroups",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceGroupId",
                table: "Services",
                column: "ServiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceGroups_ServiceGroupId",
                table: "Services",
                column: "ServiceGroupId",
                principalTable: "ServiceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceGroups_ServiceGroupId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceGroupId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceGroups",
                table: "ServiceGroups");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "ServiceGroups",
                newName: "ServicesGroup");

            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupId",
                table: "CompanyServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicesGroup",
                table: "ServicesGroup",
                column: "Id");

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
    }
}
