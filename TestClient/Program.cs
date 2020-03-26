using System;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Newtonsoft.Json.Linq;
using QueueService;

namespace TestClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var http = new HttpClient();
			http.Timeout = TimeSpan.FromSeconds(3);

			var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
			{
				HttpClient = http
			});
			var client = new QueueManager.QueueManagerClient(channel);
			var mongo = new Mongo.MongoClient(channel);

			var rand = new Random();

			while (true)
			{
				try
				{
					var number = rand.Next(0, 5);
					await client.QueueAsync(new MessageRequest
					{
						Name = "TestClient",
						Payload = number
					});

					await mongo.AddDataAsync(new MongoAddRequest
					{
						CollectionName = "Test123",
						Data = JObject.FromObject(new
						{
							Name = "TestClient",
							Value = number
						}).ToString()
					});

					await Task.Delay(1000);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}
	}
}
