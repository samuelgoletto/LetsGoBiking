using RoutingServer.Tools;
using RoutingServer.Tools.JCDecaux;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
	class JCDecauxTools
	{
		private static string _JCDecauxApiKey = "da6ce30d39371b9f12cdb93aeebd96f094466ca8";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";

		public async Task<List<Contract>> GetContracts()
		{
			JCDecauxProxyCacheService.JCDecauxServiceClient jCDecauxProxyCacheServiceClient = new JCDecauxProxyCacheService.JCDecauxServiceClient();
			string contractsJson = await jCDecauxProxyCacheServiceClient.GetContractsAsync();
			return JsonSerializer.Deserialize<List<Contract>>(contractsJson);
		}

		public async Task<List<Station>> GetStations(string contractName)
		{
			JCDecauxProxyCacheService.JCDecauxServiceClient jCDecauxProxyCacheServiceClient = new JCDecauxProxyCacheService.JCDecauxServiceClient();
			string stationsJson = await jCDecauxProxyCacheServiceClient.GetStationsAsync(contractName);
			return JsonSerializer.Deserialize<List<Station>>(stationsJson);
		}

		public async Task<Contract> GetContract(string contractName)
		{
			List<Contract> contracts = await GetContracts();
			foreach (Contract contract in contracts)
			{
				if (contract.name.Equals(contractName) || (contract.cities != null && contract.cities.Contains(contractName)))
					return contract;				
			}
			throw new ContractNotCoveredException();
		}

		public Station GetNearestStationAsync(List<Station> stations, Coordinate currentCoordinate)
		{
			GeoCoordinate currentCoordianteGeo = new GeoCoordinate(currentCoordinate.latitude, currentCoordinate.longitude);
			double distanceClosestStation = -1;
			Station closestStation = null;
			for (int i = 0; i < stations.Count; i++)
			{
				GeoCoordinate tempCoord = new GeoCoordinate(stations[i].position.latitude, stations[i].position.longitude);
				double tempDistance = currentCoordianteGeo.GetDistanceTo(tempCoord);

				if (closestStation == null || tempDistance < distanceClosestStation)
				{
					closestStation = stations[i];
					distanceClosestStation = tempDistance;
				}
			}

			return closestStation;
		}

		public async Task<Station> GetNearestStationWithAvailableBikeAsync(string contractName, Coordinate currentCoordinate)
		{
			List<Station> stations = await GetStations(contractName);
			stations.RemoveAll(s => s.totalStands.availabilities.bikes <= 0);

			return GetNearestStationAsync(stations, currentCoordinate);
		}
		public async Task<Station> GetNearestStationWithAvailableStandAsync(string contractName, Coordinate currentCoordinate)
		{
			List<Station> stations = await GetStations(contractName);
			stations.RemoveAll(s => s.totalStands.availabilities.stands <= 0);
			

			return GetNearestStationAsync(stations, currentCoordinate);
		}
	}
}
