using System;

public class PlayerScoresAddedEventArgs : EventArgs
{
    public PlayerScoreData PlayerScoreData;

    public PlayerScoresAddedEventArgs(PlayerScoreData playerScoreData)
    {
        this.PlayerScoreData = playerScoreData;
    }
}