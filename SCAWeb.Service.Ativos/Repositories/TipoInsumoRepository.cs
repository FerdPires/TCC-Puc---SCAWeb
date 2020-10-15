using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Ativos.Repositories
{
    public class TipoInsumoRepository : ITipoInsumoRepository
    {
        private readonly AtivosContext _context;

        public TipoInsumoRepository(AtivosContext context)
        {
            _context = context;
        }

        public void Create(TipoInsumoEntity tipoInsumo)
        {
            _context.TipoInsumo.Add(tipoInsumo);
            _context.SaveChanges();
        }

        public void Delete(TipoInsumoEntity tipoInsumo)
        {
            _context.TipoInsumo.Remove(tipoInsumo);
            _context.SaveChanges();
        }

        public IList<TipoInsumoEntity> GetAll()
        {
            return _context.TipoInsumo.AsNoTracking().OrderBy(x => x.descricao_tp_insumo).ToList();
        }

        public TipoInsumoEntity GetById(Guid id)
        {
            return _context.TipoInsumo.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TipoInsumoEntity tipoInsumo)
        {
            _context.Entry(tipoInsumo).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
