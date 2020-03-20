using System;
using System.Threading.Tasks;
using MongoDB.Messaging;
using MongoDB.Messaging.Subscription;

namespace MessagingLibary
{
	internal class MessageSubscriber<TPayload> : IMessageSubscriber<TPayload>
	{
		private readonly IMessageHandler<TPayload> m_MessageHandler;

		public MessageSubscriber(IMessageHandler<TPayload> messageHandler)
		{
			m_MessageHandler = messageHandler ?? throw new ArgumentNullException(nameof(messageHandler));
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
				return MessageResult.Error;
			}
		}

		public void Dispose()
		{
		}
	}
}
