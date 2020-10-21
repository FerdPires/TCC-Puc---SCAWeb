using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface IManutencaoService
    {
        IServiceActionResult ManutencaoCreate(ManutencaoEntity manutencaoEntity);
        IServiceActionResult ManutencaoCorretivaUpdate(ManutencaoEntity manutencaoEntity);
        IServiceActionResult ManutencaoPreventivaUpdate(ManutencaoEntity manutencaoEntity);
    }
}
