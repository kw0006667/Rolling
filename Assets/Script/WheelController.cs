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

    public float MaxTorque;
    public float MouseX_Value;

    #endregion

    #region private properties
    private float tempTorque;
    private float forceTorque;
    private float brakeTorque;
    private float powerTorque;

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
        this.forceTorque = 0.0f;
        this.brakeTorque = 0.0f;
        this.powerTorque = 0.0f;
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
                this.brakeTorque = this.forceTorque * (-1);
            this.isHandBreak = true;
        }
        if (Input.GetMouseButtonUp(2))
            this.isHandBreak = false;

        //print(this.isHandBreak);
        handleAnimation();
    }

    #endregion

    #region Support Methods

    private void handleAnimation()
    {
        if (MouseX_Value > 1.4f)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation["TurnRight"].wrapMode = WrapMode.Once;
                animation["TurnRight"].speed = 1.0f;
                animation.CrossFade("TurnRight");
            }
        }
        if (MouseX_Value < -1.4f)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation["TurnLeft"].wrapMode = WrapMode.Once;
                animation["TurnLeft"].speed = 1.0f;
                animation.CrossFade("TurnLeft");
            }
        }

        if (tempTorque > 0)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation["Forward"].wrapMode = WrapMode.Once;

                animation["Forward"].speed = 1.5f;
                animation.CrossFade("Forward");
            }
        }
        if (tempTorque < 0)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation["Backward"].wrapMode = WrapMode.Once;
                animation["Backward"].speed = 1.5f;
                animation.CrossFade("Backward");
            }
        }
        if (tempTorque == 0 && MouseX_Value == 0)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation.CrossFade("Idle");
            }
        }
    }

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
            this.forceTorque += this.tempTorque;
            if (this.forceTorque >= this.MaxTorque)
                this.forceTorque = this.MaxTorque;
            this.forceTorque += this.tempTorque;
        }
        else
        {
            this.forceTorque = 0.0f;
        }

        if (!isHandBreak)
        {
            this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * (this.forceTorque));
        }
        else
        {
            // HandBrakeing
            // Verify the velocity of rigidbody is not zero
            //if (!Vector3.Equals(this.rigidbody.velocity.normalized, Vector3.zero))
            {
                this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * this.brakeTorque);
                //this.rigidbody.velocity = Vector3.zero;
            }
        }

        // Draw a ray to display transform forward
        if (Debug.isDebugBuild)
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward), Color.red);
    }

    #endregion
}
