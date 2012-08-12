using UnityEngine;
using System.Collections;
using System;

public class EndingMechanism : MonoBehaviour 
{
    private CellManager cellManager;
    private bool isOpen = false;

    private FileManager fileManager;
    private SettingData settingData;
    private string machineName;

    public GameDefinition.Scene Scene;
    public GameObject GearCollectionObject;
    public int RecordKeyValue;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.isOpen)
        {
            if (this.Scene != GameDefinition.Scene.none)
            {
                
                switch (this.Scene)
                {
                    case GameDefinition.Scene.FirstStage:
                        this.setRecordKey(this.RecordKeyValue);
                        break;
                    case GameDefinition.Scene.SecondStage:
                        this.setRecordKey(this.RecordKeyValue);
                        break;
                }
                Application.LoadLevel(GameDefinition.GetSceneName(this.Scene));
            }
        }
    }

	// Use this for initialization
    void Start()
    {
        if (this.GearCollectionObject != null)
        {
            this.cellManager = GearCollectionObject.GetComponent<CellManager>();
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

    private void setRecordKey(int key)
    {
        if (Convert.ToInt32(this.settingData.Key) < key)
        {
            this.settingData.Key = key.ToString();
            this.fileManager.ConfigWrite(this.settingData);
        }
    }
}
