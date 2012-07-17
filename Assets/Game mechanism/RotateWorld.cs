using UnityEngine;
using System.Collections;

public class RotateWorld : MonoBehaviour
{

    public Vector3 Axis_v3;
    public bool isRotate = false;
    private GameObject Greta;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !isRotate)
        {
            isRotate = true;
            Greta = m_parent;
            Greta.rigidbody.isKinematic = true;
            //RotateObject();
        }
    }

    // Use this for initialization
    void Start()
    {
        deltaRotate = 90 / rotateFrame;
    }

    void RotateObject()
    {
        transform.parent.transform.RotateAround(transform.position, Axis_v3, 90);
        transform.parent.transform.position += transform.TransformDirection(new Vector3(0, 0, -1));
    }

    private float rotateFrame = 60;
    private int addFrame = 0;
    private float deltaRotate;
    private float totalTime = 0.0f;

    //Update is called once per frame
    void Update()
    {
        if (this.isRotate && this.totalTime < 0.05f)
        {
            totalTime += Time.deltaTime;
            if (totalTime >= 0.05f)
            {
                Greta.rigidbody.isKinematic = false;
                this.totalTime = 0.05f;
            }
        }
        if (isRotate)
        {
            if (addFrame < rotateFrame)
            {
                addFrame++;
                transform.parent.transform.RotateAround(transform.position, Axis_v3, deltaRotate);
            }
        }
    }
}
