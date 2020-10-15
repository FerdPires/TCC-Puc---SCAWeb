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
                tipoInsumoEntity.status, // criar enum
                tipoInsumoEntity.qtd_dias_manut_prev, //validar essa data!!!! pq o tipo do insumo define os dias de manut do insumo?
                DateTime.Now,
                tipoInsumoEntity.user
            );

            _tipoInsumoRepository.Create(tipoInsumo);

            return new ServiceActionResult(true, "Tipo de Insumo criado!", tipoInsumo);
        }

        public IServiceActionResult DeleteTipoInsumo(TipoInsumoEntity tipoInsumoEntity)
        {
            tipoInsumoEntity.Validate();

            if (tipoInsumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao excluir!", tipoInsumoEntity.Notifications);

            var insumo = _tipoInsumoRepository.GetById(tipoInsumoEntity.Id);

            _tipoInsumoRepository.Delete(insumo);

            return new ServiceActionResult(true, "Tipo de Insumo excluído!", insumo);
        }

        public IServiceActionResult UpdateTipoInsumo(TipoInsumoEntity tipoInsumoEntity)
        {
            tipoInsumoEntity.Validate();

            if (tipoInsumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", tipoInsumoEntity.Notifications);

            var tipoInsumo = _tipoInsumoRepository.GetById(tipoInsumoEntity.Id);

            tipoInsumo.UpdateTipoInsumo
            (
                tipoInsumoEntity.descricao_tp_insumo,
                tipoInsumoEntity.status,
                tipoInsumoEntity.qtd_dias_manut_prev,
                DateTime.Now,
                tipoInsumoEntity.user
            );

            _tipoInsumoRepository.Update(tipoInsumo);

            return new ServiceActionResult(true, "Tipo do Insumo salvo!", tipoInsumo);
        }
    }
}
