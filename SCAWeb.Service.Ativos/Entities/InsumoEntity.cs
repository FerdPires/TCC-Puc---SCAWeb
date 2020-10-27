using Flunt.Notifications;
using Flunt.Validations;
using SCAWeb.Service.Ativos.Util.Enums;
using System;
using System.Text.Json.Serialization;

namespace SCAWeb.Service.Ativos.Entities
{
    public class InsumoEntity : Notifiable, IValidatable
    {
        public InsumoEntity()
        {

        }

        public InsumoEntity(string descricaoInsumo, StatusInsumo statusInsumo, DateTime dataAquisicao, DateTime dataAtualizacao, int qtdDiasManutPrev, Guid tipoInsumo, Guid fornecInsumo, string User)
        {
            Id = Guid.NewGuid();
            descricao_insumo = descricaoInsumo;
            status_insumo = statusInsumo;
       //     data_manut_prev = dataManutPrev;
            data_aquisicao = dataAquisicao;
            data_atualizacao = dataAtualizacao;
            qtd_dias_manut_prev = qtdDiasManutPrev;
            id_tipo_insumo = tipoInsumo;
            id_fornec_insumo = fornecInsumo;
            user = User;
        }

        public Guid Id { get; set; }
        public string descricao_insumo { get; set; }
        public StatusInsumo status_insumo { get; set; }
        //  public DateTime data_manut_prev { get; private set; } //data calculada, de acordo com qtd_dias_manut_prev. não pode ser modificada
        public DateTime data_aquisicao { get;  set; }
        public DateTime data_atualizacao { get; set; }
        public int qtd_dias_manut_prev { get; set; }
        public Guid id_tipo_insumo { get; set; }
        public Guid id_fornec_insumo { get; set; }
        public string user { get; set; }

        public void UpdateInsumo(string descricaoInsumo, StatusInsumo statusInsumo, int qtdDiasManutPrev, 
            DateTime dataAtualizacao, string User)
        {
            descricao_insumo = descricaoInsumo;
            status_insumo = statusInsumo;
            qtd_dias_manut_prev = qtdDiasManutPrev;
            data_atualizacao = dataAtualizacao;
            user = User;    
        }

        public void DisableInsumo(DateTime dataAtualizacao, string User)
        {
            status_insumo = StatusInsumo.Inativo;
            data_atualizacao = dataAtualizacao;
            user = User;    
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNullOrEmpty(descricao_insumo, "Descrição do Insumo", "Favor informar a Descrição do Insumo.")
                    .IsNotNull(status_insumo, "Status do Insumo", "Favor informar o Status do Insumo.")
                   // .IsNotNull(data_manut_prev, "Manutenção Preventiva", "Favor informar a data da Manutenção Preventiva.")
                    .IsNotNull(data_aquisicao, "Data Aquisição", "A data de aquisição do Insumo não pode ser vazia.")
                    .IsNotNull(qtd_dias_manut_prev, "Dias Manutenção Preventiva", "A quantidade de dias da Manutenção Preventiva não pode ser vazia.")
                    //.IsLowerOrEqualsThan(data_manut_prev, data_aquisicao.AddDays(qtd_dias_manut_prev), "Manutenção Preventida", "A data da Manutenção preventiva deve ser maior que a informada.")
                    .HasMaxLen(descricao_insumo, 100, "Descrição do Insumo", "A Descrição do Insumo deve conter no máximo 100 caracteres.")
            );
        }
    }
}
