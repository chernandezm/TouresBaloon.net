using ApplicationCore.DTO;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Messaging
{
    public class OrdenPublisher : IOrdenPublisher
    {
        private readonly IProducer<Null, string> _producer;

        public OrdenPublisher(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async void DistribuirOrden(OrdenDTO orden)
        {
            string content = JsonConvert.SerializeObject(orden);
            await _producer.ProduceAsync("checkout", new Message<Null, string> { Value = content });
        }

        public void PublicarOrden(Orden orden)
        {
            throw new NotImplementedException();
        }
    }
}
