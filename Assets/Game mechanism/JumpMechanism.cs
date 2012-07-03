using UnityEngine;
using System.Collections;

public class JumpMechanism : MonoBehaviour
{
    public float JumpSpeed = 1;    
    public GameObject Greta;
    private GretaController control;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.control.SetJumpSpeed(JumpSpeed);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == Greta.name)
        {
            this.control.SetJumpSpeed(0);
        }
    }

    // Use this for initialization
	void Start () 
    {
        this.Greta = GameObject.Find("Greta");
        this.control = this.Greta.GetComponent<GretaController>();
	}	
}
