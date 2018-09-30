using System;
using System.Text;
using System.Net;
using System.IO;

namespace SharpLynn {
	class AnimalAPIs {

		public static string getCat() {
			string reply = null;
			string url = "https://aws.random.cat/meow";
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

		public static string getDog() {
			string reply = null;
			string url = "https://random.dog/woof.json";
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

		public static string getFox() {
			string reply = null;
			string url = "https://randomfox.ca/floof/";
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

		public static string getDoge() {
			string reply = null;
			string url = "http://shibe.online/api/shibes";
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

		public static string getBird() {
			string reply = null;
			string url = "http://shibe.online/api/birds";
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
