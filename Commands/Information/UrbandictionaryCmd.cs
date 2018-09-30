using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord;
using Discord.Commands;
using System.Globalization;

namespace SharpLynn.Commands {
	public class UrbandictionaryCmd : ModuleBase {

		[Command("urbandictionary"), Summary("Defines a word using the Urban Dictionary"), Remarks(Program.prefix + "urbandictionary [term]"), Alias("urban")]
		public async Task Urbandictionary([Remainder] string definition) {
			string termInfo = UrbandictionaryAPI.getDefinition(definition);
			dynamic api = JsonConvert.DeserializeObject(termInfo);
			dynamic first = api.list[0];
			string date = (string)first.written_on;
			string def = (string)first.definition;
			string example = (string)first.example;
			EmbedBuilder eb = new EmbedBuilder();
			eb.WithTitle("**Definition of " + first.word + ":**");
			eb.WithUrl((string)first.permalink);
			eb.WithDescription(def.Replace("[", "").Replace("]", "") + Environment.NewLine);
			eb.AddField("Example: ", example.Replace("[", "").Replace("]", ""));
			eb.WithColor(Color.Orange);
			eb.WithFooter((string)first.thumbs_up + "👍 - " + (string)first.thumbs_down + "👎. by " + (string)first.author);
			
			eb.WithTimestamp(DateTime.ParseExact(date.Split(' ')[0], "MM/dd/yyyy", CultureInfo.InvariantCulture));
			await Context.Channel.SendMessageAsync("", false, eb.Build());
		}
	}
}