using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Util.Interfaces;

namespace SCAWeb.Service.Ativos.Services.Interfaces
{
    public interface IFornecedorService
    {
        IServiceActionResult CreateFornecedor(FornecedorEntity fornecedor);
        IServiceActionResult UpdateFornecedor(FornecedorEntity fornecedor);
        IServiceActionResult DeleteFornecedor(FornecedorEntity fornecedor);
    }
}
