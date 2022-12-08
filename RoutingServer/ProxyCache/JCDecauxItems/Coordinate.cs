﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache.JCDecauxItems
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
			return latitude.ToString().Replace(',', '.');
		}

		public string GetLongitudeString()
		{
			return longitude.ToString().Replace(',', '.');
		}

		public override string ToString()
		{
			return GetLatitudeString() + ", " + GetLongitudeString();
		}
	}
}
