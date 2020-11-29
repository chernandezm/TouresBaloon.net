using ApplicationCore.DTO;
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

        public Task UpdateOrdenAsync(int id, OrdenDTO ordenActualizada);

        public void DeleteOrdenAsync(int id);
    }
}
