using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour
{
    public bool isOpenMenu = false;
    public GameObject CheckPointObject;
    private CheckPointManager checkPointManager;

    private GameDefinition.OptionMenu optionMenu;

    private Rect returnCheckButtonRect = new Rect(0, 0, 300, 60);
    private Rect tutorialsButtonRect = new Rect(0, 0, 300, 60);
    private Rect optionButtonRect = new Rect(0, 0, 300, 60);
    private Rect returnTitleButtonRect = new Rect(0, 0, 300, 60);
    private Rect exitButtonRect = new Rect(0, 0, 300, 60);

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
        // Initialize all button rect real time
        this.InitializeButtonRect();

        if (this.isOpenMenu)
        {
            #region Option None
            if (this.optionMenu.Equals(GameDefinition.OptionMenu.None))
            {
                // If return to Checkpoint button has been clicked or not.
                if (GUI.Button(this.returnCheckButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.ReturnCheck)))
                {
                    this.checkPointManager.ReturnCheckPoint();
                    this.isOpenMenu = false;
                }
                // If tutorials button has been clicked or not.
                if (GUI.Button(this.tutorialsButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.Tutorials)))
                {
                    this.isOpenMenu = false;
                }
                // If option button has been clicked or not.
                if (GUI.Button(this.optionButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.Option)))
                {
                    this.isOpenMenu = false;
                }
                // If return title button has been clicked or not.
                if (GUI.Button(this.returnTitleButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.ReturnTitle)))
                {
                    this.isOpenMenu = false;
                }
                // If exit button has been clicked or not.
                if (GUI.Button(this.exitButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.Exit)))
                {
                    this.isOpenMenu = false;
                    Application.Quit();
                }
            }
            #endregion
        }
    }

    // Initialize All Buttons Rect real time
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
                                              (Screen.height - (int)this.exitButtonRect.height) / 2 + 300,
                                               this.exitButtonRect.width,
                                               this.exitButtonRect.height);
    }
}