using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Ativos.Repositories
{
    public class InsumoRepository : IInsumoRepository
    {
        private readonly AtivosContext _context;

        public InsumoRepository(AtivosContext context)
        {
            _context = context;
        }

        public void Create(InsumoEntity insumo)
        {
            _context.Insumos.Add(insumo);
            _context.SaveChanges();
        }

        public void Delete(InsumoEntity insumo)
        {
            _context.Insumos.Remove(insumo);
            _context.SaveChanges();
        }

        public IList<InsumoEntity> GetAll()
        {
            return _context.Insumos.AsNoTracking().OrderBy(x => x.descricao_insumo).ToList();
        }

        public InsumoEntity GetById(Guid id)
        {
            return _context.Insumos.FirstOrDefault(x => x.Id == id);
        }

        public void Update(InsumoEntity insumo)
        {
            _context.Entry(insumo).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
