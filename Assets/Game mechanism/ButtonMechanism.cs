using UnityEngine;
using System.Collections;

public class ButtonMechanism : MonoBehaviour {
    
    public GameObject TriggerObject;

    private Animation triggerAnimation;
    private bool trigger = false;
    private bool WaitOn = false;
    private bool WaitOff = false;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.trigger = true;
            if (!this.animation.IsPlaying("ButtonOnAnimation") && !this.WaitOn)
            {
                this.animation.PlayQueued("ButtonOnAnimation");
                if (!this.triggerAnimation.IsPlaying("UpDownFloor_DownAnimation"))
                    this.triggerAnimation.PlayQueued("UpDownFloor_DownAnimation");

                this.WaitOn = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.trigger = false;
            if (!this.animation.IsPlaying("ButtonOffAnimation") && !this.WaitOff)
            {
                this.animation.PlayQueued("ButtonOffAnimation");
                if (!this.triggerAnimation.IsPlaying("UpDownFloor_UpAnimation"))
                    this.triggerAnimation.PlayQueued("UpDownFloor_UpAnimation");

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
        this.triggerAnimation = this.TriggerObject.GetComponent<Animation>();
    }
}
