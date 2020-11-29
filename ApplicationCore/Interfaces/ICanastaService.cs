using ApplicationCore.DTO;
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

        public Task UpdateCanastaAsync(int id, CanastaDTO canastaActualizada);

        public void DeleteCanastaAsync(int id);
    }
}
