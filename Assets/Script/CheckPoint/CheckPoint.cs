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
            this.checkPointManager.SetCurrentPosition(this.transform.position);
            this.checkPointManager.SetCurrentAngle(this.transform.localEulerAngles);
        }
    }

    void Start()
    {
        this.checkPointManager = this.transform.parent.GetComponent<CheckPointManager>();
    }
}