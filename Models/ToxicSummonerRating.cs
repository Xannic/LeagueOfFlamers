﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Models
{
    public class ToxicSummonerRating
    {
        public long TeamId { get; set; }
        public string SummonerName { get; set; }
        public int Rating { get; set; }
    }
}
