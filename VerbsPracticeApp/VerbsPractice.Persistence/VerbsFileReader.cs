using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace VerbsPractice.Persistence
{
    public class VerbsFileReader : IVerbsFileReader
    {
        public List<VerbData> LoadVerbs(string rootFolder)
        {
            string jsonString;
            string templatePath = Path.Combine(Path.Combine(rootFolder, "Data"), "verbs.txt");

            jsonString = File.ReadAllText(templatePath);
            var allVerbs = JsonSerializer.Deserialize<List<VerbData>>(jsonString);
            return allVerbs.OrderBy(v => v.Infinitief).ToList();
        }
    }
}