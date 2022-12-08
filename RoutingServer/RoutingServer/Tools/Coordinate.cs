﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
	class Coordinate
	{
		public double latitude { get; set; }
		public double longitude { get; set; }

		public Coordinate(double latitude, double longitude)
		{
			this.latitude = latitude;
			this.longitude = longitude;
		}

		public string GetLatitudeString()
		{
			return latitude.ToString(CultureInfo.InvariantCulture);
		}

		public string GetLongitudeString()
		{
			return longitude.ToString(CultureInfo.InvariantCulture);
		}

		public override string ToString()
		{
			return GetLatitudeString() + ", " + GetLongitudeString();
		}
	}
}
