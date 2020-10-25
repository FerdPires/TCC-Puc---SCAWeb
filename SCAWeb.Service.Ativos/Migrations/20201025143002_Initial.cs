using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAWeb.Service.Ativos.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaManutencao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    tipo_manutencao = table.Column<int>(nullable: false),
                    status_agenda = table.Column<int>(nullable: false),
                    data_manutencao = table.Column<DateTime>(type: "datetime", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_insumo = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaManutencao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    cnpj_fornecedor = table.Column<string>(nullable: false),
                    nome_fantasia = table.Column<string>(nullable: false),
                    razao_social = table.Column<string>(nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    descricao_insumo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status_insumo = table.Column<int>(nullable: false),
                    data_aquisicao = table.Column<DateTime>(type: "datetime", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    qtd_dias_manut_prev = table.Column<int>(nullable: false),
                    id_tipo_insumo = table.Column<Guid>(nullable: false),
                    id_fornec_insumo = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manutencao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    tipo_manutencao = table.Column<int>(nullable: false),
                    descricao_manutencao = table.Column<string>(nullable: false),
                    status_manutencao = table.Column<int>(nullable: false),
                    data_inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    data_fim = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_insumo = table.Column<Guid>(nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manutencao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoInsumo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    descricao_tp_insumo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime", nullable: false),
                    user = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoInsumo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaManutencao");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Manutencao");

            migrationBuilder.DropTable(
                name: "TipoInsumo");
        }
    }
}
