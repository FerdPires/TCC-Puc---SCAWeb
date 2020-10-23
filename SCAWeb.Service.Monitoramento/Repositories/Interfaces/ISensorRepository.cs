using SCAWeb.Service.Monitoramento.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Repositories.Interfaces
{
    public interface ISensorRepository
    {
        IList<SensorEntity> GetAllByArea(Guid id);
        SensorEntity GetById(Guid id);
    }
}
