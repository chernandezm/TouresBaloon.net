using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class OrdenService : IOrdenService
    {
        private IAsyncRepository<Orden> _repository;
        private IUnitOfWork _unitOfWork;
        private IOrdenConsumer _ordenConsumer;
        private IOrdenPublisher _ordenPublisher;

        public OrdenService(IAsyncRepository<Orden> repository, IUnitOfWork unitOfWork, IOrdenConsumer ordenConsumer, IOrdenPublisher ordenPublisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _ordenConsumer = ordenConsumer;
            _ordenPublisher = ordenPublisher;
        }

        public async Task<OrdenDTO> CreateOrdenAsync(OrdenDTO orden)
        {
            var nuevaOrden = new Orden
            {
                //id_orden = orden.id_orden,
                id_estado = orden.id_estado,
                id_usuario = orden.id_usuario,
                fecha_orden = orden.fecha_orden
            };
            Canasta canasta = _ordenConsumer.ProcesarOrden(nuevaOrden);
            nuevaOrden.id_canasta = canasta.id_canasta;
            nuevaOrden = await _repository.AddAsync(nuevaOrden);
            orden.id_orden = nuevaOrden.id_orden;
            _ordenPublisher.DistribuirOrden(orden);
            return orden;
        }

        public void DeleteOrdenAsync(int id)
        {
            var resultado = _repository.GetById(id);
            if (resultado == null)
                throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);

            _repository.DeleteAsync(resultado);
        }

        public async Task<IEnumerable<OrdenDTO>> GetOrdenAsync()
        {
            var resultado = await _repository.ListAllAsync();
            var ordenes = resultado.Select(orden => new OrdenDTO
            {
                id_orden = orden.id_orden,
                id_estado = orden.id_estado,
                id_usuario = orden.id_usuario,
                fecha_orden = orden.fecha_orden
            });

            return ordenes;
        }

        public async Task<OrdenDTO> GetOrdenAsync(int id)
        {
            var resultado = await _repository.GetByIdAsync(id);
            if (resultado == null)
                throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);
            var orden = new OrdenDTO
            {
                id_orden = resultado.id_orden,
                id_estado = resultado.id_estado,
                id_usuario = resultado.id_usuario,
                fecha_orden = resultado.fecha_orden
            };

            return orden;
        }

        public async Task UpdateOrdenAsync(int id, OrdenDTO ordenActualizada)
        {
            var resultado = await _repository.GetByIdAsync(id);
            if (resultado == null) throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);

            resultado.id_estado = ordenActualizada.id_estado;
            resultado.id_usuario = ordenActualizada.id_usuario;
            resultado.fecha_orden = ordenActualizada.fecha_orden;

            await _repository.UpdateAsync(resultado);
        }
    }
}
