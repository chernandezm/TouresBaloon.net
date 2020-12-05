using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Confluent.Kafka;
using Infraestructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructure.Messaging
{
    public class CheckoutConsumer : BackgroundService, IHostedService
    {
        private readonly IServiceProvider _provider;
        private readonly IConsumer<Null, string> _consumer;
        private IEnumerable<KeyValuePair<string, string>> config;
        //private HelloWorldService helloWorldService;
        private IOrdenService _servicio;

        public CheckoutConsumer(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
           // _servicio = ordenService;
            config = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "172.17.0.1:9092",
               // BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }
        //public Orden ProcesarOrden()
        //{
            
        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                _servicio = scope.ServiceProvider.GetService<IOrdenService>();
                using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    c.Subscribe("checkout");

                    CancellationTokenSource cts = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                    };

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                var cr = c.Consume(cts.Token);
                                Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                                var orden = JsonConvert.DeserializeObject<Orden>(cr.Value);
                                _servicio.UpdateOrdenAsync(orden);
                                //  var resultado = _repository.GetById(orden.id_orden);
                                //  if (resultado == null) throw new ItemNoExisteException("La orden con el siguiente id no existe: " + orden.id_estado);

                                //  resultado.id_estado = orden.id_estado;

                                //_repository.UpdateAsync(resultado);
                            }
                            catch (ConsumeException e)
                            {
                                Console.WriteLine($"Error occured: {e.Error.Reason}");
                                return null;
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Ensure the consumer leaves the group cleanly and final offsets are committed.
                        c.Close();
                        return null;
                    }
                }
            }
        }
    }
}
