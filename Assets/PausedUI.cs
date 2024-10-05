using UnityEngine;

public class PausedUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausedCanvas;

    public void ShowPaused()
    {
        _pausedCanvas.SetActive(true);
    }

    public void HidePaused()
    {
        Debug.Log("HidePaused");
        _pausedCanvas.SetActive(false);
    }
}