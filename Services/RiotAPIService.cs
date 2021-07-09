using LeagueOfFlamers.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Services
{
    public class RiotAPIService : IRiotAPIService
    {
        // TODO: Move to own class
        private readonly string baseAddress = "https://euw1.api.riotgames.com";
        private readonly HttpClient httpClient;

        public RiotAPIService(IConfiguration config, IHttpClientFactory clientFactory) {
            var apiKey = config.GetValue<string>("RiotSettings:ApiKey");
            var httpClient = clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
            this.httpClient = httpClient;
        }

        public async Task<RiotAccountModel> getUserAccountInfo(string summonerName)
        {
            string responseBody = await httpClient.GetStringAsync($"{baseAddress}/lol/summoner/v4/summoners/by-name/{summonerName}");

            return JsonConvert.DeserializeObject<RiotAccountModel>(responseBody);
        }

        public async Task<List<ChampionMastery>> getChampionMasteryPoints(string accountId)
        {
            string masteryResponse = await httpClient.GetStringAsync($"{baseAddress}/lol/champion-mastery/v4/champion-masteries/by-summoner/{accountId}");
            string championsResponse = await httpClient.GetStringAsync($"http://ddragon.leagueoflegends.com/cdn/11.13.1/data/en_US/champion.json");
            var championsResponseData = JObject.Parse(championsResponse)["data"].ToString();

            var championMasteryPointsList = JsonConvert.DeserializeObject<List<ChampionMastery>>(masteryResponse);
            var champions = JsonConvert.DeserializeObject<Dictionary<string, ChampionModel>>(championsResponseData);

            for (int i = 0; i < championMasteryPointsList.Count(); i++)
            {
                var championMasteryPoints = championMasteryPointsList[i];
                var champion = champions.FirstOrDefault(x => int.Parse(x.Value.Key) == championMasteryPoints.ChampionId);
                championMasteryPoints.Name = champion.Value.Name;
            }

            return championMasteryPointsList;
        }

        public async Task<CurrentGameInfo> getCurrentGameInfo(string accountId)
        {
            string currentGameInfo = await httpClient.GetStringAsync($"{baseAddress}/lol/spectator/v4/active-games/by-summoner/{accountId}");

            return JsonConvert.DeserializeObject<CurrentGameInfo>(currentGameInfo);
        }
    }
}
