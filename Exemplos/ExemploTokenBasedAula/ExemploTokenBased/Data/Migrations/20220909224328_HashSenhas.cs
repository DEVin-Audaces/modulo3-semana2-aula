using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploTokenBased.Data.Migrations
{
    public partial class HashSenhas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"),
                column: "HashSenha",
                value: "$argon2id$v=19$m=65536,t=3,p=1$SgDHAmk88ICmlQ1TYcfbtw$DRbApdnycdpicjmUthOkwqS5+DZYxc6cJSS2XLFle1o");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("7b94e9d7-b3a4-417e-95f8-f26bd261c609"),
                column: "HashSenha",
                value: "$argon2id$v=19$m=65536,t=3,p=1$SI3ReKgJj40ZjnQa215wIA$ATERr2Dy/AFqAejB5JTz7h7Ctf45mfwdd0KSKPtIaXM");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("3ff37ac0-75bb-4dc9-9cc8-b5259d01088a"),
                column: "HashSenha",
                value: "123abc");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("7b94e9d7-b3a4-417e-95f8-f26bd261c609"),
                column: "HashSenha",
                value: "123abc");
        }
    }
}
