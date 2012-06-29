using UnityEngine;
using System.Collections;

public class ButtonMechanism : MonoBehaviour {

    public GameObject Greta;
    public bool Trigger = false;

    void OnTriggerExit(Collider other)
    {
        if (other.name == Greta.name)
        {
            Trigger = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == Greta.name)
        {
            Trigger = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        Greta = GameObject.Find("Greta");
    }
}
