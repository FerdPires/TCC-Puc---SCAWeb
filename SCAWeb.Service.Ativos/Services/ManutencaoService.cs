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
    public class ManutencaoService : Notifiable, IManutencaoService
    {
        private readonly IManutencaoRepository _manutencaoRepository;
        private readonly IAgendaManutencaoRepository _agendaManutRepository;
        private readonly IInsumoRepository _insumoRepository;

        public ManutencaoService(IManutencaoRepository manutencaoRepository, IInsumoRepository insumoRepository, IAgendaManutencaoRepository agendaManutRepository)
        {
            _manutencaoRepository = manutencaoRepository;
            _agendaManutRepository = agendaManutRepository;
            _insumoRepository = insumoRepository;
        }

        public IServiceActionResult ManutencaoCreate(ManutencaoEntity manutencaoEntity)
        {
            manutencaoEntity.Validate();

            if (manutencaoEntity.Invalid)
                return new ServiceActionResult(false, "Algo deu errado ao criar a manutenção!", manutencaoEntity.Notifications);

            var insumo = _insumoRepository.GetById(manutencaoEntity.id_insumo);

            if (insumo == null)
                return new ServiceActionResult(false, "O Insumo para qual você está tentando realizar a manutenção não existe!", null);

            var manutencao = new ManutencaoEntity
            (
                manutencaoEntity.tipo_manutencao,
                manutencaoEntity.descricao_manutencao,
                StatusManutencao.Iniciada,
                DateTime.Now,
                DateTime.Now,
                insumo.Id,
                manutencaoEntity.user
            );
            _manutencaoRepository.ManutencaoCreate(manutencao);

            insumo.UpdateInsumo
            (
                insumo.descricao_insumo,
                StatusInsumo.Manutencao,
                insumo.qtd_dias_manut_prev,
                DateTime.Now,
                insumo.user
            );
            _insumoRepository.Update(insumo);

            var agendaManut = _agendaManutRepository.GetByInsumo(insumo.Id);

            if (agendaManut != null)
            {
                agendaManut.UpdateAgendaManut
                (
                    StatusAgendaManut.Fechado,
                    agendaManut.data_manutencao,
                    insumo.data_atualizacao,
                    manutencaoEntity.user
                );
                _agendaManutRepository.Update(agendaManut);
            }

            return new ServiceActionResult(true, "Manutenção criada!", manutencao);
        }

        public IServiceActionResult ManutencaoCorretivaUpdate(ManutencaoEntity manutencaoEntity)
        {
            manutencaoEntity.Validate();

            if (manutencaoEntity.Invalid && manutencaoEntity.tipo_manutencao != TipoManutencao.Corretiva)
                return new ServiceActionResult(false, "Algo deu errado ao editar a manutenção!", manutencaoEntity.Notifications);

            var insumo = _insumoRepository.GetById(manutencaoEntity.id_insumo);

            if (insumo == null)
                return new ServiceActionResult(false, "O Insumo para qual você está tentando editar a manutenção não existe!", null);

            var manutencao = _manutencaoRepository.GetById(manutencaoEntity.Id);

            if (manutencao == null)
                return new ServiceActionResult(false, "A manutenção para qual você está tentando editar não existe!", null);

            manutencao.UpdateManutencao
            (
                manutencaoEntity.descricao_manutencao,
                manutencaoEntity.status_manutencao,
                (manutencaoEntity.status_manutencao == StatusManutencao.Concluida) ? DateTime.Now : manutencaoEntity.data_fim,
                manutencao.user
            );
            _manutencaoRepository.Update(manutencao);

            if(manutencaoEntity.status_manutencao == StatusManutencao.Concluida)
            {
                insumo.UpdateInsumo
                (
                    insumo.descricao_insumo,
                    StatusInsumo.Ativo,
                    insumo.qtd_dias_manut_prev,
                    insumo.data_atualizacao,
                    insumo.user
                );
                _insumoRepository.Update(insumo);

                return new ServiceActionResult(true, "Manutenção finalizada!", manutencao);
            }              
            else
                return new ServiceActionResult(true, "Manutenção salva!", manutencao);
        }

        public IServiceActionResult ManutencaoPreventivaUpdate(ManutencaoEntity manutencaoEntity)
        {
            manutencaoEntity.Validate();

            if (manutencaoEntity.Invalid && manutencaoEntity.tipo_manutencao != TipoManutencao.Preventiva)
                return new ServiceActionResult(false, "Algo deu errado ao editar a manutenção!", manutencaoEntity.Notifications);

            var insumo = _insumoRepository.GetById(manutencaoEntity.id_insumo);

            if (insumo == null)
                return new ServiceActionResult(false, "O Insumo para qual você está tentando editar a manutenção não existe!", null);

            var manutencao = _manutencaoRepository.GetById(manutencaoEntity.Id);

            if (manutencao == null)
                return new ServiceActionResult(false, "A manutenção para qual você está tentando editar não existe!", null);

            manutencao.UpdateManutencao
            (
                manutencaoEntity.descricao_manutencao,
                manutencaoEntity.status_manutencao,
                (manutencaoEntity.status_manutencao == StatusManutencao.Concluida) ? DateTime.Now : manutencaoEntity.data_fim,
                manutencao.user
            );
            _manutencaoRepository.Update(manutencao);

            if (manutencaoEntity.status_manutencao == StatusManutencao.Concluida)
            {
                var agendaManut = new AgendaManutencaoEntity
                (
                    TipoManutencao.Preventiva,
                    StatusAgendaManut.Aberto,
                    DateTime.Now.AddDays(insumo.qtd_dias_manut_prev),
                    DateTime.Now,
                    insumo.Id,
                    manutencaoEntity.user
                );
                _agendaManutRepository.AgendaManutencaoCreate(agendaManut);

                insumo.UpdateInsumo
                (
                    insumo.descricao_insumo,
                    StatusInsumo.Ativo,
                    insumo.qtd_dias_manut_prev,
                    insumo.data_atualizacao,
                    insumo.user
                );
                _insumoRepository.Update(insumo);

                return new ServiceActionResult(true, "Manutenção finalizada!", manutencao);
            }
            else
                return new ServiceActionResult(true, "Manutenção salva!", manutencao);
        }
    }
}
