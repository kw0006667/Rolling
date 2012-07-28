using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomeMenu : MonoBehaviour
{
    public GameDefinition.HomeMenu homeMenu;
    public Texture OptionBackground;

    private bool isTrigger = false;

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

    #endregion

    // Use this for initialization
    void Start()
    {
        // Option Initialization
        this.optionQualityContentValue = GameDefinition.QualityContent.Fastest;
        this.optionResolutionContentValue = 0;
        this.optionResolutions = Screen.resolutions;
        this.optionResolutionList = new List<Resolution>();
        foreach (Resolution res in this.optionResolutions)
        {
            if (res.width >= 1024 && res.height >= 720)
                this.optionResolutionList.Add(res);
        }
        this.optionResolutionMaxLenght = this.optionResolutionList.Count;

        this.optionFullScreenContentValue = true;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isTrigger = true;
            TriggerEvent();
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isTrigger = false;
        }
    }

    void TriggerEvent()
    {        
        switch (homeMenu)
        {
            case GameDefinition.HomeMenu.New:
                Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.Begin));
                break;
            case GameDefinition.HomeMenu.Continute:
                break;
            case GameDefinition.HomeMenu.Load:
                break;
            case GameDefinition.HomeMenu.Stage:
                break;
            case GameDefinition.HomeMenu.Option:
                break;
            case GameDefinition.HomeMenu.Exit:
                break;
            default:
                break;
        }
    }
    
    void OnGUI()
    {
        if (this.isTrigger)
        {
            // Initialize all button rect real time
            this.InitializeButtonRect();

            switch (homeMenu)
            {
                case GameDefinition.HomeMenu.New:
                    break;

                case GameDefinition.HomeMenu.Continute:
                    break;

                case GameDefinition.HomeMenu.Load:
                    break;

                case GameDefinition.HomeMenu.Stage:
                    break;

                case GameDefinition.HomeMenu.Option:
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
                        this.isTrigger = false;
                    }
                    break;

                case GameDefinition.HomeMenu.Exit:
                    if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.425f, Screen.width * 0.2f, Screen.height * 0.15f), "Yes"))
                    {
                        Application.Quit();
                    }
                    if (GUI.Button(new Rect(Screen.width * 0.6f, Screen.height * 0.425f, Screen.width * 0.2f, Screen.height * 0.15f), "No"))
                    {
                        this.isTrigger = false;
                    }
                    break;

                default:
                    break;
            }
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
        this.optionBackButtonRect = new Rect((Screen.width - (int)this.optionBackButtonRect.width) / 2 + 150,
                                              (Screen.height - (int)this.optionBackButtonRect.height) / 2 + 140,
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
