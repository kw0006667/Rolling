using UnityEngine;
using System.Collections;

public class AccelerateMechanism : MonoBehaviour
{
    public GameObject Greta;
    private GretaController control;
    public float Acc = 8;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == Greta.name)
        {
            control.Velocity = Acc;
        }
    }

	// Use this for initialization
	void Start () {
        Greta = GameObject.Find("Greta");
        control = Greta.GetComponent<GretaController>();
	}
	
}
