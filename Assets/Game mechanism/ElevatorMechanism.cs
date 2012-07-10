using UnityEngine;
using System.Collections;

public class ElevatorMechanism : MonoBehaviour 
{    
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;
    public GameObject SwitchObject;

    private SwitchMechanism switchMechanism;
    private bool isEnter = false;    
    private float addVaule = 0;
    private RaycastHit hit;
    
    public enum ElevatorDirection
    { 
        Up , Down
    }
    public ElevatorDirection Direction;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.isEnter = true;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.isEnter = false;
        }
    }
    
    // Use this for initialization
    void Start()
    {
        this.switchMechanism = this.SwitchObject.GetComponent<SwitchMechanism>();
    }

    void Update()
    {
        if (this.switchMechanism.GetTrigger())
        {
            if (Mathf.Abs(this.addVaule) < this.MoveDistance)
            {
                if (this.Direction == ElevatorDirection.Down)
                {
                    if (Physics.Raycast(transform.position, new Vector3(0, -0.5f, 0), out hit, new Vector3(0, -0.5f, 0).magnitude))
                        if (hit.transform.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
                            return;
                    
                    this.transform.parent.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                    //if (this.isEnter)
                    //    this.Greta.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                }
                else if (this.Direction == ElevatorDirection.Up)
                {
                    transform.parent.transform.position += this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                    //if (this.isEnter)
                    //    this.Greta.transform.position += transform.parent.transform.TransformDirection(new Vector3(0, 0, MoveSpeed));
                }
                this.addVaule += this.MoveSpeed;
            }
            else
            {
                this.addVaule = 0;
                if (this.Direction == ElevatorDirection.Down)
                    this.Direction = ElevatorDirection.Up;
                else
                    this.Direction = ElevatorDirection.Down;
            }
        }     
    }
}
