using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface IAgendaManutencaoService
    {
        IServiceActionResult AgendaManutencaoCorretiva(AgendaManutencaoEntity agendaEntity);
        IServiceActionResult AgendaManutencaoPreventiva(InsumoEntity insumo);
        IServiceActionResult DeleteAgendaManutencaoCorretiva(AgendaManutencaoEntity agendaManutEntity);
        IServiceActionResult UpdateAgendaManutencaoCorretiva(AgendaManutencaoEntity agendaManutEntity);
    }
}
