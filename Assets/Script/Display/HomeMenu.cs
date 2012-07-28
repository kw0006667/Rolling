using UnityEngine;
using System.Collections;

public class HomeMenu : MonoBehaviour
{
    public GameDefinition.HomeMenu homeMenu;
    private bool isTrigger = false;


    void OnTriggerEnter(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isTrigger = true;
            TriggerEvent();
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject m_parent = other.transform.parent.gameObject;
        if (m_parent.CompareTag(GameDefinition.GetTagName(GameDefinition.Tag.Player)))
        {
            isTrigger = false;
        }
    }

    void TriggerEvent()
    {        
        switch (homeMenu)
        {
            case GameDefinition.HomeMenu.New:
                Application.LoadLevel(GameDefinition.GetSceneName(GameDefinition.Scene.Begin));
                break;
            case GameDefinition.HomeMenu.Continute:
                break;
            case GameDefinition.HomeMenu.Load:
                break;
            case GameDefinition.HomeMenu.Stage:
                break;
            case GameDefinition.HomeMenu.Option:
                break;
            case GameDefinition.HomeMenu.Exit:
                break;
            default:
                break;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (this.isTrigger)
        {
            switch (homeMenu)
            {
                case GameDefinition.HomeMenu.New:
                    break;

                case GameDefinition.HomeMenu.Continute:
                    break;

                case GameDefinition.HomeMenu.Load:
                    break;

                case GameDefinition.HomeMenu.Stage:
                    break;

                case GameDefinition.HomeMenu.Option:
                    break;

                case GameDefinition.HomeMenu.Exit:
                    if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.425f, Screen.width * 0.2f, Screen.height * 0.15f), "Yes"))
                    {
                        Application.Quit();
                    }
                    if (GUI.Button(new Rect(Screen.width * 0.6f, Screen.height * 0.425f, Screen.width * 0.2f, Screen.height * 0.15f), "No"))
                    {
                        this.isTrigger = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
