using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	public class WynncraftStatsCmd : ModuleBase {

		[Command("wynncraftstats"), Summary("Shows stats about Wynncraft players."), Remarks(Program.prefix + "wynncraftstats [user]"), Alias("wynncraft")]
		public async Task WynncraftStats([Remainder] string username) {
			string playerInfo = WynncraftAPI.getPlayerByName(username);
			dynamic api = JsonConvert.DeserializeObject(playerInfo);
			DateTime dT = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Information about " + api.username + ":**");
			eb.WithDescription(
				"**Username: " + api.username + Environment.NewLine +
				"First login: " + api.first_join_friendly + Environment.NewLine +
				"Last login: " + api.last_join_friendly + Environment.NewLine +
				"Playtime: " + (api.playtime / 60.0).ToString("F2") + "h" + Environment.NewLine +
				"Logins: " + api.global.logins + Environment.NewLine +
				"Current rank: " + api.rank + Environment.NewLine +
				"Mobs killed: " + api.global.mobs_killed + Environment.NewLine +
				"Total level: " + api.global.total_level + Environment.NewLine +
				"Chests opened: " + api.global.chests_found + Environment.NewLine +
				"Blocks walked: " + api.global.blocks_walked + "**");
			eb.WithColor(Color.Blue);
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}