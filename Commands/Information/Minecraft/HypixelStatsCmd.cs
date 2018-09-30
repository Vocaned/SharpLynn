using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	public class HypixelStatsCmd : ModuleBase {

		[Command("hypixelstats"), Summary("Shows stats about hypixel players."), Remarks(Program.prefix + "hypixelstats [user]"), Alias("Hypixel")]
		public async Task HypixelStats([Remainder] string username) {
			string playerInfo = HypixelAPI.getPlayerByName(username);
			dynamic api = JsonConvert.DeserializeObject(playerInfo);
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Information about " + api.player.displayname + ":**");
			eb.WithDescription(
				"**Username: " + api.player.displayname + Environment.NewLine +
				"User ID: " + api.player._id + Environment.NewLine +
				"First login: " + Utils.UnixTimeStampToDateTime(long.Parse(Convert.ToString(api.player.firstLogin))).ToUniversalTime() + Environment.NewLine +
				"Last login: " + Utils.UnixTimeStampToDateTime(long.Parse(Convert.ToString(api.player.lastLogin))).ToUniversalTime() + Environment.NewLine +
				"Current rank: " + api.player.newPackageRank + Environment.NewLine +
				"XP: " + api.player.networkExp + Environment.NewLine +
				"Karma: " + api.player.karma + Environment.NewLine +
				"Daily rewards claimed: " + api.player.totalDailyRewards + Environment.NewLine +
				"Highest daily reward streak: " + api.player.rewardHighScore + Environment.NewLine +
				"Last minecraft version: " + api.player.mcVersionRp + "**");
			eb.WithColor(Color.Blue);
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}