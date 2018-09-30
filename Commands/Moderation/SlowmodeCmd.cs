using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	public class SlowmodeCmd : ModuleBase {

		[Command("slowmode"), Summary("Activate slowmode"), Remarks(Program.prefix + "slowmode [secs]"), RequireUserPermission(ChannelPermission.ManageChannels)]
		public async Task Slowmode([Remainder] int secs) {
			ITextChannel chan = await Context.Guild.GetTextChannelAsync(Context.Channel.Id);
			await chan.ModifyAsync(x => x.SlowModeInterval = secs);
			await Context.Channel.SendMessageAsync(secs + " second slowmode activated.");
		}
	}
}