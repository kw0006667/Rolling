using UnityEngine;
using System.Collections;

class Timer : MonoBehaviour
{
    private static float times;
    public static float deltaTime { get { return Time.deltaTime/times; } }
    
    private float t;
    private float s;
    public GUIStyle style;

    public int hour;
    public int min;
    public float sec;

    public string str;

    void Start()
    {
        s = Time.time;
    }

    void Update()
    {
        t = Time.time - s;

        //times = t / 0.016f;

        //s = Time.time;
        //str = timer.hour.ToString("00") + ":" + timer.min.ToString("00") + ":" + timer.sec.ToString("00");
        HandleTimer();
    }

    void HandleTimer()
    {
        hour = (int)t / 3600;
        min = (int)((t - hour * 3600) / 60);
        sec = (t - hour * 3600 - min * 60);
        str = hour.ToString("00") + ":" + min.ToString("00") + ":" + sec.ToString("00.00");
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), times.ToString() + " = " + t.ToString() + " / 0.016");e
        GUI.TextField(new Rect(Screen.width-300, 100, 100, 100), str,style);

    }


}
