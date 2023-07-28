using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SecondMap.Services.StoreManagementService.DAL.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					RoleName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Roles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Stores",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
					Address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
					Rating = table.Column<int>(type: "integer", nullable: true),
					Price = table.Column<decimal>(type: "numeric", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Stores", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Username = table.Column<string>(type: "text", nullable: false),
					PasswordHash = table.Column<string>(type: "text", nullable: false),
					PasswordSalt = table.Column<string>(type: "text", nullable: false),
					RoleId = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
					table.ForeignKey(
						name: "FK_Users_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Schedules",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					StoreId = table.Column<int>(type: "integer", nullable: false),
					Day = table.Column<int>(type: "integer", nullable: false),
					OpeningTime = table.Column<TimeSpan>(type: "interval", nullable: false),
					ClosingTime = table.Column<TimeSpan>(type: "interval", nullable: false),
					IsClosed = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Schedules", x => x.Id);
					table.ForeignKey(
						name: "FK_Schedules_Stores_StoreId",
						column: x => x.StoreId,
						principalTable: "Stores",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Reviews",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					UserId = table.Column<int>(type: "integer", nullable: false),
					StoreId = table.Column<int>(type: "integer", nullable: false),
					Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
					Rating = table.Column<int>(type: "integer", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reviews", x => x.Id);
					table.ForeignKey(
						name: "FK_Reviews_Stores_StoreId",
						column: x => x.StoreId,
						principalTable: "Stores",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Reviews_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Reviews_StoreId",
				table: "Reviews",
				column: "StoreId");

			migrationBuilder.CreateIndex(
				name: "IX_Reviews_UserId",
				table: "Reviews",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Schedules_StoreId",
				table: "Schedules",
				column: "StoreId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_RoleId",
				table: "Users",
				column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Reviews");

			migrationBuilder.DropTable(
				name: "Schedules");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Stores");

			migrationBuilder.DropTable(
				name: "Roles");
		}
	}
}
