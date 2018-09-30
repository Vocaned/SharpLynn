using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace SharpLynn.Commands {
	public class PingCmd : ModuleBase {

		[Command("ping"), Summary("Pong!"), Remarks(Program.prefix + "ping")]
		public async Task Ping() {
			EmbedBuilder eb = new EmbedBuilder();
			eb.AddField("Ping at latest heartbeat:", "💓 " + Program.client.Latency + " ms");
			eb.WithColor(Color.Red);
			eb.WithCurrentTimestamp();
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}