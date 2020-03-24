using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessagingLibary;

namespace MessageWorker
{
	class DataHandler : IMessageHandler<MessageData>
	{
		private static Random _Random = new Random();

		public ValueTask HandleAsync(MessageData payload)
		{
			Console.WriteLine($"Process {payload.Name} payload: {_Random.Next(1, 100) / payload.Payload}");

			return new ValueTask();
		}
	}
}
