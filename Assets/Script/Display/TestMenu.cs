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
        checkPointManager = CheckPointObject.GetComponent<CheckPointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            isOpenMenu = !isOpenMenu;
    }

    void OnGUI()
    {
        if (isOpenMenu)
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "���ի��s\n�^��O���I"))
            {
                checkPointManager.ReturnCheckPoint();
            }
        }
    }
}