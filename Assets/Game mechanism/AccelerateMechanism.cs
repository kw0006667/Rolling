using UnityEngine;
using System.Collections;

public class AccelerateMechanism : MonoBehaviour
{
    public GameObject Greta;
    private GretaController control;
    public float SpeedUp = 8;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.control.Velocity = this.SpeedUp;
        }
    }

	// Use this for initialization
	void Start () {
        this.Greta = GameObject.Find("Greta");
        this.control = this.Greta.GetComponent<GretaController>();
	}
	
}
