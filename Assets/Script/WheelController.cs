using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {

    public WheelCollider LeftWheel;
    public WheelCollider RightWheel;
    public WheelCollider LeftWheel_Front;
    public WheelCollider RightWheel_Front;
    public Transform CenterOfMass;
    public float MaxTonque;

    public float MouseX_Value;

    public float VectorX;
    public float VectorY;
    public float VectorZ;
    private Rigidbody wheelRigidbody;

    private float turnValue;

    private bool isHandBreak;

	// Use this for initialization
	void Start () 
    {
        //this.rigidbody.centerOfMass = new Vector3(0.0f, 0.0f, 0.0f);

        // Set the rigidbody center point to lower
        //this.wheelRigidbody.centerOfMass = new Vector3(-0.1f, 0.0f, 0.0f);
        this.rigidbody.centerOfMass = this.CenterOfMass.localPosition;

        this.turnValue = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float tempTorque = this.MaxTonque * Input.GetAxis("Vertical");

        if (!isHandBreak)
        {
            //this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * Time.deltaTime * tempTorque * this.MaxTonque);
            this.LeftWheel.motorTorque = tempTorque * this.MaxTonque;
            this.RightWheel.motorTorque = tempTorque * this.MaxTonque;
        }
        else
        {
            tempTorque = 0;
            //this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * Time.deltaTime * tempTorque * this.MaxTonque);
            this.rigidbody.velocity = Vector3.zero;
            this.LeftWheel.motorTorque = 0;
            this.RightWheel.motorTorque = 0;
            this.LeftWheel_Front.motorTorque = 0;
            this.RightWheel_Front.motorTorque = 0;
        }

        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward), Color.red);

        //this.turnValue = Input.GetAxis("Horizontal");

        //if (this.MouseX_Value < 0)
        //{
        //    this.LeftWheel.motorTorque += this.MaxTonque * this.MouseX_Value;
        //    //this.RightWheel.motorTorque += 10.0f;
        //}
        //else if (this.MouseX_Value > 0)
        //{
        //    //this.LeftWheel.motorTorque = 50.0f;
        //    this.RightWheel.motorTorque += this.MaxTonque * this.MouseX_Value;
        //}

        

        //this.leftWheel.steerAngle = -30 * Input.GetAxis("Horizontal");
        //this.rightWheel.steerAngle = 30 * Input.GetAxis("Horizontal");
        
	}

    void Update()
    {
        this.VectorX = this.rigidbody.worldCenterOfMass.x;
        this.VectorY = this.rigidbody.worldCenterOfMass.y;
        this.VectorZ = this.rigidbody.worldCenterOfMass.z;

        this.MouseX_Value = Input.GetAxis("Mouse X");

        if (this.MouseX_Value >= 5)
            this.MouseX_Value = 5;

        if (Input.GetMouseButtonDown(2))
            this.isHandBreak = true;
        if (Input.GetMouseButtonUp(2))
            this.isHandBreak = false;

        print(this.isHandBreak);
    }
}
