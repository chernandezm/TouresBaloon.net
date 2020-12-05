using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infraestructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class CanastaServices : ICanastaService
    {
        private IAsyncRepository<ApplicationCore.Entities.Canasta> _repository;

        private IUnitOfWork _unitOfWork;

        private ICanastaPublisher _publisher;

       // private ICanastaConsumer _consumer;

        //  private IPublisher _publisher;

        public CanastaServices(IAsyncRepository<ApplicationCore.Entities.Canasta> repository, IUnitOfWork unitOfWork, ICanastaPublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
          //  _consumer = consumer;
            //  _publisher = publisher;
        }

        public async Task<CanastaDTO> CreateCanastaAsync(CanastaDTO canasta)
        {
            var nuevaCanasta = new ApplicationCore.Entities.Canasta
            {
                //id_canasta = canasta.Id,
                id_estado = canasta.Id_estado,
                id_producto = canasta.Id_producto,
                id_usuario = canasta.Id_usuario,
                cantidad_canasta = canasta.Cantidad_canasta,
                precio_canasta = canasta.precio_canasta
            };

            nuevaCanasta = await _repository.AddAsync(nuevaCanasta);
            await _unitOfWork.ConfirmarAsync();
            canasta.Id = nuevaCanasta.id_canasta;
            _publisher.DistribuirCanasta(nuevaCanasta);
            return canasta;
        }


        public void DeleteCanastaAsync(int id)
        {
            var resultado = _repository.GetById(id);
            if (resultado == null)
                throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);

            _repository.DeleteAsync(resultado);
        }

        public async Task<IEnumerable<CanastaDTO>> GetCanastaAsync()
        {
            var resultado = await _repository.ListAllAsync();
            var canastas = resultado.Select(canasta => new ApplicationCore.DTO.CanastaDTO
            {
                Id = canasta.id_canasta,
                Id_estado = canasta.id_estado,
                Id_producto = canasta.id_producto,
                Id_usuario = canasta.id_usuario,
                Cantidad_canasta = canasta.cantidad_canasta,
                precio_canasta = canasta.precio_canasta
            });

            return canastas;
        }

        public async Task<CanastaDTO> GetCanastaAsync(int id)
        {
            var resultado = await _repository.GetByIdAsync(id);
            if (resultado == null) 
                throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);
            var canastas = new ApplicationCore.DTO.CanastaDTO
            { 
                Id = resultado.id_canasta,
                Id_estado = resultado.id_estado,
                Id_producto = resultado.id_producto,
                Id_usuario = resultado.id_usuario,
                Cantidad_canasta = resultado.cantidad_canasta,
                precio_canasta = resultado.precio_canasta
            };

            return canastas;
        }

        public CanastaDTO GetCanastaPorId(int id)
        {
            var resultado = _repository.GetById(id);
            if (resultado == null)
                throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + id);
            var canastas = new ApplicationCore.DTO.CanastaDTO
            {
                Id = resultado.id_canasta,
                Id_estado = resultado.id_estado,
                Id_producto = resultado.id_producto,
                Id_usuario = resultado.id_usuario,
                Cantidad_canasta = resultado.cantidad_canasta,
                precio_canasta = resultado.precio_canasta
            };

            return canastas;
        }

        public void UpdateCanastaAsync(Canasta canasta)
        {
            //var canasta = _consumer.ProcesarCanasta();
            var resultado =  _repository.GetById(canasta.id_canasta);
            if (resultado == null) throw new ItemNoExisteException("La canasta con el siguiente id no existe: " + canasta.id_canasta);
            resultado.id_estado = canasta.id_estado;
            //resultado.id_producto = canastaActualizada.Id_producto;
            //resultado.id_usuario = canastaActualizada.Id_usuario;
            //resultado.precio_canasta = canastaActualizada.precio_canasta;
            //resultado.cantidad_canasta = canastaActualizada.Cantidad_canasta;

            _repository.UpdateAsync(resultado);
        }
    }
}