using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MongoDB.Messaging;

namespace QueueService.Services
{
	public class QueueManagerService : QueueManager.QueueManagerBase
	{
		private const string QueueName = "TestQueue";

		public QueueManagerService()
		{
			MessageQueue.Default.Configure(c => c
				.ConnectionString("mongodb://mongo_mongo_1/Messaging")
				.Queue(s => s
					.Name(QueueName)
					.Retry(5)));
			;
		}
		public override async Task<MessageReply> Queue(MessageRequest request, ServerCallContext context)
		{
			var message = await MessageQueue.Default.Publish(m => m
				.Queue(QueueName)
				.Data(new { request.Payload })
				.Correlation(Guid.NewGuid().ToString())
				.Description("User friendly description of the message")
				.Priority(MessagePriority.Normal)
				.Retry(1));

			return new MessageReply();
		}
	}
}
