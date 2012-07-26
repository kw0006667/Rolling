using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
    public Texture hintTexture;

    public GUIStyle style;
    
    public float left;
    public float top;
    public float width;
    public float height;

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintTexture, style);
    }
}