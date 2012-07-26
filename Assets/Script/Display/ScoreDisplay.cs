using UnityEngine;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    public GUIStyle style;

    public Texture iconBig;
    public Texture iconMiddle;
    public Texture iconSmall;

    private CellManager cellManager;

    void Start()
    {
        cellManager = transform.GetComponent<CellManager>();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 100, Screen.width - 50, 32), new GUIContent(cellManager.bigCount.ToString("00"), iconBig), style);
        GUI.Label(new Rect(0, 150, Screen.width - 50, 32), new GUIContent(cellManager.middleCount.ToString("00"), iconMiddle), style);
        GUI.Label(new Rect(0, 200, Screen.width - 50, 32), new GUIContent(cellManager.smallCount.ToString("00"), iconSmall), style);
    }
    
}
