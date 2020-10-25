using SCAWeb.Service.Ativos.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{
    public interface IFornecedorRepository
    {
        void Create(FornecedorEntity fornecedor);
        void Update(FornecedorEntity fornecedor);
        void Delete(FornecedorEntity fornecedor);
        FornecedorEntity GetByCnpj(string cnpj);
        FornecedorEntity GetById(Guid id);
        IList<FornecedorEntity> GetAll();
    }
}
