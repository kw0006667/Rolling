using UnityEngine;
using System.Collections;

public class SwitchSecondOneManager : MonoBehaviour
{
    public GameObject[] GroupObjects;
    private SwitchSecondOne[] switchSecondOneScripts_Group;

    public GameObject LeftObject;
    private SwitchSecondOne switchSecondOneScripts_Left;

    // Use this for initialization
    void Start()
    {
        if (GroupObjects.Length != 0)
        {
            int count = 0;
            switchSecondOneScripts_Group = new SwitchSecondOne[GroupObjects.Length];
            foreach (var Obj in GroupObjects)
            {
                switchSecondOneScripts_Group[count] = Obj.GetComponent<SwitchSecondOne>();
                count++;
            }
        }

        switchSecondOneScripts_Left = LeftObject.GetComponent<SwitchSecondOne>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchSecondOneScripts_Left.isOn)
        {
            bool isCheck = false;
            foreach (var script in switchSecondOneScripts_Group)
            {
                if (script.isOn)
                    isCheck = true;
                else
                {
                    isCheck = false;
                    break;
                }
            }
            if (isCheck)
            {
                switchSecondOneScripts_Left.ChangeState(false);
            }
        }

        else
        { 
            
        }

        
    }
}