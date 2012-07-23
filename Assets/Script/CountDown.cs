using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

    public GameObject StartWall;

    private GameObject TimerObject;
    private Timer timer;

    private float originTime;
    private int currentTime;

    private string Text;

    public int countDown = 3;
    public int waitTime = 2;
    public GUIStyle style;

	// Use this for initialization
    void Start()
    {
        TimerObject = transform.gameObject;
        timer = TimerObject.GetComponent<Timer>();
        originTime = Time.time;
    }

    bool isFinish = false;
	
	// Update is called once per frame
    void Update()
    {
        currentTime = (int)(Time.time - originTime);
        if (currentTime > waitTime)
        {
            if (countDown > 0)
            {
                Text = countDown.ToString();
                countDown--;
                waitTime++;
            }
            else
            {
                Text = "GO";
                isFinish = true;
                originTime = Time.time;
            }
        }
        if (isFinish)
        {
            if (Time.time - originTime > 0.5f)
            {
                timer.isStart = true;
                timer.ResetTimer();
                Destroy(StartWall);
                Destroy(this);
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), Text, style);
    }
}
