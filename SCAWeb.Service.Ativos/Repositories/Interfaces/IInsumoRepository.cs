using SCAWeb.Service.Ativos.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{ 
    public interface IInsumoRepository
    {
        void Create(InsumoEntity insumo);
        void Update(InsumoEntity insumo);
    //    void Delete(InsumoEntity insumo);
        InsumoEntity GetById(Guid id);
        IList<InsumoEntity> GetAll();
        IList<InsumoEntity> GetAllEmManutencao();
        IList<InsumoEntity> GetAllAtivos();
        IList<InsumoEntity> GetAllInativos();
    }
}
