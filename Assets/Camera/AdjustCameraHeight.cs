using UnityEngine;
using System.Collections;

public class AdjustCameraHeight : MonoBehaviour
{
    public float AdjustHeight = 3;
    public float AdjustTime = 2;
    public GameObject CameraObject;

    private SmoothFollow smoothFollow;
    private float addValue = 0;
    private bool isAdjust = false;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isAdjust = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!CameraObject)
            CameraObject = GameObject.Find("Camera");
        this.smoothFollow = this.CameraObject.GetComponent<SmoothFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isAdjust)
        {
            this.addValue += Time.deltaTime;
            this.smoothFollow.Height = Mathf.Lerp(this.smoothFollow.Height, this.AdjustHeight, this.addValue / this.AdjustTime);
            if (this.addValue > this.AdjustTime)
            {
                this.isAdjust = false;
                this.addValue = 0;
            }
        }
    }
}