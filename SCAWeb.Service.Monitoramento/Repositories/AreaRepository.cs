using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Monitoramento.Data;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Monitoramento.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly MonitoramentoContext _context;

        public AreaRepository(MonitoramentoContext context)
        {
            _context = context;
        }

        public IList<AreaEntity> GetAll()
        {
            return _context.Areas.AsNoTracking().OrderBy(x => x.nome_barragem).ToList();
        }

        public AreaEntity GetById(Guid id)
        {
            return _context.Areas.FirstOrDefault(x => x.Id == id);
        }
    }
}
