using UnityEngine;
using System.Collections;

public class ButtonMechanism : MonoBehaviour {

    private bool trigger = false;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.trigger = true;
            if (!this.animation.IsPlaying("ButtonOnAnimation"))
                this.animation.PlayQueued("ButtonOnAnimation");
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.trigger = false;
            if (!this.animation.IsPlaying("ButtonOffAnimation"))
                this.animation.PlayQueued("ButtonOffAnimation");
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
