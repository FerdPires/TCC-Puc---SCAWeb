using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    interface ITipoInsumoService
    {
        IServiceActionResult CreateTipoInsumo(TipoInsumoEntity tipoInsumo);
        IServiceActionResult UpdateTipoInsumo(TipoInsumoEntity tipoInsumo);
        IServiceActionResult DeleteTipoInsumo(TipoInsumoEntity tipoInsumo);
    }
}
