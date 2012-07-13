using UnityEngine;
using System.Collections;

public class CellManager : MonoBehaviour
{
    public int totalCount { get; private set; }
    public int currentCount { get; private set; }
	// Use this for initialization
	void Start () {
        this.totalCount = gameObject.GetComponentsInChildren<Transform>().Length - 1;
        this.currentCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void AddCell()
    {
        this.currentCount++;
    }

}
