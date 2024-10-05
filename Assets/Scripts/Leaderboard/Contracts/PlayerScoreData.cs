using Newtonsoft.Json;

public class PlayerScoreData
{
    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("score")]
    public int Score { get; set; }
}