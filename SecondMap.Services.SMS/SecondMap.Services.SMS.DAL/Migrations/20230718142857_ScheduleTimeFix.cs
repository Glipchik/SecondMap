using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondMap.Services.StoreManagementService.DAL.Migrations
{
	/// <inheritdoc />
	public partial class ScheduleTimeFix : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<TimeOnly>(
				name: "OpeningTime",
				table: "Schedules",
				type: "time without time zone",
				nullable: false,
				oldClrType: typeof(TimeSpan),
				oldType: "interval");

			migrationBuilder.AlterColumn<TimeOnly>(
				name: "ClosingTime",
				table: "Schedules",
				type: "time without time zone",
				nullable: false,
				oldClrType: typeof(TimeSpan),
				oldType: "interval");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<TimeSpan>(
				name: "OpeningTime",
				table: "Schedules",
				type: "interval",
				nullable: false,
				oldClrType: typeof(TimeOnly),
				oldType: "time without time zone");

			migrationBuilder.AlterColumn<TimeSpan>(
				name: "ClosingTime",
				table: "Schedules",
				type: "interval",
				nullable: false,
				oldClrType: typeof(TimeOnly),
				oldType: "time without time zone");
		}
	}
}
