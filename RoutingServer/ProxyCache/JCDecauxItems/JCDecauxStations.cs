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
	internal class JCDecauxStations
	{
		private static string _JCDecauxApiKey = "da6ce30d39371b9f12cdb93aeebd96f094466ca8";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";
		private List<Station> _stations;

		public JCDecauxStations(string contractName)
		{
			_stations = GetStationsAsync(contractName).Result;
		}

		private async Task<List<Station>> GetStationsAsync(string contractName)
		{
			string stationsUrl = _baseUrl + "stations" + "?apiKey=" + _JCDecauxApiKey
				+ "&contract=" + contractName;
			string stationsJson = await RequestTools.GetRequest(stationsUrl);
			return JsonSerializer.Deserialize<List<Station>>(stationsJson);
		}

		public string GetStationsJson() 
		{
			if (_stations == null)
				return "No stations";
			return JsonSerializer.Serialize(_stations);
		}
	}
}
