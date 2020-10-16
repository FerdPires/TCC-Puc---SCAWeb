using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface ITipoInsumoService
    {
        IServiceActionResult CreateTipoInsumo(TipoInsumoEntity tipoInsumo);
        IServiceActionResult UpdateTipoInsumo(TipoInsumoEntity tipoInsumo);
        IServiceActionResult DeleteTipoInsumo(Guid id);
    }
}
