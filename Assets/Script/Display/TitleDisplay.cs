using UnityEngine;
using System.Collections;

public class TitleDisplay : MonoBehaviour
{
    public GUIStyle style;
    private Rect titleAreaRect = new Rect(0, 0, 900, 585);

    private string titleStr;

    public int waitTime = 5;
    private float currentTime = 0;

    // Use this for initialization
    void Start()
    {
        switch (Application.loadedLevelName)
        {
            case "Begin":
                titleStr = "教學關";
                break; 
            case "FirstStage":
                titleStr = "第一關";
                break;
            case "SecondStage":
                titleStr = "第二關";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.currentTime += Time.deltaTime;
    }

    void OnGUI()
    {
        this.titleAreaRect = new Rect((Screen.width - (int)this.titleAreaRect.width) / 2,
                                     (Screen.height - (int)this.titleAreaRect.height) / 2,
                                      this.titleAreaRect.width,
                                      this.titleAreaRect.height);
        GUI.Label(titleAreaRect, titleStr, style);
    }
}