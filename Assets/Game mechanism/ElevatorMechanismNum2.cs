using UnityEngine;
using System.Collections;

public class ElevatorMechanismNum2 : MonoBehaviour
{
    public GameObject Greta;
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;

    private bool isEnter = false;
    private float addVaule = 0;
    private BoxCollider[] boxs;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isEnter)
        {
            this.isEnter = true;
            for (int i = 1; i < this.boxs.Length; i++)
                this.boxs[i].isTrigger = false;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
            this.isEnter = false;        
    }

    
    // Use this for initialization
    void Start()
    {
        this.boxs = this.transform.GetComponentsInChildren<BoxCollider>();
    }

    void Update()
    {
        if (this.isEnter)
        {
            if (Mathf.Abs(this.addVaule) < this.MoveDistance)
            {
                this.transform.parent.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                //if (this.isEnter)
                //    Greta.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));

                this.addVaule += this.MoveSpeed;
            }
            else
            {
                for (int i = 1; i < this.boxs.Length; i++)
                    this.boxs[i].isTrigger = true;                
            }
        }
    }
}
