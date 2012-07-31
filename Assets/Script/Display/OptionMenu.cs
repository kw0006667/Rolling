using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
public class OptionMenu : MonoBehaviour
{

    public GameObject CheckPointObject;
    public Texture OptionBackground;
    public Texture[] HintTextures;

    private bool isOpenMenu = false;
    private CheckPointManager checkPointManager;
    private GameDefinition.OptionMenu optionMenu;

    #region Tutorials Properties
    // Tutorials properties
    private Rect tutorialsAreaRect = new Rect(0, 0, 600, 390);
    private Vector2 tutorialsScrolViewPosition = new Vector2(0, 0);
    private Vector2 tutorialsScrolViewSize = new Vector2(275, 336);
    private Vector2 tutorialsHintTextureSize = new Vector2(241, 300);
    private int tutorialsValue;
    #endregion

    #region Option Properties

    // Option Quality properties
    private Rect optionQualityContentRect = new Rect(0, 0, 200, 25);
    private Rect optionQualityTitleRect = new Rect(0, 0, 150, 25);
    private Rect optionQualityButtonLeftRect = new Rect(0, 0, 30, 25);
    private Rect optionQualityButtonRightRect = new Rect(0, 0, 30, 25);
    private string optionQualityTitleString = "Quality";
    private GameDefinition.QualityContent optionQualityContentValue;

    // Option Resolution properties
    private Rect optionResolutionTitleRect = new Rect(0, 0, 150, 25);
    private Rect optionResolutionContentRect = new Rect(0, 0, 200, 25);
    private Rect optionResolutionButtonLeftRect = new Rect(0, 0, 30, 25);
    private Rect optionResolutionButtonRightRect = new Rect(0, 0, 30, 25);
    private string optionResolutionTitleString = "Resolution";
    private int optionResolutionContentValue;
    private Resolution[] optionResolutions;
    private List<Resolution> optionResolutionList;
    private int optionResolutionMaxLenght;

    // Option FullScreen properties
    private Rect optionFullScreenTitleRect = new Rect(0, 0, 150, 25);
    private Rect optionFullScreenContentRect = new Rect(0, 0, 200, 25);
    private string optionFullScreenTitleString = "Full Screen";
    private bool optionFullScreenContentValue;

    // Option Back Button properties
    private Rect optionBackButtonRect = new Rect(0, 0, 150, 25);
    private string optionBackString = "Back";

    // Option Background properties
    private Rect optionBackgroundRect = new Rect(0, 0, 600, 390);

    private Rect returnCheckButtonRect = new Rect(0, 0, 300, 60);
    private Rect tutorialsButtonRect = new Rect(0, 0, 300, 60);
    private Rect optionButtonRect = new Rect(0, 0, 300, 60);
    private Rect returnTitleButtonRect = new Rect(0, 0, 300, 60);
    private Rect exitButtonRect = new Rect(0, 0, 300, 60);

    #endregion

