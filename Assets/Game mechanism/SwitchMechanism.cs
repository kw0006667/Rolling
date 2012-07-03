using UnityEngine;
using System.Collections;

public class SwitchMechanism : MonoBehaviour {

    public GameObject Greta;
    public bool Trigger = false;

    void OnTriggerEnter(Collider other)
    {        
        if (other.name == this.Greta.name)
        {
            Trigger = !Trigger;
            if (Trigger)
            {
                if (!this.animation.IsPlaying("SwithchOnAnimation"))
                    this.animation.PlayQueued("SwithchOnAnimation");                
            }
            else
            {
                if (!this.animation.IsPlaying("SwithchOffAnimation"))
                    this.animation.PlayQueued("SwithchOffAnimation");
            }
        }        
    }

    public bool GetTrigger()
    {
        return Trigger;
    }

    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
    }
}
