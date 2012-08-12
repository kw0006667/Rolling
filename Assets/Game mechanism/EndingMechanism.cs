using UnityEngine;
using System.Collections;

public class EndingMechanism : MonoBehaviour 
{
    private CellManager cellManager;
    private bool isOpen = false;

    public GameDefinition.Scene Scene;
    public GameObject GearCollectionObject;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.isOpen)
        {
            if (this.Scene != GameDefinition.Scene.none)
                Application.LoadLevel(GameDefinition.GetSceneName(this.Scene));
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
}
