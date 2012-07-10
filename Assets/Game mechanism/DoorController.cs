using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
            if (!this.animation.IsPlaying("OpenDoorAnimation"))
                this.animation.PlayQueued("OpenDoorAnimation");
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
            if (!this.animation.IsPlaying("CloseDoorAnimation"))
                this.animation.PlayQueued("CloseDoorAnimation");
    }

    // Use this for initialization
    void Start()
    {
        
    }
}