using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _leaderBoardCanvas;

    [SerializeField]
    private Transform _leaderboardItemParent;

    [SerializeField]
    private GameObject _leaderboardItemPrefab;

    [SerializeField]
    private GeneralEvent _retrievePlayerScoresEvent;

    [SerializeField]
    private GameObject _waitingForData;

    public void ShowLeaderboard() {
       
        Debug.Log("ShowLeaderboard");

        _leaderBoardCanvas.SetActive(true);
        
        foreach (Transform child in _leaderboardItemParent)
        {
            Destroy(child.gameObject);
        }

        _waitingForData.SetActive(true);
        _retrievePlayerScoresEvent.Raise(new RetrievePlayerScoresEventArgs(this.gameObject));
    }

    public void HideLeaderboard()
    {
        _leaderBoardCanvas.SetActive(false);
    }

    public void OnPlayerScoresRetrieved(EventArgs eventArgs)
    {
        PlayerScoresRetrievedEventArgs playerScoresRetrievedEventArgs = (PlayerScoresRetrievedEventArgs)eventArgs;

        if (playerScoresRetrievedEventArgs.SourceGameObject != this.gameObject)
        {
            return;
        }

        var scores = playerScoresRetrievedEventArgs.PlayerScores.PlayerScores.OrderByDescending(s => s.Score).ToList();

        for (int i = 0; i < scores.Count; i++)
        {
            var leaderboardItem = Instantiate(_leaderboardItemPrefab, _leaderboardItemParent);
            var score = scores[i];
            leaderboardItem.GetComponent<LeaderboardItemUI>().SetLeaderboardItem(i + 1, score.Username, score.Score);
        }

        _waitingForData.SetActive(false);
    }
}