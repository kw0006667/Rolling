using UnityEngine;
using System.Collections;

public class NormalEnding : MonoBehaviour
{
    private Rect normalEndBackgroundRect = new Rect(0, 0, 800, 450);
    private CellManager cellManager;
    private bool isOpen = false;
    private bool isShowEndingUI = false;

    public Texture normalEndBackgroundTexture;
    public GameObject GearCollectionObject;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.isOpen)
        {
            this.isShowEndingUI = true;
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
}
