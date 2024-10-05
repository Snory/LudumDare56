using System;

public class AddPlayerEventArgs : EventArgs
{
    public PlayerData PlayerData;

    public AddPlayerEventArgs(PlayerData playerData)
    {
        this.PlayerData = playerData;
    }
}