using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using System.Globalization;

namespace SharpLynn.Commands {
	public class WeatherCmd : ModuleBase {

		[Command("weather"), Summary("See weather from places"), Remarks(Program.prefix + "weather [location]")]
		public async Task Weather([Remainder] string location) {
			string weatherInfo = OpenWeatherMapAPI.getWeatherByCity(location);
			dynamic api = JsonConvert.DeserializeObject(weatherInfo);
			dynamic weather = api.weather[0];
			double temp = double.Parse((string)api.main.temp, CultureInfo.InvariantCulture);
			double Ctemp = temp - 273.15;
			double Ftemp = temp * 9 / 5 - 459.67;
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Weather in " + api.name + ", " + api.sys.country + ":**");
			eb.WithThumbnailUrl("https://openweathermap.org/img/w/" + weather.icon + ".png");
			eb.AddField("Temperature:", Ctemp.ToString("F2") + "°C (" + Ftemp.ToString("F2") + "°F)", true);
			eb.AddField("Weather:", weather.main + Environment.NewLine + "(" + weather.description + ")", true);
			eb.AddField("Atmosphere:", "**Air pressure: **" + api.main.pressure + " hPa" + Environment.NewLine + "**Humidity: **" + api.main.humidity + "%");
			eb.AddField("Wind speed:", api.wind.speed + " m/s");
			eb.WithFooter("Powered by OpenWeatherMap");
			switch (weather.icon) {
				case "01d":
				case "01n":
					eb.WithColor(Color.Orange);
					break;
				case "02d":
				case "02n":
					eb.WithColor(Color.LightOrange);
					break;
				case "03d":
				case "03n":
					eb.WithColor(Color.LightGrey);
					break;
				case "04d":
				case "04n":
					eb.WithColor(Color.DarkGrey);
					break;
				case "09d":
				case "09n":
					eb.WithColor(Color.DarkBlue);
					break;
				case "10d":
				case "10n":
					eb.WithColor(Color.DarkerGrey);
					break;
				case "11d":
				case "11n":
					eb.WithColor(Color.DarkRed);
					break;
				case "13d":
				case "13n":
					eb.WithColor(Color.Blue);
					break;
				case "50d":
				case "50n":
					eb.WithColor(Color.LighterGrey);
					break;
				default:
					break;
			}
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}