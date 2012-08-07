using UnityEngine;
using System.Collections;

public class ElevatorMechanismNum2 : MonoBehaviour
{
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;

    private bool isEnter = false;
    private float addValue = 0;
    private GameObject Greta;

    private float revertTime = 2.0f;
    private float addTime = 0;
    
    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isEnter)
        {
            this.isEnter = true;
            Greta = m_parent;           
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.isEnter = false;
            this.addTime = 0;
        }
    }

    
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (this.Greta)
        {
            if (GameDefinition.GetIsWeight(Greta.rigidbody.mass) == GameDefinition.Weight.heavy)
            {
                if (this.isEnter)
                {
                    if (Mathf.Abs(this.addValue) < this.MoveDistance)
                    {
                        this.transform.parent.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                        if (this.isEnter)
                            Greta.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));

                        this.addValue += this.MoveSpeed;
                    }
                }
                else
                {
                    if (addTime < revertTime)
                    {
                        addTime += Time.deltaTime;
                        return;
                    }
                    if (this.addValue > 0)
                    {
                        this.transform.parent.transform.position += this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                        this.addValue -= MoveSpeed;
                    }
                }

            }
        }
    }
}
