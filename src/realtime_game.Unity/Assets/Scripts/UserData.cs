using Newtonsoft.Json;
public class UserData
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }
}
