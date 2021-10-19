using Microsoft.EntityFrameworkCore.Migrations;

namespace Cadastro_Pessoas.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbCargo",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cargo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NiveldeAcesso = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCargo", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "tbColaborador",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nchar(80)", fixedLength: true, maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nchar(80)", fixedLength: true, maxLength: 80, nullable: false),
                    Senha = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    CodigoCargo = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbColaborador", x => x.Codigo);
                    table.ForeignKey(
                        name: "fkCargo",
                        column: x => x.CodigoCargo,
                        principalTable: "tbCargo",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbColaborador_CodigoCargo",
                table: "tbColaborador",
                column: "CodigoCargo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbColaborador");

            migrationBuilder.DropTable(
                name: "tbCargo");
        }
    }
}
