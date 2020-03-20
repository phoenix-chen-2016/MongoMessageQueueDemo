using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MongoDB.Messaging;
using MongoDB.Messaging.Service;

namespace MessagingLibary
{
	class MessagingHostedService : IHostedService
	{
		private readonly IEnumerable<IMessageService> m_MessageServices;

		public MessagingHostedService(IEnumerable<IMessageService> messageServices)
		{
			m_MessageServices = messageServices ?? throw new ArgumentNullException(nameof(messageServices));
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			foreach (var svc in m_MessageServices)
				svc.Start();

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			foreach (var svc in m_MessageServices)
				svc.Stop();

			return Task.CompletedTask;
		}
	}
}
