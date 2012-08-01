using UnityEngine;
using System.Collections;

#region Global Define
enum SETVALUE
{
    DECREASE = -1,
    INCREASE = 1,
};
#endregion

public class GameDefinition
{


    #region Public Properties  

    public const string ScoresFilePath = ".\\Scores.xml";

    public enum Tag
    {
        Player = 1,
        MainCamera = 2,
    };

    public enum Weight : int
    {
        light = 1,
        heavy = 2,
    };

    public enum Scene
    {
        none,
        StartMenu,
        Begin,
        BeginChallenge,
        FirstStage,
        FirstStage_Hard,
        SecondStage,
        SecondStage_Hard,
        SpeedStageOne,
        SpeedStageTwo,
        SpecialStage_SpeedUp,        
    };

    public enum HomeMenu
    {
        New,
        Continute,
        Load,
        Stage,
        Option,
        Exit,
    };

    public enum OptionMenu
    {
        None,
        ReturnCheck,
        Tutorials,
        Option,
        ReturnTitle,
        Exit,
    };

    public enum QualityContent : int
    {
        Fastest = 0,
        Fast = 1,
        Simple = 2,
        Good = 3,
        Beautiful = 4,
        Fantastic = 5,
    };

    

    #endregion

    #region private const string

    private const string Player = "Player";
    private const string MainCamera = "MainCamera";

    private const string StartMenu = "StartMenu";
    private const string Begin = "Begin";
    private const string BeginChallenge = "BeginChallenge";
    private const string FirstStage = "FirstStage";
    private const string FirstStage_Hard = "FirstStage_Hard";
    private const string SecondStage = "SecondStage";
    private const string SecondStage_Hard = "SecondStage_Hard";
    private const string SpeedStageOne = "SpeedStageOne";
    private const string SpeedStageTwo = "SpeedStageTwo";
    private const string SpecialStage_SpeedUp = "SpecialStage_SpeedUp";

    private const string ReturnCheckString = "Return CheckPoint";
    private const string TutorialsString = "Tutorials";
    private const string OptionString = "Option";
    private const string ReturnTitleString = "Return to Title";
    private const string ExitString = "Exit";

    #endregion

    #region Public static Methods

    /// <summary>
    /// Get application scene name.
    /// </summary>
    /// <param name="tag">Scene name</param>
    /// <returns>Scene string name</returns>
    public static string GetSceneName(Scene scene)
    {
        switch (scene)
        {
            case Scene.StartMenu:
                return GameDefinition.StartMenu;
            case Scene.Begin:
                return GameDefinition.Begin;
            case Scene.FirstStage:
                return GameDefinition.FirstStage;
            case Scene.SpeedStageOne:
                return GameDefinition.SpeedStageOne;
            case Scene.SpeedStageTwo:
                return GameDefinition.SpeedStageTwo;
            case Scene.BeginChallenge:
                return GameDefinition.BeginChallenge;
            default:
                return null;
        }
    }

    /// <summary>
    /// Get application tag name that you have setted.
    /// </summary>
    /// <param name="tag">Tag</param>
    /// <returns>Tag string name</returns>
    public static string GetTagName(Tag tag)
    {
        switch (tag)
        {
            case Tag.Player:
                return GameDefinition.Player;
            case Tag.MainCamera:
                return GameDefinition.MainCamera;
            default:
                return null;
        }
    }

    /// <summary>
    /// Get the current character rigidbody mass is heavy or light.
    /// </summary>
    /// <param name="mass">Current rigidbody mass</param>
    /// <returns>Return light if less or equeal light even not.</returns>
    public static Weight GetIsWeight(float mass)
    {
        if (mass > (float)GameDefinition.Weight.light)
            return Weight.heavy;
        else
            return Weight.light;
    }

    /// <summary>
    /// Get the game option menu string.
    /// </summary>
    /// <param name="option">Which option.</param>
    /// <returns>Option String.</returns>
    public static string GetOptionMenuString(OptionMenu option)
    {
        switch (option)
        {
            case OptionMenu.None:
                return null;
            case OptionMenu.ReturnCheck:
                return GameDefinition.ReturnCheckString;
            case OptionMenu.Tutorials:
                return GameDefinition.TutorialsString;
            case OptionMenu.Option:
                return GameDefinition.OptionString;
            case OptionMenu.ReturnTitle:
                return GameDefinition.ReturnTitleString;
            case OptionMenu.Exit:
                return GameDefinition.ExitString;
            default:
                return null;
        }
    }
    
    #endregion
}
