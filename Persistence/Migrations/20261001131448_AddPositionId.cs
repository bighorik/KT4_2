using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipApplications_Projects_ProjectId",
                table: "MembershipApplications");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "MembershipApplications",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipApplications_ProjectId",
                table: "MembershipApplications",
                newName: "IX_MembershipApplications_PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentGroupNumberId",
                table: "Users",
                column: "StudentGroupNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipApplications_ProjectPositions_PositionId",
                table: "MembershipApplications",
                column: "PositionId",
                principalTable: "ProjectPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_StudentGroupNumbers_StudentGroupNumberId",
                table: "Users",
                column: "StudentGroupNumberId",
                principalTable: "StudentGroupNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipApplications_ProjectPositions_PositionId",
                table: "MembershipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_StudentGroupNumbers_StudentGroupNumberId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StudentGroupNumberId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "MembershipApplications",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_MembershipApplications_PositionId",
                table: "MembershipApplications",
                newName: "IX_MembershipApplications_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipApplications_Projects_ProjectId",
                table: "MembershipApplications",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
