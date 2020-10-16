using Flunt.Notifications;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services
{
    public class TipoInsumoService : Notifiable, ITipoInsumoService
    {
        private readonly ITipoInsumoRepository _tipoInsumoRepository;

        public TipoInsumoService(ITipoInsumoRepository tipoInsumoRepository)
        {
            _tipoInsumoRepository = tipoInsumoRepository;
        }

        public IServiceActionResult CreateTipoInsumo(TipoInsumoEntity tipoInsumoEntity)
        {
            tipoInsumoEntity.Validate();

            if (tipoInsumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", tipoInsumoEntity.Notifications);

            var tipoInsumo = new TipoInsumoEntity
            (
                tipoInsumoEntity.descricao_tp_insumo,
               // tipoInsumoEntity.qtd_dias_manut_prev, //validar essa data!!!! pq o tipo do insumo define os dias de manut do insumo?
                DateTime.Now,
                tipoInsumoEntity.user
            );

            _tipoInsumoRepository.Create(tipoInsumo);

            return new ServiceActionResult(true, "Tipo de Insumo criado!", tipoInsumo);
        }

        public IServiceActionResult DeleteTipoInsumo(Guid id)
        {
            var tipoInsumo = _tipoInsumoRepository.GetById(id);

            if (tipoInsumo == null)
                return new ServiceActionResult(false, "O registro que você está excluindo não existe!", null);

            _tipoInsumoRepository.Delete(tipoInsumo);

            return new ServiceActionResult(true, "Tipo de Insumo excluído!", tipoInsumo);
        }

        public IServiceActionResult UpdateTipoInsumo(TipoInsumoEntity tipoInsumoEntity)
        {
            tipoInsumoEntity.Validate();

            if (tipoInsumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", tipoInsumoEntity.Notifications);

            var tipoInsumo = _tipoInsumoRepository.GetById(tipoInsumoEntity.Id);

            if (tipoInsumo == null)
                return new ServiceActionResult(false, "O registro que você está editando não existe!", null);

            tipoInsumo.UpdateTipoInsumo
            (
                tipoInsumoEntity.descricao_tp_insumo,
                DateTime.Now,
                tipoInsumoEntity.user
            );

            _tipoInsumoRepository.Update(tipoInsumo);

            return new ServiceActionResult(true, "Tipo do Insumo salvo!", tipoInsumo);
        }
    }
}
