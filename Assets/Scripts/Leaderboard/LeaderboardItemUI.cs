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
        _score.text = score.ToString();
    }
}