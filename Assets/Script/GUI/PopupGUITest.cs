using UnityEngine;
using System.Collections;

public class PopupGUITest : MonoBehaviour {

    public GUISkin guiskin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnGUI()
    {
        if (this.guiskin)
        {
            GUI.skin = guiskin;
        }

        GUI.TextArea(new Rect(Screen.width - 240, Screen.height - 340, 220, 300), "Hello world.");
        //GUI.TextField(new Rect(Screen.width - 240, Screen.height - 340, 220, 300), "Hello world.");
    }
}
