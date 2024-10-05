using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public partial class Leaderboard : Singleton<Leaderboard>
{
    private string _token;
    private const string _getPlayersEndpoint = "https://batiko.pythonanywhere.com/api/players";
    private const string _postPlayerEndpoint = "https://batiko.pythonanywhere.com/api/players/player";
    private const string _getScoreEndpoint = "https://batiko.pythonanywhere.com/api/score";
    private const string _postScoreEndpoint = "https://batiko.pythonanywhere.com/api/score/player_score";

    [SerializeField]
    private GeneralEvent _playerAdded, _playerScoreRetrieved, _playerScoreAdded;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        _token = LeaderboardConfiguration.Token;

        Debug.Log("Leaderboard loaded");
    }

    public async void OnAddPlayerScoreEvent(EventArgs eventArgs)
    {
        AddPlayerScoreEventArgs playerScoreDataAddEvent = (AddPlayerScoreEventArgs)eventArgs;
        await PostPlayerScore(playerScoreDataAddEvent.PlayerScoreData);
    }

    public async void OnAddPlayerEvent(EventArgs eventArgs)
    {
        AddPlayerEventArgs playerdataAddEvent = (AddPlayerEventArgs)eventArgs;

        // check if player aready exists
        PlayerDataList players = await GetPlayers();

        foreach (PlayerData player in players.Players)
        {
            if (player.Username == playerdataAddEvent.PlayerData.Username)
            {
                _playerAdded.Raise(new PlayerAddedEventArgs(false));
                return;
            }
        }

        await PostNewPlayer(playerdataAddEvent.PlayerData);
    }

    public async void OnRetrievePlayerScores(EventArgs eventArgs)
    {
        RetrievePlayerScoresEventArgs retrievePlayerScoresEventArgs = (RetrievePlayerScoresEventArgs)eventArgs;
        PlayerScoreDataList playerScores = await GetScores();
        _playerScoreRetrieved.Raise(new PlayerScoresRetrievedEventArgs(retrievePlayerScoresEventArgs.SourceGameObject, playerScores));
    }

    public async Task<PlayerDataList> GetPlayers()
    {
        return await GetData<PlayerDataList>(_getPlayersEndpoint);
    }

    public async Task<PlayerScoreDataList> GetScores()
    {
        return await GetData<PlayerScoreDataList>(_getScoreEndpoint);
    }

    public async Task PostNewPlayer(PlayerData playerData)
    {
        var playerdataAddEventSanitize = new PlayerData
        {
            Username = playerData.Username.Replace("\u200B", "")
        };
        await PostData(_postPlayerEndpoint, playerdataAddEventSanitize);
    }

    public async Task PostPlayerScore(PlayerScoreData playerScoreData)
    {

        //check if username contains zero width space

        var playerScoreSanitize = new PlayerScoreData
        {
            Username = playerScoreData.Username.Replace("\u200B", ""),
            Score = playerScoreData.Score
        };

        await PostData(_postScoreEndpoint, playerScoreSanitize);
    }

    private async Task PostData<T>(string endpoint, T data)
    {
        Debug.Log("Posting data to " + endpoint);

        string jsonData = JsonConvert.SerializeObject(data);

        using (UnityWebRequest request = UnityWebRequest.Post(endpoint, jsonData, "application/json"))
        {
            request.SetRequestHeader("Authorization", _token);

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error posting data: " + request.error);
            }

            _playerAdded.Raise(new PlayerAddedEventArgs(true));
        }
    }

    private async Task<T> GetData<T>(string endpoint)
    {
        Debug.Log("Getting data from " + endpoint);

        using (UnityWebRequest request = UnityWebRequest.Get(endpoint))
        {
            request.SetRequestHeader("Authorization", _token);

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error getting data: " + request.error);
            }

            return JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
        }
    }
}