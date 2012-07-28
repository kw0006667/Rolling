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
            if (GUI.Button(new Rect(0, 0, 100, 100), "測試按鈕\n回到記錄點"))
            {
                this.checkPointManager.ReturnCheckPoint();
                this.isOpenMenu = false;
            }
        }
    }
}