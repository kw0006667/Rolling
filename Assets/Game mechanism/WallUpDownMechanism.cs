using UnityEngine;
using System.Collections;

public class WallUpDownMechanism : MonoBehaviour 
{
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;
    public GameObject ButtonObject;

    private GameObject Greta;
    private ButtonMechanism buttonMechanism;
    private bool isEnter = false;
    private float addValue = 0;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
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
            Greta = null;
        }
    }

	// Use this for initialization
    void Start()
    {
        this.buttonMechanism = this.ButtonObject.GetComponent<ButtonMechanism>();
    }
	
	// Update is called once per frame
    void Update()
    {
        if (this.buttonMechanism.GetTrigger())
        {
            if (this.addValue < this.MoveDistance)
            {
                this.addValue += MoveSpeed;
                this.transform.position -= this.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
            }
        }
        else
        {
            if (this.addValue > 0)
            {
                this.addValue -= MoveSpeed;
                this.transform.position += this.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
            }
        }
    }
}
