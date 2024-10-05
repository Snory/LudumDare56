using Newtonsoft.Json;

public class PlayerData
{
    [JsonProperty("username")]
    public string Username { get; set; }
}