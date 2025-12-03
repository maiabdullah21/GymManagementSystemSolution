using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class test02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
