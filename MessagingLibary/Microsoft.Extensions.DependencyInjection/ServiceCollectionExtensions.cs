using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MessagingLibary;
using MongoDB.Messaging;
using MongoDB.Messaging.Service;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMessageHandler<TPayload, THandle>(this IServiceCollection services) where THandle : class, IMessageHandler<TPayload>
		{
			return services
				.AddTransient<IMessageHandler<TPayload>, THandle>()
				.AddTransient<IMessageSubscriber<TPayload>, MessageSubscriber<TPayload>>();
		}

		public static IServiceCollection AddMessaging<TPayload>(this IServiceCollection services)
		{
			return services
				.AddSingleton<IMessageService>(sp =>
				{
					MessageQueue.Default.Configure(c => c
						.ConnectionString("mongodb://mongo_mongo_1/Messaging")
						.Queue(s => s
							.Name("TestQueue")
							.Retry(5))
						.Subscribe(s => s
							.Queue("TestQueue")
							.Handler(() => sp.GetRequiredService<IMessageSubscriber<TPayload>>())
							.PollTime(TimeSpan.FromSeconds(1))
							.Workers(1)
							.Trigger()));

					return new MessageService(MessageQueue.Default.QueueManager);
				})
				.AddHostedService<MessagingHostedService>();
		}
	}
}
