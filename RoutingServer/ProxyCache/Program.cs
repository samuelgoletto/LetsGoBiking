using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Uri httpUrl = new Uri("http://localhost:8733/Design_Time_Addresses/ProxyCache/JCDecauxService/");

			ServiceHost host = new ServiceHost(typeof(JCDecauxService), httpUrl);

			BasicHttpBinding binding = new BasicHttpBinding();
			binding.MaxReceivedMessageSize = 1000000;
            binding.MaxBufferPoolSize = 1000000;
            binding.MaxBufferSize = 1000000;

			host.AddServiceEndpoint(typeof(IJCDecauxService), binding, "");

			ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
			smb.HttpGetEnabled = true;
			host.Description.Behaviors.Add(smb);

			host.Open();

			Console.WriteLine("ProxyCache Service is host at " + DateTime.Now.ToString());
			Console.WriteLine("Host is running... Press <Enter> key to stop");
			Console.ReadLine();
		}
	}
}
