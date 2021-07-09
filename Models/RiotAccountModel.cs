using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Models
{
    public class RiotAccountModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Puuid { get; set; }
        public string Name { get; set; }
        public int ProfileIconId { get; set; }
        public int SummonerLevel { get; set; }
    }
}
