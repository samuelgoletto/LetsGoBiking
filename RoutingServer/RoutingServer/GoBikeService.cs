using RoutingServer.Tools;
using RoutingServer.Tools.JCDecaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
	public class GoBikeService : IGoBikeService
	{
		public string GetItinary(string originAdress, string destinationAdress)
		{
			string itinaryInstructions;
			try
			{
				Itinerary itinary = new Itinerary();
				itinaryInstructions = itinary.GetItinaryAsync(originAdress, destinationAdress).Result;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return "Servor error";
			}

			try
			{
				string queueName = ActiveMqHelper.GenerateRandomQueueName();
				Console.WriteLine("Queue name : " + queueName);

				ActiveMqHelper activeMqHelper = new ActiveMqHelper(queueName);
				activeMqHelper.SendMessagesOnNewLine(itinaryInstructions);
				activeMqHelper.CloseSession();

				Console.WriteLine("New queue : " + queueName);
				return queueName;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return "Queue error";
			}
		}
	}
}
