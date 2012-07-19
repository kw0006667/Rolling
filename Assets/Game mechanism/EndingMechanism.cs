using UnityEngine;
using System.Collections;

public class EndingMechanism : MonoBehaviour 
{
    private CellManager cellManager;
    private bool isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)) && this.isOpen)
        {
            Application.LoadLevel("FirstStage");
        }
    }

	// Use this for initialization
    void Start()
    {
        this.cellManager = GameObject.Find("GearCollection").GetComponent<CellManager>();
    }
	
	// Update is called once per frame
    void Update()
    {
        if (this.cellManager.currentCount == this.cellManager.totalCount)
        {
            this.renderer.enabled = true;
            this.isOpen = true;
        }
    }
}
