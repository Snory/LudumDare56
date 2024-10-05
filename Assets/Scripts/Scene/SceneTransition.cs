using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Scene _currentScene;

    [SerializeField]
    private float _waitBeforeTransit = 1f;

    [SerializeField]
    private Animator _sceneTransitionAnimatorController;

    [SerializeField]
    private GeneralEvent _sceneLoadedEvent;

    private void Awake()
    {
        _currentScene = SceneManager.GetActiveScene();
        Debug.Log($"Current scene: {_currentScene.name}");
    }

    public void ReloadScene()
    {
        TransitToScene(_currentScene.name);
    }

    public async void TransitToScene(string newSceneName)
    {
        var previousScene = _currentScene.name;
        _sceneTransitionAnimatorController.SetTrigger("EndScene");
        await WaitForSeconds(_waitBeforeTransit);
        await LoadScene(newSceneName);

        if (SceneManager.sceneCount > 1)
        {
            await UnloadScene(previousScene);
        }
    }

    public async Task WaitForSeconds(float seconds)
    {
        float endTime = Time.unscaledTime + seconds;

        while (Time.unscaledTime < endTime)
        {
            Debug.Log("Waiting for seconds");
            await Task.Yield();
        }
    }

    /// <summary>
    /// Used to call from animator so I can set time to 1 in game manager
    /// </summary>
    public void SceneLoaded()
    {
        if (_sceneLoadedEvent is not null)
        {
            _sceneLoadedEvent.Raise();
        }
    }

    private async Task UnloadScene(string currentSceneName)
    {
        Debug.Log("Unloading current scene");
        await SceneManager.UnloadSceneAsync(currentSceneName);
    }

    private async Task LoadScene(string newSceneName)
    {
        Debug.Log($"Loading new scene: {newSceneName}");
        await SceneManager.LoadSceneAsync(newSceneName);
        _currentScene = SceneManager.GetSceneByName(newSceneName);
    }
}