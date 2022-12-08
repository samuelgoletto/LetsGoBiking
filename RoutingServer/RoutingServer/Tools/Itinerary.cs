using RoutingServer.Tools.JCDecaux;
using RoutingServer.Tools.OpenStreetMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools
{
	class Itinerary
	{
		public async Task<string> GetItinaryAsync(string originAdress, string destinationAdress)
		{
			if (originAdress.Length == 0 || destinationAdress.Length == 0)
				return "No address";

			OpenStreetMapTools openStreetMapTools = new OpenStreetMapTools();

			Coordinate originCoordinate;
			Coordinate destinationCoordinate;
			try
			{
				originCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(originAdress);
				destinationCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(destinationAdress);
			}
			catch (IncorrectAdressException)
			{
				return "No starting or destination address";
			}

			string origineCityName = await openStreetMapTools.GetCityFromCoordinateAsync(originCoordinate);
			string destinationCityName = await openStreetMapTools.GetCityFromCoordinateAsync(destinationCoordinate);

			string contractName;
			try
			{
				contractName = await GetContractFromItinaryCities(origineCityName, destinationCityName);
			}
			catch (MultipleCitiesItinaryException)
			{
				return "Itinerary error : multiple cities";
			}
			catch (ContractNotCoveredException)
			{
				return "Itinerary error : contract not covered";
			}

			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Station closestOriginStation = await jCDecauxTools.GetNearestStationWithAvailableBikeAsync(contractName, originCoordinate);
			Station closestDestinationStation = await jCDecauxTools.GetNearestStationWithAvailableStandAsync(contractName, destinationCoordinate);

			Direction originToStationDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, closestOriginStation.position, false);
			Direction stationToStationDirection = await openStreetMapTools.GetDirectionsAsync(closestOriginStation.position, closestDestinationStation.position, true);
			Direction stationToDestionationDirection = await openStreetMapTools.GetDirectionsAsync(closestDestinationStation.position, destinationCoordinate, false);

			Direction walkDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, destinationCoordinate, false);

			double totalDurationWalking = walkDirection.GetFirstSegmentDuration();
			double totalDurationWithBike = originToStationDirection.GetFirstSegmentDuration() + stationToStationDirection.GetFirstSegmentDuration() + stationToDestionationDirection.GetFirstSegmentDuration();

			if (totalDurationWalking < totalDurationWithBike)
			{
				double totalDistance = walkDirection.GetFirstSegmentDuration();

				return "Total distance : " + totalDistance + "m" + Environment.NewLine
					+ "Total duration : " + totalDurationWalking + "s" + Environment.NewLine
					+ walkDirection.ToString();
			}
			else
			{
				double totalDistance = originToStationDirection.GetFirstSegmentDistance() + stationToStationDirection.GetFirstSegmentDistance() + stationToDestionationDirection.GetFirstSegmentDistance();

				return "Total distance : " + totalDistance + "m" + Environment.NewLine
					+ "Total duration : " + totalDurationWithBike + "s" + Environment.NewLine
					+ originToStationDirection.ToString() + Environment.NewLine
					+ stationToStationDirection.ToString() + Environment.NewLine
					+ stationToDestionationDirection.ToString();
			}
		}

		private async Task<string> GetContractFromItinaryCities(string origineCityName, string destinationCityName)
		{
			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Contract origineContract = await jCDecauxTools.GetContract(origineCityName);
			Contract destinationContract = await jCDecauxTools.GetContract(destinationCityName);

			if (origineContract.name.Equals(destinationContract.name, StringComparison.CurrentCultureIgnoreCase))
				return origineContract.name;

			throw new MultipleCitiesItinaryException();
		}



	}



}
