using SCAWeb.Service.Ativos.Entities;
using System;
using System.Collections.Generic;

namespace SCAWeb.Service.Ativos.Repositories.Interfaces
{
    public interface IManutencaoRepository
    {
        void ManutencaoCreate(ManutencaoEntity manutencao);
        ManutencaoEntity GetById(Guid id);
        void Update(ManutencaoEntity manutencao);
        IList<ManutencaoEntity> GetAll();
        IList<ManutencaoEntity> GetAllIniciada();
        IList<ManutencaoEntity> GetAllConcluida();
        IList<ManutencaoEntity> GetAllCorretiva();
        IList<ManutencaoEntity> GetAllPreventiva();
    }
}
