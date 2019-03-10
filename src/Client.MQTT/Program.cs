using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;

namespace Client.MQTT
{
    class SensorInfo
    {
        //{ "temperature": 5.4, "humidity": 78, "battery": "0", "quality": 100 }
        public string SensorId { get; set; }
        public decimal temperature { get; set; }
        public int humidity { get; set; }
        public string battery { get; set; }   
        public int quality { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {

            // Create TCP based options using the builder.
            var options = new MqttClientOptionsBuilder()
                .WithClientId("Client1")
                .WithTcpServer("192.168.1.101")
                .Build();

            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            mqttClient.ApplicationMessageReceived += (s, e) =>
                    {
                        try
                        {
                            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                            Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                            Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                            Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                            Console.WriteLine();

                            if (e.ApplicationMessage.Topic.Contains("sensor"))
                            {
                                var json = $"{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}";

                                SensorInfo sensorInfo = JsonConvert.DeserializeObject<SensorInfo>(json);
                                sensorInfo.SensorId = e.ApplicationMessage.Topic;
                            }
                        }
                        catch(Exception ex)
                        {
                            var message = ex.Message;
                        }
                       
                    };

            mqttClient.Connected += async (s,e) =>
            {
                //await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("sensor/temperature").Build());    
                await mqttClient.SubscribeAsync(new TopicFilterBuilder().WithTopic("nexus433/#").Build());    
            };

            var connection = await mqttClient.ConnectAsync(options);    





            //while(true)
            {
                //if (connection.IsSessionPresent)
                {
                   
                }
            }


            

            Console.Read();
        }
    }
}
