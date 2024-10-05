using System;

public class GameStateChangeEventArgs : EventArgs
{
    public GameStates CurrentGameState;

    public GameStateChangeEventArgs(GameStates currentGameState)
    {
        this.CurrentGameState = currentGameState;
    }
}