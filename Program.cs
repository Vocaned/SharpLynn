using System;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Linq;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace SharpLynn {
	public class Program {
		public static DiscordSocketClient client;
		public static CommandService commands;
		public static IServiceProvider services;
		//1 char only!!!!
		public const string prefix = "\"";

		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync() {	
			client = new DiscordSocketClient();
			commands = new CommandService();
			services = new ServiceCollection().BuildServiceProvider();
			client.Log += Log;
			

			await InstallCommands();
			await client.LoginAsync(TokenType.Bot, "NDExMjMwNzM1MDQ3ODUyMDMz.DolV4w.Hs5luolow76s-C-wJ66H_s0RTXU");
			await client.StartAsync();

			while (true) await ConsoleCommands(Console.ReadLine());
		}

		public async Task ConsoleCommands(string command) {
			string args = command.Substring(command.IndexOf(" ") + 1);
			if (Utils.LowerEquals(command, "help")) {
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "Available commands:"));
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "status [status]"));
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "game [game]"));
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "send [guild id] [channel id] [message]"));
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "Available statuses: "));
				await Log(new LogMessage(LogSeverity.Info, "ConsoleCommands", "Online, Idle, DND, Offline"));
			} else if (command.StartsWith("status")) {
				if (Utils.LowerEquals(args, "online")) await client.SetStatusAsync(UserStatus.Online);
				if (Utils.LowerEquals(args, "idle")) await client.SetStatusAsync(UserStatus.Idle);
				if (Utils.LowerEquals(args, "dnd")) await client.SetStatusAsync(UserStatus.DoNotDisturb);
				if (Utils.LowerEquals(args, "offline")) await client.SetStatusAsync(UserStatus.Offline);
			} else if (command.StartsWith("game")) {
				ActivityType activity = new ActivityType();
				if (args.Contains("listening")) activity = ActivityType.Listening;
				else if (args.Contains("watching")) activity = ActivityType.Watching;
				else if (args.Contains("streaming")) activity = ActivityType.Streaming;
				else activity = ActivityType.Playing;

				//fix at some point, too lazy its 2 am pleas help
				string game = args.Replace("listening", "").Replace("watching", "").Replace("streaming", "");
				await client.SetGameAsync(game, null, activity);
			} else if (command.StartsWith("send")) {
				await ((ISocketMessageChannel)client.GetChannel(ulong.Parse(args.Split(' ')[0]))).SendMessageAsync(string.Join(" ", args.Split(' ').Skip(1).ToArray()));
			}
		}

		public async Task InstallCommands() {
			client.MessageReceived += HandleCommand;
			await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
		}

		public async Task HandleCommand(SocketMessage messageParam) {
			// Don't process the command if it was a System Message
			if (!(messageParam is SocketUserMessage message)) return;

			int argPos = 0;
			if (!(message.HasCharPrefix(Char.Parse(prefix), ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

			var context = new CommandContext(client, message);
			var result = await commands.ExecuteAsync(context, argPos, services);
			if (!result.IsSuccess && result.ErrorReason != "Unknown command.") {
				EmbedBuilder eb = new EmbedBuilder();
				eb.WithTitle("OOPSIE WOOPSIE!! OwO We made a fucky wucky!! A wittle fucko boingo! The code monkeys at our headquarters are working VEWY HAWD to fix this!");
				eb.WithColor(Color.Red);
				eb.WithDescription(result.ErrorReason);
				await context.Channel.SendMessageAsync("", false, eb.Build());
				await Log(new LogMessage(LogSeverity.Error, context.Message.ToString(), result.ErrorReason));
			}
		}

		public static Task Log(LogMessage msg) {
			
			if (msg.Severity == LogSeverity.Critical || msg.Severity == LogSeverity.Error) Console.ForegroundColor = ConsoleColor.Red;
			if (msg.Severity == LogSeverity.Warning) Console.ForegroundColor = ConsoleColor.Yellow;
			if (msg.Severity == LogSeverity.Info) Console.ForegroundColor = ConsoleColor.Cyan;
			if (msg.Severity == LogSeverity.Debug) Console.ForegroundColor = ConsoleColor.Magenta;
			using (StreamWriter file = new StreamWriter("client.log", true)) {
				file.WriteLine(DateTime.UtcNow + " | " + msg.Severity + " | " + msg.Source + " | " + msg.Message);
			}
			Console.WriteLine(msg.ToString());
			Console.ForegroundColor = ConsoleColor.White;
			return Task.CompletedTask;
		}
	}
}