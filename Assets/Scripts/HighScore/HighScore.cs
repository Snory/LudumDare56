using System;
using UnityEngine;
using UnityEngine.Events;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private string _playerName;

    [SerializeField]
    private GeneralEvent _addPlayerScore;

    [SerializeField]
    public int _score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerName = PlayerPrefs.GetString(NameChooserUI.LastUsedName);
    }

    public void OnTimerChanged(float currentTimeInSeconds)
    {
        _score = (int)currentTimeInSeconds;
    }

    public void OnEndGame()
    {
        var playerScoreAddData = new PlayerScoreData
        {
            Username = _playerName,
            Score = _score
        };

        _addPlayerScore.Raise(new AddPlayerScoreEventArgs(playerScoreAddData));
    }
}
