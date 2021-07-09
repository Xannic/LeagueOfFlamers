using LeagueOfFlamers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Logic
{
    public interface IRatingCalculater
    {
        Task<int> CalculateRating(string summonerName);

        Task<List<ToxicSummonerRating>> CalculateGameRatings(string summonerName);
    }
}
