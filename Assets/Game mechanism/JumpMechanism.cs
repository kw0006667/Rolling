using UnityEngine;
using System.Collections;

public class JumpMechanism : MonoBehaviour {

    public GameObject Greta;
    private GretaController control;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == Greta.name)
        {
            control.isCanJump = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == Greta.name)
        {
            control.isCanJump = false;
        }
    }
    
    // Use this for initialization
	void Start () {
        Greta = GameObject.Find("Greta");
        control = Greta.GetComponent<GretaController>();
	}
	
}
