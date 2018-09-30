using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using System.Globalization;
using System.IO;

namespace SharpLynn.Commands {
	public class ModuleManagerCmd : ModuleBase {

		[Command("modulemanager"), Summary("Manages modules"), Alias("module", "modules"), RequireOwner()]
		public async Task ModuleManager(string action, string module) {
			switch (action) {
				case "load":
					
				break;
				case "unload":

				break;
				default:
				break;
			}
		}
	}
}