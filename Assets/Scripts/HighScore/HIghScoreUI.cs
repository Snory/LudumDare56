using TMPro;
using UnityEngine;

public class HIghScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _highScore;
    private void Awake()
    {
        _highScore.text = "0";
    }

    public void OnHighScoreUpdated(int score)
    {
        _highScore.text = score.ToString();
    }
}