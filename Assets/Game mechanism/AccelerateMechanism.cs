using UnityEngine;
using System.Collections;

public class AccelerateMechanism : MonoBehaviour
{
    public float SpeedUp = 10;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
            //Greta.rigidbody.velocity = Greta.transform.TransformDirection(0, 0, SpeedUp);
            m_parent.rigidbody.AddForce(m_parent.transform.TransformDirection(Vector3.forward) * this.SpeedUp);
    }

    // Use this for initialization
    void Start()
    {

    }
}