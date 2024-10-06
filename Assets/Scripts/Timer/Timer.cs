using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float _currentTimeInSeconds;

    public UnityEvent<float> TimerChanged;

    private void Start()
    {
        TimerChanged.Invoke(_currentTimeInSeconds);
    }

    private void Update()
    {
        _currentTimeInSeconds += Time.deltaTime;
        TimerChanged.Invoke(_currentTimeInSeconds);
    }
}
