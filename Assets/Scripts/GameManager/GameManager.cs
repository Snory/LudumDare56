using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    MainMenu,
    Leaderboard,
    Gameplay,
    Pause,
    GameOver,
    Win
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameStates _currentGameState;

    public GeneralEvent GameStateChanged;

    public void OnGameplaySceneLoaded()
    {
        TransitToState(GameStates.Gameplay);
    }

    public void OnLeaderboardLoaded()
    {
        TransitToState(GameStates.Leaderboard);
    }

    public void OnMainMenuLoaded()
    {
        TransitToState(GameStates.MainMenu);
    }

    public void OnGameOver()
    {
        TransitToState(GameStates.GameOver);
    }

    public void OnExitGame()
    {
        QuitGame();
    }

    public void OnWin()
    {
        TransitToState(GameStates.Win);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.LoadScene(SceneNames.Logo, LoadSceneMode.Single);
        TransitToState(GameStates.MainMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if(_currentGameState != GameStates.Gameplay && _currentGameState != GameStates.Pause) return;

        if (_currentGameState == GameStates.Pause)
        {
            TransitToState(GameStates.Gameplay);
        }
        else if (_currentGameState == GameStates.Gameplay)
        {
            TransitToState(GameStates.Pause);
        }
    }

    private async void TransitToState(GameStates newState)
    {
        _currentGameState = newState;
        switch (_currentGameState)
        {
            case GameStates.Pause:
                Time.timeScale = 0;
                break;
            case GameStates.GameOver:
                Time.timeScale = 0;
                break;
            case GameStates.Win:
                Time.timeScale = 0;
                await WaitForSeconds(1); // wait for 1 second
                break;
            default:
                Time.timeScale = 1;
                break;
        }

        GameStateChanged.Raise(new GameStateChangeEventArgs(_currentGameState));
        Debug.Log($"Game state changed to {_currentGameState}");
    }

    public async Task WaitForSeconds(float seconds)
    {
        float endTime = Time.unscaledTime + seconds;

        while (Time.unscaledTime < endTime)
        {
            await Task.Yield();
        }
    }

    private void QuitGame()
    {
        PlayerPrefs.Save();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}