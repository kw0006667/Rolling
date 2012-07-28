using UnityEngine;
using System.Collections;

public class CheckPointManager : MonoBehaviour
{
    public Vector3 currentPosition { get; private set; }
    public Vector3 currentAngle { get; private set; }

    public GameObject Greta;

    // Use this for initialization
    void Start()
    {
        this.currentPosition = Greta.transform.position;
        this.currentAngle = Greta.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.ReturnCheckPoint();
        }
    }

    public void ReturnCheckPoint()
    {
        Greta.transform.position = currentPosition;
        Greta.transform.eulerAngles = currentAngle;
        Greta.rigidbody.velocity = Vector3.zero;
    }

    public void SetCurrentPosition(Vector3 vct3)
    {
        this.currentPosition = vct3;
    }

    public void SetCurrentAngle(Vector3 vct3)
    {
        this.currentAngle = vct3;
    }
}
