using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Services.Models
{
    public class CollectApiPharmacyDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("dist")]
        public string Dist { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("loc")]
        public string Loc { get; set; }
    }
}
