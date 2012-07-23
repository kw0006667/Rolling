using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour
{

    public GameObject StartWall;

    private GameObject TimerObject;
    private Timer timer;

    private float originTime;
    private int currentTime;

    private string Text;
    private bool isFinish = false;

    private float fontSize = 0;
    public float minfontSize = 75;
    public float maxfontSize = 155;

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



    // Update is called once per frame
    void Update()
    {
        currentTime = (int)(Time.time - originTime);
        fontSize = Mathf.Lerp(minfontSize, maxfontSize, Time.time % 1);
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
        style.fontSize = (int)fontSize;
        style.normal.textColor = new Color(style.normal.textColor.r, style.normal.textColor.g, style.normal.textColor.b, (Time.time % 1));

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), Text, style);
    }
}