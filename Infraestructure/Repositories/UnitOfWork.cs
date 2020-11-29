using ApplicationCore.Interfaces;
using Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CanastaContext _dbContext;

        public UnitOfWork(CanastaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Confirmar()
        {
            _dbContext.SaveChanges();
        }

        public async Task ConfirmarAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}