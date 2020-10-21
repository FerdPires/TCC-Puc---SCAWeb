using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Util.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Ativos.Repositories
{
    public class ManutencaoRepository : IManutencaoRepository
    {
        private readonly AtivosContext _context;

        public ManutencaoRepository(AtivosContext context)
        {
            _context = context;
        }

        public void ManutencaoCreate(ManutencaoEntity manutencao)
        {
            _context.Manutencao.Add(manutencao);
            _context.SaveChanges();
        }

        public ManutencaoEntity GetById(Guid id)
        {
            return _context.Manutencao.FirstOrDefault(x => x.Id == id);
        }

        public void Update(ManutencaoEntity manutencao)
        {
            _context.Entry(manutencao).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IList<ManutencaoEntity> GetAll()
        {
            return _context.Manutencao.AsNoTracking()
                .OrderBy(x => x.data_inicio).ThenBy(x => x.status_manutencao).ToList();
        }

        public IList<ManutencaoEntity> GetAllIniciada()
        {
            return _context.Manutencao.AsNoTracking().Where(x => x.status_manutencao == StatusManutencao.Iniciada)
                .OrderBy(x => x.data_inicio).ToList();
        }

        public IList<ManutencaoEntity> GetAllConcluida()
        {
            return _context.Manutencao.AsNoTracking().Where(x => x.status_manutencao == StatusManutencao.Concluida)
                .OrderBy(x => x.data_inicio).ToList();
        }

        public IList<ManutencaoEntity> GetAllCorretiva()
        {
            return _context.Manutencao.AsNoTracking().Where(x => x.tipo_manutencao == TipoManutencao.Corretiva)
                .OrderBy(x => x.data_inicio).ThenBy(x => x.status_manutencao).ToList();
        }

        public IList<ManutencaoEntity> GetAllPreventiva()
        {
            return _context.Manutencao.AsNoTracking().Where(x => x.tipo_manutencao == TipoManutencao.Preventiva)
                .OrderBy(x => x.data_inicio).ThenBy(x => x.status_manutencao).ToList();
        }
    }
}
