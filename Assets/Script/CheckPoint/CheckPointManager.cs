using UnityEngine;
using System.Collections;

public class CheckPointManager : MonoBehaviour
{
    public Vector3 currentPosition;
    public Vector3 currentAngle;

    public GameObject Greta;

    public bool isCheck;

    public void ReturnCheckPoint()
    {
        Greta.transform.position = currentPosition;        
        Greta.transform.eulerAngles = currentAngle;
        Greta.rigidbody.velocity = Vector3.zero;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            ReturnCheckPoint();
        }
    }

    // Use this for initialization
    void Start()
    {
        currentPosition = Greta.transform.position;
        currentAngle = Greta.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck)
        {
            ReturnCheckPoint();
            isCheck = false;
        }
    }
}
