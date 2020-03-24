using System;
using MongoDB.Messaging;
using MongoDB.Messaging.Change;
using MongoDB.Messaging.Configuration;
using MongoDB.Messaging.Service;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Messaging.Subscription;

namespace MessageWorker
{
	class Program
	{
		static void Main(string[] args)
		{
			Host.CreateDefaultBuilder(args)
				.ConfigureServices(services =>
				{
					services
						.AddMessageHandler<MessageData, DataHandler>();
				})
				.Build()
				.Run();
		}
	}
}
