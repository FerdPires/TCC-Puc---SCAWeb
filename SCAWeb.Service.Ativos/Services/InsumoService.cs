using Flunt.Notifications;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services.Interfaces;
using SCAWeb.Service.Ativos.Util;
using SCAWeb.Service.Ativos.Util.Enums;
using SCAWeb.Service.Ativos.Util.Interfaces;
using System;

namespace SCAWeb.Service.Ativos.Services
{
    public class InsumoService : Notifiable, IInsumoService
    {
        private readonly IInsumoRepository _insumoRepository;
        private readonly IAgendaManutencaoRepository _agendaManutRepository;      

        public InsumoService(IInsumoRepository insumoRepository, IAgendaManutencaoRepository agendaManutRepository)
        {
            _insumoRepository = insumoRepository;
            _agendaManutRepository = agendaManutRepository;
        }

        public IServiceActionResult CreateInsumo(InsumoEntity insumoEntity)
        {
            insumoEntity.Validate();

            if (insumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", insumoEntity.Notifications);

            var insumoNew = new InsumoEntity
            (
                insumoEntity.descricao_insumo,
                StatusInsumo.Ativo,
                insumoEntity.data_aquisicao,
                DateTime.Now,
                insumoEntity.qtd_dias_manut_prev,
                insumoEntity.id_tipo_insumo,
                insumoEntity.id_fornec_insumo,
                insumoEntity.user
            );

            _insumoRepository.Create(insumoNew);

            var insumo = _insumoRepository.GetById(insumoNew.Id);

            if(insumo != null)
            {
                var agendaManut = new AgendaManutencaoEntity
                (
                    TipoManutencao.Preventiva,
                    StatusAgendaManut.Aberto,
                    insumo.data_aquisicao.AddDays(insumo.qtd_dias_manut_prev),
                    DateTime.Now,
                    insumo.Id,
                    insumo.user
                );

                _agendaManutRepository.AgendaManutencaoCreate(agendaManut);
            }
            else
                return new ServiceActionResult(false, "Algo deu errado ao incluir!", null);

            return new ServiceActionResult(true, "Insumo criado!", insumo);
        }

        public IServiceActionResult DeleteInsumo(InsumoEntity insumoEntity)
        {
            var insumo = _insumoRepository.GetById(insumoEntity.Id);

            if (insumo == null)
                return new ServiceActionResult(false, "O registro que você está tentando desativar não existe!", null);

            insumo.DisableInsumo
            (
                DateTime.Now,
                insumoEntity.user
            );

            _insumoRepository.Update(insumo);

            return new ServiceActionResult(true, "Insumo excluído!", insumo);
        }

        public IServiceActionResult UpdateInsumo(InsumoEntity insumoEntity)
        {
            insumoEntity.Validate();

            if (insumoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", insumoEntity.Notifications);

            var insumo = _insumoRepository.GetById(insumoEntity.Id);

            if (insumo == null)
                return new ServiceActionResult(false, "O registro que você está editando não existe!", null);

            insumo.UpdateInsumo
            (
                insumoEntity.descricao_insumo,
                insumoEntity.status_insumo,
                insumoEntity.qtd_dias_manut_prev,
                DateTime.Now,
                insumoEntity.user
            );

            _insumoRepository.Update(insumo);

            if(insumoEntity.status_insumo == StatusInsumo.Inativo)
                return new ServiceActionResult(true, "Insumo desativado!", insumo);

            return new ServiceActionResult(true, "Insumo salvo!", insumo);
        }
    }
}
