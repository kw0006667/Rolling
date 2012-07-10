using UnityEngine;
using System.Collections;

public class RotateWorld : MonoBehaviour {

    public Vector3 Axis_v3;
    public float speed = 1;
    public bool isEnter = false;
    public bool isRotate = false;
    private GameObject Greta;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !isRotate)
        {
            isRotate = true;
            Greta = other.gameObject;
            RotateObject();
        }
    }

	// Use this for initialization
	void Start ()
    {        
	}

    void RotateObject()
    {
        transform.parent.transform.RotateAround(transform.position, Axis_v3, 90);
        //Greta.transform.position += Greta.transform.TransformDirection(0, 5, 0);
        transform.parent.transform.position += transform.TransformDirection(new Vector3(0, 0, -1));
    }

	//Update is called once per frame
    //void Update()
    //{
    //    if (isEnter && !isRotate)
    //        RotateObject();
    //    //transform.RotateAround(Vector3.zero, v3, 20 * Time.deltaTime);
    //    //transform.RotateAround(trans.position, Axis_v3, Time.deltaTime * speed);
    //    //transform.RotateAroundLocal(Axis_v3, Time.deltaTime * speed);
    //}
}
