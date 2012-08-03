using UnityEngine;
using System.Collections;

public class MagentWall : MonoBehaviour 
{
    public GameObject MagentWallObject;
    public Transform centerOfMass;
    public bool isRevers;

    private float angleValue;
    private float rotateValue;
    private bool isTrigger;
	// Use this for initialization
	void Start () 
    {
        this.isTrigger = false;
        this.rotateValue = this.isRevers ? -2.0f : 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (this.isTrigger)
        {
            this.angleValue += 2.0f;
            if (this.angleValue >= 90)
            {
                this.isTrigger = false;
                Destroy(this);
            }
            this.MagentWallObject.transform.RotateAround(this.centerOfMass.position, new Vector3(0, 0, 1.0f), this.rotateValue);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        GameObject m_parent = col.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.isTrigger = true;
        }
    }
}
