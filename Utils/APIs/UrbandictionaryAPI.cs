using System;
using System.Text;
using System.Net;
using System.IO;

namespace SharpLynn {
	class UrbandictionaryAPI {

		private static string BASE_URL = "http://api.urbandictionary.com/v0/";

		public static string getDefinition(string term) {
			string reply = null;
			// http://api.urbandictionary.com/v0/define?term=<term>
			string url = BASE_URL + "define?term=" + term;
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
