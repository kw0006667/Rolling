using UnityEngine;
using System.Collections;

public class AccelerateMechanism : MonoBehaviour
{
    public GameObject Greta;
    public float SpeedUp = 10;

    void OnTriggerEnter(Collider other)
    {
        Greta.rigidbody.velocity = Greta.transform.TransformDirection(0, 0, SpeedUp);
        Greta.rigidbody.AddForce(Greta.transform.TransformDirection(0, 0, SpeedUp));
    }

    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
    }
}