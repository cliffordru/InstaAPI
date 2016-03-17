using InstaAPI.Services.BusinessLogicServices.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using InstaAPI.Services.DomainModel.Instragram;

namespace InstaAPI.Services.BusinessLogicServices
{
    public class InstagramApiService : IInstagramApiService
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["InstagramClientId"];
        private readonly string _baseUri = "https://api.instagram.com/v1/";
        InstaPostsRoot IInstagramApiService.GetPostsByTag(string tag)
        {            
            return GetPostsByTagAsync(tag).Result;            
        }

        InstaPostRoot IInstagramApiService.GetPost(string instagramId)
        {
            return GetPostAsync(instagramId).Result;
        }        

        private async Task<InstaPostsRoot> GetPostsByTagAsync(string tag)
        {
            var jsonString = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"tags/{tag}/media/recent?client_id={_clientId}"
                    , HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<InstaPostsRoot>(jsonString);            
        }

        private async Task<InstaPostRoot> GetPostAsync(string instagramId)
        {
            var jsonString = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUri);

                var response = await client.GetAsync($"media/{instagramId}?client_id={_clientId}"
                    , HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStringAsync();
                }
            }

            return JsonConvert.DeserializeObject<InstaPostRoot>(jsonString);
        }
    }
}
