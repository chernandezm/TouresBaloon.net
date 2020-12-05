using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Messaging
{
    public class CanastaPublisher : ICanastaPublisher
    {
        private readonly IProducer<Null, string> _producer;

        public CanastaPublisher(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
               BootstrapServers = "172.17.0.1:9092"
               //BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async void DistribuirCanasta(Canasta canasta)
        {
            string content = JsonConvert.SerializeObject(canasta);
            await _producer.ProduceAsync("canasta", new Message<Null, string> { Value = content });
        }

        public void PublicarCanasta(Canasta canasta)
        {
            throw new NotImplementedException();
        }
    }
}
