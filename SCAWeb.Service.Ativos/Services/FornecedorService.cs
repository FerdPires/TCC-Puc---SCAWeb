using Flunt.Notifications;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services
{
    public class FornecedorService : Notifiable, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public IServiceActionResult CreateFornecedor(FornecedorEntity fornecedorEntity)
        {
            fornecedorEntity.Validate();

            if (fornecedorEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", fornecedorEntity.Notifications);

            var fornecedorNew = new FornecedorEntity
            (
                fornecedorEntity.cnpj_fornecedor,
                fornecedorEntity.nome_fantasia, 
                fornecedorEntity.razao_social,
                DateTime.Now,
                fornecedorEntity.user
            );

            _fornecedorRepository.Create(fornecedorNew);

            var fornecedor = _fornecedorRepository.GetById(fornecedorNew.Id);

            if(fornecedor == null)
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", null);

            return new ServiceActionResult(true, "Fornecedor criado!", fornecedor);
        }

        public IServiceActionResult DeleteFornecedor(Guid id)
        {
            var fornecedor = _fornecedorRepository.GetById(id);

            if (fornecedor == null)
                return new ServiceActionResult(false, "O registro que você está excluindo não existe!", null);

            _fornecedorRepository.Delete(fornecedor);

            return new ServiceActionResult(true, "Fornecedor excluído!", fornecedor);
        }

        public IServiceActionResult UpdateFornecedor(FornecedorEntity fornecedorEntity)
        {
            fornecedorEntity.Validate();

            if (fornecedorEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", fornecedorEntity.Notifications);

            var fornecedor = _fornecedorRepository.GetById(fornecedorEntity.Id);

            if (fornecedor == null)
                return new ServiceActionResult(false, "O registro que você está editando não existe!", null);

            fornecedor.UpdateFornecedor
            (
                fornecedorEntity.nome_fantasia,
                fornecedorEntity.razao_social,
                DateTime.Now,
                fornecedorEntity.user
            );

            _fornecedorRepository.Update(fornecedor);

            return new ServiceActionResult(true, "Fornecedor salvo!", fornecedor);
        }
    }
}
