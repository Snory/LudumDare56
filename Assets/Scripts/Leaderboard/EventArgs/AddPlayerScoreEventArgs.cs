using System;

internal class AddPlayerScoreEventArgs : EventArgs
{
    public PlayerScoreData PlayerScoreData;

    public AddPlayerScoreEventArgs(PlayerScoreData playerScoreData)
    {
        this.PlayerScoreData = playerScoreData;
    }
}