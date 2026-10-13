using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "ProjectMember",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectEntityId",
                table: "MembershipApplications",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_PositionId",
                table: "ProjectMember",
                column: "PositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembershipApplications_ProjectEntityId",
                table: "MembershipApplications",
                column: "ProjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipApplications_Projects_ProjectEntityId",
                table: "MembershipApplications",
                column: "ProjectEntityId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMember_ProjectPositions_PositionId",
                table: "ProjectMember",
                column: "PositionId",
                principalTable: "ProjectPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipApplications_Projects_ProjectEntityId",
                table: "MembershipApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMember_ProjectPositions_PositionId",
                table: "ProjectMember");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMember_PositionId",
                table: "ProjectMember");

            migrationBuilder.DropIndex(
                name: "IX_MembershipApplications_ProjectEntityId",
                table: "MembershipApplications");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "ProjectMember");

            migrationBuilder.DropColumn(
                name: "ProjectEntityId",
                table: "MembershipApplications");
        }
    }
}
