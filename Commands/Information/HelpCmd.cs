using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;


namespace SharpLynn.Commands {
	// Main module
	public class HelpCmd : ModuleBase {

		[Command("help"), Summary("Shows information about other commands"), Remarks(Program.prefix + "help")]
		public async Task NormalHelp() {
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("Current commands:");
			List<string> used = new List<string>();
			foreach (CommandInfo command in Program.commands.Commands) {
				if (used.Contains(command.Name)) continue;

				string summary = command.Summary;
				string aliases = "";
				string[] aliaslist = new string[command.Aliases.Count];
				for (int i = 0; i < command.Aliases.Count; i++) {
					aliaslist[i] = command.Aliases[i];
				}
				aliases += string.Join(" / ", aliaslist);
				if (command.Summary.Length < 1) summary = "No information available";

				eb.AddField(aliases, summary);
				used.Add(command.Name);
			}
			eb.WithColor(Color.Purple);
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}

		[Command("help"), Remarks("Usage: " + Program.prefix + "help [command]"), Priority(1)]
		public async Task Help([Remainder()] string commandName) {
			if (commandName.Length < 1) await NormalHelp();
			string[] args = commandName.Split(' ');
			commandName = args[args.Length - 1];
			foreach (CommandInfo command in Program.commands.Commands) {
				if (command.Name.ToLower() == commandName.ToLower()) {
					await CommandHelp(command);
					return;
				}
			}
		}

		public async Task CommandHelp(CommandInfo command) {
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("Information about command **__" + command.Name + "__**");
			string summary = command.Summary;
			string usage = command.Remarks;
			if (command.Summary.Length < 1) summary = "No information available";
			if (command.Remarks.Length < 1) usage = "No information available";
			eb.AddField(command.Name, summary);
			eb.AddField("Usage:", usage);

			eb.WithColor(Color.Purple);
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}

	/* Submodule example
	[Group("help")]
	public class HelpGroup : ModuleBase {
		[Command("me"), Summary("test")]
		public async Task Me() {
			await Context.Channel.SendMessageAsync("No, I won't help you.");
		}
	}*/
}