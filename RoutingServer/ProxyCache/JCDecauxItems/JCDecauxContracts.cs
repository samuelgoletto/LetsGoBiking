using ProxyCache.JCDecauxItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyCache.JCDecauxItems
{
	internal class JCDecauxContracts
	{
		private static string _JCDecauxApiKey = "da6ce30d39371b9f12cdb93aeebd96f094466ca8";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";
		private List<Contract> _contracts;

		public JCDecauxContracts() {
			_contracts = GetContracts().Result;
		}

		private async Task<List<Contract>> GetContracts()
		{
			string contractsUrl = _baseUrl + "contracts" + "?apiKey=" + _JCDecauxApiKey;
			string contractsJson = await RequestTools.GetRequest(contractsUrl);
			return JsonSerializer.Deserialize<List<Contract>>(contractsJson);
		}

		public string GetContractsJson()
		{
			if (_contracts == null)
				return "No contracts";
			return JsonSerializer.Serialize(_contracts);
		}
	}
}
