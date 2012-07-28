using UnityEngine;
using System.Collections;

public class TestMenu : MonoBehaviour
{
    public bool isOpenMenu = false;
    public GameObject CheckPointObject;
    private CheckPointManager checkPointManager;

    // Use this for initialization
    void Start()
    {
        this.checkPointManager = CheckPointObject.GetComponent<CheckPointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.isOpenMenu = true;
    }

    void OnGUI()
    {
        if (isOpenMenu)
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "���ի��s\n�^��O���I"))
            {
                this.checkPointManager.ReturnCheckPoint();
                this.isOpenMenu = false;
            }
        }
    }
}