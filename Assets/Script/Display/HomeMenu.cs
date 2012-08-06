using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HomeMenu : MonoBehaviour
{
    public GameDefinition.HomeMenu homeMenu;
    public Texture OptionBackground;
    public Texture[] StageTextures;

    private bool isTrigger = false;
    private FileManager fileManager;
    private SettingData settingData;
    private List<ScoreData> scoreList;
    private List<RecordData> recordList;
    private string machineName;

    #region Load Properties
    private Rect loadAreaRect = new Rect(0, 0, 600, 585);
    private Rect loadBackgroundRect = new Rect(0, 0, 900, 585);


    private Vector2 loadButtonSize = new Vector2(300, 75);
    private Vector2 loadButtonSize2 = new Vector2(200, 50);
    private Vector2 loadBoxAreaSize = new Vector2(285, 395);
    private Vector2 loadOptionHorizonalSize = new Vector2(600, 70);
    private string recordContentValue;
    private int recordChoice;
    #endregion

    #region Stage Properties

    private Rect stageAreaRect = new Rect(0, 0, 900, 585);
    private Rect stageBackgroundRect = new Rect(0, 0, 900, 585);

    private Vector2 stageScrolViewPosition = new Vector2(0, 0);
    private Vector2 stageScrolViewSize = new Vector2(300, 400);
    private Vector2 stageButtonSize = new Vector2(300, 100);
    private Vector2 stageTextureSize = new Vector2(500, 400);
    private Vector2 stageHintBoxSize = new Vector2(500, 100);
    private int stageValue;
    private GameDefinition.Scene stageScene = GameDefinition.Scene.none;

    #endregion

    #region HighScore Properties

    private Rect highScoreAreaRect = new Rect(0, 0, 900, 585);
    private Rect highScoreBackgroundRect = new Rect(0, 0, 900, 585);
    private int highScoreSceneValue;
    private int highScoreSceneMaxLenght;
    private int highScoreCount = 10;            // how many high score will be get
    private bool isHighScoreSceneChange = true;

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

    #endregion

    // Use this for initialization
    void Start()
    {
        // Option Initialization
        this.optionResolutions = Screen.resolutions;
        this.optionResolutionList = new List<Resolution>();
        foreach (Resolution res in this.optionResolutions)
        {
            if (res.width >= 1024 && res.height >= 720)
                this.optionResolutionList.Add(res);
        }
        this.optionResolutionMaxLenght = this.optionResolutionList.Count;

        this.machineName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        this.fileManager = new FileManager();

        // score reader
        this.fileManager.ScoresReader(GameDefinition.ScoresFilePath);
        this.highScoreSceneValue = (int)GameDefinition.Scene.BeginChallenge;
        this.highScoreSceneMaxLenght = Enum.GetValues(typeof(GameDefinition.Scene)).Length;

        this.fileManager.ConfigReader(GameDefinition.SettingFilePath, this.machineName);
        this.settingData = this.fileManager.GetSettingData();

        // Get xml setting value
        this.optionQualityContentValue = (GameDefinition.QualityContent)Convert.ToInt32(this.settingData.Quality);
        this.optionFullScreenContentValue = Convert.ToBoolean(this.settingData.FullScreen);
        if (Convert.ToInt32(this.settingData.Resolution) > this.optionResolutionMaxLenght)
        {
            this.optionResolutionContentValue = this.optionResolutionMaxLenght - 1;
            this.settingData.Resolution = this.optionResolutionContentValue.ToString();
            this.fileManager.ConfigWrite(this.settingData);
        }
        else
            this.optionResolutionContentValue = Convert.ToInt32(this.settingData.Resolution);

        // Set xml setting value
        QualitySettings.SetQualityLevel((int)this.optionQualityContentValue, true);
        Screen.SetResolution(this.optionResolutionList[this.optionResolutionContentValue].width,
                             this.optionResolutionList[this.optionResolutionContentValue].height,
                             this.optionFullScreenContentValue);

        this.recordContentValue = String.Empty;
        this.recordChoice = -1;
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

                #region Menu : HighScore
                case GameDefinition.HomeMenu.Continute:

                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.highScoreBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
                    }
                    GUILayout.BeginArea(this.highScoreAreaRect);
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Space(25);

                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.Space(25);

                                if (GUILayout.Button("<", GUILayout.Width(50)))
                                {
                                    this.setScene(SETVALUE.DECREASE);
                                    this.isHighScoreSceneChange = true;
                                }

                                GUILayout.Box(GameDefinition.GetSceneName((GameDefinition.Scene)this.highScoreSceneValue), GUILayout.Width(200));

                                if (GUILayout.Button(">", GUILayout.Width(50)))
                                {
                                    this.setScene(SETVALUE.INCREASE);
                                    this.isHighScoreSceneChange = true;
                                }
                                GUILayout.Space(300);

                                if (GUILayout.Button("back", GUILayout.Width(100)))
                                    this.isTrigger = false;

                                GUILayout.Space(25);
                            }
                            GUILayout.EndHorizontal();

                            GUILayout.Space(50);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.Space(25);
                                GUILayout.Box("名次", GUILayout.Width(50));
                                GUILayout.Box("分數", GUILayout.Width(100));
                                GUILayout.Box("遊戲時間", GUILayout.Width(150));
                                GUILayout.Box("銅", GUILayout.Width(50));
                                GUILayout.Box("銀", GUILayout.Width(50));
                                GUILayout.Box("金", GUILayout.Width(50));
                                GUILayout.Box("等級", GUILayout.Width(50));
                                GUILayout.Box("紀錄時間");
                                GUILayout.Space(25);
                            }
                            GUILayout.EndHorizontal();


                            GUILayout.BeginVertical();
                            {
                                if (this.isHighScoreSceneChange)
                                {
                                    this.scoreList = this.fileManager.GetHighScores(GameDefinition.GetSceneName((GameDefinition.Scene)this.highScoreSceneValue), this.highScoreCount);
                                    this.isHighScoreSceneChange = false;
                                }
                                for (int i = 0; i < this.highScoreCount; i++)
                                {
                                    if (this.scoreList.Count > i)
                                    {
                                        GUILayout.BeginHorizontal();
                                        {
                                            GUILayout.Space(25);
                                            GUILayout.Box((i + 1).ToString(), GUILayout.Width(50));
                                            GUILayout.Box(this.scoreList[i].Score, GUILayout.Width(100));
                                            GUILayout.Box(this.scoreList[i].GameTime, GUILayout.Width(150));
                                            GUILayout.Box(this.scoreList[i].Coppers, GUILayout.Width(50));
                                            GUILayout.Box(this.scoreList[i].Silvers, GUILayout.Width(50));
                                            GUILayout.Box(this.scoreList[i].Golds, GUILayout.Width(50));
                                            GUILayout.Box(this.scoreList[i].Rank, GUILayout.Width(50));
                                            GUILayout.Box(this.scoreList[i].PlayDate);
                                            GUILayout.Space(25);
                                        }
                                        GUILayout.EndHorizontal();
                                    }
                                    else
                                    {
                                        GUILayout.BeginHorizontal();
                                        {
                                            GUILayout.Space(25);
                                            GUILayout.Box((i + 1).ToString(), GUILayout.Width(50));
                                            GUILayout.Box("------", GUILayout.Width(100));
                                            GUILayout.Box("------", GUILayout.Width(150));
                                            GUILayout.Box("------", GUILayout.Width(50));
                                            GUILayout.Box("------", GUILayout.Width(50));
                                            GUILayout.Box("------", GUILayout.Width(50));
                                            GUILayout.Box("------", GUILayout.Width(50));
                                            GUILayout.Box("------");
                                            GUILayout.Space(25);
                                        }
                                        GUILayout.EndHorizontal();
                                    }
                                }
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();

                    break;
                #endregion

                case GameDefinition.HomeMenu.Load:
                    this.fileManager.RecordsReader(GameDefinition.RecordFilePath);
                    this.recordList = this.fileManager.GetRecords();
                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.stageBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
                    }
                    GUILayout.BeginArea(this.loadAreaRect);
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Space(57);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.BeginVertical();
                                {
                                    if (GUILayout.Button("Record 1", GUILayout.MaxWidth(this.loadButtonSize.x), GUILayout.MaxHeight(this.loadButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[0].RecordName + "\n" + this.recordList[0].Scene + "\n" + this.recordList[0].SaveDate;
                                        this.recordChoice = 0;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 2", GUILayout.MaxWidth(this.loadButtonSize.x), GUILayout.MaxHeight(this.loadButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[1].RecordName + "\n" + this.recordList[1].Scene + "\n" + this.recordList[1].SaveDate;
                                        this.recordChoice = 1;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 3", GUILayout.MaxWidth(this.loadButtonSize.x), GUILayout.MaxHeight(this.loadButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[2].RecordName + "\n" + this.recordList[2].Scene + "\n" + this.recordList[2].SaveDate;
                                        this.recordChoice = 2;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 4", GUILayout.MaxWidth(this.loadButtonSize.x), GUILayout.MaxHeight(this.loadButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[3].RecordName + "\n" + this.recordList[3].Scene + "\n" + this.recordList[3].SaveDate;
                                        this.recordChoice = 3;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 5", GUILayout.MaxWidth(this.loadButtonSize.x), GUILayout.MaxHeight(this.loadButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[4].RecordName + "\n" + this.recordList[4].Scene + "\n" + this.recordList[4].SaveDate;
                                        this.recordChoice = 4;
                                    }
                                    GUILayout.Space(5);
                                }
                                GUILayout.EndVertical();
                                GUILayout.Space(10);
                                GUILayout.Box(this.recordContentValue, GUILayout.MaxWidth(this.loadBoxAreaSize.x), GUILayout.MaxHeight(this.loadBoxAreaSize.y));
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal(GUILayout.MaxWidth(this.loadOptionHorizonalSize.x), GUILayout.MaxHeight(this.loadOptionHorizonalSize.y));
                            {
                                GUILayout.Space(95);
                                if (GUILayout.Button("Load", GUILayout.MaxWidth(this.loadButtonSize2.x), GUILayout.MaxHeight(this.loadButtonSize2.y)))
                                {
                                    if (!this.recordList[this.recordChoice].Scene.Equals(string.Empty))
                                        Application.LoadLevel(this.recordList[this.recordChoice].Scene);
                                    else
                                        this.recordContentValue = "No Record!";
                                }
                                GUILayout.Space(10);
                                if (GUILayout.Button("Delete", GUILayout.MaxWidth(this.loadButtonSize2.x), GUILayout.MaxHeight(this.loadButtonSize2.y)))
                                {
                                    if (!this.recordList[this.recordChoice].Scene.Equals(string.Empty))
                                    {
                                        if (this.fileManager.RecordDelete(this.recordChoice))
                                        {
                                            this.recordContentValue = "";
                                            this.fileManager.RecordUpdate();
                                            this.recordList = this.fileManager.GetRecords();
                                        }
                                    }
                                    else
                                        this.recordContentValue = "No Record!";
                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();
                    break;

                #region Menu : Stage
                case GameDefinition.HomeMenu.Stage:
                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.stageBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
                    }

                    GUILayout.BeginArea(this.stageAreaRect);
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Space(25);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.Space(25);
                                GUILayout.BeginVertical();
                                {
                                    this.stageScrolViewPosition = GUILayout.BeginScrollView(this.stageScrolViewPosition, false, true, GUILayout.MaxWidth(this.stageScrolViewSize.x), GUILayout.MaxHeight(this.stageScrolViewSize.y));
                                    {
                                        GUILayout.BeginVertical();
                                        if (GUILayout.Button("\nBeginChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 0;
                                            stageScene = GameDefinition.Scene.BeginChallenge;
                                        }
                                        if (GUILayout.Button("\nFirstStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 1;
                                            stageScene = GameDefinition.Scene.FirstStageChallenge;
                                        }
                                        if (GUILayout.Button("\nFirstStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 2;
                                            stageScene = GameDefinition.Scene.FirstStage_Hard;
                                        }
                                        if (GUILayout.Button("\nSecondStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 3;
                                            stageScene = GameDefinition.Scene.SecondStageChallenge;
                                        }
                                        if (GUILayout.Button("\nSecondStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 4;
                                            stageScene = GameDefinition.Scene.SecondStage_Hard;
                                        }
                                        if (GUILayout.Button("\nSpeedStageOne\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 5;
                                            stageScene = GameDefinition.Scene.SpeedStageOne;
                                        }
                                        if (GUILayout.Button("\nSpeedStageTwo\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 6;
                                            stageScene = GameDefinition.Scene.SpeedStageTwo;
                                        }
                                        if (GUILayout.Button("\nSpecialStage_SpeedUp\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                        {
                                            this.stageValue = 7;
                                            stageScene = GameDefinition.Scene.SpecialStage_SpeedUp;
                                        }
                                        GUILayout.EndVertical();
                                    }
                                    GUILayout.EndScrollView();

                                    GUILayout.Space(25);

                                    if (GUILayout.Button("\n GO \n"))
                                    {
                                        if (stageScene != GameDefinition.Scene.none)
                                            Application.LoadLevel(GameDefinition.GetSceneName(stageScene));
                                    }
                                }
                                GUILayout.EndVertical();
                            }

                            GUILayout.Space(25);

                            GUILayout.BeginVertical();
                            {
                                GUILayout.Box(this.StageTextures[this.stageValue], GUILayout.Width(this.stageTextureSize.x), GUILayout.Height(this.stageTextureSize.y));
                                GUILayout.Space(25);
                                GUILayout.Box("第" + (this.stageValue + 1).ToString() + "關說明(待補)", GUILayout.Width(this.stageHintBoxSize.x), GUILayout.Height(this.stageHintBoxSize.y));
                            }
                            GUILayout.EndVertical();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();
                    break;
                #endregion

                #region Menu : Option

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
                #endregion

                #region Menu : Exit

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

                #endregion

                default:
                    break;
            }
        }
        else
        {
            this.recordContentValue = string.Empty;
            this.recordChoice = -1;
        }
    }

    #region Support Methods

    // Initialize All Buttons Rect real time
    private void InitializeButtonRect()
    {
        #region Load Rect
        this.loadAreaRect = new Rect((Screen.width - (int)this.loadAreaRect.width) / 2,
                                     (Screen.height - (int)this.loadAreaRect.height) / 2,
                                      this.loadAreaRect.width,
                                      this.loadAreaRect.height);
        this.loadBackgroundRect = new Rect((Screen.width - (int)this.loadBackgroundRect.width) / 2,
                                           (Screen.height - (int)this.loadBackgroundRect.height) / 2,
                                           this.loadBackgroundRect.width,
                                           this.loadBackgroundRect.height);
        #endregion

        #region HighScore Rect

        // --------------------HighScore Background------------------
        this.highScoreBackgroundRect = new Rect((Screen.width - (int)this.highScoreBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.highScoreBackgroundRect.height) / 2,
                                               this.highScoreBackgroundRect.width,
                                               this.highScoreBackgroundRect.height);

        this.highScoreAreaRect = new Rect((Screen.width - (int)this.highScoreAreaRect.width) / 2 ,
                                              (Screen.height - (int)this.highScoreAreaRect.height) / 2,
                                               this.highScoreAreaRect.width,
                                               this.highScoreAreaRect.height);
        #endregion

        #region Stage Rect

        // --------------------Stage Background------------------
        this.stageBackgroundRect = new Rect((Screen.width - (int)this.stageBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.stageBackgroundRect.height) / 2,
                                               this.stageBackgroundRect.width,
                                               this.stageBackgroundRect.height);

        this.stageAreaRect = new Rect((Screen.width - (int)this.stageAreaRect.width) / 2,
                                              (Screen.height - (int)this.stageAreaRect.height) / 2,
                                               this.stageAreaRect.width,
                                               this.stageAreaRect.height);
        #endregion

        #region Option Rect

        // --------------------Background------------------
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
        #endregion
        
    }

    // Menu:Option use functtion
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
        this.settingData.Quality = ((int)this.optionQualityContentValue).ToString();
        this.fileManager.ConfigWrite(this.settingData);
    }

    // Menu:Option use functtion
    private void setResolution(SETVALUE value)
    {
        this.optionResolutionContentValue += (int)value;

        if (this.optionResolutionContentValue < 0)
            this.optionResolutionContentValue = this.optionResolutionMaxLenght - 1;
        else if (this.optionResolutionContentValue > this.optionResolutionMaxLenght - 1)
            this.optionResolutionContentValue = 0;

        Screen.SetResolution(this.optionResolutionList[this.optionResolutionContentValue].width, this.optionResolutionList[this.optionResolutionContentValue].height, this.optionFullScreenContentValue);
        this.settingData.Resolution = this.optionResolutionContentValue.ToString();
        this.fileManager.ConfigWrite(this.settingData);
    }

    // Menu:Option use functtion
    private void setResolution(bool isFullScreen)
    {
        Screen.SetResolution(this.optionResolutionList[this.optionResolutionContentValue].width, this.optionResolutionList[this.optionResolutionContentValue].height, this.optionFullScreenContentValue);
        this.settingData.FullScreen = this.optionFullScreenContentValue.ToString();
        this.fileManager.ConfigWrite(this.settingData);
    }

    // Menu:HighScore use functtion
    private void setScene(SETVALUE value)
    {
        this.highScoreSceneValue += (int)value;

        if (this.highScoreSceneValue < 0)
            this.highScoreSceneValue = this.highScoreSceneMaxLenght - 1;
        else if (this.highScoreSceneValue > this.highScoreSceneMaxLenght - 1)//12 >  12
            this.highScoreSceneValue = 0;

        switch ((GameDefinition.Scene)this.highScoreSceneValue)
        {
            case GameDefinition.Scene.none:
            case GameDefinition.Scene.StartMenu:
            case GameDefinition.Scene.Begin:
            case GameDefinition.Scene.FirstStage:
            case GameDefinition.Scene.SecondStage:
                this.setScene(value);
                break;

            default:
                break;
        }
    }

    #endregion
}
