using UnityEngine;
using System.Collections;
using System;

public class JumpMechanism : MonoBehaviour
{
    public float JumpSpeed = 1;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            m_parent.rigidbody.AddForce(this.transform.TransformDirection(0, 0, JumpSpeed));
            if(this.audio != null)
                this.audio.Play();
        }
    }

    // Use this for initialization
	void Start () 
    {
        
	}	
}