    // Use this for initialization
    void Start()
    {
        this.checkPointManager = CheckPointObject.GetComponent<CheckPointManager>();
        // Tutorials Initialization
        this.tutorialsValue = 0;
        // Option Initialization
        this.optionMenu = GameDefinition.OptionMenu.None;
        this.optionQualityContentValue = GameDefinition.QualityContent.Fastest;
        this.optionResolutionContentValue = 0;
        this.optionResolutions = Screen.resolutions;
        this.optionResolutionList = new List<Resolution>();
        foreach (var res in this.optionResolutions)
        {
            if (res.width >= 1024 && res.height >= 768)
                this.optionResolutionList.Add(res);
        }
        this.optionResolutionMaxLenght = this.optionResolutionList.Count;
        this.optionFullScreenContentValue = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.isOpenMenu = !this.isOpenMenu;
            this.optionMenu = GameDefinition.OptionMenu.None;
        }
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
                    this.optionMenu = GameDefinition.OptionMenu.Tutorials;
                }
                // If option button has been clicked or not.
                if (GUI.Button(this.optionButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.Option)))
                {
                    this.optionMenu = GameDefinition.OptionMenu.Option;
                }
                // If return title button has been clicked or not.
                if (GUI.Button(this.returnTitleButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.ReturnTitle)))
                {
                    Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.StartMenu));
                }
                // If exit button has been clicked or not.
                if (GUI.Button(this.exitButtonRect, GameDefinition.GetOptionMenuString(GameDefinition.OptionMenu.Exit)))
                {
                    this.isOpenMenu = false;
                    Application.Quit();
                }
            }
            #endregion

            #region Option Menu : Tutorials
            if (this.optionMenu.Equals(GameDefinition.OptionMenu.Tutorials))
            {
                // Display Option Background picture
                if (this.OptionBackground != null)
                {
                    GUI.DrawTexture(this.optionBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
                }
                GUILayout.BeginArea(this.tutorialsAreaRect);
                {
                    GUILayout.BeginHorizontal();
                    {
                        this.tutorialsScrolViewPosition = GUILayout.BeginScrollView(this.tutorialsScrolViewPosition, false, true, GUILayout.Width(this.tutorialsScrolViewSize.x), GUILayout.Height(this.tutorialsScrolViewSize.y));
                        {
                            GUILayout.BeginVertical();
                            if (GUILayout.Button("\nHint 1\n"))
                                this.tutorialsValue = 0;
                            if (GUILayout.Button("\nHint 2\n"))
                                this.tutorialsValue = 1;
                            if (GUILayout.Button("\nHint 3\n"))
                                this.tutorialsValue = 2;
                            if (GUILayout.Button("\nHint 4\n"))
                                this.tutorialsValue = 3;
                            if (GUILayout.Button("\nHint 5\n"))
                                this.tutorialsValue = 4;
                            if (GUILayout.Button("\nHint 6\n"))
                                this.tutorialsValue = 5;
                            if (GUILayout.Button("\nHint 7\n"))
                                this.tutorialsValue = 6;
                            if (GUILayout.Button("\nHint 8\n"))
                                this.tutorialsValue = 7;
                            if (GUILayout.Button("\nHint 9\n"))
                                this.tutorialsValue = 8;
                            if (GUILayout.Button("\nHint 10\n"))
                                this.tutorialsValue = 9;
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndScrollView();
                        GUILayout.Box(this.HintTextures[this.tutorialsValue], GUILayout.Width(this.tutorialsHintTextureSize.x), GUILayout.Height(this.tutorialsHintTextureSize.y));
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndArea();

                // Back Button
                if (GUI.Button(this.optionBackButtonRect, this.optionBackString))
                {
                    this.optionMenu = GameDefinition.OptionMenu.None;
                }
            }
            #endregion

            #region Option Menu : Option
            if (this.optionMenu.Equals(GameDefinition.OptionMenu.Option))
            {
                // Display Option Background picture
                if (this.OptionBackground != null)
                {
                    GUI.DrawTexture(this.optionBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
                }

                // Quality Options
                GUI.Box(this.optionQualityTitleRect, this.optionQualityTitleString);
                if (GUI.Button(this.optionQualityButtonLeftRect, "<"))
                {
                    this.setQuality(SETVALUE.DECREASE);
                }
                GUI.Box(this.optionQualityContentRect, QualitySettings.names[(int)this.optionQualityContentValue]);
                if (GUI.Button(this.optionQualityButtonRightRect, ">"))
                {
                    this.setQuality(SETVALUE.INCREASE);
                }
                // Resolution Options
                GUI.Box(this.optionResolutionTitleRect, this.optionResolutionTitleString);
                if (GUI.Button(this.optionResolutionButtonLeftRect, "<"))
                {
                    this.setResolution(SETVALUE.DECREASE);
                }
                GUI.Box(this.optionResolutionContentRect, string.Format("{0} x {1}", this.optionResolutionList[this.optionResolutionContentValue].width, this.optionResolutionList[this.optionResolutionContentValue].height));
                if (GUI.Button(this.optionResolutionButtonRightRect, ">"))
                {
                    this.setResolution(SETVALUE.INCREASE);
                }
                bool tempFullScreenValue = this.optionFullScreenContentValue;
                this.optionFullScreenContentValue = GUI.Toggle(this.optionFullScreenContentRect, this.optionFullScreenContentValue, this.optionFullScreenTitleString);
                if (!tempFullScreenValue.Equals(this.optionFullScreenContentValue))
                    this.setResolution(this.optionFullScreenContentValue);

                // Back Button
                if (GUI.Button(this.optionBackButtonRect, this.optionBackString))
                {
                    this.optionMenu = GameDefinition.OptionMenu.None;
                }
            }
            #endregion
        }
    }

    #region Support Methods

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
        // ---------------Tutorials-----------------------------
        this.tutorialsAreaRect = new Rect((Screen.width - (int)this.tutorialsAreaRect.width) / 2 + 25,
                                              (Screen.height - (int)this.tutorialsAreaRect.height) / 2 + 15,
                                               this.tutorialsAreaRect.width,
                                               this.tutorialsAreaRect.height);

        // ---------------Quality-----------------------------
        this.optionQualityTitleRect = new Rect((Screen.width - (int)this.optionQualityTitleRect.width) / 2 - 150,
                                              (Screen.height - (int)this.optionQualityTitleRect.height) / 2 - 140,
                                               this.optionQualityTitleRect.width,
                                               this.optionQualityTitleRect.height);
        this.optionQualityButtonLeftRect = new Rect((Screen.width - (int)this.optionQualityButtonLeftRect.width) / 2,
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

        // ---------------Resolution------------------------------
        this.optionResolutionTitleRect = new Rect((Screen.width - (int)this.optionResolutionTitleRect.width) / 2 - 150,
                                              (Screen.height - (int)this.optionResolutionTitleRect.height) / 2 - 90,
                                               this.optionResolutionTitleRect.width,
                                               this.optionResolutionTitleRect.height);
        this.optionResolutionButtonLeftRect = new Rect((Screen.width - (int)this.optionResolutionButtonLeftRect.width) / 2,
                                              (Screen.height - (int)this.optionResolutionButtonLeftRect.height) / 2 - 90,
                                               this.optionResolutionButtonLeftRect.width,
                                               this.optionResolutionButtonLeftRect.height);
        this.optionResolutionContentRect = new Rect((Screen.width - (int)this.optionResolutionContentRect.width) / 2 + 125,
                                              (Screen.height - (int)this.optionResolutionContentRect.height) / 2 - 90,
                                               this.optionResolutionContentRect.width,
                                               this.optionResolutionContentRect.height);
        this.optionResolutionButtonRightRect = new Rect((Screen.width - (int)this.optionResolutionButtonRightRect.width) / 2 + 250,
                                              (Screen.height - (int)this.optionResolutionButtonRightRect.height) / 2 - 90,
                                               this.optionResolutionButtonRightRect.width,
                                               this.optionResolutionButtonRightRect.height);
        this.optionFullScreenContentRect = new Rect((Screen.width - (int)this.optionFullScreenContentRect.width) / 2 + 125,
                                              (Screen.height - (int)this.optionFullScreenContentRect.height) / 2 - 40,
                                               this.optionFullScreenContentRect.width,
                                               this.optionFullScreenContentRect.height);

        // ---------------Back------------------------------
        this.optionBackButtonRect = new Rect((Screen.width - (int)this.optionBackButtonRect.width) / 2 + 170,
                                              (Screen.height - (int)this.optionBackButtonRect.height) / 2 + 150,
                                               this.optionBackButtonRect.width,
                                               this.optionBackButtonRect.height);
    }

    private void setQuality(SETVALUE value)
    {
        int qualityTempValue = (int)this.optionQualityContentValue + (int)value;
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

    private void setResolution(SETVALUE value)
    {
        this.optionResolutionContentValue += (int)value;

        if (this.optionResolutionContentValue < 0)
            this.optionResolutionContentValue = this.optionResolutionMaxLenght - 1;
        else if (this.optionResolutionContentValue > this.optionResolutionMaxLenght - 1)
            this.optionResolutionContentValue = 0;

        Screen.SetResolution(this.optionResolutionList[this.optionResolutionContentValue].width, this.optionResolutionList[this.optionResolutionContentValue].height, this.optionFullScreenContentValue);
    }

    private void setResolution(bool isFullScreen)
    {
        Screen.SetResolution(this.optionResolutionList[this.optionResolutionContentValue].width, this.optionResolutionList[this.optionResolutionContentValue].height, this.optionFullScreenContentValue);
    }

    #endregion
}