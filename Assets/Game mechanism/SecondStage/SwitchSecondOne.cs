using UnityEngine;
using System.Collections;

public class SwitchSecondOne : MonoBehaviour
{
    public float MoveDistance = 4.0f;
    public float MoveSpeed = 0.03f;

    public enum ControlUpDown
    {
        Up, Down
    }
    public ControlUpDown controlUpdown;
    public bool isOn = false;
    public bool isWorking = false;

    public GameObject ControlMoveWallObject;

    public GameObject[] RelationSwitchObjects;
    private SwitchSecondOne[] relationSwitchScripts;
    
    private float addValue = 0;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isWorking && !this.isOn)
        {
            this.isWorking = true;
            this.ChangeState(true);

            if (RelationSwitchObjects.Length != 0)
            {
                foreach (var script in relationSwitchScripts)
                {
                    script.ChangeState(false);
                }
            }
        }
    }

    /// <summary>
    /// Change current switch state
    /// </summary>
    public void ChangeState(bool isOn)
    {
        if (isOn)
        {
            if (!this.animation.IsPlaying("SwithchOnAnimation"))
                this.animation.PlayQueued("SwithchOnAnimation");
        }
        else
        {
            if (!this.animation.IsPlaying("SwithchOffAnimation"))
                this.animation.PlayQueued("SwithchOffAnimation");
        }
        this.isOn = isOn;            
    }


    // Use this for initialization
    void Start()
    {
        if (RelationSwitchObjects.Length != 0)
        {
            int count = 0;
            relationSwitchScripts = new SwitchSecondOne[RelationSwitchObjects.Length];
            foreach (var Obj in RelationSwitchObjects)
            {
                relationSwitchScripts[count] = Obj.GetComponent<SwitchSecondOne>();
                count++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (this.isWorking)
        {
            if (this.isOn)
            {
                if (this.addValue < this.MoveDistance)
                {
                    this.addValue += this.MoveSpeed;
                    if (this.controlUpdown == ControlUpDown.Down)
                        this.ControlMoveWallObject.transform.position -= this.ControlMoveWallObject.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
                    else
                        this.ControlMoveWallObject.transform.position += this.ControlMoveWallObject.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
                }
                else
                {
                    this.addValue = 0;
                    this.isWorking = false;                   
                }
            }
            //else
            //{
            //    if (this.addValue > 0)
            //    {
            //        this.addValue -= this.MoveSpeed;
            //        if (this.controlUpdown == ControlUpDown.Up)
            //            this.ControlMoveWallObject.transform.position -= this.ControlMoveWallObject.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
            //        else
            //            this.ControlMoveWallObject.transform.position += this.ControlMoveWallObject.transform.TransformDirection(new Vector3(0, this.MoveSpeed, 0));
            //    }
            //    else
            //        this.isWorking = false;
            //}
        }
    }
}