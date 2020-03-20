using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessagingLibary;

namespace MessageWorker
{
	class DataHandler : IMessageHandler<MessageData>
	{
		public ValueTask HandleAsync(MessageData payload)
		{
			Console.WriteLine($"Process payload: {payload.Payload}");

			return new ValueTask();
		}
	}
}
