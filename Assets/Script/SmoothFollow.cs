using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    // The target we are following
    public Transform Target;
    // The distance in the x-z plane to the target
    public float Distance = 1.6f;
    // The height we want the camera to be above the target
    public float Height = 1.0f;
    // How much value of return speed
    public float HeightDamping = 2.0f;
    public float RotationDamping = 3.0f;

    //public LayerMask LineOfSightMask = 0;

    //public float CloserRadius = 0.2f;
    //public float CloserSnapLag = 0.2f;

    //private float currentDistance = 10.0f;
    //private float distanceVelocity = 0.0f;

	// Use this for initialization
	void Start () 
    {
        //this.currentDistance = this.Distance;
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        // Early out if we don't have a target
        if (!this.Target)
            return;

        // Calculate the current rotation angles
        float wanteddRotationAngle = this.Target.eulerAngles.y;
        float wantedHeight = this.Target.position.y + this.Height;

        float currentRotationAngle = this.transform.eulerAngles.y;
        float currentHeight = this.transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wanteddRotationAngle, this.RotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, this.HeightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        this.transform.position = this.Target.position;
        this.transform.position -= currentRotation * Vector3.forward * this.Distance;

        // Set the height of the camera
        this.transform.position = new Vector3(this.transform.position.x, currentHeight, this.transform.position.z);

        // Always look at the target
        this.transform.LookAt(this.Target);

        //// Adjust Camera position when raycast the wall
        //Vector3 targetPos = this.transform.position + this.transform.TransformDirection(Vector3.forward);
        //float targetDistance = this.adjustLineOfSight(ref targetPos, this.transform.TransformDirection(-Vector3.forward));
        //this.currentDistance = Mathf.SmoothDamp(this.currentDistance, targetDistance, ref this.distanceVelocity, this.CloserSnapLag * 0.3f);

        //this.transform.position = targetPos + this.transform.TransformDirection(Vector3.forward) * this.currentDistance;
	}

    //private float adjustLineOfSight(ref Vector3 targetPos, Vector3 direction)
    //{
    //    RaycastHit hit;
    //    Debug.DrawRay(targetPos, direction * this.Distance, Color.yellow);
    //    if (Physics.Raycast(targetPos, direction, out hit, this.Distance, this.LineOfSightMask.value))
    //    {
    //        float t = hit.distance - this.CloserRadius;

    //        targetPos += new Vector3(0, Mathf.Lerp(this.Height, 0, Mathf.Clamp(t, 0.0f, 1.0f)), 0);
    //        return hit.distance - this.CloserRadius;
    //    }
    //    else
    //    {
    //        return hit.distance - this.CloserRadius;
    //    }
        
    //}
}
