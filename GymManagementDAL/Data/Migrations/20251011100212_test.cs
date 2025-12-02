using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Members_Id",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_Members_MemberId",
                table: "MemberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Members_MemberId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_trainers_TrainerId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainers",
                table: "trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidPhoneCheck1",
                table: "trainers");

            migrationBuilder.RenameTable(
                name: "trainers",
                newName: "GymUsers");

            migrationBuilder.RenameIndex(
                name: "IX_trainers_Phone",
                table: "GymUsers",
                newName: "IX_GymUsers_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_trainers_Email",
                table: "GymUsers",
                newName: "IX_GymUsers_Email");

            migrationBuilder.AlterColumn<int>(
                name: "Specialties",
                table: "GymUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "GymUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "GymUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymUsers",
                table: "GymUsers",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "GymUsers",
                sql: "Email Like '_%@_%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidPhoneCheck",
                table: "GymUsers",
                sql: "Phone Like '01%' and Phone Not Like'%[^0-9]%' ");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_GymUsers_Id",
                table: "Member",
                column: "Id",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_GymUsers_MemberId",
                table: "MemberSessions",
                column: "MemberId",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_GymUsers_MemberId",
                table: "Memberships",
                column: "MemberId",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "GymUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_GymUsers_Id",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSessions_GymUsers_MemberId",
                table: "MemberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_GymUsers_MemberId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_GymUsers_TrainerId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymUsers",
                table: "GymUsers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidEmailCheck",
                table: "GymUsers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserValidPhoneCheck",
                table: "GymUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "GymUsers");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "GymUsers");

            migrationBuilder.RenameTable(
                name: "GymUsers",
                newName: "trainers");

            migrationBuilder.RenameIndex(
                name: "IX_GymUsers_Phone",
                table: "trainers",
                newName: "IX_trainers_Phone");

            migrationBuilder.RenameIndex(
                name: "IX_GymUsers_Email",
                table: "trainers",
                newName: "IX_trainers_Email");

            migrationBuilder.AlterColumn<int>(
                name: "Specialties",
                table: "trainers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainers",
                table: "trainers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "Varchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "Varchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuildingNumber = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "Varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.CheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                    table.CheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' and Phone Not Like'%[^0-9]%' ");
                });

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidEmailCheck1",
                table: "trainers",
                sql: "Email Like '_%@_%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserValidPhoneCheck1",
                table: "trainers",
                sql: "Phone Like '01%' and Phone Not Like'%[^0-9]%' ");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Phone",
                table: "Members",
                column: "Phone",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Members_Id",
                table: "Member",
                column: "Id",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSessions_Members_MemberId",
                table: "MemberSessions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Members_MemberId",
                table: "Memberships",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_trainers_TrainerId",
                table: "Sessions",
                column: "TrainerId",
                principalTable: "trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
