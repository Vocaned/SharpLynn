using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	public class ClassicubeStatsCmd : ModuleBase {

		[Command("classicubestats"), Summary("Shows stats about Classicube players."), Remarks(Program.prefix + "classicubestats [user]"), Alias("classicube")]
		public async Task ClassicubeStats([Remainder] string username) {
			string playerInfo = ClassicubeAPI.getPlayerByName(username);
			dynamic api = JsonConvert.DeserializeObject(playerInfo);
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Information about " + api.username + ":**");
			eb.WithDescription(
				"**Username: " + api.username + Environment.NewLine +
				"First login: " + Utils.UnixTimeStampToDateTime(long.Parse(Convert.ToString(api.registered))).ToUniversalTime() + Environment.NewLine +
				"ID: " + api.id + Environment.NewLine +
				"Flags: " + api.flags + "**");
			eb.WithColor(Color.Blue);
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}