using AssignmentCms.Models;
using Azure.Messaging.ServiceBus;
using System.Text.Json;
using Umbraco.Cms.Core.Configuration.Models;

namespace AssignmentCms.Services;

public class EmailService(IConfiguration configuration)
{
	private readonly string _serviceBusConnectionString = configuration.GetConnectionString("ServiceBusConnection")!;
	private readonly string _queueName = configuration.GetConnectionString("ServiceBusQueue")!;

	public async Task<bool> SendEmailConfirmationAsync(string to)
	{
		try
		{
			await using var client = new ServiceBusClient(_serviceBusConnectionString);
			ServiceBusSender sender = client.CreateSender(_queueName);

			ConfirmationEmailModel emailInfo = new() { To = to };
			string messageBody = JsonSerializer.Serialize(emailInfo);

			ServiceBusMessage message = new ServiceBusMessage(messageBody);
			await sender.SendMessageAsync(message);
			return true;
		}
		catch (Exception ex) { }
		return false;
	}
}


