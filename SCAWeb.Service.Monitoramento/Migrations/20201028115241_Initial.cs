using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAWeb.Service.Monitoramento.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    tipo_aletra = table.Column<int>(nullable: false),
                    descricao_alerta = table.Column<string>(nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_sensor = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nome_barragem = table.Column<string>(nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nome_sensor = table.Column<string>(nullable: false),
                    status_sensor = table.Column<int>(nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_area = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Sensores");
        }
    }
}
