using UnityEngine;
using System.Collections;

public class HintDisplay : MonoBehaviour
{
    public string hintStr;
    public GUIStyle style;

    public enum HintEvent
    {
        SlopeSlow, Conveyor, BigSlopeSlow, SwitchAndElevator, JumpAndSpeedUp, WeightAndElevator, Button, S_SlopeAndMagnetism , GearAndTheEnd
    }

    public HintEvent hintEvent;

    // Use this for initialization
    void Start()
    {
        boxs = transform.GetComponentsInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float left;
    public float top;
    public float width;
    public float height;

    public Texture t1;

    public BoxCollider[] boxs;

    //void OnTriggerEnter(Collider other)
    //{
    //    print(other.attachedRigidbody.name);
    //}

    //void OnCollisionEnter()
    //{
        
    //}

    //void OnGUI()
    //{
    //    //GUI.Box(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintStr, style);
    //    //GUI.DrawTexture(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), t1);

    //    //GUI.Label(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintStr , style);

    //}

    
}
