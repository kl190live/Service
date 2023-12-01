using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PeopleClient
{
    public class PersonData
    {
        private HttpClient client = new HttpClient();
        private string url = "https://retoolapi.dev/eCd8pr/data";

        public List<People> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<List<People>>(json);
        }

        public People Add(People person)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<People>(responseContent);
        }

        public bool Delete(People person)
        {
            int id = person.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public People Update(int id, People person)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<People>(responseContent);
        }
    }
}
