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
                titleStr = "�о���";
                break; 
            case "FirstStage":
                titleStr = "�Ĥ@��";
                break;
            case "SecondStage":
                titleStr = "�ĤG��";
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