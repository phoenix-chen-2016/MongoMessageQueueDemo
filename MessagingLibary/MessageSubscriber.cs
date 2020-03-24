using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Messaging;
using MongoDB.Messaging.Subscription;

namespace MessagingLibary
{
	internal class MessageSubscriber<TPayload> : IMessageSubscriber<TPayload>
	{
		private readonly IMessageHandler<TPayload> m_MessageHandler;
		private readonly ILogger<MessageSubscriber<TPayload>> m_Logger;

		public MessageSubscriber(IMessageHandler<TPayload> messageHandler, ILogger<MessageSubscriber<TPayload>> logger)
		{
			m_MessageHandler = messageHandler ?? throw new ArgumentNullException(nameof(messageHandler));
			m_Logger = logger;
		}

		public MessageResult Process(ProcessContext processContext)
		{
			// get message data
			var payload = processContext.Data<TPayload>();

			try
			{
				Task.Run(() => m_MessageHandler.HandleAsync(payload).GetAwaiter().GetResult()).Wait();

				return MessageResult.Successful;
			}
			catch (Exception ex)
			{
				m_Logger.LogError(ex, "Process fail.");

				return MessageResult.Error;
			}
		}

		public void Dispose()
		{
		}
	}
}
