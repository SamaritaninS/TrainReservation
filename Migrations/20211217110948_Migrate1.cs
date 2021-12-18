using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainReservation.Migrations
{
    public partial class Migrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "ReservationInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "ReservationInfos");
        }
    }
}
