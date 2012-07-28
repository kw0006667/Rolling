using UnityEngine;
using System.Collections;

public class AdjustCameraFar : MonoBehaviour
{
    public float AdjustFar;
    public GameObject CameraObject;
    

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            CameraObject.camera.far = AdjustFar;
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