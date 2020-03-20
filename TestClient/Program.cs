using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
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

			var idx = 0;

			while (true)
			{
				try
				{
					await client.QueueAsync(new MessageRequest { Payload = $"data {idx++}" });
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
