using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class procedimientoSpGetCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedimiento = @"create procedure spGetCategories
                                as
                                begin
	                                select * from Category
                                end";
            migrationBuilder.Sql(procedimiento);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedimiento = @"drop procedure spGetCategories";
            migrationBuilder.Sql(procedimiento);

        }
    }
}
