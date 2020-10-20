using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface IAgendaManutencaoService
    {
        IServiceActionResult AgendaManutencaoCorretiva(AgendaManutencaoEntity agendaEntity);
        IServiceActionResult AgendaManutencaoPreventiva(InsumoEntity insumo);
        IServiceActionResult DeleteAgendaManutencaoCorretiva(Guid id);
        IServiceActionResult UpdateAgendaManutencaoCorretiva(AgendaManutencaoEntity agendaManutEntity);
        IServiceActionResult UpdateAgendaManutencaoPreventiva(ManutencaoEntity manutencaoEntity);
    }
}
