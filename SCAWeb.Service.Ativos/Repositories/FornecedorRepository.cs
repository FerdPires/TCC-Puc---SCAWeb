using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Entities;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCAWeb.Service.Ativos.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AtivosContext _context;

        public FornecedorRepository(AtivosContext context)
        {
            _context = context;
        }

        public void Create(FornecedorEntity fornecedor)
        {
            _context.Fornecedor.Add(fornecedor);
            _context.SaveChanges();
        }

        public void Delete(FornecedorEntity fornecedor)
        {
            _context.Fornecedor.Remove(fornecedor);
            _context.SaveChanges();
        }

        public IList<FornecedorEntity> GetAll()
        {
            return _context.Fornecedor.AsNoTracking().OrderBy(x => x.razao_social).ToList();
        }

        public FornecedorEntity GetByCnpj(int cnpj)
        {
            return _context.Fornecedor.FirstOrDefault(x => x.cnpj_fornecedor == cnpj);
        }

        public FornecedorEntity GetById(Guid id)
        {
            return _context.Fornecedor.FirstOrDefault(x => x.Id == id);
        }

        public void Update(FornecedorEntity fornecedor)
        {
            _context.Entry(fornecedor).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
