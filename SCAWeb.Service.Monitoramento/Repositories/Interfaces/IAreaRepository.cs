using SCAWeb.Service.Monitoramento.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Monitoramento.Repositories.Interfaces
{
    public interface IAreaRepository
    {
        IList<AreaEntity> GetAll();
        AreaEntity GetById(Guid id);
    }
}
