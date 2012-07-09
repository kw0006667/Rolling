using UnityEngine;
using System.Collections;

public class JumpMechanism : MonoBehaviour
{
    public float JumpSpeed = 1;    
    public GameObject Greta;

    void OnTriggerEnter(Collider other)
    {        
        Greta.rigidbody.AddForce(transform.TransformDirection(0, 0, JumpSpeed));
    }    

    // Use this for initialization
	void Start () 
    {
        this.Greta = GameObject.Find("Greta");
	}	
}
