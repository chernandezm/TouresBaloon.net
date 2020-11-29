using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infraestructure.Messaging
{
    public class OrdenConsumer : IOrdenConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private IEnumerable<KeyValuePair<string, string>> config;

        public OrdenConsumer()
        {
            config = new ConsumerConfig
            {
                GroupId = "topico-canasta",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }
        public Canasta ProcesarOrden(Orden orden)
        {
            using (var c = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                c.Subscribe("topico-canasta");

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
                            var canasta = JsonConvert.DeserializeObject<Canasta>(cr.Value);
                            return canasta;
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
