using Apache.NMS.ActiveMQ;
using Apache.NMS;
using System;
using System.Linq;
using Apache.NMS.ActiveMQ.Commands;

namespace RoutingServer.Tools
{
	internal class ActiveMqHelper
	{
		private static Random RANDOM = new Random();

		private IConnection _connection;
		private ISession _session;
		private IMessageProducer _producer;

		public ActiveMqHelper(string queueName)
		{
			Uri connecturi = new Uri("activemq:tcp://localhost:61616");
			ConnectionFactory connectionFactory = new ConnectionFactory(connecturi);

			_connection = connectionFactory.CreateConnection();
			_connection.Start();

			_session = _connection.CreateSession();

			IDestination destination = _session.GetQueue(queueName);

			_producer = _session.CreateProducer(destination);

			_producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
		}

		public void SendMessage(string messageText)
		{
			ITextMessage message = _session.CreateTextMessage(messageText);
			_producer.Send(message);
		}

		public void CloseSession()
		{
			_session.Close();
			_connection.Close();
		}
		public void SendMessagesOnNewLine(string longText)
		{
			string[] lines = longText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			foreach (string line in lines)
			{
				SendMessage(line);
			}
		}

		public static string GenerateRandomQueueName()
		{
			return "GoBike_Queue_" + RandomString(10);
		}

		public static string RandomString(int length)
		{
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[RANDOM.Next(s.Length)]).ToArray());
		}
	}
}
