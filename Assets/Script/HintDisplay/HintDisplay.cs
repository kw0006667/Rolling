using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
    public string hintStr;
    public GUIStyle style;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 300, 100, 250, 200), hintStr, style);
    }
}
