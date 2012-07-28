using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour
{

    public GameObject CheckPointObject;
    public Texture OptionBackground;

    private bool isOpenMenu = false;
    private CheckPointManager checkPointManager;
    private GameDefinition.OptionMenu optionMenu;

    private Rect optionQualityContentRect = new Rect(0, 0, 200, 25);
    private Rect optionQualityTitleRect = new Rect(0, 0, 150, 25);
    private Rect optionQualityButtonLeftRect = new Rect(0, 0, 30, 25);
    private Rect optionQualityButtonRightRect = new Rect(0, 0, 30, 25);
    private string optionQualityTitleString = "Quality";
    private GameDefinition.QualityContent optionQualityContentValue;

    private Rect optionBackgroundRect = new Rect(0, 0, 600, 390);

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
        this.optionQualityContentValue = GameDefinition.QualityContent.Fastest;
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
            #region Option Menu : None
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
                    this.optionMenu = GameDefinition.OptionMenu.Option;
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

            #region Option Menu : Option
            if (this.optionMenu.Equals(GameDefinition.OptionMenu.Option))
            {
                if (this.OptionBackground != null)
                {
                    GUI.DrawTexture(this.optionBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
                }
                
                GUI.Box(this.optionQualityTitleRect, this.optionQualityTitleString);
                if (GUI.Button(this.optionQualityButtonLeftRect, "<"))
                {
                    this.setQuality(-1);
                }
                GUI.Box(this.optionQualityContentRect, QualitySettings.names[(int)this.optionQualityContentValue]);
                if (GUI.Button(this.optionQualityButtonRightRect, ">"))
                {
                    this.setQuality(1);
                }
                
            }
            #endregion
        }
    }

    // Initialize All Buttons Rect real time
    private void InitializeButtonRect()
    {
        this.optionBackgroundRect = new Rect((Screen.width - (int)this.optionBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.optionBackgroundRect.height) / 2,
                                               this.optionBackgroundRect.width,
                                               this.optionBackgroundRect.height);
        this.returnCheckButtonRect = new Rect((Screen.width - (int)this.returnCheckButtonRect.width) / 2,
                                              (Screen.height - (int)this.returnCheckButtonRect.height) / 2 - 140,
                                               this.returnCheckButtonRect.width,
                                               this.returnCheckButtonRect.height);
        this.tutorialsButtonRect = new Rect((Screen.width - (int)this.tutorialsButtonRect.width) / 2,
                                              (Screen.height - (int)this.tutorialsButtonRect.height) / 2 - 70,
                                               this.tutorialsButtonRect.width,
                                               this.tutorialsButtonRect.height);
        this.optionButtonRect = new Rect((Screen.width - (int)this.optionButtonRect.width) / 2,
                                              (Screen.height - (int)this.optionButtonRect.height) / 2,
                                               this.optionButtonRect.width,
                                               this.optionButtonRect.height);
        this.returnTitleButtonRect = new Rect((Screen.width - (int)this.returnTitleButtonRect.width) / 2,
                                              (Screen.height - (int)this.returnTitleButtonRect.height) / 2 + 70,
                                               this.returnTitleButtonRect.width,
                                               this.returnTitleButtonRect.height);
        this.exitButtonRect = new Rect((Screen.width - (int)this.exitButtonRect.width) / 2,
                                              (Screen.height - (int)this.exitButtonRect.height) / 2 + 140,
                                               this.exitButtonRect.width,
                                               this.exitButtonRect.height);

        this.optionQualityTitleRect = new Rect((Screen.width - (int)this.optionQualityTitleRect.width) / 2 - 150,
                                              (Screen.height - (int)this.optionQualityTitleRect.height) / 2 - 140,
                                               this.optionQualityTitleRect.width,
                                               this.optionQualityTitleRect.height);
        this.optionQualityButtonLeftRect = new Rect((Screen.width - (int)this.optionQualityButtonLeftRect.width) / 2 ,
                                              (Screen.height - (int)this.optionQualityButtonLeftRect.height) / 2 - 140,
                                               this.optionQualityButtonLeftRect.width,
                                               this.optionQualityButtonLeftRect.height);

        this.optionQualityContentRect = new Rect((Screen.width - (int)this.optionQualityContentRect.width) / 2 + 125,
                                              (Screen.height - (int)this.optionQualityContentRect.height) / 2 - 140,
                                               this.optionQualityContentRect.width,
                                               this.optionQualityContentRect.height);

        this.optionQualityButtonRightRect = new Rect((Screen.width - (int)this.optionQualityButtonRightRect.width) / 2 + 250,
                                              (Screen.height - (int)this.optionQualityButtonRightRect.height) / 2 - 140,
                                               this.optionQualityButtonRightRect.width,
                                               this.optionQualityButtonRightRect.height);
    }

    private void setQuality(int value)
    {
        int qualityTempValue = (int)this.optionQualityContentValue + value;
        switch (qualityTempValue)
        {
            case -1:
                this.optionQualityContentValue = GameDefinition.QualityContent.Fantastic;
                break;
            case 0:
                this.optionQualityContentValue = GameDefinition.QualityContent.Fastest;
                break;
            case 1:
                this.optionQualityContentValue = GameDefinition.QualityContent.Fast;
                break;
            case 2:
                this.optionQualityContentValue = GameDefinition.QualityContent.Simple;
                break;
            case 3:
                this.optionQualityContentValue = GameDefinition.QualityContent.Good;
                break;
            case 4:
                this.optionQualityContentValue = GameDefinition.QualityContent.Beautiful;
                break;
            case 5:
                this.optionQualityContentValue = GameDefinition.QualityContent.Fantastic;
                break;
            case 6:
                this.optionQualityContentValue = GameDefinition.QualityContent.Fastest;
                break;
            default:
                break;
        }
        QualitySettings.SetQualityLevel((int)this.optionQualityContentValue, true);
    }
}