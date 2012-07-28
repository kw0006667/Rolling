using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour
{
    public bool isOpenMenu = false;
    public GameObject CheckPointObject;
    private CheckPointManager checkPointManager;

    private GameDefinition.OptionMenu optionMenu;

    private Rect returnCheckButtonRect = new Rect(0, 0, 150, 30);
    private Rect tutorialsButtonRect = new Rect(0, 0, 150, 30);
    private Rect optionButtonRect = new Rect(0, 0, 150, 30);
    private Rect returnTitleButtonRect = new Rect(0, 0, 150, 30);
    private Rect exitButtonRect = new Rect(0, 0, 150, 30);

    // Use this for initialization
    void Start()
    {
        this.checkPointManager = CheckPointObject.GetComponent<CheckPointManager>();
        this.optionMenu = GameDefinition.OptionMenu.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.isOpenMenu = !this.isOpenMenu;
    }

    void OnGUI()
    {
        this.InitializeButtonRect();

        if (this.isOpenMenu)
        {
            #region Option None
            if (this.optionMenu.Equals(GameDefinition.OptionMenu.None))
            {
                // If return to Checkpoint button has been clicked
                if (GUI.Button(this.returnCheckButtonRect, "測試按鈕\n回到記錄點"))
                {
                    this.checkPointManager.ReturnCheckPoint();
                    this.isOpenMenu = false;
                }
            }
            #endregion
        }
    }

    private void InitializeButtonRect()
    {
        this.returnCheckButtonRect = new Rect((Screen.width - (int)this.returnCheckButtonRect.width) / 2,
                                              (Screen.height - (int)this.returnCheckButtonRect.height) / 2 - 300,
                                               this.returnCheckButtonRect.width,
                                               this.returnCheckButtonRect.height);
        this.tutorialsButtonRect = new Rect((Screen.width - (int)this.tutorialsButtonRect.width) / 2,
                                              (Screen.height - (int)this.tutorialsButtonRect.height) / 2 - 150,
                                               this.tutorialsButtonRect.width,
                                               this.tutorialsButtonRect.height);
        this.optionButtonRect = new Rect((Screen.width - (int)this.optionButtonRect.width) / 2,
                                              (Screen.height - (int)this.optionButtonRect.height) / 2,
                                               this.optionButtonRect.width,
                                               this.optionButtonRect.height);
        this.returnTitleButtonRect = new Rect((Screen.width - (int)this.returnTitleButtonRect.width) / 2,
                                              (Screen.height - (int)this.returnTitleButtonRect.height) / 2 + 150,
                                               this.returnTitleButtonRect.width,
                                               this.returnTitleButtonRect.height);
        this.exitButtonRect = new Rect((Screen.width - (int)this.exitButtonRect.width) / 2,
                                              (Screen.height - (int)this.exitButtonRect.height) / 2 + 150,
                                               this.exitButtonRect.width,
                                               this.exitButtonRect.height);
    }
}