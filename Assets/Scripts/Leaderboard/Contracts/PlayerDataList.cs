using Newtonsoft.Json;
using System.Collections.Generic;

public class PlayerDataList
{
    [JsonProperty("users")]
    public List<PlayerData> Players { get; set; } = new List<PlayerData>();
}