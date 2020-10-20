using SCAWeb.Service.Ativos.Entities;
using System;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{
    public interface IAgendaManutencaoRepository
    {
        void AgendaManutencaoCreate(AgendaManutencaoEntity agendaManutencao);
        AgendaManutencaoEntity GetById(Guid id);
        void Delete(AgendaManutencaoEntity agendaManutencao);
        void Update(AgendaManutencaoEntity agendaManutencao);
    }
}
