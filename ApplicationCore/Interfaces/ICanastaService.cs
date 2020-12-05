using ApplicationCore.DTO;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICanastaService
    {
        public Task<IEnumerable<CanastaDTO>> GetCanastaAsync();

        public Task<CanastaDTO> GetCanastaAsync(int id);

        public Task<CanastaDTO> CreateCanastaAsync(CanastaDTO canasta);

        public void UpdateCanastaAsync(Canasta canasta);

        public void DeleteCanastaAsync(int id);
    }
}
