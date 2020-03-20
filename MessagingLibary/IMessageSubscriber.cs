using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Messaging.Subscription;

namespace MessagingLibary
{
	interface IMessageSubscriber<TPayload> : IMessageSubscriber
	{
	}
}
