using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public GameObject TimerObject;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            WheelController wheelController;
            wheelController = m_parent.GetComponent<WheelController>();
            wheelController.enabled = false;

            Timer timer;
            timer = TimerObject.GetComponent<Timer>();
            timer.isStart = false;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
