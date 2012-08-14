using UnityEngine;
using System.Collections;

public class TitleDisplay : MonoBehaviour
{
    public GameDefinition.Scene scene;
    public int waitTime = 3;
    public GUIStyle style;

    private Rect titleAreaRect = new Rect(0, 0, 900, 585);
    private string titleStr;
    private float currentTime = 0;

    // Use this for initialization
    void Start()
    {
        switch (this.scene)
        {
            
            case GameDefinition.Scene.Begin:
                titleStr = "教學關";
                break;
            case GameDefinition.Scene.FirstStage:
                titleStr = "第一關";
                break;
            case GameDefinition.Scene.SecondStage:
                titleStr = "第二關";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.time);
        this.currentTime += Time.deltaTime;
        if (this.currentTime > this.waitTime)
        {
            this.style.normal.textColor = new Color(this.style.normal.textColor.r, this.style.normal.textColor.g, this.style.normal.textColor.b, 1 - (this.currentTime - this.waitTime));
            if (this.currentTime > this.waitTime + 1)
                Destroy(this);
        }
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