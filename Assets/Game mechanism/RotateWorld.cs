using UnityEngine;
using System.Collections;

public class RotateWorld : MonoBehaviour
{

    public Vector3 Axis_v3;
    public float FreezeTime;

    private GameObject greta;
    private bool isRotate = false;
    private float rotateFrame = 60;
    private int addFrame = 0;
    private float deltaRotate;
    private float totalTime;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isRotate)
        {
            this.isRotate = true;
            this.greta = m_parent;
            this.greta.rigidbody.isKinematic = true;
            //RotateObject();
        }
    }

    // Use this for initialization
    void Start()
    {
        this.deltaRotate = 90 / this.rotateFrame;
    }

    void RotateObject()
    {
        transform.parent.transform.RotateAround(transform.position, Axis_v3, 90);
        transform.parent.transform.position += transform.TransformDirection(new Vector3(0, 0, -1));
    }

    

    //Update is called once per frame
    void Update()
    {
        if (this.isRotate && this.totalTime < this.FreezeTime)
        {
            totalTime += Time.deltaTime;
            if (totalTime >= this.FreezeTime)
            {
                greta.rigidbody.isKinematic = false;
                this.totalTime = this.FreezeTime;
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
