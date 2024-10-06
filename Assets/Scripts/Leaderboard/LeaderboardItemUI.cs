using TMPro;
using UnityEngine;

public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _position;
    [SerializeField]
    private TextMeshProUGUI _player;
    [SerializeField]
    private TextMeshProUGUI _score;

    public void SetLeaderboardItem(int position, string playerName, int score)
    {
        _position.text = position.ToString();
        _player.text = playerName;

        float minutes = Mathf.FloorToInt(score / 60);
        float seconds = Mathf.FloorToInt(score % 60);

        _score.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}