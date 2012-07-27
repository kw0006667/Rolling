using UnityEngine;
using System.Collections;

public class ResultDisplay : MonoBehaviour
{
    public string hintStr;
    public GUIStyle style;

    public float left = 0.7f;
    public float top = 0.15f;
    public float width = 0.4f;
    public float height = 0.7f;


    public GameObject TimerObject;
    private Timer timer;

    public GameObject GearCollectionObject;
    private CellManager cellManager;
    public Texture iconBig;
    public Texture iconMiddle;
    public Texture iconSmall;
    public GUIStyle scoreStyle;

    void Start()
    {
        timer = TimerObject.GetComponent<Timer>();
        cellManager = GearCollectionObject.GetComponent<CellManager>();
    }

    void Update()
    {
        
    }

    Rect backgroundRect;
    Rect scoreRect;
    Rect BigGearRect;
    Rect MiddleGearRect;
    Rect SmallGearRect;
    Rect RankRect;
    public Texture rankTecture;
    public GUIStyle rankStyle;

    public bool isShowScore;

    void OnGUI()
    {
        if (isShowScore)
        {
            backgroundRect = new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height);
            scoreRect = new Rect(backgroundRect.xMin + backgroundRect.width * 0.1f, backgroundRect.yMin + backgroundRect.height * 0.05f, backgroundRect.width * 0.8f, backgroundRect.height * 0.2f);
            BigGearRect = new Rect(backgroundRect.xMin + backgroundRect.width * 0.1f, backgroundRect.yMin + backgroundRect.height * 0.3f, backgroundRect.width * 0.8f, backgroundRect.height * 0.2f);
            MiddleGearRect = new Rect(backgroundRect.xMin + backgroundRect.width * 0.1f, backgroundRect.yMin + backgroundRect.height * 0.5f, backgroundRect.width * 0.8f, backgroundRect.height * 0.2f);
            SmallGearRect = new Rect(backgroundRect.xMin + backgroundRect.width * 0.1f, backgroundRect.yMin + backgroundRect.height * 0.7f, backgroundRect.width * 0.8f, backgroundRect.height * 0.2f);
            RankRect = new Rect(backgroundRect.xMin + backgroundRect.width * 0.5f, backgroundRect.yMin + backgroundRect.height * 0.5f, backgroundRect.width * 0.4f, backgroundRect.height * 0.4f);

            GUI.Box(backgroundRect, hintStr, style);
            GUI.Box(scoreRect, timer.TimerStr, style);
            GUI.Label(BigGearRect, new GUIContent(" " + cellManager.bigCount.ToString("00"), iconBig), scoreStyle);
            GUI.Label(MiddleGearRect, new GUIContent(" " + cellManager.middleCount.ToString("00"), iconMiddle), scoreStyle);
            GUI.Label(SmallGearRect, new GUIContent(" " + cellManager.smallCount.ToString("00"), iconSmall), scoreStyle);
            GUI.Label(RankRect, rankTecture, rankStyle);
        }
        //GUI.DrawTexture(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), t1);

        //GUI.Label(new Rect(Screen.width - Screen.width * left, Screen.height * top, Screen.width * width, Screen.height * height), hintStr , style);

        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isShowScore = true;
        }
    }
}
