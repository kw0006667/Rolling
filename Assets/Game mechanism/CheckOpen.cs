using UnityEngine;
using System.Collections;

public class CheckOpen : MonoBehaviour
{
    private BoxCollider box;
    private CellManager cellManager;

    // Use this for initialization
    void Start()
    {
        box = this.GetComponent<BoxCollider>();
        box.enabled = false;
        cellManager = GameObject.Find("GearCollection").GetComponent<CellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cellManager.currentCount == cellManager.totalCount)
        {
            box.enabled = true;
            Destroy(this);
        }
    }
}
