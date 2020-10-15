using SCAWeb.Service.Ativos.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{
    public interface ITipoInsumoRepository
    {
        void Create(TipoInsumoEntity tipoInsumo);
        void Update(TipoInsumoEntity tipoInsumo);
        void Delete(TipoInsumoEntity tipoInsumo);
        TipoInsumoEntity GetById(Guid id);
        IList<TipoInsumoEntity> GetAll();
    }
}
