using UnityEngine;
using System.Collections;

public class ButtonMechanism : MonoBehaviour {

    public GameObject Greta;
    public bool Trigger = false;
    public GameObject TriggerObject;
    public Animation TriggerAnimation;

    private bool WaitOn = false;
    private bool WaitOff = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.Trigger = true;
            if (!this.animation.IsPlaying("ButtonOnAnimation") && !this.WaitOn)
            {
                this.animation.PlayQueued("ButtonOnAnimation");
                if (!this.TriggerAnimation.IsPlaying("UpDownFloor_DownAnimation"))
                    this.TriggerAnimation.PlayQueued("UpDownFloor_DownAnimation");

                this.WaitOn = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.Trigger = false;
            if (!this.animation.IsPlaying("ButtonOffAnimation") && !this.WaitOff)
            {
                this.animation.PlayQueued("ButtonOffAnimation");
                if (!this.TriggerAnimation.IsPlaying("UpDownFloor_UpAnimation"))
                    this.TriggerAnimation.PlayQueued("UpDownFloor_UpAnimation");

                this.WaitOff = true;
            }
        }
    }

    void unWaitOff()
    {
        this.WaitOff = false;
    }

    void unWaitOn()
    {
        this.WaitOn = false;
    }
    
    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
        this.TriggerAnimation = this.TriggerObject.GetComponent<Animation>();
    }
}
