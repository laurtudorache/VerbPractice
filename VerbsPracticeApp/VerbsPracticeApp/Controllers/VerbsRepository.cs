using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace VerbsPracticeApp.Controllers
{
    public class VerbsRepository : IVerbsRepository
    {
        private List<VerbData> verbs;

        public VerbsRepository()
        {
            verbs = new List<VerbData>();
        }

        public int Count(bool isRegular, bool isBasic)
        {
            return verbs.Count(v => v.IsRegular == isRegular && v.IsBasic == isBasic);
        }

        public VerbData GetVerbByIndex(int index)
        {
            return verbs[index];
        }

        public void Initialize(IWebHostEnvironment environment)
        {
            string jsonString;
            string templatePath = Path.Combine(Path.Combine(environment.ContentRootPath, "Data"), "verbs.txt");

            jsonString = File.ReadAllText(templatePath);
            var allVerbs = JsonSerializer.Deserialize<List<VerbData>>(jsonString);
            verbs = allVerbs.OrderBy(v => v.Infinitief).ToList();
        }
    }
}