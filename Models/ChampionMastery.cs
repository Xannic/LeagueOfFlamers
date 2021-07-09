using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfFlamers.Models
{
    public class ChampionMastery
    {
        public int ChampionId { get; set; }
        public string Name { get; set; }
        public double ChampionPoints { get; set; }
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime LastPlayTime { get; set; }
    }

    public class MicrosecondEpochConverter : DateTimeConverterBase
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null) { return null; }

        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds((long)reader.Value);
        return dateTimeOffset.DateTime;
    }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
