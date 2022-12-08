using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RoutingServer.Tools.JCDecaux;
using RoutingServer.Tools;
using System.Text.Json;

namespace RoutingServer
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/RoutingServer/GoBikeService/");

			ServiceHost host = new ServiceHost(typeof(GoBikeService), httpUrl);

			BasicHttpBinding binding = new BasicHttpBinding();
			binding.MaxReceivedMessageSize = 1000000;
            binding.MaxBufferPoolSize = 1000000;
            binding.MaxBufferSize = 1000000;

			host.AddServiceEndpoint(typeof(IGoBikeService), binding, "");

			ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
			smb.HttpGetEnabled = true;
			host.Description.Behaviors.Add(smb);

			host.Open();

			Console.WriteLine("RoutingServer Service is host at " + DateTime.Now.ToString());
			Console.WriteLine("Host is running... Press <Enter> key to stop");
			Console.ReadLine();
		}
	}
}
