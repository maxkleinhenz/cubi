using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float SlowDownTimeScale = 0.05f;

    private float _startFixedDeltaTime;

    void Awake()
    {
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void SlowDown()
    {
        SetTimeScale(SlowDownTimeScale);
    }

    public void Realtime()
    {
        SetTimeScale(1);
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * _startFixedDeltaTime;
    }
}
