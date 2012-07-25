using UnityEngine;
using System.Collections;

public class GameDefinition
{

    #region Public Properties

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
        Begin,
        FirstStage,
        SpeedStageOne,
    }

    #endregion

    #region private const string

    private const string Player = "Player";
    private const string MainCamera = "MainCamera";

    private const string Begin = "Begin";
    private const string FirstStage = "FirstStage";
    private const string SpeedStageOne = "SpeedStageOne";

    #endregion

    /// <summary>
    /// Get application scene name.
    /// </summary>
    /// <param name="tag">Scene name</param>
    /// <returns>Scene string name</returns>
    public static string GetSceneName(Scene scene)
    {
        switch (scene)
        {
            case Scene.Begin:
                return GameDefinition.Begin;
            case Scene.FirstStage:
                return GameDefinition.FirstStage;
            case Scene.SpeedStageOne:
                return GameDefinition.SpeedStageOne;
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
}
