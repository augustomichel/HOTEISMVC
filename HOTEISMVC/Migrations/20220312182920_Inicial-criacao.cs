using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOTEISMVC.Migrations
{
    public partial class Inicialcriacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hoteis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoteis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fotos_hoteis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Hotel = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fotos_hoteis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_fotos_hoteis_hoteis_Id_Hotel",
                        column: x => x.Id_Hotel,
                        principalTable: "hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quartos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Hotel = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOcupantes = table.Column<int>(type: "int", nullable: false),
                    NumAdultos = table.Column<int>(type: "int", nullable: false),
                    NumCriancas = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quartos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quartos_hoteis_Id_Hotel",
                        column: x => x.Id_Hotel,
                        principalTable: "hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fotos_hoteis_Id_Hotel",
                table: "fotos_hoteis",
                column: "Id_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_quartos_Id_Hotel",
                table: "quartos",
                column: "Id_Hotel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fotos_hoteis");

            migrationBuilder.DropTable(
                name: "quartos");

            migrationBuilder.DropTable(
                name: "hoteis");
        }
    }
}
