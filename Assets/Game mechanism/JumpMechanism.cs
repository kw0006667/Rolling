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
            m_parent.rigidbody.AddForce(m_parent.transform.TransformDirection(0, JumpSpeed, 0));            
        }
    }

    // Use this for initialization
	void Start () 
    {
        
	}	
}
