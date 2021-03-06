﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        //Task DeleteAsync(T entity);
        T GetById(int id);
        void DeleteAsync(T entity);
    }
}
