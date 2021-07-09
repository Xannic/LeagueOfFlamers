using LeagueOfFlamers.Logic;
using LeagueOfFlamers.Models;
using LeagueOfFlamers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToxicityController : ControllerBase
    {
        private readonly IRatingCalculater toxicRatingCalculator;

        public ToxicityController(IRatingCalculater toxicRatingCalculator)
        {
            this.toxicRatingCalculator = toxicRatingCalculator;
        }

        [HttpGet]
        [Route("{summonerName}")]
        public async Task<JsonResult> Get(string summonerName)
        {
            var toxicRating = await toxicRatingCalculator.CalculateRating(summonerName);

            return new JsonResult(new { summonerName =  summonerName, rating = toxicRating});
        }

        [HttpGet]
        [Route("livegame/{summonerName}")]
        public async Task<JsonResult> GetAllSummonersInGame(string summonerName)
        {
            var toxicRatings = await toxicRatingCalculator.CalculateGameRatings(summonerName);

            return new JsonResult(toxicRatings);
        }
    }
}
