using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	// Main module
	public class WhoisCmd : ModuleBase {

		[Command("whois"), Summary("Shows information about users"), Remarks(Program.prefix + "whois [user]")]
		public async Task Whois([Remainder] IUser user = null) {
			IUser userInfo = user ?? Context.Message.Author;
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Information about " + userInfo.Username + ":**");
			eb.WithThumbnailUrl(userInfo.GetAvatarUrl());
			eb.WithColor(Color.Blue);
			eb.WithCurrentTimestamp();
			IGuildUser guildUserInfo = await Context.Guild.GetUserAsync(userInfo.Id);
			List<ulong> roles = new List<ulong>(guildUserInfo.RoleIds);
			string[] roleList = new string[roles.Count];
			for (int i = 0; i < roles.Count; i++) {
				roleList[i] = Context.Guild.GetRole(roles[i]).Mention;
			}
			string extra = "";
			if (userInfo.IsBot) extra += "Is a bot. " + Environment.NewLine;
			if (userInfo.IsWebhook) extra += "Is a webhook. " + Environment.NewLine;
			eb.WithDescription(
				"**Username: " + userInfo.Username + "#" + userInfo.Discriminator + Environment.NewLine +
				"Nickname: " + guildUserInfo.Nickname + Environment.NewLine +
				"User ID: " + userInfo.Id + Environment.NewLine +
				"Joined discord: " + userInfo.CreatedAt + Environment.NewLine +
				"Joined server: " + guildUserInfo.JoinedAt + Environment.NewLine +
				"Status: " + userInfo.Status + Environment.NewLine +
				extra +
				"Mention: " + userInfo.Mention + Environment.NewLine +
				Environment.NewLine +
				"Roles: **" + string.Join(" ", roleList)
				);
			eb.WithColor(Color.Purple);
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}