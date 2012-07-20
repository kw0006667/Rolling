using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public enum Size 
    {
        Small,Middle,Big
    }
    public Size size = Size.Small;

    private CellManager manager;
    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;        
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            this.manager.AddCell(this.size);
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
    void Start()
    {
        this.manager = this.transform.parent.gameObject.GetComponent<CellManager>();
    }
	
}
