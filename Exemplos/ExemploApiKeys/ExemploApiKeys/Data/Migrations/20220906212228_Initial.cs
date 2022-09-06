using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploApiKeys.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "ApiKey", "Ativo", "Nome" },
                values: new object[] { new Guid("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"), "527EA1E66C3C1D9D278B6FC4FEE5919E", true, "Cliente X" });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "ApiKey", "Ativo", "Nome" },
                values: new object[] { new Guid("adc6e73f-0f0a-4acf-97ed-b40af27b108b"), "8A66C40830B388B86C2C89926E80C29F", false, "Cliente Z" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
