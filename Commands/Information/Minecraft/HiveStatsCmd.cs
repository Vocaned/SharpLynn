using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	public class HiveStatsCmd : ModuleBase {

		[Command("hivestats"), Summary("Shows stats about HiveMC players."), Remarks(Program.prefix + "hivestats [user]"), Alias("hivemc")]
		public async Task HiveStats([Remainder] string username) {
			string playerInfo = HiveAPI.getPlayerByName(username);
			dynamic api = JsonConvert.DeserializeObject(playerInfo);
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Information about " + api.username + ":**");
			eb.WithDescription(
				"**Username: " + api.username + Environment.NewLine +
				"First login: " + Utils.UnixTimeStampToDateTime(long.Parse(Convert.ToString(api.firstLogin))).ToUniversalTime() + Environment.NewLine +
				"Last login: " + Utils.UnixTimeStampToDateTime(long.Parse(Convert.ToString(api.lastLogin))).ToUniversalTime() + Environment.NewLine +
				"Current rank: " + api.rankName + Environment.NewLine +
				"Current tokens: " + api.tokens + Environment.NewLine +
				"Current credits: " + api.credits + Environment.NewLine +
				"Current medals: " + api.medals + "**");
			eb.WithColor(Color.Blue);
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}