using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour
{

    #region Public properties

    public WheelCollider LeftWheel;
    public WheelCollider RightWheel;
    public WheelCollider LeftWheel_Front;
    public WheelCollider RightWheel_Front;
    
    // The center mass of Rigidbody
    public Transform CenterOfMass;
    // Set the max value of ForceTorque
    public float MaxTorque;
    
    // Normal rigidbody drag value
    public float WheelDrag = 0.5f;
    // Handbreaking rigidbody drag value
    public float HandBreakDrag = 3.0f;

    #endregion

    #region private properties
    // Save temp torque value
    private float tempTorque;
    // forward and back force value via input device(MouseWheel)
    private float forceTorque;
    // Axis value to turn left and right via input device(Mouse left or right)
    private float MouseX_Value;
    // Verify it's handbreaking current
    private bool isHandBreak;

    #endregion

    #region UnityEngine

    // Use this for initialization
    void Start()
    {
        // Set the CenterOfMass of RigidBody
        if (this.CenterOfMass != null)
            this.rigidbody.centerOfMass = this.CenterOfMass.localPosition;
        this.rigidbody.drag = this.WheelDrag;

        // Initialization of values
        this.tempTorque = 0.0f;
        this.forceTorque = 0.0f;
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
        this.getInput();

        // Play character animation
        this.handleAnimation();

        // Hand braking
        this.handbraking();
    }

    #endregion

    #region Support Methods

    /// <summary>
    /// Get keyboard, mouse or another input device value
    /// </summary>
    private void getInput()
    {
        // Wheel the mouse scroll to increase or decrease value
        tempTorque = Input.GetAxis("Vertical");
        // Turn right or left mouse to get axis value
        this.MouseX_Value = Input.GetAxis("Mouse X");

        if (this.MouseX_Value >= 5)
            this.MouseX_Value = 5;

        // Click the middle click of mouse
        if (Input.GetMouseButtonDown(2) && RightWheel.isGrounded)
            this.isHandBreak = true;
        if (Input.GetMouseButtonUp(2))
            this.isHandBreak = false;
    }

    /// <summary>
    /// Set drag value when handbreaking or not
    /// </summary>
    private void handbraking()
    {
        if (this.isHandBreak)
        {
            this.rigidbody.drag = this.HandBreakDrag;
        }
        else
        {
            this.rigidbody.drag = this.WheelDrag;
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
            else if(this.forceTorque <= (-this.MaxTorque))
                this.forceTorque = (-this.MaxTorque);
        }
        else
        {
            this.forceTorque = 0;
        }

        if (!isHandBreak)
        {
            this.rigidbody.AddForce(this.transform.TransformDirection(Vector3.forward) * (this.forceTorque));
        }

        // Draw a ray to display transform forward
        if (Debug.isDebugBuild)
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward), Color.red);
    }

    /// <summary>
    /// Play animation of idle, walk forward, back and turn right or left.
    /// </summary>
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

                animation["Forward"].speed = 1.0f;
                animation.CrossFade("Forward");
            }
        }
        if (tempTorque < 0)
        {
            if (!animation.IsPlaying("Forward") && !animation.IsPlaying("Backward") && !animation.IsPlaying("TurnRight") && !animation.IsPlaying("TurnLeft"))
            {
                animation["Backward"].wrapMode = WrapMode.Once;
                animation["Backward"].speed = 1.0f;
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

    #endregion
}
