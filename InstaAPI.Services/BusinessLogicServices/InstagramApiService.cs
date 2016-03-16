using InstaAPI.Services.BusinessLogicServices.Interfaces;
using InstaAPI.Services.DomainModel;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaAPI.Services.BusinessLogicServices
{
    public class InstagramApiService : IInstagramApiService
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["InstagramClientId"];
        private readonly string _baseUri = "https://api.instagram.com/v1/";
        InstaPosts IInstagramApiService.GetPostsByTag(string tag)
        {            
            return GetRecent(tag).Result;            
        }
        
        private async Task<InstaPosts> GetRecent(string tag)
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

            return JsonConvert.DeserializeObject<InstaPosts>(jsonString);

            //InstaMedia model;
            
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(_baseUri);
            //    using (
            //        var response = await client.GetAsync(
            //                   $"tags/{tag}/media/recent?client_id={_clientId}", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            //        )
            //    {
            //        var jsonString = await response.Content.ReadAsStringAsync();                   
            //        model = JsonConvert.DeserializeObject<InstaMedia>(jsonString);
            //    }
            //}
                       
            //return model;
        }
    }
}
