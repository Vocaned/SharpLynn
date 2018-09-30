using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace SharpLynn.Commands {
	public class SayCmd : ModuleBase {

		[Command("say"), Summary("Make the bot say something"), Remarks(Program.prefix + "say [text]"), RequireUserPermission(ChannelPermission.ManageMessages), Alias("echo")]
		public async Task Slowmode([Remainder] string text) {
			await Program.Log(new LogMessage(LogSeverity.Info, Context.Message.Id.ToString(), Context.Message.Author + " used echo " + text));
			Context.Message.DeleteAsync();
			await Context.Channel.SendMessageAsync(text);
		}
	}
}