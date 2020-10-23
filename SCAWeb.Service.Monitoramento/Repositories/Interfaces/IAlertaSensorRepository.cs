using SCAWeb.Service.Monitoramento.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Repositories.Interfaces
{
    public interface IAlertaSensorRepository
    {
        IList<AlertaSensorEntity> GetAllBySensor(Guid id);
        AlertaSensorEntity GetById(Guid id);
    }
}
