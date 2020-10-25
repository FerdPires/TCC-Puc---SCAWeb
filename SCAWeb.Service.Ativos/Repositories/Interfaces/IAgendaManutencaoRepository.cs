using SCAWeb.Service.Ativos.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{
    public interface IAgendaManutencaoRepository
    {
        void AgendaManutencaoCreate(AgendaManutencaoEntity agendaManutencao);
        AgendaManutencaoEntity GetById(Guid id);
        AgendaManutencaoEntity GetByInsumo(Guid id);
        void Delete(AgendaManutencaoEntity agendaManutencao);
        void Update(AgendaManutencaoEntity agendaManutencao);
        IList<AgendaManutencaoEntity> GetAll();
        IList<AgendaManutencaoEntity> GetAllUntilToday();
        IList<AgendaManutencaoEntity> GetAllAberto();
        IList<AgendaManutencaoEntity> GetAllFechado();
        IList<AgendaManutencaoEntity> GetAllCorretiva();
        IList<AgendaManutencaoEntity> GetAllPreventiva();
    }
}
