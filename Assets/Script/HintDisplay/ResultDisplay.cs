using UnityEngine;
using System.Collections;

public class ResultDisplay : MonoBehaviour
{
    public string hintStr;
    public GUIStyle style;

    public float left;
    public float top;
    public float width;
    public float height;


    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintStr, style);
        //GUI.DrawTexture(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), t1);

        //GUI.Label(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintStr , style);

    }
}
