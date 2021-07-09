using LeagueOfFlamers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Services
{
    public interface IRiotAPIService
    {
        Task<RiotAccountModel> getUserAccountInfo(string summonerName);

        Task<List<ChampionMastery>> getChampionMasteryPoints(string accountId);

        Task<CurrentGameInfo> getCurrentGameInfo(string accountId);
    }
}
