using UnityEngine;
using System.Collections;

public class RotateWorld : MonoBehaviour
{

    public Vector3 Axis_v3;
    public float speed = 1;
    public bool isEnter = false;
    public bool isRotate = false;
    private GameObject Greta;
    private Vector3 v3;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !isRotate)
        {
            isRotate = true;
            Greta = m_parent;
            v3 = Greta.transform.position;
            RotateObject();
        }
    }

    // Use this for initialization
    void Start()
    {
        deltaRotate = 90 / rotateFrame;
    }

    void RotateObject()
    {
        //transform.parent.transform.RotateAround(transform.position, Axis_v3, 90);
        //Greta.transform.position += Greta.transform.TransformDirection(0, 5, 0);
        //transform.parent.transform.position += transform.TransformDirection(new Vector3(0, 0, -1));
        if (Vector3.Equals(Physics.gravity, new Vector3(0.0f, -9.8f, 0.0f)))
            Physics.gravity = new Vector3(0.0f, 0.0f, 9.8f);
        else
            Physics.gravity = new Vector3(0.0f, -9.8f, 0.0f);
    }

    float rotateFrame = 60;
    int addFrame = 0;
    public float deltaRotate;

    //Update is called once per frame
    void Update()
    {
        //if (isRotate)
        //{
        //    if (addFrame < rotateFrame)
        //    {
        //        addFrame++;
        //        transform.parent.transform.RotateAround(transform.position, Axis_v3, deltaRotate);
        //        //transform.parent.transform.position += transform.TransformDirection(new Vector3(0, 0, -0.5f));
        //        //Greta.rigidbody.isKinematic = true;
        //        //Greta.rigidbody.velocity = Vector3.zero;
        //        //print(Greta.rigidbody.velocity);
        //    }
        //    else
        //    {
        //        //Greta.rigidbody.isKinematic = false;
        //        isRotate = false;
        //    }
        //}
        //if (isEnter && !isRotate)
        //    RotateObject();
        //transform.RotateAround(Vector3.zero, v3, 20 * Time.deltaTime);
        //transform.RotateAround(trans.position, Axis_v3, Time.deltaTime * speed);
        //transform.RotateAroundLocal(Axis_v3, Time.deltaTime * speed);
    }
}
