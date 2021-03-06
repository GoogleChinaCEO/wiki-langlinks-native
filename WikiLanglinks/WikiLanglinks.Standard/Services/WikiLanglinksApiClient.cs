﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WikiLanglinks
{
    public class WikiLanglinksApiClient : IWikiLanglinksApiClient
    {
        private static readonly HttpClient _httpClient;

        static WikiLanglinksApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://wiki-langlinks-api.now.sh/")
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<SearchResults> GetLanglinks(SearchRequest searchRequest)
        {
            var targetString = string.Join("|", searchRequest.Targets);
            var path = $"/langlinks?search={searchRequest.SearchTerm}&source={searchRequest.Source}&target={targetString}";
            var response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<SearchResults>(responseString);
            return results;
        }
    }
}
