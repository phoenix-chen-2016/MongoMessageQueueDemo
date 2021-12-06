using System;
using System.Threading.Tasks;
using Grpc.Core;
using QueueService;

namespace Net45Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			//			var channel = new Channel("localhost", 5001, new SslCredentials(@"
			//-----BEGIN CERTIFICATE-----
			//MIIDDTCCAfWgAwIBAgIJAIfka3aftWCTMA0GCSqGSIb3DQEBCwUAMBQxEjAQBgNV
			//BAMTCWxvY2FsaG9zdDAeFw0xOTEwMzAwNjE2NTlaFw0yMDEwMjkwNjE2NTlaMBQx
			//EjAQBgNVBAMTCWxvY2FsaG9zdDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoC
			//ggEBAKMkOv6iulqFE5MrRy7Z2yRc0ETsjJmrfU59mM2Tme2YhhjmJGZZEzh63pDS
			//hrIi31bBYVsJml3O4qqYk1hPjedFV7F49jxNOAza8g7hSQtGy4vBFYbwMHbFBPX+
			//y3DNsUjUJjG3+2JA3uY3JMM/EqnRxiZzzZVWHn5l08g4+v02zN7BPqQ6+MJj+h+l
			//3n6w3H8I9N5tjJDVYmOoS1IpjltLxaLE6Ysgb4aqsYW58f8u7omfguyk4If/95Uc
			//+2p5mjMpDQj+TkGEHJ8WBCrtFiMiWmHPPNJHk4u6q06sPdgyMN3QIB6laXXIQlew
			//N5DeQsJUZNOHA8OfqIXQkTVS4JkCAwEAAaNiMGAwDAYDVR0TAQH/BAIwADAOBgNV
			//HQ8BAf8EBAMCBaAwFgYDVR0lAQH/BAwwCgYIKwYBBQUHAwEwFwYDVR0RAQH/BA0w
			//C4IJbG9jYWxob3N0MA8GCisGAQQBgjdUAQEEAQEwDQYJKoZIhvcNAQELBQADggEB
			//ACtv4uk756XXrtQa/vS25UB+I4sJtaY4FMNq7kmGoKHbPh/1rZfhfxNEL0ru0p2/
			//o9WMtXvB1XFIFpgefuBzRUq+smfsckkfs4Fh3M64ss2h+zg3ZeGpBxXfVs1V+RXs
			//Q+6P6/kZ+33ARkls0MqLijaEIzZvQWaxrU4uUbQNQeMAhGx5DVikiTVNocPFJO7l
			//GzlkoGh5XHrX8xFbYEsIpy8v8oGwvLUee84CG8Xe7+vtxsuLE8eBLd8VKX1GIhTl
			//TDLnFxsVhVaDbJgEs+7baUwxcegrl4sOYHgTEmKpAqZ6vdH27mGMXR1hEkvf9xA/
			//8J+F2lqSqOSXDn6nuOkxxL4=
			//-----END CERTIFICATE-----"));
			var channel = new Channel("localhost", 5000, ChannelCredentials.Insecure);
			var client = new QueueManager.QueueManagerClient(channel);

			var rand = new Random();

			Task.Factory.StartNew(async () =>
			{
				while (true)
				{
					try
					{
						await client.QueueAsync(new MessageRequest
						{
							Name = "Net45",
							Payload = rand.Next(0, 5)
						});
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}

					await Task.Delay(1000);
				}
			}).Wait();

			Console.ReadKey();
		}
	}
}
