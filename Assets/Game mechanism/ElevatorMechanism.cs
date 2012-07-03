using UnityEngine;
using System.Collections;

public class ElevatorMechanism : MonoBehaviour 
{    
    public float moveDistance = 4.0f;
    public float moveSpeed = 0.03f;
    public GameObject Greta;
    public GameObject SwitchObject;

    private SwitchMechanism switchMechanism;
    private bool isEnter = false;    
    private float addVaule = 0;    
    
    public enum ElevatorDirection
    { 
        Up , Down
    }
    public ElevatorDirection direction;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.isEnter = true;            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == this.Greta.name)
        {
            this.isEnter = false;
        }
    }
    
    // Use this for initialization
    void Start()
    {
        this.Greta = GameObject.Find("Greta");
        this.switchMechanism = this.SwitchObject.GetComponent<SwitchMechanism>();
    }

    private RaycastHit hit;
    void Update()
    {
        if (this.switchMechanism.GetTrigger())
        {
            if (Mathf.Abs(this.addVaule) < this.moveDistance)
            {
                if (this.direction == ElevatorDirection.Down)
                {
                    if (Physics.Raycast(transform.position, new Vector3(0, -0.5f, 0), out hit, new Vector3(0, -0.5f, 0).magnitude))
                        if (hit.transform.name == Greta.name)
                            return;
                    
                    transform.parent.transform.position -= transform.parent.transform.TransformDirection(new Vector3(0, 0, moveSpeed));
                    if (this.isEnter)
                        this.Greta.transform.position -= transform.parent.transform.TransformDirection(new Vector3(0, 0, moveSpeed));
                }
                else if (direction == ElevatorDirection.Up)
                {
                    transform.parent.transform.position += transform.parent.transform.TransformDirection(new Vector3(0, 0, moveSpeed));
                    if (this.isEnter)
                        this.Greta.transform.position += transform.parent.transform.TransformDirection(new Vector3(0, 0, moveSpeed));
                }
                addVaule += moveSpeed;
            }
            else
            {
                this.addVaule = 0;
                if (this.direction == ElevatorDirection.Down)
                    this.direction = ElevatorDirection.Up;
                else
                    this.direction = ElevatorDirection.Down;
            }
        }     
    }
}
