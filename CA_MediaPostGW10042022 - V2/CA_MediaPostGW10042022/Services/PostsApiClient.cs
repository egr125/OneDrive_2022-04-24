using MediaUI.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MediaUI.Services
{
    public class PostsApiClient
    {
        public HttpClient Client { get; set; }

        public PostsApiClient(HttpClient client) {

            client.BaseAddress = new System.Uri("https://localhost:44310");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;
        }

        public async Task<IEnumerable<Post>> GetPostsList()
        {
            return await Client.GetFromJsonAsync<IEnumerable<Post>>("/api/Posts");
        }

    }
}
