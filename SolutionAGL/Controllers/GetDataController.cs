using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SolutionAGL.Models;
using Newtonsoft.Json;

namespace SolutionAGL.Controllers
{
    public class GetDataController : ApiController
    {
        
        private async Task<string> ReadJsonData()
        {
            const string url = "http://agl-developer-test.azurewebsites.net/people.json";

            string data;
            using (var client = new HttpClient())
            {
                // Read JSON data
                data = await client.GetStringAsync(url);
            }

            return data;
        }

        private async Task<List<PetData>> ParseJsonData()
        {
            var data = await ReadJsonData();

            // JSON to object
            var petDatas = JsonConvert.DeserializeObject<List<PetData>>(data);
            return petDatas;
        }

        [HttpGet]
        public async Task<PetDataDisplay> GetFormattedData()
        {
            var petDatas = await ParseJsonData();

            var malePetCats = new List<string>();
            var femalePetCats = new List<string>();

            foreach(var petData in petDatas)
            {
                var gender = petData.Gender.ToUpper();

                // Some people may not have pets
                if (petData.Pets == null)
                    continue;

                // Needs to pick all cat names - Some people have more than 1 pet cats
                var catNames = (from pet in petData.Pets where pet.Type.Equals("Cat", StringComparison.InvariantCultureIgnoreCase) select pet.Name).ToList();

                switch (gender)
                {
                    case "MALE":
                        if (catNames.Count > 0)
                            malePetCats.AddRange(catNames);
                        break;

                    case "FEMALE":
                        if (catNames.Count > 0)
                            femalePetCats.AddRange(catNames);
                        break;
                }
            }

            // Sort cat names alphabetically
            malePetCats.Sort();
            femalePetCats.Sort();

            var petDataDisplay = new PetDataDisplay
            {
                Male = malePetCats.ToArray(),
                Female = femalePetCats.ToArray()
            };

            return petDataDisplay;
        }
    }


}
