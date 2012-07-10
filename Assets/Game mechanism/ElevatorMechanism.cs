using UnityEngine;
using System.Collections;

public class ElevatorMechanism : MonoBehaviour 
{    
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;
    public GameObject SwitchObject;

    public GameObject NotPicture;

    private BoxCollider[] boxs;
    private GameObject Greta;
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
            Greta = m_parent;
            for (int i = 1; i < this.boxs.Length; i++)
                this.boxs[i].isTrigger = false;         
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
        this.switchMechanism = this.SwitchObject.GetComponent<SwitchMechanism>();
        this.boxs = this.transform.GetComponentsInChildren<BoxCollider>();
    }

    private float waitTime = 0.0f;
    private bool isWait = false;

    void Update()
    {
        if (this.switchMechanism.GetTrigger())
        {
            NotPicture.active = false;
            if (isWait)
            {
                if (waitTime < 2.0f)
                    waitTime += Time.deltaTime;
                else
                {
                    isWait = false;
                    waitTime = 0;
                    for (int i = 1; i < this.boxs.Length; i++)
                        this.boxs[i].isTrigger = false;  
                }
                return;
            }

            if (Mathf.Abs(this.addVaule) < this.MoveDistance)
            {
                if (this.Direction == ElevatorDirection.Down)
                {
                    if (Physics.Raycast(transform.position, new Vector3(0, -0.5f, 0), out hit, new Vector3(0, -0.5f, 0).magnitude))
                        if (hit.transform.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
                            return;

                    this.transform.parent.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                    if (this.isEnter)
                        this.Greta.transform.position -= this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                }
                else if (this.Direction == ElevatorDirection.Up)
                {
                    transform.parent.transform.position += this.transform.parent.transform.TransformDirection(new Vector3(0, 0, this.MoveSpeed));
                    if (this.isEnter)
                        this.Greta.transform.position += transform.parent.transform.TransformDirection(new Vector3(0, 0, MoveSpeed));
                }
                this.addVaule += this.MoveSpeed;
            }
            else
            {
                this.isWait = true;
                this.addVaule = 0;
                for (int i = 1; i < this.boxs.Length; i++)
                    this.boxs[i].isTrigger = true;  

                if (this.Direction == ElevatorDirection.Down)
                    this.Direction = ElevatorDirection.Up;
                else
                    this.Direction = ElevatorDirection.Down;
            }
        }
        else
            NotPicture.active = true;

    }
}
