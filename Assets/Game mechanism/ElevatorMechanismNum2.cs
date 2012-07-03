using UnityEngine;
using System.Collections;

public class ElevatorMechanismNum2 : MonoBehaviour
{
    public GameObject Greta;
    public float moveDistance = 4.0f;
    public float moveSpeed = 0.03f;

    private bool isEnter = false;
    private float addVaule = 0;
    private BoxCollider[] boxs;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name && !this.isEnter)
        {
            this.isEnter = true;
            for (int i = 1; i < this.boxs.Length; i++)
                this.boxs[i].isTrigger = false;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == Greta.name)
            this.isEnter = false;        
    }

    
    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
        this.boxs = transform.GetComponentsInChildren<BoxCollider>();
    }

    void Update()
    {
        if (this.isEnter)
        {
            if (Mathf.Abs(addVaule) < moveDistance)
            {
                transform.parent.transform.position -= transform.parent.transform.TransformDirection(new Vector3(0, 0, this.moveSpeed));
                if (this.isEnter)
                    Greta.transform.position -= transform.parent.transform.TransformDirection(new Vector3(0, 0, this.moveSpeed));

                this.addVaule += this.moveSpeed;
            }
            else
            {
                for (int i = 1; i < this.boxs.Length; i++)
                    this.boxs[i].isTrigger = true;                
            }
        }
    }
}
