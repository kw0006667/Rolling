using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    private GameObject Greta;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            Greta = m_parent;            
            if (!this.isOpen)
            {
                if (!this.animation.IsPlaying("OpenDoorAnimation"))
                    this.animation.PlayQueued("OpenDoorAnimation");
            }
            isOpen = true;
        }
    }
    
    void Update()
    {
        if (this.isOpen)
        {
            if (Vector3.Distance(this.Greta.transform.position, this.transform.position) > 4)
            {
                this.isOpen = false;
                if (!this.animation.IsPlaying("CloseDoorAnimation"))
                    this.animation.PlayQueued("CloseDoorAnimation");
            }
        }
    }
}