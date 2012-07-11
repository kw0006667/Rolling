using UnityEngine;
using System.Collections;

public class ConveyorMechanism : MonoBehaviour
{    
    public float MoveSpeed = 0.05f;
    public Vector3 Direction;

    void OnTriggerStay(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            //m_parent.rigidbody.AddForce(this.transform.TransformDirection(this.Direction * this.MoveSpeed));
            m_parent.transform.position += this.transform.TransformDirection(this.Direction * this.MoveSpeed);
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

}
