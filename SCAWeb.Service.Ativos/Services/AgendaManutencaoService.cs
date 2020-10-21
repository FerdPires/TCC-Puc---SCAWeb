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
    public class AgendaManutencaoService : Notifiable, IAgendaManutencaoService
    {
        private readonly IAgendaManutencaoRepository _agendaManutRepository;
        private readonly IInsumoRepository _insumoRepository;

        public AgendaManutencaoService(IAgendaManutencaoRepository agendaManutRepository, IInsumoRepository insumoRepository)
        {
            _agendaManutRepository = agendaManutRepository;
            _insumoRepository = insumoRepository;
        }

        public IServiceActionResult AgendaManutencaoCorretiva(AgendaManutencaoEntity agendaManutEntity)
        {
            agendaManutEntity.Validate();

            if (agendaManutEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao agendar a manutenção!", agendaManutEntity.Notifications);

            if(agendaManutEntity.tipo_manutencao == TipoManutencao.Preventiva)
                return new ServiceActionResult(false, "Não é possível agendar uma manutenção preventiva! Este tipo de agendamento é feito de forma automática.", null);

            var insumo = _insumoRepository.GetById(agendaManutEntity.id_insumo);

            if (insumo == null)
                return new ServiceActionResult(false, "O Insumo para qual você está tentando agendar a manutenção não existe!", null);

            var agendaManut = new AgendaManutencaoEntity
            (
                TipoManutencao.Corretiva,
                StatusAgendaManut.Aberto,
                agendaManutEntity.data_manutencao,
                DateTime.Now,
                insumo.Id,
                agendaManutEntity.user
            );

            _agendaManutRepository.AgendaManutencaoCreate(agendaManut);

            return new ServiceActionResult(true, "Manutenção corretiva agendada!", agendaManut);
        }

        public IServiceActionResult AgendaManutencaoPreventiva(InsumoEntity insumo)
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

            return new ServiceActionResult(true, "Manutenção preventiva agendada!", agendaManut);
        }

        public IServiceActionResult DeleteAgendaManutencaoCorretiva(Guid id)
        {
            var agendaManut = _agendaManutRepository.GetById(id);

            if (agendaManut == null)
                return new ServiceActionResult(false, "O agendamento que você está excluindo não existe!", null);

            if (agendaManut.tipo_manutencao == TipoManutencao.Preventiva)
                return new ServiceActionResult(false, "Não é possível excluir uma manutenção corretiva!", null);

            _agendaManutRepository.Delete(agendaManut);

            return new ServiceActionResult(true, "Agendamento excluído!", agendaManut);
        }

        public IServiceActionResult UpdateAgendaManutencaoCorretiva(AgendaManutencaoEntity agendaManutEntity)
        {
            agendaManutEntity.Validate();

            if (agendaManutEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao editar!", agendaManutEntity.Notifications);

            var agendaManut = _agendaManutRepository.GetById(agendaManutEntity.Id);

            if (agendaManut == null)
                return new ServiceActionResult(false, "O agendamento que você está editando não existe!", null);

            if(agendaManut.tipo_manutencao == TipoManutencao.Preventiva)
                return new ServiceActionResult(false, "Você está tentando alterar a data do agendamento de uma manutenção preventiva e isso não é possível!", null);

            agendaManut.UpdateAgendaManut
            (
                agendaManutEntity.status_agenda,
                agendaManutEntity.data_manutencao,
                DateTime.Now,
                agendaManutEntity.user
            );

            _agendaManutRepository.Update(agendaManut);

            return new ServiceActionResult(true, "Agendamento salvo!", agendaManut);
        }

        //public IServiceActionResult UpdateAgendaManutencaoPreventiva(ManutencaoEntity manutencaoEntity)
        //{
        //    var insumo = _insumoRepository.GetById(manutencaoEntity.Id);
        //    var agendaManut = _agendaManutRepository.GetById(manutencaoEntity.Id);

        //    if (agendaManut == null || insumo == null)
        //        return new ServiceActionResult(false, "Ocorreu algum erro ao localizar os dados para novo agendamento de manutenção preventiva.", null);

        //    agendaManut.UpdateAgendaManut
        //    (
        //        StatusAgendaManut.Agendada,
        //        manutencaoEntity.data_fim.AddDays(insumo.qtd_dias_manut_prev),
        //        DateTime.Now,
        //        insumo.user
        //    );

        //    _agendaManutRepository.Update(agendaManut);

        //    return new ServiceActionResult(true, "Agendamento salvo!", agendaManut);
        //}
    }
}
