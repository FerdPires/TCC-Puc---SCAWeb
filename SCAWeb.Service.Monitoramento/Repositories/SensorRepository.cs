using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Monitoramento.Data;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Monitoramento.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly MonitoramentoContext _context;

        public SensorRepository(MonitoramentoContext context)
        {
            _context = context;
        }

        public IList<SensorEntity> GetAllByArea(Guid id)
        {
            return _context.Sensores.AsNoTracking().Where(x => x.id_area == id)
                .OrderBy(x => x.status_sensor).ThenBy(x => x.nome_sensor).ToList();
        }

        public SensorEntity GetById(Guid id)
        {
            return _context.Sensores.FirstOrDefault(x => x.Id == id);
        }
    }
}
