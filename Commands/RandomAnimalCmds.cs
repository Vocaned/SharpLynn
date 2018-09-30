using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using System.Globalization;
using System.IO;

namespace SharpLynn.Commands {
	public class RandomCatCmd: ModuleBase {

		[Command("randomcat"), Summary("UwU"), Remarks(Program.prefix + "randomcat"), Alias("cat")]
		public async Task RandomCat() {
			dynamic api = JsonConvert.DeserializeObject(AnimalAPIs.getCat());
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithImageUrl((string)api.file);
			eb.WithFooter("Powered by random.cat");
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
	public class RandomDogCmd : ModuleBase {

		[Command("randomdog"), Summary("UwU"), Remarks(Program.prefix + "randomdog"), Alias("dog")]
		public async Task RandomDog() {
			dynamic api = JsonConvert.DeserializeObject(AnimalAPIs.getDog());
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithImageUrl((string)api.url);
			eb.WithFooter("Powered by random.dog");
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
	public class RandomFoxCmd : ModuleBase {

		[Command("randomfox"), Summary("UwU"), Remarks(Program.prefix + "randomfox"), Alias("fox")]
		public async Task RandomFox() {
			dynamic api = JsonConvert.DeserializeObject(AnimalAPIs.getFox());
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithImageUrl((string)api.image);
			eb.WithFooter("Powered by randomfox.ca");
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
	public class RandomShibaCmd : ModuleBase {

		[Command("randomshiba"), Summary("UwU"), Remarks(Program.prefix + "randomshiba"), Alias("doge", "shiba")]
		public async Task RandomShiba() {
			dynamic api = JsonConvert.DeserializeObject(AnimalAPIs.getDoge());
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithImageUrl((string)api[0]);
			eb.WithFooter("Powered by shibe.online");
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
	public class RandomBirdCmd : ModuleBase {

		[Command("randombird"), Summary("UwU"), Remarks(Program.prefix + "randombird"), Alias("birb", "bird")]
		public async Task RandomBird() {
			dynamic api = JsonConvert.DeserializeObject(AnimalAPIs.getBird());
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithImageUrl((string)api[0]);
			eb.WithFooter("Powered by shibe.online");
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
	public class RandomCharaCmd : ModuleBase {

		[Command("randomchara"), Summary("UwU"), Remarks(Program.prefix + "randomchara"), Alias("chara")]
		public async Task RandomChara() {
			var files = Directory.GetFiles("D:\\Repos\\SharpLynn\\res\\Chara", "*");
			await Context.Channel.SendFileAsync(files[new Random().Next(files.Length)]);
		}
	}
	public class AleksDoggoCmd : ModuleBase {

		[Command("aleksdoggo"), Summary("UwU"), Remarks(Program.prefix + "aleksdoggo"), Alias("alexdoggo")]
		public async Task AleksDoggo() {
			var files = Directory.GetFiles("D:\\Repos\\SharpLynn\\res\\Alexdoggo", "*");
			await Context.Channel.SendFileAsync(files[new Random().Next(files.Length)]);
		}
	}
}