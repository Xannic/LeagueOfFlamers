using LeagueOfFlamers.Models;
using LeagueOfFlamers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Logic
{
    public class ToxicityRatingCalculator : IRatingCalculater
    {
        private readonly IRiotAPIService riotAPIService;
        private readonly List<string> toxicChampions = new List<string>() { "Akali", "Yasuo", "Cassiopeia", "Draven", "Darius", "Fiora", "Katarina", "Master Yi", "Qiyana", "Rengar", "Riven", "Shaco", "Teemo", "Zed", "Yone", "Lee Sin" };

        public ToxicityRatingCalculator(IRiotAPIService riotAPIService) {
            this.riotAPIService = riotAPIService;
        }

        public async Task<int> CalculateRating(string summonerName)
        {
            var accountInfo = await riotAPIService.getUserAccountInfo(summonerName);
            var championMasteryPoints = await riotAPIService.getChampionMasteryPoints(accountInfo.Id);

            return CalculateToxicity(championMasteryPoints);
        }

        public async Task<List<ToxicSummonerRating>> CalculateGameRatings(string summonerName) { 
            var toxicityList = new List<ToxicSummonerRating>();
            
            var accountInfo = await riotAPIService.getUserAccountInfo(summonerName);
            var currentGameInfo = await riotAPIService.getCurrentGameInfo(accountInfo.Id);

            foreach (var participant in currentGameInfo.Participants)
            {
                var rating = await CalculateRating(participant.SummonerName);
                toxicityList.Add(new ToxicSummonerRating() { TeamId = participant.TeamId, SummonerName = participant.SummonerName, Rating = rating });
            }

            return toxicityList;
        }

        private int CalculateToxicity(List<ChampionMastery> championMasteryPoints)
        {
            double totalMastery = 0;
            double totalToxicMastery = 0;

            foreach (var c in championMasteryPoints)
            {
                totalMastery += c.ChampionPoints;
                if (toxicChampions.Contains(c.Name))
                {
                    totalToxicMastery += c.ChampionPoints;
                }
            }

            var oldMax = (totalMastery / 100) * 30;
            var rating = ((100 - 0) / (oldMax - 0)) * (totalToxicMastery - 0) + 0;

            return (int)Math.Round(rating);
        }
    }
}
