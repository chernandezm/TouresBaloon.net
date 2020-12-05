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
    public class CanastaRepository : IAsyncRepository<Canasta>
    {
        protected readonly CanastaContext _dbContext;

        public CanastaRepository(CanastaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Canasta> AddAsync(Canasta entity)
        {
            await _dbContext.canasta.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }


        public void DeleteAsync(Canasta entity)
        {
            try
            {
                //_dbContext.Remove(entity);
                 _dbContext.canasta.Remove(entity);
                 _dbContext.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        public async Task<Canasta> GetByIdAsync(int id)
        {
            return await _dbContext.canasta.FindAsync(id);
        }

        public Canasta GetById(int id)
        {
            return _dbContext.canasta.Find(id);
        }

        public async Task<IReadOnlyList<Canasta>> ListAllAsync()
        {
            return await _dbContext.canasta.ToListAsync();
        }

        public void UpdateAsync(Canasta entity)
        {
            //_dbContext.canasta.Update(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
