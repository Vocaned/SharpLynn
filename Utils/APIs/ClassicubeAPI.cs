using System;
using System.Text;
using System.Net;
using System.IO;

namespace SharpLynn {
	class ClassicubeAPI {

		private static string BASE_URL = "https://classicube.net/api/";

		public static string getPlayerByName(string name) {
			string reply = null;
			// https://classicube.net/api/player/
			string url = BASE_URL + "player/" + name;
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
