using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Services
{
    public class HelloWorldService
    {
        private IAsyncRepository<Orden> _repository;

        public HelloWorldService(IAsyncRepository<Orden> repository)
        {
            _repository = repository;
        }

        public void UpdateOrden(Orden orden)
        {
            var resultado = _repository.GetById(orden.id_orden);
              if (resultado == null) throw new ItemNoExisteException("La orden con el siguiente id no existe: " + orden.id_estado);

              resultado.id_estado = orden.id_estado;

            _repository.UpdateAsync(resultado);
        }
    }
}
