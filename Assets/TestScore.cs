using UnityEngine;

public class TestScore : MonoBehaviour
{
    public GeneralEvent ScoreAdd;

    public void AddScore()
    {
        var player = PlayerPrefs.GetString("LastUsedName");
        ScoreAdd.Raise(new AddPlayerScoreEventArgs(new PlayerScoreData { Username = player, Score = 100 }));
    }

}
