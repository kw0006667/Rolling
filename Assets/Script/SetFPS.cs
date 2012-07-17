using UnityEngine;
using System.Collections;

public class SetFPS : MonoBehaviour {

    private DisplayFPS displayFPS;

	// Use this for initialization
	void Start () 
    {
        this.displayFPS = this.GetComponent<DisplayFPS>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (this.displayFPS.GetFPS() >= 70)
            Application.targetFrameRate = 70;
	}
}
