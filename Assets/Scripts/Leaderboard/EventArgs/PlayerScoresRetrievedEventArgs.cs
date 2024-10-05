using System;
using UnityEngine;

internal class PlayerScoresRetrievedEventArgs : EventArgs
{
    public GameObject SourceGameObject;
    public PlayerScoreDataList PlayerScores;

    public PlayerScoresRetrievedEventArgs(GameObject sourceGameObject, PlayerScoreDataList playerScores)
    {
        SourceGameObject = sourceGameObject;
        PlayerScores = playerScores;
    }
}