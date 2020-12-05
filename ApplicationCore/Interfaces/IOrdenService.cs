using ApplicationCore.DTO;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrdenService
    {
        public Task<IEnumerable<OrdenDTO>> GetOrdenAsync();

        public Task<OrdenDTO> GetOrdenAsync(int id);

        public Task<OrdenDTO> CreateOrdenAsync(OrdenDTO orden);

        public void UpdateOrdenAsync(Orden orden);

        public void DeleteOrdenAsync(int id);
    }
}
