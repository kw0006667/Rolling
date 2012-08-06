using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResultDisplay : MonoBehaviour
{
    public bool isShowScore;

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
    private string sceneName;
    private int totalScore;
    private string rankStr;

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
        this.sceneName = Application.loadedLevelName;
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
                                GUILayout.Box(new GUIContent(" ", GearIconSmallTexture), GearStyle, GUILayout.MaxHeight(100));
                                GUILayout.Space(25);
                                GUILayout.Box(new GUIContent(" ", GearIconSmallTexture), GearStyle, GUILayout.MaxHeight(100));
                                GUILayout.Space(25);
                                GUILayout.Box(new GUIContent(" ", GearIconSmallTexture), GearStyle, GUILayout.MaxHeight(100));
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
            GUILayout.EndArea();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && !this.isShowScore)
        {
            this.isShowScore = true;
            ScoreData score = new ScoreData((this.fileManager.GetScores().Count + 1).ToString(),
                                            Environment.GetEnvironmentVariable("COMPUTERNAME"),
                                            totalScore.ToString(),
                                            this.timer.TimerStr, cellManager.smallCount.ToString("000"),
                                            cellManager.middleCount.ToString("000"),
                                            cellManager.bigCount.ToString("000"), rankStr,
                                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                            this.sceneName);
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
