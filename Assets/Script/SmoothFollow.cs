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

    public Vector3 forward = new Vector3(0.0f, 1.0f, 0.0f);
    public LayerMask LineOfSightMask = 0;

    public float CloserRadius = 0.2f;
    public float CloserSnapLag = 0.2f;

    private float currentDistance = 10.0f;
    private float distanceVelocity = 0.0f;

	// Use this for initialization
	void Start () 
    {
        //this.currentDistance = this.Distance;
        if (this.rigidbody)
            this.rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        // Early out if we don't have a target
        if (!this.Target)
            return;

        if (Vector3.Equals(Physics.gravity, new Vector3(0.0f, -9.8f, 0.0f)))
        {
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
            this.transform.LookAt(this.Target, new Vector3(0.0f, 1.0f, 0.0f));
        }
        else
        {
            float height = -this.Height;
            // Calculate the current rotation angles
            float wantedRotationAngle_z = this.Target.rotation.z;
            float wantedRotationAngle_x = this.Target.rotation.x;
            float wantedRotationAngle_y = this.Target.rotation.y;
            float wantedHeight = this.Target.position.z + height;

            float currentRotationAngle_z = this.transform.rotation.z;
            float currentRotationAngle_x = this.transform.rotation.x;
            float currentRotationAngle_y = this.transform.rotation.y;
            float currentHeight = this.transform.position.z;

            // Damp the rotation around the z-axis
            currentRotationAngle_z = Mathf.LerpAngle(currentRotationAngle_z, wantedRotationAngle_z, this.RotationDamping * Time.deltaTime);
            currentRotationAngle_x = Mathf.LerpAngle(currentRotationAngle_x, wantedRotationAngle_x, this.RotationDamping * Time.deltaTime);
            currentRotationAngle_y = Mathf.LerpAngle(currentRotationAngle_y, wantedRotationAngle_y, this.RotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, -this.HeightDamping * Time.deltaTime);

            //Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(currentRotationAngle_x, currentRotationAngle_y, currentRotationAngle_z);

            // Set the position of the camera on the x-y plane to:
            // distance meters behind the target
            this.transform.position = this.Target.position;
            this.transform.position -= currentRotation * this.forward * this.Distance;


            // Set the height of the camera
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, currentHeight);

            // Always look at the target
            this.transform.LookAt(this.Target, new Vector3(0.0f, 0.0f, -1.0f));
        }

        // Adjust Camera position when raycast the wall
        Vector3 targetPos = this.Target.transform.position;
        float targetDistance = this.adjustLineOfSight(ref targetPos, Vector3.Normalize(this.transform.position - this.Target.position));
        this.currentDistance = Mathf.SmoothDamp(this.currentDistance, targetDistance, ref this.distanceVelocity, this.CloserSnapLag * 1.0f);

        this.transform.position = targetPos + Vector3.Normalize(this.transform.position - this.Target.position) * this.currentDistance;
	}

    private float adjustLineOfSight(ref Vector3 targetPos, Vector3 direction)
    {
        RaycastHit hit;
        Debug.DrawRay(targetPos, direction * this.Distance, Color.yellow);
        if (Physics.Raycast(targetPos, direction, out hit, this.Distance, this.LineOfSightMask.value))
        {
            float t = hit.distance - this.CloserRadius;

            targetPos += new Vector3(0, Mathf.Lerp(this.Height, 0, Mathf.Clamp(t, 0.0f, 1.0f)), 0);
            return hit.distance - this.CloserRadius;
        }
        else
        {
            return this.Distance;
        }
        
    }
}
