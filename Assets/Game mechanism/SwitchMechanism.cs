using UnityEngine;
using System.Collections;

public class SwitchMechanism : MonoBehaviour {

    private bool trigger = false;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.trigger = !this.trigger;
            if (trigger)
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
        return this.trigger;
    }

    // Use this for initialization
    void Start()
    {
    }
}
