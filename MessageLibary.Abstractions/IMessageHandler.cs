using System.Threading.Tasks;

namespace MessagingLibary
{
	public interface IMessageHandler<TPayload>
	{
		ValueTask HandleAsync(TPayload payload);
	}
}
