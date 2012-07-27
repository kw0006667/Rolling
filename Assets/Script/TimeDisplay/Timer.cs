using UnityEngine;
using System.Collections;

class Timer : MonoBehaviour
{
    private float currentTime;
    private float originTime;
    private int hour;
    private int min;
    private float sec;
    public string TimerStr;

    public GUIStyle style;

    public bool isStart = false;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (isStart)
        {
            currentTime = Time.time - originTime;
        }
        HandleTimer();
    }

    void HandleTimer()
    {
        hour = (int)currentTime / 3600;
        min = (int)((currentTime - hour * 3600) / 60);
        sec = (currentTime - hour * 3600 - min * 60);
        TimerStr = hour.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00.00");
    }

    void OnGUI()
    {
        GUI.TextField(new Rect(Screen.width - 300, 100, 100, 100), TimerStr, style);
    }

    public void ResetTimer()
    {
        currentTime = 0;
        originTime = Time.time;
    }
}