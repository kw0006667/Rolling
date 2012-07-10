using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{

    #region Public properties

    public WheelCollider LeftWheel;
    public WheelCollider RightWheel;
    public WheelCollider LeftWheel_Front;
    public WheelCollider RightWheel_Front;
    public Transform CenterOfMass;

    public float MaxTonque;
    public float MouseX_Value;

    #endregion

    #region private properties
    private float tempTorque;
    private float forceTonque;
    private float brakeTonque;
    private float powerTonque;

    private bool isHandBreak;

    #endregion

    #region UnityEngine

    // Use this for initialization
    void Start()
    {
        // Set the CenterOfMass of RigidBody
        this.rigidbody.centerOfMass = this.CenterOfMass.localPosition;

        // Initialization of values
        this.tempTorque = 0.0f;
        this.forceTonque = 0.0f;
        this.brakeTonque = 0.0f;
        this.powerTonque = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Let the rigidbody to force forward or backward
        this.calculateRigidbodyForce();

        // Let the rigidbody to turn left or right
        this.adjustRigidbodyRotation();
    }

    void Update()
    {
        // Get values of Input
        tempTorque = Input.GetAxis("Vertical");
        this.MouseX_Value = Input.GetAxis("Mouse X");

        if (this.MouseX_Value >= 5)
            this.MouseX_Value = 5;

        if (Input.GetMouseButtonDown(2))
        {
            if (!this.isHandBreak)
                this.brakeTonque = this.forceTonque * (-1);
            this.isHandBreak = true;
        }
        if (Input.GetMouseButtonUp(2))
            this.isHandBreak = false;

        print(this.isHandBreak);
    }

    #endregion

    #region Support Methods

    /// <summary>
    /// Let the rigidbody to turn left or right via value of Mouse X
    /// </summary>
    private void adjustRigidbodyRotation()
    {
        this.rigidbody.transform.rotation *= Quaternion.AngleAxis(this.MouseX_Value, Vector3.up);
    }

    /// <summary>
    /// Calculate the total force of this rigidbody
    /// </summary>
    private void calculateRigidbodyForce()
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
        }

        if (!isHandBreak)
        {
            this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * (this.forceTonque));
        }
        else
        {
            // HandBrakeing
            // Verify the velocity of rigidbody is not zero
            //if (!Vector3.Equals(this.rigidbody.velocity.normalized, Vector3.zero))
            {
                this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * this.brakeTonque);
                //this.rigidbody.velocity = Vector3.zero;
            }
        }

        // Draw a ray to display transform forward
        if (Debug.isDebugBuild)
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward), Color.red);
    }

    #endregion
}
