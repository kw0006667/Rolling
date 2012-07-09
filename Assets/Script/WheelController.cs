using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{

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
    private float tempTorque;
    private float forceTonque;
    private float brakeTonque;
    private float powerTonque;

    private bool isHandBreak;

    // Use this for initialization
    void Start()
    {
        this.rigidbody.centerOfMass = this.CenterOfMass.localPosition;

        this.turnValue = 0.0f;
        this.tempTorque = 0.0f;
        this.forceTonque = 0.0f;
        this.brakeTonque = 0.0f;
        this.powerTonque = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.tempTorque != 0.0f)
        {
            this.forceTonque += this.tempTorque;
            if (this.forceTonque >= this.MaxTonque)
                this.forceTonque = this.MaxTonque;
            this.forceTonque += this.tempTorque;
        }
        else
        {
            this.forceTonque = 0.0f;
            //if (this.forceTonque == 0.0f)
            //    this.forceTonque = 0.0f;
        }

        if (!isHandBreak)
        {
            this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * (this.forceTonque));
        }
        else
        {
            // Verify the velocity of rigidbody is not zero
            if (!Vector3.Equals(this.rigidbody.velocity.normalized, Vector3.zero))
            {
                this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * (this.brakeTonque) * 2.0f);
                //this.rigidbody.velocity = Vector3.zero;
            }
        }

        Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward), Color.red);

    }

    void Update()
    {
        this.VectorX = this.rigidbody.worldCenterOfMass.x;
        this.VectorY = this.rigidbody.worldCenterOfMass.y;
        this.VectorZ = this.rigidbody.worldCenterOfMass.z;

        tempTorque = Input.GetAxis("Vertical");
        this.MouseX_Value = Input.GetAxis("Mouse X");

        if (this.MouseX_Value >= 5)
            this.MouseX_Value = 5;

        if (Input.GetMouseButtonDown(2))
        {
            if (!this.isHandBreak)
                this.brakeTonque = -this.forceTonque;
            this.isHandBreak = true;
        }
        if (Input.GetMouseButtonUp(2))
            this.isHandBreak = false;

        print(this.isHandBreak);
    }
}
