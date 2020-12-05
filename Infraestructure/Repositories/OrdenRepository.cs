using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class OrdenRepository : IAsyncRepository<Orden>
    {
        protected readonly CanastaContext _dbContext;

        public OrdenRepository(CanastaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Orden> AddAsync(Orden entity)
        {
            await _dbContext.orden.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public void DeleteAsync(Orden entity)
        {
            try
            {
                //_dbContext.Remove(entity);
                _dbContext.orden.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Orden GetById(int id)
        {
            return _dbContext.orden.Find(id);
        }

        public async Task<Orden> GetByIdAsync(int id)
        {
            return await _dbContext.orden.FindAsync(id);
        }

        public async Task<IReadOnlyList<Orden>> ListAllAsync()
        {
            return await _dbContext.orden.ToListAsync();
        }

        public void UpdateAsync(Orden entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
