using UnityEngine;
using System.Collections;
using System;

public class NormalEnding : MonoBehaviour
{
    private Rect normalEndBackgroundRect = new Rect(0, 0, 800, 450);
    private CellManager cellManager;
    private bool isOpen = false;
    private bool isShowEndingUI = false;

    private FileManager fileManager;
    private SettingData settingData;
    private string machineName;

    public Texture normalEndBackgroundTexture;
    public int RecordKeyValue;

    public GameObject GearCollectionObject;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.isOpen)
        {
            this.isShowEndingUI = true;
            this.setRecordKey(this.RecordKeyValue);
        }
    }

    // Use this for initialization
    void Start()
    {
        // Initialize all button rect real time
        this.InitializeButtonRect();

        if (this.GearCollectionObject != null)
        {
            this.cellManager = this.GearCollectionObject.GetComponent<CellManager>();
            this.isOpen = false;
        }
        else
            this.isOpen = true;

        this.machineName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        this.fileManager = new FileManager();
        this.fileManager.ConfigReader(GameDefinition.SettingFilePath, this.machineName);
        this.settingData = this.fileManager.GetSettingData();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isOpen)
        {
            if (this.cellManager.currentCount == this.cellManager.totalCount)
            {
                this.renderer.enabled = true;
                this.collider.enabled = true;
                this.isOpen = true;
            }
        }
    }

    void OnGUI()
    {
        if (this.isShowEndingUI)
        {
            GUILayout.BeginArea(this.normalEndBackgroundRect, normalEndBackgroundTexture);
            {
                if (Input.GetMouseButtonDown(0))
                    Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.StartMenu));
            }
            GUILayout.EndArea();
        }
    }

    private void InitializeButtonRect()
    {
        // --------------------Result Background------------------
        this.normalEndBackgroundRect = new Rect((Screen.width - (int)this.normalEndBackgroundRect.width) / 2,
                                              (Screen.height - (int)this.normalEndBackgroundRect.height) / 2,
                                               this.normalEndBackgroundRect.width,
                                               this.normalEndBackgroundRect.height);
    }

    private void setRecordKey(int key)
    {
        if (Convert.ToInt32(this.settingData.Key) < key)
        {
            this.settingData.Key = key.ToString();
            this.fileManager.ConfigWrite(this.settingData);
        }
    }
}
