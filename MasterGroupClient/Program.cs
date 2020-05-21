using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MasterGroupClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static async Task GetMessageId(String id)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:1337");
            client.DefaultRequestHeaders.Add("X-Key", "8796");
            client.DefaultRequestHeaders.Add("X-Route", "/message");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var url = "message/"+ id;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<message> contributors = JsonConvert.DeserializeObject<List<message>>(resp);
            contributors.ForEach(Console.WriteLine);
        }

        private static async Task GetMessageTags(String tag)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:1337");
            client.DefaultRequestHeaders.Add("X-Key", "8796");
            client.DefaultRequestHeaders.Add("X-Route", "/message");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var url = "messages/" + tag;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<message> contributors = JsonConvert.DeserializeObject<List<message>>(resp);
            contributors.ForEach(Console.WriteLine);
        }

        static async Task PostMessage(message msg)
        {

            var json = JsonConvert.SerializeObject(msg);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:1337/message";
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Key", "8796");
            client.DefaultRequestHeaders.Add("X-Route", "/message");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }

        static async Task PostMessage(credential cred)
        {

            var json = JsonConvert.SerializeObject(cred);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:1337/credential";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
        }
    }
}
