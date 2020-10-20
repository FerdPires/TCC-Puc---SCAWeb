using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using System;
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
    }
}
