using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshProGUI;

    public void OnTimerChanged(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _textMeshProGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
