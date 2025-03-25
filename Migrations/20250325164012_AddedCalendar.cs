using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace verticalSliceArchitecture.Migrations
{
    /// <inheritdoc />
    public partial class AddedCalendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblNepaliMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNepaliMonth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblNepaliCalendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    EnglishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHoliday = table.Column<bool>(type: "bit", nullable: false),
                    HolidayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblNepaliCalendar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblNepaliCalendar_tblNepaliMonth_MonthId",
                        column: x => x.MonthId,
                        principalTable: "tblNepaliMonth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblYearlyMonthDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    DaysInMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblYearlyMonthDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblYearlyMonthDays_tblNepaliMonth_MonthId",
                        column: x => x.MonthId,
                        principalTable: "tblNepaliMonth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblNepaliCalendar_MonthId",
                table: "tblNepaliCalendar",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_tblYearlyMonthDays_MonthId",
                table: "tblYearlyMonthDays",
                column: "MonthId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblNepaliCalendar");

            migrationBuilder.DropTable(
                name: "tblYearlyMonthDays");

            migrationBuilder.DropTable(
                name: "tblNepaliMonth");
        }
    }
}
