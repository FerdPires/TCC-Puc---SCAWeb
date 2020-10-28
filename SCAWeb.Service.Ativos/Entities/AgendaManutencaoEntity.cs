using Flunt.Notifications;
using Flunt.Validations;
using SCAWeb.Service.Ativos.Util.Enums;
using System;

namespace SCAWeb.Service.Ativos.Entities
{
    public class AgendaManutencaoEntity : Notifiable, IValidatable
    {
        public AgendaManutencaoEntity()
        {

        }

        public AgendaManutencaoEntity(TipoManutencao tipoManutencao, StatusAgendaManut statusAgenda, DateTime dataManutencao,
            DateTime dataAtualizacao, Guid idInsumo, string User)
        {
            Id = Guid.NewGuid();
            tipo_manutencao = tipoManutencao;
            status_agenda = statusAgenda;
            data_manutencao = dataManutencao;
            data_atualizacao = dataAtualizacao;
            id_insumo = idInsumo;
            user = User;
        }

        public Guid Id { get; set; }
        public TipoManutencao tipo_manutencao { get; set; } 
        public StatusAgendaManut status_agenda { get; set; }
        public DateTime data_manutencao { get; set; }
        public DateTime data_atualizacao { get; set; }
        public Guid id_insumo { get; set; }
        public string user { get; set; }

        public void UpdateAgendaManut(StatusAgendaManut statusAgenda, DateTime dataManutencao, DateTime dataAtualizacao, string User)
        {
            status_agenda = statusAgenda;
            data_manutencao = dataManutencao;
            data_atualizacao = dataAtualizacao;
            user = User;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNull(tipo_manutencao, "Tipo Manutenção", "O tipo da manutenção não pode ser vazio.")
                    .IsNotNull(data_manutencao, "Data Manutenção", "Favor informar a data do agendamento da manutenção.")
                    .IsNotNull(id_insumo, "Insumo", "Favor informar o Insumo a ser feito a manutenção.")
             );
        }
    }
}
