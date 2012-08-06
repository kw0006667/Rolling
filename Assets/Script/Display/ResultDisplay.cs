using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResultDisplay : MonoBehaviour
{
    public GameDefinition.Scene CurrentScene;
    public GameObject GearCollectionObject;
    public GameObject TimerObject;
    
    public Texture OptionBackground;
    public Texture GearIconBigTexture;
    public Texture GearIconMiddleTexture;
    public Texture GearIconSmallTexture;
    public Texture rankTecture;

    public GUIStyle TimeStyle;
    public GUIStyle GearStyle;    
    public GUIStyle ScoreStyle;

    private CellManager cellManager;

    private FileManager fileManager;
    private List<ScoreData> scoreList;
    private int highScoreCount = 10;            // how many high score will be get
    private int totalScore = 0;
    private string rankStr = "A";

    private bool isShowScore = false;
    private bool isShowHighScoreUI = false;

    private Timer timer;

    private Rect resultAreaRect = new Rect(0, 0, 900, 585);
    private Rect resultBackgroundRect = new Rect(0, 0, 900, 585);

    void Start()
    {
        timer = TimerObject.GetComponent<Timer>();
        if (GearCollectionObject != null)
            cellManager = GearCollectionObject.GetComponent<CellManager>();

        this.fileManager = new FileManager();
        this.fileManager.ScoresReader(GameDefinition.ScoresFilePath);
        this.scoreList = new List<ScoreData>();
    }

    void Update()
    {
        if (this.isShowScore && !this.isShowHighScoreUI)
        {
            if (Input.GetMouseButtonDown(0))
                this.isShowHighScoreUI = true;
        }
    }

    void OnGUI()
    {
        if (isShowScore)
        {
            // Initialize all button rect real time
            this.InitializeButtonRect();

            // Display Option Background picture
            if (this.OptionBackground != null)
            {
                GUI.DrawTexture(this.resultBackgroundRect, this.OptionBackground, ScaleMode.StretchToFill, false, 0.0f);
            }

            GUILayout.BeginArea(this.resultAreaRect);
            {
                if (!this.isShowHighScoreUI)
                    ResultGUI();
                else
                    HighScoreGUI();
                
            }
            GUILayout.EndArea();
        }
    }

    private void HighScoreGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.Space(25);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(25);

                GUILayout.Label("是否再玩一次？", GUILayout.Width(100));
                GUILayout.Space(25);
                if (GUILayout.Button("是", GUILayout.Width(50)))
                    Application.LoadLevel(GameDefinition.GetSceneName(this.CurrentScene));

                GUILayout.Space(25);

                if (GUILayout.Button("否", GUILayout.Width(50)))
                    Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.StartMenu));

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

    private void ResultGUI()
    {
        GUILayout.BeginVertical();
        {
            GUILayout.Space(50);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(50);
                GUILayout.Box(this.timer.TimerStr, TimeStyle);
                GUILayout.Space(50);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(50);

            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(50);
                GUILayout.BeginVertical(GUILayout.MaxWidth(250));
                {
                    if (cellManager != null)
                    {
                        GUILayout.Box(new GUIContent(" " + cellManager.bigCount.ToString("000"), GearIconBigTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                        GUILayout.Box(new GUIContent(" " + cellManager.middleCount.ToString("000"), GearIconMiddleTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                        GUILayout.Box(new GUIContent(" " + cellManager.smallCount.ToString("000"), GearIconSmallTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                    }
                    else
                    {
                        GUILayout.Box(new GUIContent(" " + Convert.ToInt32("0").ToString("000"), GearIconBigTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                        GUILayout.Box(new GUIContent(" " + Convert.ToInt32("0").ToString("000"), GearIconMiddleTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                        GUILayout.Box(new GUIContent(" " + Convert.ToInt32("0").ToString("000"), GearIconSmallTexture), GearStyle, GUILayout.MaxHeight(100));
                        GUILayout.Space(25);
                    }
                }
                GUILayout.EndVertical();

                GUILayout.Space(25);

                GUILayout.BeginVertical();
                {
                    GUILayout.Box(totalScore.ToString(), ScoreStyle);
                    GUILayout.Space(25);
                    GUILayout.Box(this.rankTecture, ScoreStyle, GUILayout.MaxHeight(200));
                    GUILayout.Space(25);
                }
                GUILayout.EndVertical();

                GUILayout.Space(25);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    int VerifyTimeScore()
    {
        int score = score = 300 - (int)this.timer.currentTime;
        if (score < 0)
            score = 0;
        score *= 10;

        switch (this.CurrentScene)
        {
            case GameDefinition.Scene.none:
                break;
            case GameDefinition.Scene.StartMenu:
                break;
            case GameDefinition.Scene.Begin:
                break;
            case GameDefinition.Scene.BeginChallenge:
                break;
            case GameDefinition.Scene.FirstStage:
                break;
            case GameDefinition.Scene.FirstStage_Hard:
                break;
            case GameDefinition.Scene.FirstStageChallenge:
                break;
            case GameDefinition.Scene.SecondStage:
                break;
            case GameDefinition.Scene.SecondStage_Hard:
                break;
            case GameDefinition.Scene.SecondStageChallenge:
                break;
            case GameDefinition.Scene.SpeedStageOne:
                break;
            case GameDefinition.Scene.SpeedStageTwo:
                break;
            case GameDefinition.Scene.SpecialStage_SpeedUp:
                break;
            default:
                break;
        }
        return score;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isShowScore)
        {
            this.isShowScore = true;
            if (cellManager != null)
            {
                this.totalScore += (this.cellManager.smallCount * 1 + this.cellManager.middleCount * 3 + this.cellManager.bigCount * 5);
            }

            this.totalScore += this.VerifyTimeScore();

            ScoreData score;
            if (cellManager != null)
            {
                score = new ScoreData((this.fileManager.GetScores().Count + 1).ToString(),
                                       Environment.GetEnvironmentVariable("COMPUTERNAME"),
                                       this.totalScore.ToString(),
                                       this.timer.TimerStr,
                                       this.cellManager.smallCount.ToString("000"),
                                       this.cellManager.middleCount.ToString("000"),
                                       this.cellManager.bigCount.ToString("000"),
                                       this.rankStr,
                                       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                       GameDefinition.GetSceneName(this.CurrentScene));
            }
            else
            {
                score = new ScoreData((this.fileManager.GetScores().Count + 1).ToString(),
                                       Environment.GetEnvironmentVariable("COMPUTERNAME"),
                                       this.totalScore.ToString(),
                                       this.timer.TimerStr,
                                       Convert.ToInt32("0").ToString("000"),
                                       Convert.ToInt32("0").ToString("000"),
                                       Convert.ToInt32("0").ToString("000"),
                                       this.rankStr,
                                       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                       GameDefinition.GetSceneName(this.CurrentScene));
            }
            this.fileManager.ScoresWrite(score, GameDefinition.ScoresFilePath);
            this.scoreList = this.fileManager.GetHighScores(GameDefinition.GetSceneName(this.CurrentScene), this.highScoreCount);
        }
    }

    // Initialize All Buttons Rect real time
    private void InitializeButtonRect()
    {
        // --------------------Result Background------------------
        this.resultBackgroundRect = new Rect((Screen.width - (int)this.resultBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.resultBackgroundRect.height) / 2,
                                               this.resultBackgroundRect.width,
                                               this.resultBackgroundRect.height);

        this.resultAreaRect = new Rect((Screen.width - (int)this.resultAreaRect.width) / 2,
                                              (Screen.height - (int)this.resultAreaRect.height) / 2,
                                               this.resultAreaRect.width,
                                               this.resultAreaRect.height);
    }
}
