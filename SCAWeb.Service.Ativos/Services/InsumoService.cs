using Flunt.Notifications;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services
{
    public class InsumoService : Notifiable, IInsumoService
    {
        private readonly IInsumoRepository _insumoRepository;
        private readonly ITipoInsumoRepository _tipoInsumoRepository;

        public InsumoService(IInsumoRepository insumoRepository, ITipoInsumoRepository tipoInsumoRepository)
        {
            _insumoRepository = insumoRepository;
            _tipoInsumoRepository = tipoInsumoRepository;
        }

        public IServiceActionResult CreateInsumo(InsumoEntity insumoEntity)
        {
            insumoEntity.Validate();

            if (insumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", insumoEntity.Notifications);

            var tipoInsumo = _tipoInsumoRepository.GetById(insumoEntity.tipo_insumo);

            if (tipoInsumo != null)
            {
                var insumo = new InsumoEntity
                (
                    insumoEntity.descricao_insumo,
                    insumoEntity.status_insumo,
                    insumoEntity.data_aquisicao.AddDays(tipoInsumo.qtd_dias_manut_prev), //data_manut_prev só será mostrado para alterar na edição
                    insumoEntity.data_aquisicao,
                    DateTime.Now,
                    tipoInsumo.qtd_dias_manut_prev,
                    insumoEntity.tipo_insumo,
                    insumoEntity.fornec_insumo,
                    insumoEntity.user
                );

                _insumoRepository.Create(insumo);

                return new ServiceActionResult(true, "Insumo criado!", insumo);
            }

            return new ServiceActionResult(false, "Algo deu errado", null);
        }

        public IServiceActionResult DeleteInsumo(InsumoEntity insumoEntity)
        {
            insumoEntity.Validate();

            if (insumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao excluir!", insumoEntity.Notifications);

            var insumo = _insumoRepository.GetById(insumoEntity.Id);

            _insumoRepository.Delete(insumo);

            return new ServiceActionResult(true, "Insumo excluído!", insumo);
        }

        public IServiceActionResult UpdateInsumo(InsumoEntity insumoEntity)
        {
            insumoEntity.Validate();

            if (insumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", insumoEntity.Notifications);

            var insumo = _insumoRepository.GetById(insumoEntity.Id);

            insumo.UpdateInsumo
            (
                insumoEntity.descricao_insumo,
                insumoEntity.status_insumo,
                insumoEntity.data_manut_prev,
                DateTime.Now,
                insumoEntity.user
            );

            _insumoRepository.Update(insumo);

            return new ServiceActionResult(true, "Insumo salvo!", insumo);
        }
    }
}
