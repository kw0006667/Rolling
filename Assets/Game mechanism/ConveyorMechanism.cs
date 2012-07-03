using UnityEngine;
using System.Collections;

public class ConveyorMechanism : MonoBehaviour
{    
    public float moveSpeed = 0.05f;
    public Vector3 direction;
    public GameObject Greta;

    private GretaController control;

    void OnTriggerStay(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.Greta.transform.position += transform.TransformDirection(direction * moveSpeed);                
        }
    }

    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
        this.control = this.Greta.GetComponent<GretaController>();
    }

}
