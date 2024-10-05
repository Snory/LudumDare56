using System;
using UnityEngine;
using UnityEngine.Events;

public class GameStateChangeListener : MonoBehaviour
{
    public UnityEvent OnSceneChanged;

    [SerializeField]
    private GameStates _gameState;

    public void OnGameStateChanged(EventArgs args)
    {
        GameStateChangeEventArgs gameStateChangeEventArgs = (GameStateChangeEventArgs)args;
        if (gameStateChangeEventArgs.CurrentGameState != _gameState)
        {
            return;
        }
        OnSceneChanged.Invoke();
    }
}
