using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface IInsumoService
    {
        IServiceActionResult CreateInsumo(InsumoEntity insumo);
        IServiceActionResult UpdateInsumo(InsumoEntity insumo);
        IServiceActionResult DeleteInsumo(Guid id);
    }
}
