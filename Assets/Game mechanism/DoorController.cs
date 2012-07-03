using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public GameObject Greta;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name)
            if (!this.animation.IsPlaying("OpenDoorAnimation"))
                this.animation.PlayQueued("OpenDoorAnimation");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == this.Greta.name)
            if (!this.animation.IsPlaying("CloseDoorAnimation"))
                this.animation.PlayQueued("CloseDoorAnimation");
    }

    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
    }
}