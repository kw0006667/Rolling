using UnityEngine;
using System.Collections;

public class CellManager : MonoBehaviour
{
    public int totalCount { get; private set; }
    public int currentCount { get; private set; }

    public int smallCount { get; private set; }
    public int middleCount { get; private set; }
    public int bigCount { get; private set; }

	// Use this for initialization
    void Start()
    {
        this.totalCount = gameObject.GetComponentsInChildren<Transform>().Length - 1;
        this.currentCount = 0;
        this.smallCount = 0;
        this.middleCount = 0;
        this.bigCount = 0;
    }
	
    public void AddCell(Cell.Size size)
    {
        switch (size)
        { 
            case Cell.Size.Small:
                this.smallCount++;
                break;
            case Cell.Size.Middle:
                this.middleCount++;
                break;
            case Cell.Size.Big:
                this.bigCount++;
                break;
        }
        this.currentCount++;
    }

}
