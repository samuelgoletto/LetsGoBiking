﻿using RoutingServer.Tools;
using RoutingServer.Tools.OpenStreetMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RoutingServer
{
	class OpenStreetMapTools
	{
        private static string _openRouteMapApiKey = "5b3ce3597851110001cf6248e0c687f6cf6b41df80f443ec0d8e2b07";
        private string _baseUrl = "https://api.openrouteservice.org/";

        public async Task<Coordinate> GetCoordinateFromAdressAsync(string adress)
		{
            string positionUrl = _baseUrl + "geocode/search" + "?api_key=" + _openRouteMapApiKey + "&text=" + adress;
            string positionJson = await RequestTools.GetRequest(positionUrl);
            Geocode geocode = JsonSerializer.Deserialize<Geocode>(positionJson);

            if (geocode.features.Count == 0)
                throw new IncorrectAdressException();

            double latitude = geocode.features[0].geometry.coordinates[1];
            double longitude = geocode.features[0].geometry.coordinates[0];
            return new Coordinate(latitude, longitude);
		}

        public async Task<string> GetCityFromCoordinateAsync(Coordinate coordinate)
		{
            string geocodeUrl = _baseUrl + "geocode/reverse" + "?api_key=" + _openRouteMapApiKey
                + "&point.lat=" + coordinate.GetLatitudeString()
                + "&point.lon=" + coordinate.GetLongitudeString();
            string geocodeJson = await RequestTools.GetRequest(geocodeUrl);
            Geocode geocode = JsonSerializer.Deserialize<Geocode>(geocodeJson);

            string cityName = "";
			try
			{
                cityName = geocode.features[0].properties.locality;
            }
			catch (Exception)
			{
                Console.WriteLine("Unreachable city name");
			}
            return cityName;
        }

        public async Task<Direction> GetDirectionsAsync(Coordinate startCoordinate, Coordinate endCoordinate, bool onBike)
		{
            string modeType = "foot-walking";
            if (onBike)
                modeType = "cycling-regular";

            string directionsUrl = _baseUrl + "v2/directions/" + modeType + "?api_key=" + _openRouteMapApiKey
                + "&start=" + startCoordinate.GetLongitudeString() + "," + startCoordinate.GetLatitudeString()
                + "&end=" + endCoordinate.GetLongitudeString() + "," + endCoordinate.GetLatitudeString();
            string directionsJson = await RequestTools.GetRequest(directionsUrl);
            Direction direction = JsonSerializer.Deserialize<Direction>(directionsJson);

            return direction;
        }
    }
}
