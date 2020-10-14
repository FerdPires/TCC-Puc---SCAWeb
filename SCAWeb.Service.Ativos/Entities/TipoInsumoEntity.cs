using Flunt.Validations;
using System;

namespace SCAWeb.Service.Ativos.Entities
{
    public class TipoInsumoEntity : Entity, IValidatable
    {
        public TipoInsumoEntity()
        {

        }

        public TipoInsumoEntity(string descricaoTpInsumo, bool Status, int qtdDiasManutPrev, DateTime dataAtualizacao, string User)
        {
            descricao_tp_insumo = descricaoTpInsumo;
            status = Status;
            qtd_dias_manut_prev = qtdDiasManutPrev;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public string descricao_tp_insumo { get; private set; }
        public bool status { get; private set; }
        public int qtd_dias_manut_prev { get; private set; }
        public DateTime data_atualizacao { get; private set; }
        public string user { get; private set; }

        public void UpdateTipoInsumoEntity(string descricaoTpInsumo, bool Status, int qtdDiasManutPrev, DateTime dataAtualizacao, string User)
        {
            descricao_tp_insumo = descricaoTpInsumo;
            status = Status;
            qtd_dias_manut_prev = qtdDiasManutPrev;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(descricao_tp_insumo, "Tipo Insumo", "Favor informar o Tipo do Insumo.")
                    .IsNotNull(status, "Status Tipo Insumo", "Favor informar o status do Tipo do Insumo.")
                    .IsNotNull(status, "Status Tipo Insumo", "Favor informar o status do Tipo do Insumo.")
                    .IsNotNull(qtd_dias_manut_prev, "Dias Manutenção Preventiva", "Favor informar a quantidade de dias da Manutenção Preventiva.")
                    .HasMaxLen(descricao_tp_insumo, 100, "Tipo Insumo", "O Tipo do Insumo deve conter no máximo 100 caracteres.")
            );
        }
    }
}
