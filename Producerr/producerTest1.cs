﻿using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Producerr
{
    public class producerTest1
    {
        public async Task<string> PublishAsync(string message)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092", };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("demo", new Message<Null, string> { Value = message });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                    return "succes";
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    return "failed";

                }
            }
        }
    }
}
