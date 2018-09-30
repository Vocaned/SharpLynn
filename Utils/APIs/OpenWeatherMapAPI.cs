using System;
using System.Text;
using System.Net;
using System.IO;

namespace SharpLynn {
	class OpenWeatherMapAPI {

		private static string BASE_URL = "https://api.openweathermap.org/data/2.5/weather";
		private static string API_KEY = "d9ff647531d23296509e955051805abf";

		public static string getWeatherByCity(string name) {
			string reply = null;
			// https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=d9ff647531d23296509e955051805abf
			string url = BASE_URL + "?q=" + name + "&appid=" + API_KEY;
			WebResponse response = null;
			StreamReader reader = null;

			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";
				response = request.GetResponse();
				reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
				reply = reader.ReadToEnd();
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			} finally {
				if (reader != null)
					reader.Close();
				if (response != null)
					response.Close();
			}

			return reply;
		}
	}
}
