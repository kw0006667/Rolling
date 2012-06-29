using UnityEngine;
using System.Collections;

public class SwitchMechanism : MonoBehaviour {

    public GameObject Greta;
    public bool Trigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (!Trigger)
        {
            if (other.name == Greta.name)
            {
                Trigger = true;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Greta = GameObject.Find("Greta");
    }
}
