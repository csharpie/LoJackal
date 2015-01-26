using System;
using System.Xml;

namespace geoip
{
	// Wrapper class for retrieved data
	class GeoLocation
	{
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionCode { get; set; }
		public string RegionName { get;  set;}
		public string City { get; set; }
		public string ZipCode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public GeoLocation(string countryCode, string countryName, string regionCode, string regionName, string city, string zipCode, double latitude, double longitude) {
			CountryCode = countryCode;
			CountryName = countryName;
			RegionCode = regionCode;
			RegionName = regionName;
			City = city;
			ZipCode = zipCode;
			Latitude = latitude;
			Longitude = longitude;
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			foreach (string arg in args) {
				GeoLocation geoLoc = GeoLoc (arg);
				Console.WriteLine (geoLoc.City + ", " + geoLoc.RegionName + " " + geoLoc.ZipCode);
			}
		}

		// Grab XML data for an IP address from service
		private static GeoLocation GeoLoc(string ipAddress) {
			string url = "http://freegeoip.net/xml/" + ipAddress;
			XmlDocument doc = new XmlDocument ();
			doc.Load (url.Trim ());
			XmlNode node = doc.DocumentElement.SelectSingleNode ("/Response");

			return new GeoLocation (
				node.SelectSingleNode ("CountryCode").InnerText,
				node.SelectSingleNode ("CountryName").InnerText,
				node.SelectSingleNode ("RegionCode").InnerText,
				node.SelectSingleNode ("RegionName").InnerText,
				node.SelectSingleNode ("City").InnerText,
				node.SelectSingleNode ("ZipCode").InnerText,
				Convert.ToDouble (node.SelectSingleNode ("Latitude").InnerText),
				Convert.ToDouble (node.SelectSingleNode ("Longitude").InnerText));
		}
	}
}
