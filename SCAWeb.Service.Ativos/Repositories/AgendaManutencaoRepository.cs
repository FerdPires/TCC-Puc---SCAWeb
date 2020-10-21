using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Util.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Ativos.Repositories
{
    public class AgendaManutencaoRepository : IAgendaManutencaoRepository
    {
        private readonly AtivosContext _context;

        public AgendaManutencaoRepository(AtivosContext context)
        {
            _context = context;
        }

        public void AgendaManutencaoCreate(AgendaManutencaoEntity agendaManutencao)
        {
            _context.AgendaManutencao.Add(agendaManutencao);
            _context.SaveChanges();
        }

        public AgendaManutencaoEntity GetById(Guid id)
        {
            return _context.AgendaManutencao.FirstOrDefault(x => x.Id == id);
        }

        public AgendaManutencaoEntity GetByInsumo(Guid id)
        {
            return _context.AgendaManutencao.FirstOrDefault(x => x.id_insumo == id);
        }

        public void Delete(AgendaManutencaoEntity agendaManutencao)
        {
            _context.AgendaManutencao.Remove(agendaManutencao);
            _context.SaveChanges();
        }

        public void Update(AgendaManutencaoEntity agendaManutencao)
        {
            _context.Entry(agendaManutencao).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IList<AgendaManutencaoEntity> GetAll()
        {
            return _context.AgendaManutencao.AsNoTracking()
                .OrderBy(x => x.data_manutencao).ThenBy(x => x.status_agenda).ToList();
        }

        public IList<AgendaManutencaoEntity> GetAllToday()
        {
            return _context.AgendaManutencao.AsNoTracking()
                .Where(x => x.data_manutencao == DateTime.Today && x.status_agenda == StatusAgendaManut.Aberto).ToList();
        }

        public IList<AgendaManutencaoEntity> GetAllAberto()
        {
            return _context.AgendaManutencao.AsNoTracking().Where(x => x.status_agenda == StatusAgendaManut.Aberto)
                .OrderBy(x => x.data_manutencao).ToList();
        }

        public IList<AgendaManutencaoEntity> GetAllFechado()
        {
            return _context.AgendaManutencao.AsNoTracking().Where(x => x.status_agenda == StatusAgendaManut.Fechado)
                .OrderBy(x => x.data_manutencao).ToList();
        }

        public IList<AgendaManutencaoEntity> GetAllCorretiva()
        {
            return _context.AgendaManutencao.AsNoTracking().Where(x => x.tipo_manutencao == TipoManutencao.Corretiva)
                .OrderBy(x => x.data_manutencao).ThenBy(x => x.status_agenda).ToList();
        }

        public IList<AgendaManutencaoEntity> GetAllPreventiva()
        {
            return _context.AgendaManutencao.AsNoTracking().Where(x => x.tipo_manutencao == TipoManutencao.Preventiva)
                .OrderBy(x => x.data_manutencao).ThenBy(x => x.status_agenda).ToList();
        }
    }
}
