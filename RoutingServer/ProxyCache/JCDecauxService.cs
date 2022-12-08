using ProxyCache.JCDecauxItems;
using System;
using System.Collections.Generic;

namespace ProxyCache
{
	public class JCDecauxService : IJCDecauxService
	{
		private GenericProxyCache<JCDecauxContracts> _contractsProxyCache;
		private GenericProxyCache<JCDecauxStations> _stationsProxyCache;

		public JCDecauxService() 
		{
			_contractsProxyCache = new GenericProxyCache<JCDecauxContracts>();
			_stationsProxyCache = new GenericProxyCache<JCDecauxStations>();
		}

		public string GetContracts()
		{
			return _contractsProxyCache.GetT("contracts", 600).GetContractsJson();
		}

		public string GetStations(string contractName)
		{
			Console.WriteLine("Requete : " + contractName);
			object[] args = { contractName };

			string res =  _stationsProxyCache.GetT(contractName, args).GetStationsJson();
			return res;
		}
	}
}
