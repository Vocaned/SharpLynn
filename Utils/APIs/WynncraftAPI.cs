using System;
using System.Text;
using System.Net;
using System.IO;

namespace SharpLynn {
	class WynncraftAPI {

		private static string BASE_URL = "https://api.wynncraft.com/public_api.php";

		public static string getPlayerByName(string name) {
			string reply = null;
			// https://api.wynncraft.com/public_api.php?action=playerStats&command=<name>
			string url = BASE_URL + "?action=playerStats&command=" + name;
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
