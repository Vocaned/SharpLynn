using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.IO;
using System.Diagnostics;

namespace SharpLynn.Commands {
	public class ShellCmd : ModuleBase {
		public static IUserMessage outMessage;

		[Command("shell"), Summary("Access the shell"), Alias("cmd"), RequireOwner()]
		public async Task Shell([Remainder] string command) {
			Process process = new Process();
			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.FileName = "cmd.exe";
			startInfo.UseShellExecute = false;
			startInfo.Arguments = "/C @echo off & cls & " + command + " & exit";
			startInfo.RedirectStandardOutput = true;
			process.EnableRaisingEvents = true;
			process.StartInfo = startInfo;
			await Program.Log(new LogMessage(LogSeverity.Info, "Shell access @ " + Context.Message.Id, Context.Message.Author.ToString() + " used " + Context.Message.Content));
			process.Exited += async (sender, args) =>
			{
				StreamReader sr = process.StandardOutput;
				string output = sr.ReadToEnd();
				await outMessage.ModifyAsync(msg => msg.Content = "```" + Environment.NewLine + output + "```");
				process.Dispose();
			};
			outMessage = await Context.Channel.SendMessageAsync("Shell started. Please wait...");
			await Task.Run(() => process.Start());
		}
	}
}