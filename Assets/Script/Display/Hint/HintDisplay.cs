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
        GUI.Box(new Rect(Screen.width - Screen.width * this.left, 
                         Screen.height * this.top, 
                         Screen.width * this.width, 
                         Screen.height * this.height), this.hintTexture, this.style);
    }
}