#nullable disable

namespace Data.PostGreSql.Migration
{
    using Microsoft.EntityFrameworkCore.Migrations;
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exchangerates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from_currency_name = table.Column<string>(type: "text", nullable: false),
                    from_currency_code = table.Column<string>(type: "text", nullable: false),
                    to_currency_name = table.Column<string>(type: "text", nullable: false),
                    to_currency_code = table.Column<string>(type: "text", nullable: false),
                    rate = table.Column<double>(type: "double precision", nullable: false),
                    bid_price = table.Column<double>(type: "double precision", nullable: false),
                    ask_price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exchangerates", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exchangerates");
        }
    }
}
