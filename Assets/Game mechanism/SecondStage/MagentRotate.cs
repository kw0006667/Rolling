using UnityEngine;
using System.Collections;

public class MagentRotate : MonoBehaviour {

    public Transform CenterOfMass;
    public float RotateValue;
    public bool isRevers;

    private float angleValue;
	// Use this for initialization
	void Start () 
    {
        this.angleValue = 0.0f;
        this.RotateValue = this.isRevers ? -this.RotateValue : this.RotateValue;
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.angleValue += this.RotateValue;
        if (this.angleValue >= 360)
        {
            this.angleValue = 0.0f;
        }
        this.gameObject.transform.RotateAround(this.CenterOfMass.position, new Vector3(0, 0, 1.0f), this.RotateValue);
	}
}
