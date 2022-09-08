using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploTokenBased.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Setor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeUsuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Papel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashSenha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.UniqueConstraint("AK_Usuarios_NomeUsuario", x => x.NomeUsuario);
                });

            migrationBuilder.CreateTable(
                name: "ValoresMoeda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValoresMoeda", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "HashSenha", "NomeUsuario", "Papel", "Setor" },
                values: new object[,]
                {
                    { new Guid("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"), "123abc", "admin", "Admin", "Administração" },
                    { new Guid("7b94e9d7-b3a4-417e-95f8-f26bd261c609"), "123abc", "carlos", "Admin", "Vendas" }
                });

            migrationBuilder.InsertData(
                table: "ValoresMoeda",
                columns: new[] { "Id", "Data", "Valor" },
                values: new object[,]
                {
                    { new Guid("073ce8bb-d9be-4f72-9d1d-e7152d58974d"), new DateTime(2022, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0499999999999998 },
                    { new Guid("77da11b5-efc9-466a-abdb-edbb7639016f"), new DateTime(2022, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.25 },
                    { new Guid("901a33b7-9949-4330-832a-aa9bba66db45"), new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4.9500000000000002 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "ValoresMoeda");
        }
    }
}
