using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HomeMenu : MonoBehaviour
{
    public GameDefinition.HomeMenu homeMenu;
    public Texture OptionBackground;
    public Texture[] StageTextures;
    public Texture StageHintTexture;
    public GUIStyle StaffStyle;

    private bool isTrigger = false;
    private bool isExceptionChoice;
    private FileManager fileManager;
    private SettingData settingData;
    private List<ScoreData> scoreList;
    private List<RecordData> recordList;
    private string machineName;

    #region Start Properties
    private Rect startAreaRect = new Rect(0, 0, 600, 585);
    private Rect startBackgroundRect = new Rect(0, 0, 900, 585);


    private Vector2 startButtonSize = new Vector2(300, 75);
    private Vector2 startButtonSize2 = new Vector2(125, 50);
    private Vector2 startBoxAreaSize = new Vector2(285, 395);
    private Vector2 startOptionHorizonalSize = new Vector2(600, 70);
    private string recordContentValue;
    private int recordChoice;
    #endregion

    #region Staff Properties
    private Rect staffAreaRect = new Rect(0, 0, 600, 525);
    private Rect staffBackgroundRect = new Rect(0, 0, 900, 585);

    #endregion

    #region Stage Properties

    private Rect stageAreaRect = new Rect(0, 0, 900, 585);
    private Rect stageBackgroundRect = new Rect(0, 0, 900, 585);

    private Vector2 stageScrolViewPosition = new Vector2(0, 0);
    private Vector2 stageScrolViewSize = new Vector2(300, 400);
    private Vector2 stageButtonSize = new Vector2(300, 100);
    private Vector2 stageTextureSize = new Vector2(500, 400);
    private Vector2 stageHintBoxSize = new Vector2(500, 100);
    private int stageTextureValue;
    private int stageKeyValue;
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
        this.stageKeyValue = Convert.ToInt32(this.settingData.Key);
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
        this.isExceptionChoice = false;

        Screen.showCursor = true;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isTrigger = true;
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

    void OnGUI()
    {
        if (this.isTrigger)
        {
            // Initialize all button rect real time
            this.InitializeButtonRect();

            switch (homeMenu)
            {
                #region Menu : Start
                case GameDefinition.HomeMenu.Start:
                    this.fileManager.RecordsReader(GameDefinition.RecordFilePath);
                    this.recordList = this.fileManager.GetRecords();
                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.stageBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
                    }
                    GUILayout.BeginArea(this.startAreaRect);
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Space(57);
                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.BeginVertical();
                                {
                                    if (GUILayout.Button("Record 1", GUILayout.MaxWidth(this.startButtonSize.x), GUILayout.MaxHeight(this.startButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[0].RecordName + "\n" + this.recordList[0].Scene + "\n" + this.recordList[0].SaveDate;
                                        this.recordChoice = 0;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 2", GUILayout.MaxWidth(this.startButtonSize.x), GUILayout.MaxHeight(this.startButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[1].RecordName + "\n" + this.recordList[1].Scene + "\n" + this.recordList[1].SaveDate;
                                        this.recordChoice = 1;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 3", GUILayout.MaxWidth(this.startButtonSize.x), GUILayout.MaxHeight(this.startButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[2].RecordName + "\n" + this.recordList[2].Scene + "\n" + this.recordList[2].SaveDate;
                                        this.recordChoice = 2;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 4", GUILayout.MaxWidth(this.startButtonSize.x), GUILayout.MaxHeight(this.startButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[3].RecordName + "\n" + this.recordList[3].Scene + "\n" + this.recordList[3].SaveDate;
                                        this.recordChoice = 3;
                                    }
                                    GUILayout.Space(5);
                                    if (GUILayout.Button("Record 5", GUILayout.MaxWidth(this.startButtonSize.x), GUILayout.MaxHeight(this.startButtonSize.y)))
                                    {
                                        this.recordContentValue = this.recordList[4].RecordName + "\n" + this.recordList[4].Scene + "\n" + this.recordList[4].SaveDate;
                                        this.recordChoice = 4;
                                    }
                                    GUILayout.Space(5);
                                }
                                GUILayout.EndVertical();
                                GUILayout.Space(10);
                                GUILayout.Box(this.recordContentValue, GUILayout.MaxWidth(this.startBoxAreaSize.x), GUILayout.MaxHeight(this.startBoxAreaSize.y));
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal(GUILayout.MaxWidth(this.startOptionHorizonalSize.x), GUILayout.MaxHeight(this.startOptionHorizonalSize.y));
                            {
                                if (this.recordChoice >= 0)
                                {
                                    if (this.recordList[this.recordChoice].Scene.Equals(string.Empty))
                                    {
                                        GUILayout.Space(95);
                                        if (GUILayout.Button("開新檔案", GUILayout.MaxWidth(this.startButtonSize2.x), GUILayout.MaxHeight(this.startButtonSize2.y)))
                                        {
                                            PlayerPrefs.SetString(GameDefinition.RecordChoicePrefsString, this.recordChoice.ToString());
                                            Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.Begin));
                                        }
                                    }
                                    else
                                    {
                                        GUILayout.Space(90);
                                        if (GUILayout.Button("讀取", GUILayout.MaxWidth(this.startButtonSize2.x), GUILayout.MaxHeight(this.startButtonSize2.y)))
                                        {
                                            if (this.fileManager.RecordDelete(this.recordChoice))
                                            {
                                                PlayerPrefs.SetString(GameDefinition.RecordChoicePrefsString, this.recordChoice.ToString());
                                                Application.LoadLevel(this.recordList[this.recordChoice].Scene);
                                            }
                                        }
                                        GUILayout.Space(10);
                                        if (GUILayout.Button("覆蓋", GUILayout.MaxWidth(this.startButtonSize2.x), GUILayout.MaxHeight(this.startButtonSize2.y)))
                                        {
                                            if (this.fileManager.RecordDelete(this.recordChoice))
                                            {
                                                this.recordContentValue = "";
                                                this.fileManager.RecordUpdate();
                                                this.recordList = this.fileManager.GetRecords();
                                                PlayerPrefs.SetString(GameDefinition.RecordChoicePrefsString, this.recordChoice.ToString());
                                                Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.Begin));
                                            }
                                        }
                                        GUILayout.Space(10);
                                        if (GUILayout.Button("刪除", GUILayout.MaxWidth(this.startButtonSize2.x), GUILayout.MaxHeight(this.startButtonSize2.y)))
                                        {
                                            if (this.fileManager.RecordDelete(this.recordChoice))
                                            {
                                                this.recordContentValue = "";
                                                this.fileManager.RecordUpdate();
                                                this.recordList = this.fileManager.GetRecords();
                                            }
                                        }
                                    }

                                }
                            }
                            GUILayout.EndHorizontal();
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();
                    break;
                #endregion

                #region Menu : HighScore
                case GameDefinition.HomeMenu.HighScore:

                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.highScoreBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
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

                #region Menu : Staff
                case GameDefinition.HomeMenu.Staff:
                    
                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.staffBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
                    }
                    GUILayout.BeginArea(this.staffAreaRect);
                    {
                        GUILayout.BeginVertical();
                        {
                            GUILayout.Label("製作團隊： T.I.C.S",StaffStyle);
                            GUILayout.Space(20);
                            GUILayout.Label("    程式：", StaffStyle);
                            GUILayout.Label("        黃御恩、張廷宇", StaffStyle);
                            GUILayout.Space(10);
                            GUILayout.Label("    人物：", StaffStyle);
                            GUILayout.Label("        康學昕", StaffStyle);
                            GUILayout.Space(10);
                            GUILayout.Label("    場景：", StaffStyle);
                            GUILayout.Label("        王昶中", StaffStyle);
                            GUILayout.Space(10);
                            GUILayout.Label("    企劃：", StaffStyle);
                            GUILayout.Label("        黃御恩、張廷宇、王昶中、康學昕", StaffStyle);
                            GUILayout.Space(10);
                        }
                        GUILayout.EndVertical();
                    }
                    GUILayout.EndArea();
                    break;
                #endregion

                #region Menu : Stage
                case GameDefinition.HomeMenu.Stage:
                    // Display Option Background picture
                    if (this.OptionBackground != null)
                    {
                        GUI.DrawTexture(this.stageBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
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
                                        switch (this.stageKeyValue)
                                        {
                                            case 0:
                                                GUILayout.Box("\nBeginChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nFirstStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nFirstStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSecondStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSpeedStageOne\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSpeedStageTwo\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSpecialStage_SpeedUp\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                break;

                                            case 1:
                                                if (GUILayout.Button("\nBeginChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 0;
                                                    stageScene = GameDefinition.Scene.BeginChallenge;
                                                }
                                                GUILayout.Box("\nFirstStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nFirstStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSecondStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                if (GUILayout.Button("\nSpeedStageOne\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 5;
                                                    stageScene = GameDefinition.Scene.SpeedStageOne;
                                                }
                                                GUILayout.Box("\nSpeedStageTwo\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                GUILayout.Box("\nSpecialStage_SpeedUp\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                break;

                                            case 2:
                                                if (GUILayout.Button("\nBeginChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 0;
                                                    stageScene = GameDefinition.Scene.BeginChallenge;
                                                }
                                                if (GUILayout.Button("\nFirstStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 1;
                                                    stageScene = GameDefinition.Scene.FirstStageChallenge;
                                                }
                                                if (GUILayout.Button("\nFirstStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 2;
                                                    stageScene = GameDefinition.Scene.FirstStage_Hard;
                                                }
                                                GUILayout.Box("\nSecondStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                if (GUILayout.Button("\nSpeedStageOne\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 5;
                                                    stageScene = GameDefinition.Scene.SpeedStageOne;
                                                }
                                                if (GUILayout.Button("\nSpeedStageTwo\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 6;
                                                    stageScene = GameDefinition.Scene.SpeedStageTwo;
                                                }
                                                GUILayout.Box("\nSpecialStage_SpeedUp\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y));
                                                break;

                                            case 3:
                                                if (GUILayout.Button("\nBeginChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 0;
                                                    stageScene = GameDefinition.Scene.BeginChallenge;
                                                }
                                                if (GUILayout.Button("\nFirstStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 1;
                                                    stageScene = GameDefinition.Scene.FirstStageChallenge;
                                                }
                                                if (GUILayout.Button("\nFirstStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 2;
                                                    stageScene = GameDefinition.Scene.FirstStage_Hard;
                                                }
                                                if (GUILayout.Button("\nSecondStageChallenge\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 3;
                                                    stageScene = GameDefinition.Scene.SecondStageChallenge;
                                                }
                                                //if (GUILayout.Button("\nSecondStage_Hard\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                //{
                                                //    this.stageValue = 4;
                                                //    stageScene = GameDefinition.Scene.SecondStage_Hard;
                                                //}
                                                if (GUILayout.Button("\nSpeedStageOne\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 5;
                                                    stageScene = GameDefinition.Scene.SpeedStageOne;
                                                }
                                                if (GUILayout.Button("\nSpeedStageTwo\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 6;
                                                    stageScene = GameDefinition.Scene.SpeedStageTwo;
                                                }
                                                if (GUILayout.Button("\nSpecialStage_SpeedUp\n", GUILayout.MaxWidth(stageButtonSize.x), GUILayout.MaxHeight(stageButtonSize.y)))
                                                {
                                                    this.stageTextureValue = 7;
                                                    stageScene = GameDefinition.Scene.SpecialStage_SpeedUp;
                                                }
                                                break;
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

                                GUILayout.Space(25);

                                GUILayout.BeginVertical();
                                {
                                    if(this.stageKeyValue == 0)
                                        GUILayout.Label("\n\n\n\n\n\n\n\n                                請先進入普通模式解鎖後才可以使用                                \n\n\n\n\n\n\n\n");
                                    else
                                        GUILayout.Box(this.StageTextures[this.stageTextureValue], GUILayout.Width(this.stageTextureSize.x), GUILayout.Height(this.stageTextureSize.y));
                                    GUILayout.Space(25);
                                    GUILayout.Box(StageHintTexture, GUILayout.Width(this.stageHintBoxSize.x), GUILayout.Height(this.stageHintBoxSize.y));
                                }
                                GUILayout.Space(25);
                                GUILayout.EndVertical();
                            }
                            GUILayout.EndHorizontal();
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
                        GUI.DrawTexture(this.optionBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, true, 0.0f);
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
        #region Start Rect
        this.startAreaRect = new Rect((Screen.width - (int)this.startAreaRect.width) / 2,
                                     (Screen.height - (int)this.startAreaRect.height) / 2,
                                      this.startAreaRect.width,
                                      this.startAreaRect.height);
        this.startBackgroundRect = new Rect((Screen.width - (int)this.startBackgroundRect.width) / 2,
                                           (Screen.height - (int)this.startBackgroundRect.height) / 2,
                                           this.startBackgroundRect.width,
                                           this.startBackgroundRect.height);
        #endregion

        #region HighScore Rect

        // --------------------HighScore Background------------------
        this.highScoreBackgroundRect = new Rect((Screen.width - (int)this.highScoreBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.highScoreBackgroundRect.height) / 2,
                                               this.highScoreBackgroundRect.width,
                                               this.highScoreBackgroundRect.height);

        this.highScoreAreaRect = new Rect((Screen.width - (int)this.highScoreAreaRect.width) / 2,
                                              (Screen.height - (int)this.highScoreAreaRect.height) / 2,
                                               this.highScoreAreaRect.width,
                                               this.highScoreAreaRect.height);
        #endregion

        #region Staff Rect
        this.staffAreaRect = new Rect((Screen.width - (int)this.staffAreaRect.width) / 2,
                                     (Screen.height - (int)this.staffAreaRect.height) / 2,
                                      this.staffAreaRect.width,
                                      this.staffAreaRect.height);
        this.staffBackgroundRect = new Rect((Screen.width - (int)this.staffBackgroundRect.width) / 2,
                                           (Screen.height - (int)this.staffBackgroundRect.height) / 2,
                                           this.staffBackgroundRect.width,
                                           this.staffBackgroundRect.height);
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
            case GameDefinition.Scene.SecondStage_Hard:
                this.setScene(value);
                break;

            default:
                break;
        }
    }

    #endregion
}
