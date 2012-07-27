using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class CheckPoint : MonoBehaviour
{
    private CheckPointManager checkPointManager;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            checkPointManager.currentPosition = transform.position;
            checkPointManager.currentAngle = transform.localEulerAngles;
        }
    }

    void Start()
    {
        checkPointManager = transform.parent.GetComponent<CheckPointManager>();
    }
}