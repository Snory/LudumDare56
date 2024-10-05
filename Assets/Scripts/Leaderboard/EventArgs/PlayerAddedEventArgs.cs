using System;

public class PlayerAddedEventArgs : EventArgs
{
    public bool Success;

    public PlayerAddedEventArgs(bool success)
    {
        this.Success = success;
    }
}