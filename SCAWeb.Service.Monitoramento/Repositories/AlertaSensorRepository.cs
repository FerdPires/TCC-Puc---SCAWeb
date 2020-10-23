using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Monitoramento.Data;
using SCAWeb.Service.Monitoramento.Entities;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Monitoramento.Repositories
{
    public class AlertaSensorRepository : IAlertaSensorRepository
    {
        private readonly MonitoramentoContext _context;

        public AlertaSensorRepository(MonitoramentoContext context)
        {
            _context = context;
        }

        public IList<AlertaSensorEntity> GetAllBySensor(Guid id)
        {
            return _context.Alertas.AsNoTracking().Where(x => x.id_sensor == id)
                .OrderBy(x => x.data_atualizacao).ThenBy(x => x.tipo_aletra).ToList();
        }

        public AlertaSensorEntity GetById(Guid id)
        {
            return _context.Alertas.FirstOrDefault(x => x.Id == id);
        }
    }
}
