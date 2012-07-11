using UnityEngine;
using System.Collections;

public class AccelerateMechanism : MonoBehaviour
{
    public float SpeedLight = 10;
    public float SpeedHeavy = 7;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            if(GameDefinition.GetIsWeight(m_parent.rigidbody.mass) == GameDefinition.Weight.light)
                m_parent.rigidbody.velocity = m_parent.transform.TransformDirection(0, 0, SpeedLight);
            else
                m_parent.rigidbody.velocity = m_parent.transform.TransformDirection(0, 0, SpeedHeavy);
            //m_parent.rigidbody.AddForce(m_parent.transform.TransformDirection(Vector3.forward) * this.SpeedUp);
        }
    }
    // Use this for initialization
    void Start()
    {

    }
}